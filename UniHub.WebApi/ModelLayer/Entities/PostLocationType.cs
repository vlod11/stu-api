using System.Collections.Generic;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class PostLocationType : BaseEnum
    {
        public virtual ICollection<Post> Posts { get; set; }
    }
}