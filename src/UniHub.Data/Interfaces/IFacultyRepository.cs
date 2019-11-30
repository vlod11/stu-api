using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.Data.Entities;

namespace UniHub.Data.Interfaces
{
    public interface IFacultyRepository : IBaseRepository<Faculty> 
    {
        Task<IEnumerable<Faculty>> GetFacultiesByUniversityAsync(int universityId, int skip, int take);
    }
}