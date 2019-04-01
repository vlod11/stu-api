using System.Threading.Tasks;
using UniHub.WebApi.DataAccess.RepositoryService.Interfaces;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public interface IUnitOfWork
    {
        ICountryRepository CountryRepository { get; }
        ICityRepository CityRepository { get; }
        IFacultyRepository FacultyRepository { get; }
        IFileRepository FileRepository { get; }
        IPostRepository PostRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        IUniversityRepository UniversityRepository { get; }
        ICredentionalRepository CredentionalRepository { get; }
        IUsersProfileRepository UsersProfileRepository { get; }
        ITeacherRepository TeacherRepository { get; }
        IPostActionRepository PostActionRepository { get; }

        Task CommitAsync();
    }
}