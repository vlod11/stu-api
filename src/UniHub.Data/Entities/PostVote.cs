using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.Data.Entities
{
    public class PostVote : BaseEntity
    {
        public PostVote(int userId = default, User user = null, int voteTypeId = default, PostVoteType voteType = null, int postId = default, Post post = null)
        {
            UserId = userId;
            User = user;
            VoteTypeId = voteTypeId;
            VoteType = voteType;
            PostId = postId;
            Post = post;
        }

        public PostVote()
        {
        }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public int VoteTypeId { get; set; }
        [ForeignKey(nameof(VoteTypeId))]
        public virtual PostVoteType VoteType { get; set; }

        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; }
    }
}