using System.Collections.Generic;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class PostValueType : BaseEnum
    {
        public virtual ICollection<Post> Posts { get; set; }
    }
}