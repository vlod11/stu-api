using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using UniHub.WebApi.Model;
using Microsoft.AspNetCore.Builder;
using UniHub.WebApi.DataAccess;

namespace UniHub.WebApi.Extensions
{
    public static class DbContextExtension
    {
        public static void AddDebugDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<UniHubDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Debug")));
        }

        public static void AddDockerDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<UniHubDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Docker")));
        }

        public static void AddReleaseDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<UniHubDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Release")));
        }
    }
}