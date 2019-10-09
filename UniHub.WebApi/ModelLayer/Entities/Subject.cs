using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class Subject : BaseEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime ModifiedAtUtc { get; set; }
        public DateTime? DeletedAtUtc { get; set; }

        public int FacultyId { get; set; }
        [ForeignKey(nameof(FacultyId))]
        public virtual Faculty Faculty { get; set; }

        public int TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public virtual Teacher Teacher { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}