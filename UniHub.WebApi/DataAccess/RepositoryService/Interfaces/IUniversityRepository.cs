using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService.Interfaces
{
    public interface IUniversityRepository : IBaseRepository<University>
    {
        Task<IEnumerable<University>> GetUniversitiesByCityAsync(int cityId, int skip, int take);
        Task<IEnumerable<University>> GetAllUniversitiesAsync(int skip, int take);
    }
}