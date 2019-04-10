using System.Threading.Tasks;
using UniHub.WebApi.DataAccess.RepositoryService.Interfaces;
using UniHub.WebApi.DataAccess.RepositoryService.Repositories;
using UniHub.WebApi.Model;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public class UnitOfWork : IUnitOfWork
    {
        UniHubDbContext _dbContext;
        public UnitOfWork(UniHubDbContext dbContext,
                IFacultyRepository facultyRepository,
                IFileRepository fileRepository,
                IPostRepository postRepository,
                IUniversityRepository universityRepository,
                ICredentionalRepository credentionalRepository,
                ISubjectRepository subjectRepository,
                IUsersProfileRepository usersProfileRepository,
                ICountryRepository countryRepository,
                ICityRepository cityRepository,
                ITeacherRepository teacherRepository,
                IPostActionRepository postActionRepository)
        {
            FacultyRepository = facultyRepository;
            FileRepository = fileRepository;
            PostRepository = postRepository;
            UniversityRepository = universityRepository;
            CredentionalRepository = credentionalRepository;
            SubjectRepository = subjectRepository;
            UsersProfileRepository = usersProfileRepository;
            CountryRepository = countryRepository;
            CityRepository = cityRepository;
            TeacherRepository = teacherRepository;
            PostActionRepository = postActionRepository;
            _dbContext = dbContext;
        }

        public IFacultyRepository FacultyRepository { get; }
        public IFileRepository FileRepository { get; }
        public IPostRepository PostRepository { get; }
        public ISubjectRepository SubjectRepository { get; }
        public IUniversityRepository UniversityRepository { get; }
        public ICredentionalRepository CredentionalRepository { get; }
        public IUsersProfileRepository UsersProfileRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public ICityRepository CityRepository { get; }
        public ITeacherRepository TeacherRepository { get; }
        public IPostActionRepository PostActionRepository { get; }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}