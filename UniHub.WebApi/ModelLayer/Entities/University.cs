using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DateTime CreatedAtUtc { get; set; }
        public DateTime ModifiedAtUtc { get; set; }
        public DateTime? DeletedAtUtc { get; set; }

        public int CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        public virtual City City { get; set; }

        public virtual ICollection<Faculty> Faculties { get; set; }
    }
}