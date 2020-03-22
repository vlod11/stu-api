using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using UniHub.Common.Helpers.Contract;
using UniHub.Data;
using UniHub.Data.Entities;
using UniHub.Data.Interfaces;
using UniHub.Data.Repositories;
using UniHub.Services;
using UniHub.UnitTest.TestHelpers;
using Xunit;

namespace UniHub.UnitTest.IntegrationTests.Repositories
{
    public class UniversityRepositoryTest
    {
        private UniHubDbContext _dbContext;
        private readonly UniversityRepository _universityRepository;

        public UniversityRepositoryTest(IConfiguration configuration)
        {
            _universityRepository = CreateUniversityRepository(configuration);
        }
        public async Task<IEnumerable<University>> GetAllUniversitiesAsync(int skip, int take)
        {
            
            return await _dbContext.Universities
                                   .OrderBy(u => u.Id)
                                   .Skip(skip).Take(take)
                                   .ToListAsync();
        }
        
        [Fact]
        public async Task GetAllUniversitiesAsync_ThreeUniversitiesInDb_ReturnsAllUniversities()
        {   
            // ARRANGE
            var universities = new List<University>()
                     {
                         new University(),
                         new University()
                     };
            _universityRepository.AddRange(universities);
            await _dbContext.SaveChangesAsync();
            // ACT 
            var dbUniversities = await _universityRepository.GetAllUniversitiesAsync(0,int.MaxValue);
            // ASSET
            Assert.NotNull(dbUniversities);
            Assert.Equal(universities.Count, dbUniversities.ToList().Count);
        }

        public async Task<IEnumerable<University>> GetUniversitiesByCityAsync(int cityId, int skip, int take)
        {
            return await _dbContext.Universities
                                   .Where(u => u.CityId == cityId)
                                   .OrderBy(u => u.Id)
                                   .Skip(skip).Take(take)
                                   .ToListAsync();
        }
        private UniversityRepository CreateUniversityRepository(IConfiguration configuration)
        {
            _dbContext = new DbContextGenerator().GetDbContext(configuration);
            return new UniversityRepository(_dbContext);
        }
    }
}