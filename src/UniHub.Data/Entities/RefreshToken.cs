using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.Data.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        
        public DateTime ExpiredAt { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))] 
        public virtual User User { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime ModifiedAtUtc { get; set; }
    }
}