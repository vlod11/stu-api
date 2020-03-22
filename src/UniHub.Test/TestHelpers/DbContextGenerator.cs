using Microsoft.EntityFrameworkCore;
using UniHub.Data;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace UniHub.UnitTest.TestHelpers
{
    public class DbContextGenerator
    {
        private UniHubDbContext _dbContext;
        private static DbTestInitializer _dbTestInitializer;
        public UniHubDbContext GetDbContext(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UniHubDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("Test"));
            _dbContext = new UniHubDbContext(optionsBuilder.Options);
            _dbTestInitializer = new DbTestInitializer(_dbContext);
          
            RefreshDbData();
            return _dbContext;
        }
        private  async void RefreshDbData()
        {
            _dbTestInitializer.DeleteAll().Wait();
            _dbTestInitializer.Seed(true).Wait();
            await _dbContext.SaveChangesAsync();
        }
    }
}