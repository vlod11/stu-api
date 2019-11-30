using System.Collections.Generic;

namespace UniHub.Data.Entities
{
    public class PostVoteType : BaseEnum
    {
        public virtual ICollection<PostVote> PostActions { get; set; }
    }
}