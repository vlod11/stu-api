using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public interface ICityRepository : IBaseRepository<City>
    {
        Task<IEnumerable<City>> GetCitiesByCountryAsync(int countryId);
        Task<bool> IsCityExistAsync(string cityTitle, int countryId);
    }
}