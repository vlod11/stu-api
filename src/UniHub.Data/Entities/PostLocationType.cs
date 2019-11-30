using System.Collections.Generic;

namespace UniHub.Data.Entities
{
    public class PostLocationType : BaseEnum
    {
        public virtual ICollection<Post> Posts { get; set; }
    }
}