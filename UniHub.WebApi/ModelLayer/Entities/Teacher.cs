using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class Teacher : BaseEntity
    {
        public string Avatar { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public int UniversityId { get; set; }
        [ForeignKey(nameof(UniversityId))]
        public virtual University University { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}