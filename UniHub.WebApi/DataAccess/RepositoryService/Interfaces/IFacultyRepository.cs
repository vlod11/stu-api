using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.Models.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService.Interfaces
{
    public interface IFacultyRepository : IBaseRepository<Faculty> 
    {
        Task<IEnumerable<Faculty>> GetFacultiesByUniversityAsync(int universityId, int skip, int take);
    }
}