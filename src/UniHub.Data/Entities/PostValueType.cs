using System.Collections.Generic;

namespace UniHub.Data.Entities
{
    public class PostValueType : BaseEnum
    {
        public virtual ICollection<Post> Posts { get; set; }
    }
}