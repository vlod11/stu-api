using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using UniHub.WebApi.OperationFilters;
using System.Reflection;
using System.IO;
using System;
using UniHub.WebApi.Web.OperationFilters;

namespace UniHub.WebApi.Extensions
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

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}