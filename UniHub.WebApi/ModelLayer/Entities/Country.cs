using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class Country : BaseEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        //Relation to Cities
        public virtual ICollection<City> Cities { get; set; }
    }
}