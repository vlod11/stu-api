using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class UsersProfile : BaseEntity
    {
        public UsersProfile()
        {
            Posts = new HashSet<Post>();
        }

        public string Avatar { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.-]*$")]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public int CurrencyCount { get; set; } = 0;
        public DateTime LastVisit { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        // relation to Credentional
        public int CredentionalId { get; set; }
        public virtual Credentional Credentional { get; set; }

        // relation to Posts
        public virtual ICollection<Post> Posts { get; set; }

        // relation to Votes
        public virtual ICollection<PostVote> Votes { get; set; }
    }
}