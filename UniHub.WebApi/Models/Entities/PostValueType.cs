using System.Collections.Generic;

namespace UniHub.WebApi.Models.Entities
{
    public class PostValueType : BaseEnum
    {
        public virtual ICollection<Post> Posts { get; set; }
    }
}