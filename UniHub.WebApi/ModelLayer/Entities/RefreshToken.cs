using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        
        public DateTime ExpirationDate { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))] 
        public virtual User User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}