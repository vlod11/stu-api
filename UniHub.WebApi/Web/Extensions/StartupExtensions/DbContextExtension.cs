using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

using UniHub.WebApi.Model;
using Microsoft.AspNetCore.Builder;
using UniHub.WebApi.DataAccess;

namespace UniHub.WebApi.Extensions
{
    public static class DbContextExtension
    {
        public static void AddDebugDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<UniHubDbContext>(options => options.UseMySql(
                configuration.GetConnectionString("Debug"), // replace with your Connection String
                                mySqlOptions =>
                                {
                                    mySqlOptions.ServerVersion(new Version(8, 0, 13), ServerType.MySql); // replace with your Server Version and Type
                                }));
            //services.AddDbContext<UniHubDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Debug"))); // debug connection for msSql
        }

        public static void AddReleaseDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<UniHubDbContext>(options => options.UseMySql(
                configuration.GetConnectionString("Realese"), // replace with your Connection String
                                mySqlOptions =>
                                {
                                    mySqlOptions.ServerVersion(new Version(8, 0, 12), ServerType.MySql); // replace with your Server Version and Type
                                }));
            // services.AddDbContext<UniHubDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Release"))); // realese connection for msSql
        }
    }
}