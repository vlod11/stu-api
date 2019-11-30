using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniHub.Data.Entities
{
    public class Country : BaseEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}