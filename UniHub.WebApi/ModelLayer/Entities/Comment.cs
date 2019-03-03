using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class Comment : BaseEntity
    {
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        //Relation to UserProfile
        public int UserProfileId { get; set; }
        public virtual UsersProfile UserProfile { get; set; }

        //Relation to Post
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        //Relation to File
        public virtual ICollection<File> Files { get; set; }
    }
}