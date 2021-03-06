using Microsoft.Extensions.DependencyInjection;
using UniHub.WebApi.BusinessLogic.Services;
using UniHub.WebApi.BusinessLogic.Services.Contract;
using UniHub.WebApi.BusinessLogic.Services.Shared;
using UniHub.WebApi.BusinessLogic.Services.Shared.Contract;

namespace UniHub.WebApi.Web.Extensions.StartupExtensions
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
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IPostTradeService, PostTradeService>();
            services.AddTransient<IRefreshTokenService, RefreshTokenService>();
            services.AddTransient<IComplaintService, ComplaintService>();
            services.AddTransient<ITeacherService, TeacherService>();
        }
    }
}