using System;
using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class Credentional : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(84, MinimumLength = 84)]
        public string PasswordHash { get; set; }
        public virtual UsersProfile UserProfile { get; set; }
    }
}