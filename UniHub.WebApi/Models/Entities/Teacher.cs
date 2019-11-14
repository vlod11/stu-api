using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.WebApi.Models.Entities
{
    public class Teacher : BaseEntity
    {
        public string Avatar { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string FirstName { get; set; }
        [StringLength(50, MinimumLength = 1)]
        public string MiddleName { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime ModifiedAtUtc { get; set; }
        public DateTime? DeletedAtUtc { get; set; }

        public int UniversityId { get; set; }
        [ForeignKey(nameof(UniversityId))]
        public virtual University University { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}