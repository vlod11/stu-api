using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class User : BaseEntity
    {
        public string Avatar { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.-]*$")]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(84, MinimumLength = 84)]
        public string PasswordHash { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public bool IsValidated { get; set; }
        public decimal CurrencyCount { get; set; } = 40;
        public DateTime LastVisit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public int RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public virtual RoleType Role { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<PostVote> Votes { get; set; }
        public virtual ICollection<UserAvailablePost> UserAvailablePosts { get; set; }
    }
}