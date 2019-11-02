using UniHub.WebApi.DataAccess.RepositoryService.Interfaces;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService.Repositories
{
    public class ComplaintRepository : BaseRepository<Complaint>, IComplaintRepository
    {
        public ComplaintRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}