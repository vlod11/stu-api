using System.Threading.Tasks;
using UniHub.WebApi.DataAccess.RepositoryService.Interfaces;
using UniHub.WebApi.DataAccess.RepositoryService.Repositories;
using UniHub.WebApi.Model;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UniHubDbContext _dbContext;

        public IFacultyRepository FacultyRepository { get; }
        public IFileRepository FileRepository { get; }
        public IPostRepository PostRepository { get; }
        public ISubjectRepository SubjectRepository { get; }
        public IUniversityRepository UniversityRepository { get; }
        public IUserRepository UserRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public ICityRepository CityRepository { get; }
        public ITeacherRepository TeacherRepository { get; }
        public IPostVoteRepository PostVoteRepository { get; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }
        public UnitOfWork(UniHubDbContext dbContext,
                IFacultyRepository facultyRepository,
                IFileRepository fileRepository,
                IPostRepository postRepository,
                IUniversityRepository universityRepository,
                ISubjectRepository subjectRepository,
                IUserRepository userRepository,
                ICountryRepository countryRepository,
                ICityRepository cityRepository,
                ITeacherRepository teacherRepository,
                IPostVoteRepository postActionRepository,
                IRefreshTokenRepository refreshTokenRepository)
        {
            RefreshTokenRepository = refreshTokenRepository;
            FacultyRepository = facultyRepository;
            FileRepository = fileRepository;
            PostRepository = postRepository;
            UniversityRepository = universityRepository;
            SubjectRepository = subjectRepository;
            UserRepository = userRepository;
            CountryRepository = countryRepository;
            CityRepository = cityRepository;
            TeacherRepository = teacherRepository;
            PostVoteRepository = postActionRepository;

            _dbContext = dbContext;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}