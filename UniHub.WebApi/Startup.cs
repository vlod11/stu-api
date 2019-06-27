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
using UniHub.WebApi.Extensions;
using UniHub.WebApi.Helpers.Mappers;
using UniHub.WebApi.Shared.Options;
using UniHub.WebApi.Shared.Token;
using UniHub.WebApi.DataAccess;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UniHub.WebApi.ModelLayer.Entities;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Reflection;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.DataAccess.RepositoryService.Interfaces;
using UniHub.WebApi.DataAccess.RepositoryService.Repositories;
using UniHub.WebApi.Web.Extensions.StartupExtensions;
using NLog.Web;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore.HttpOverrides;
using UniHub.WebApi.Helpers.Contract;
using UniHub.WebApi.Helpers;
using UniHub.WebApi.BLL.Helpers.Contract;
using UniHub.WebApi.BLL.Helpers;
using UniHub.WebApi.Web.Middleware;

namespace UniHub.WebApi
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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

            if (_configuration["usedocker"] == "true") {
                services.AddDockerDbContext(_configuration);
            } else {
                services.AddDebugDbContext(_configuration);
            }
            
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
        }

        public void Configure(IApplicationBuilder app,
                                 SeedDatabase seedDatabase,
                                 IHostingEnvironment env,
                                 IFolderHelper folderHelper,
                                 IOptions<FilesOptions> filesOptions)
        {
            env.ConfigureNLog("nlog.config");
            _loggerFactory.AddNLog();

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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UniHub V1");
            });

            // app.UseHsts();
            // app.UseDefaultFiles();
            // app.UseCors("EnableCORS");
            // app.UseCors(builder => builder.WithOrigins("http://localhost:4200"));

            app.UseAuthentication();
            // app.UseHttpsRedirection();

            // app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseMvcWithDefaultRoute();
            app.UseMvc();
        }
    }
}