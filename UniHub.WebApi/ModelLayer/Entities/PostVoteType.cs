using System.Collections.Generic;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class PostVoteType : BaseEnum
    {
        public virtual ICollection<PostVote> PostActions { get; set; }
    }
}