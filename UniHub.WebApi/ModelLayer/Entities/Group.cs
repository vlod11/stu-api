using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class Group : BaseEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
        [Required]
        public int YearStart { get; set; }
        [Required]
        public int Number { get; set; }

        // relation to Posts
        public virtual ICollection<Post> Posts { get; set; }
    }
}