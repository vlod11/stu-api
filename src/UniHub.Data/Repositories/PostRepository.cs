using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniHub.Common.Enums;
using UniHub.Data.Entities;
using UniHub.Data.Interfaces;
using UniHub.Data.Models;

namespace UniHub.Data.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        const int INITIAL_POSTS_COUNT = 13;

        public PostRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Post>> GetPostsBySubjectAsync(int subjectId, 
            string title = "", int groupId = 0, int? semester = 0, EPostValueType? valueType = null, EPostLocationType? locationType = null,
            DateTimeOffset? givenDateFrom = null, DateTimeOffset? givenDateTo = null, int skip = 0, int take = 0)
        {
            IQueryable<Post> posts = _dbContext.Posts
                                    .Where(p => p.SubjectId == subjectId)
                                    .Include(p => p.Group)
                                    .Include(p => p.Votes)
                                    .Include(p => p.UserAvailablePosts)
                                    .OrderByDescending(s => s.ModifiedAtUtc);

            if (!string.IsNullOrEmpty(title))
            {
                posts = posts.Where(p => p.Title.Contains(title, StringComparison.InvariantCulture));
            }

            if (groupId != 0)
            {
                posts = posts.Where(p => p.GroupId == groupId);
            }

            if (semester != 0)
            {
                posts = posts.Where(p => p.Semester == semester);
            }

            if (valueType != null)
            {
                posts = posts.Where(p => p.PostValueTypeId == (int)valueType);
            }

            if (locationType != null)
            {
                posts = posts.Where(p => p.PostLocationTypeId == (int)locationType);
            }

            if (givenDateFrom != null)
            {
                posts = posts.Where(p => p.GivenAt >= givenDateFrom);
            }

            if (givenDateTo != null)
            {
                posts = posts.Where(p => p.GivenAt <= givenDateTo);
            }

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

        public async Task<IEnumerable<PostBySemesterGroup>> GetInitialGroupedPostsBySubjectAsync(int subjectId,
            string title = "", int groupId = 0, int? semester = 0, EPostValueType? valueType = null, EPostLocationType? locationType = null,
            DateTimeOffset? givenDateFrom = null, DateTimeOffset? givenDateTo = null)
        {
            IQueryable<Post> posts = _dbContext.Posts
                                    .Where(p => p.SubjectId == subjectId)
                                    .Include(p => p.Group)
                                    .Include(p => p.Votes)
                                    .Include(p => p.UserAvailablePosts)
                                    .OrderByDescending(p => p.GroupId)
                                    .ThenBy(p => p.Semester)
                                    .ThenBy(p => p.GivenAt);

            if (!string.IsNullOrEmpty(title))
            {
                posts = posts.Where(p => p.Title.Contains(title, StringComparison.InvariantCulture));
            }

            if (groupId != 0)
            {
                posts = posts.Where(p => p.GroupId == groupId);
            }

            if (semester != 0)
            {
                posts = posts.Where(p => p.Semester == semester);
            }

            if (valueType != null)
            {
                posts = posts.Where(p => p.PostValueTypeId == (int)valueType);
            }

            if (locationType != null)
            {
                posts = posts.Where(p => p.PostLocationTypeId == (int)locationType);
            }

            if (givenDateFrom != null)
            {
                posts = posts.Where(p => p.GivenAt >= givenDateFrom);
            }

            if (givenDateTo != null)
            {
                posts = posts.Where(p => p.GivenAt <= givenDateTo);
            }

            IQueryable<PostBySemesterGroup> postsGrouped = posts.GroupBy(p => new { p.Group, p.Semester }, p => p)
                                    .Select(g => new PostBySemesterGroup
                                    {
                                        GroupId = g.Key.Group.Id,
                                        Semester = g.Key.Semester,
                                        GroupName = $"{g.Key.Group.Title}-{g.Key.Group.YearStart}-{g.Key.Group.Number}",
                                        Posts = g.OrderByDescending(p => p.ModifiedAtUtc).Take(INITIAL_POSTS_COUNT).ToList()
                                    });

            return await postsGrouped.ToListAsync();
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