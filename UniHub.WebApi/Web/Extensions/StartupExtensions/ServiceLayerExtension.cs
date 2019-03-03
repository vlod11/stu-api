using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniHub.WebApi.BLL.Services;

namespace UniHub.WebApi.Extensions
{
    public static class ServiceLayerExtension
    {
        public static void AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUniversityService, UniversityService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IFacultyService, FacultyService>();
            services.AddScoped<IUsersProfileService, UsersProfileService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IPostService, PostService>();
        }
    }
}