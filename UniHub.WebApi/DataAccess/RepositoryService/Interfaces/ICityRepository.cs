using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public interface ICityRepository : IRepositoryBase<City>
    {
        Task<IEnumerable<City>> GetCitiesByCountryAsync(int countryId);
        Task<bool> IsCityExist(string cityTitle, int countryId);
    }
}