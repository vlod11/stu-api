using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniHub.WebApi.BLL.Services;

namespace UniHub.WebApi.Extensions
{
    public static class ServiceLayerExtension
    {
        public static void AddServiceLayer(this IServiceCollection services)
        {
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUniversityService, UniversityService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IFacultyService, FacultyService>();
            services.AddTransient<IUsersProfileService, UsersProfileService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<IPostService, PostService>();
        }
    }
}