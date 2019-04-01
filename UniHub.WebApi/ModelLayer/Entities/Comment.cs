using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class Comment : BaseEntity
    {
        public string Description { get; set; }
                public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public int UserProfileId { get; set; }
        [ForeignKey(nameof(UserProfileId))]
        public virtual UsersProfile UserProfile { get; set; }

        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; }
        
        public virtual ICollection<File> Files { get; set; }
    }
}