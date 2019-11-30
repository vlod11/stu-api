using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.Data.Entities
{
    public class UserAvailablePost : BaseEntity
    {
        public UserAvailablePost(int userId = default, User user = null, int postId = default, Post post = null)
        {
            UserId = userId;
            User = user;
            PostId = postId;
            Post = post;
        }

        public UserAvailablePost()
        {
        }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; }
    }
}