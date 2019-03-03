using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public interface ICountryRepository : IRepositoryBase<Country>
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
    }
}