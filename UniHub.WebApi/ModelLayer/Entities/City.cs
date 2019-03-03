using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class City : BaseEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        //Relation to Country
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        //Relation to University
        public virtual ICollection<University> Universities { get; set; }
    }
}