using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Models;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(UniHubDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<PostByGroup>> GetAllPostsBySubjectAsync(int subjectId, int skip, int take)
        {
            return await _dbContext.Posts
                                    .Where(p => p.SubjectId == subjectId)
                                    .OrderByDescending(s => s.GroupId)
                                    .ThenBy(p => p.Semester)
                                    .ThenBy(p => p.GivenAt)
                                    .Include(p => p.Group)
                                    .GroupBy(p => new
                                    {
                                        GroupId = p.GroupId,
                                        GroupName = $"{p.Group.Title}-{p.Group.YearStart}-{p.Group.Number}"
                                    },
                                    p => p)
                                    .Select(g => new PostByGroup
                                    {
                                        GroupId = g.Key.GroupId,
                                        GroupName = g.Key.GroupName,
                                        Posts = g.Select(p => p).ToList()
                                    })
                                    .Skip(skip).Take(take)
                                    .ToListAsync();
        }

        public async Task<Post> GetFullPostInfoAsync(int postId)
        {
            return await _dbContext.Posts
                                    .Where(p => p.Id == postId)
                                    .Include(p => p.Answers)
                                    .FirstOrDefaultAsync();
        }
    }
}