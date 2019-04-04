using System.Collections.Generic;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class PostActionType : BaseEnum
    {
        public virtual ICollection<PostAction> PostActions { get; set; }
    }
}