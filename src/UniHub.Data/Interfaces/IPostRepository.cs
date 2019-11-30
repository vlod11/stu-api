using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.Common.Enums;
using UniHub.Data.Entities;
using UniHub.Data.Models;

namespace UniHub.Data.Interfaces
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