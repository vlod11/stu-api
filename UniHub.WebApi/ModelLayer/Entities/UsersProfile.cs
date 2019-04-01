using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using UniHub.WebApi.ModelLayer.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class UsersProfile : BaseEntity
    {
        public string Avatar { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.-]*$")]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public int CurrencyCount { get; set; } = 0;
        public DateTime LastVisit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public int RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public virtual RoleType Role { get; set; }

        public int CredentionalId { get; set; }
        [ForeignKey(nameof(CredentionalId))]
        public virtual Credentional Credentional { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<PostActionType> Votes { get; set; }
    }
}