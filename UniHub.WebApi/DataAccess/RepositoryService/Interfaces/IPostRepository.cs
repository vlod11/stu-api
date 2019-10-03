using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Enums;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostsBySubjectAsync(int subjectId,
            string title = "", int groupId = 0, int? semester = 0, EPostValueType? valueType = null, EPostLocationType? locationType = null,
            DateTimeOffset? givenDateFrom = null, DateTimeOffset? givenDateTo = null, int skip = 0, int take = 0);
        Task<IEnumerable<PostBySemesterGroup>> GetInitialGroupedPostsBySubjectAsync(int subjectId,
        string title = "", int groupId = 0, int? semester = 0, EPostValueType? valueType = null, EPostLocationType? locationType = null,
        DateTimeOffset? givenDateFrom = null, DateTimeOffset? givenDateTo = null);
        Task<Post> GetFullPostInfoAsync(int postId);
        Task<IEnumerable<Post>> GetFullUsersPostAsync(int userId, int skip, int take);
    }
}