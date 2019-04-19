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

            // debug connection to MySQL
            // services.AddDbContextPool<UniHubDbContext>(options => options.UseMySql(
            //     configuration.GetConnectionString("Debug"), // replace with your Connection String
            //                     mySqlOptions =>
            //                     {
            //                         mySqlOptions.ServerVersion(new Version(8, 0, 13), ServerType.MySql); // replace with your Server Version and Type
            //                     }));

            // debug connection to MSSql
            //services.AddDbContext<UniHubDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Debug"))); 
        }

        public static void AddReleaseDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<UniHubDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Release")));

            // debug connection to
            // services.AddDbContextPool<UniHubDbContext>(options => options.UseMySql(
            //     configuration.GetConnectionString("Realese"), // replace with your Connection String
            //                     mySqlOptions =>
            //                     {
            //                         mySqlOptions.ServerVersion(new Version(8, 0, 12), ServerType.MySql); // replace with your Server Version and Type
            //                     }));

            // realese connection to MSSql
            // services.AddDbContext<UniHubDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Release")));
        }
    }
}