using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UniHub.Web.SwaggerFilters
{
    public class AuthOperationFilter : IOperationFilter
    {
        void IOperationFilter.Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }

            if (!context.ApiDescription.TryGetMethodInfo(out MethodInfo methodInfo))
            {
                return;
            }

            var authorizeAttributes = methodInfo.DeclaringType
                .GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<AuthorizeAttribute>();

            var allowAnonymousAttributes = methodInfo.DeclaringType
            .GetCustomAttributes(true)
            .OfType<AllowAnonymousAttribute>();

            if (!authorizeAttributes.Any() && !allowAnonymousAttributes.Any())
            {
                return;
            }

            var parameter = new NonBodyParameter
            {
                Name = "Authorization",
                In = "header",
                Description = "The Bearer token",
                Required = true,
                Type = "string"
            };

            operation.Parameters.Add(parameter);
        }
    }
}
