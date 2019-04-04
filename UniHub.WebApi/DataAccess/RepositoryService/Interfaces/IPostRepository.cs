using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Models;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public interface IPostRepository : IBaseRepository<Post> 
    {
        Task<IEnumerable<PostByGroup>> GetAllPostsBySubjectAsync(int subjectId, int skip, int take);
        Task<Post> GetFullPostInfoAsync(int postId);
        Task<IEnumerable<Post>> GetUsersPostAsync(int userProfileId, int skip, int take);
    }
}