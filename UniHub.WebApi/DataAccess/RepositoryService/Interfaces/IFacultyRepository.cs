using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public interface IFacultyRepository : IBaseRepository<Faculty> 
    {
        Task<IEnumerable<Faculty>> GetFacultiesByUniversityAsync(int universityId, int skip, int take);
    }
}