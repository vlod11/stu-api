using Microsoft.Extensions.DependencyInjection;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.DataAccess.RepositoryService.Interfaces;
using UniHub.WebApi.DataAccess.RepositoryService.Repositories;

namespace UniHub.WebApi.Web.Extensions.StartupExtensions
{
    public static class RepositoryExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IFacultyRepository, FacultyRepository>();
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<IUniversityRepository, UniversityRepository>();
            services.AddTransient<IUserAvailablePostRepository, UserAvailablePostRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();
            services.AddTransient<IPostVoteRepository, PostVoteRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IComplaintRepository, ComplaintRepository>();
        }
    }
}