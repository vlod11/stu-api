using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniHub.Data;

namespace UniHub.UnitTest.TestHelpers
{
    public class DbTestInitializer
    {
        private UniHubDbContext _dbContext;
        public DbTestInitializer(UniHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private void checkOnExistingDatabase()
        {
            
        }

        public async Task DeleteAll()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.SaveChangesAsync();
        }

        public async Task Seed(bool isTests)
        {
            await _dbContext.Database.EnsureCreatedAsync();
            
            string sqlDatabaseFill = isTests ?
                File.ReadAllText("addTestData.sql")
                : File.ReadAllText(
                    Directory.GetCurrentDirectory() + "\\Services\\DBInitialService\\TestData\\addTestData.sql");
            
            int numberOfRowInserted = _dbContext.Database.ExecuteSqlCommand(sqlDatabaseFill);
            await _dbContext.SaveChangesAsync();
        }
    }
}