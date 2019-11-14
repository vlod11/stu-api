using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using UniHub.WebApi.Common.Options;
using UniHub.WebApi.Common.Token;
using UniHub.WebApi.DataAccess;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Reflection;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.DataAccess.RepositoryService.Interfaces;
using UniHub.WebApi.DataAccess.RepositoryService.Repositories;
using UniHub.WebApi.Web.Extensions.StartupExtensions;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Serilog.Exceptions;
using UniHub.WebApi.BusinessLogic.Helpers;
using UniHub.WebApi.BusinessLogic.Helpers.Contract;
using UniHub.WebApi.Web.Helpers.Mappers;

namespace UniHub.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // TODO: move logging configuraitions in 
            string elastisearchUri;
            if (_configuration["usedocker"] == "true")
            {
                services.AddDockerDbContext(_configuration);
                elastisearchUri = _configuration["ElasticConfiguration:UriDocker"];
            }
            else
            {
                services.AddDebugDbContext(_configuration);
                elastisearchUri = _configuration["ElasticConfiguration:Uri"];
            }

            var serilogConfiguration = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elastisearchUri))
                {
                    AutoRegisterTemplate = true,
                });

            Log.Logger = serilogConfiguration.CreateLogger();
            AppDomain.CurrentDomain.DomainUnload += (o, e) => Log.CloseAndFlush();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddDebug();
                loggingBuilder.AddSerilog();
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.Configure<SendGridOptions>(_configuration.GetSection("SendGrid"));
            services.Configure<FilesOptions>(_configuration.GetSection("Files"));
            services.Configure<UrlsOptions>(_configuration.GetSection("Urls"));

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("EnableCORS"));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build();
                });
            });

            services.AddRepositories();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(SeedDatabase));

            services.AddServiceLayer();

            services.AddTransient<IEmailTemplatePicker, MemoryEmailTemplatePicker>();
            services.AddTransient<IFolderHelper, FolderHelper>();
            services.AddTransient<IDateHelper, DateHelper>();
            services.AddTransient<IEncryptHelper, EncryptHelper>();

            services.AddTransient<IServiceResultMapper, ServiceResultMapper>();
            services.AddTransient<ITokenService, TokenService>();

            services.AddSwagger(_configuration);

            Mapper.Reset();
            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddJwtAuth(_configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddApiVersioning();

            services.AddApiVersioning(
                o =>
                {
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                });
        }

        public void Configure(IApplicationBuilder app,
                                 SeedDatabase seedDatabase,
                                 IHostingEnvironment env,
                                 IFolderHelper folderHelper,
                                 IOptions<FilesOptions> filesOptions,
                                 ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            }

            folderHelper.CreateFilesFoldersIfNotExist();

            seedDatabase.Seed();

            app.UseStaticFiles();

            app.AllowFilesGettingFromServer(env, filesOptions.Value.UploadFolder);

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UniHub V1");
            });

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
            app.UseMvc();
        }
    }
}