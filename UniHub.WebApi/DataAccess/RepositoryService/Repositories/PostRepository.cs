using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Models;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<PostByGroup>> GetAllPostsFullBySubjectAsync(int subjectId, int skip, int take)
        {
            return await _dbContext.Posts
                                    .Where(p => p.SubjectId == subjectId)
                                    .Include(p => p.Group)
                                    .Include(p => p.Votes)
                                    .OrderByDescending(s => s.GroupId)
                                    .ThenBy(p => p.Semester)
                                    .ThenBy(p => p.GivenAt)
                                    .GroupBy(p => p.Group, p => p)
                                    .Select(g => new PostByGroup
                                    {
                                        GroupId = g.Key.Id,
                                        GroupName = $"{g.Key.Title}-{g.Key.YearStart}-{g.Key.Number}",
                                        Posts = g.ToList()
                                    })
                                    .Skip(skip).Take(take)
                                    .ToListAsync();
        }

        public async Task<Post> GetFullPostInfoAsync(int postId)
        {
            return await _dbContext.Posts
                                    .Where(p => p.Id == postId)
                                    .Include(p => p.Answers)
                                    .Include(p => p.Files)
                                    .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Post>> GetFullUsersPostAsync(int userId, int skip, int take)
        {
            var posts = _dbContext.Posts
                                    .Include(p => p.Group)
                                    .Include(p => p.Votes)
                                    .Include(p => p.Subject)
                                        .ThenInclude(s => s.Teacher)
                                    .Where(p => p.UserId == userId);

            if (skip != 0)
            {
                posts = posts.Skip(skip);
            }

            if (take != 0)
            {
                posts = posts.Take(take);
            }

            return await posts.ToListAsync();
        }
    }
}