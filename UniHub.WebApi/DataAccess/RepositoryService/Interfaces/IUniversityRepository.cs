using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public interface IUniversityRepository : IRepositoryBase<University>
    {
        Task<IEnumerable<University>> GetUniversitiesByCityAsync(int cityId, int skip, int take);
        Task<IEnumerable<University>> GetAllUniversitiesAsync(int skip, int take);
    }
}