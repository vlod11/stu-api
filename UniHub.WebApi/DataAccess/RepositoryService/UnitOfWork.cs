using System.Threading.Tasks;
using UniHub.WebApi.DataAccess.RepositoryService.Interfaces;
using UniHub.WebApi.DataAccess.RepositoryService.Repositories;
using UniHub.WebApi.Model;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public class UnitOfWork : IUnitOfWork
    {
        UniHubDbContext _dbContext;
        public UnitOfWork(UniHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        IFacultyRepository facultyRepository;
        IFileRepository fileRepository;
        IPostRepository postRepository;
        IUniversityRepository universityRepository;
        ICredentionalRepository credentionalRepository;
        ISubjectRepository subjectRepository;
        IUsersProfileRepository usersProfileRepository;
        ICountryRepository countryRepository;
        ICityRepository cityRepository;
        ITeacherRepository teacherRepository;
        IPostActionRepository postActionRepository;      

        public IFacultyRepository FacultyRepository { get { return facultyRepository ?? (facultyRepository = new FacultyRepository(_dbContext)); } }
        public IFileRepository FileRepository { get { return fileRepository ?? (fileRepository = new FileRepository(_dbContext)); } }
        public IPostRepository PostRepository { get { return postRepository ?? (postRepository = new PostRepository(_dbContext)); } }
        public ISubjectRepository SubjectRepository { get { return subjectRepository ?? (subjectRepository = new SubjectRepository(_dbContext)); } }
        public IUniversityRepository UniversityRepository { get { return universityRepository ?? (universityRepository = new UniversityRepository(_dbContext)); } }
        public ICredentionalRepository CredentionalRepository { get { return credentionalRepository ?? (credentionalRepository = new CredentionalRepository(_dbContext)); } }
        public IUsersProfileRepository UsersProfileRepository { get { return usersProfileRepository ?? (usersProfileRepository = new UsersProfileRepository(_dbContext)); } }
        public ICountryRepository CountryRepository { get { return countryRepository ?? (countryRepository = new CountryRepository(_dbContext)); } }
        public ICityRepository CityRepository { get { return cityRepository ?? (cityRepository = new CityRepository(_dbContext)); } }
        public ITeacherRepository TeacherRepository { get { return teacherRepository ?? (teacherRepository = new TeacherRepository(_dbContext)); } }
        public IPostActionRepository PostActionRepository { get { return postActionRepository ?? (postActionRepository = new PostActionRepository(_dbContext)); } }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}