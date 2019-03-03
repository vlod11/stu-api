using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class University : BaseEntity
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FullTitle { get; set; }
        [Required]
        [StringLength(7)]
        public string ShortTitle { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        //Relation to City
        public int CityId { get; set; }
        public virtual City City { get; set; }

        //Relation to Faculty
        public virtual ICollection<Faculty> Faculties { get; set; }
    }
}