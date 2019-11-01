using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniHub.WebApi.DataAccess;

namespace UniHub.WebApi.Web.Extensions.StartupExtensions
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