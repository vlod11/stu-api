using System.Collections.Generic;

namespace UniHub.WebApi.Models.Entities
{
    public class PostVoteType : BaseEnum
    {
        public virtual ICollection<PostVote> PostActions { get; set; }
    }
}