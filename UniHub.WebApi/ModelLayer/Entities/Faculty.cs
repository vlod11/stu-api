using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class Faculty : BaseEntity
    {
        [Required]
        [StringLength(7, MinimumLength = 3)]
        public string ShortTitle { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FullTitle { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public int UniversityId { get; set; }
        [ForeignKey(nameof(UniversityId))]
        public virtual University University { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}