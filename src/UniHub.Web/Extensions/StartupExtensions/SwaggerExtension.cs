using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using UniHub.Web.SwaggerFilters;

namespace UniHub.Web.Extensions.StartupExtensions
{
    public static class SwaggerExtension
    {
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerExamples();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(configuration.GetSection("Swagger:SwaggerGen:Name").Value,
                new Info
                {
                    Title = configuration.GetSection("Swagger:SwaggerGen:Info:Title:").Value
                });

                c.OperationFilter<AuthOperationFilter>();

                c.ExampleFilters();

                c.DocumentFilter<SetVersionInPaths>();

                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            });
        }
    }
}