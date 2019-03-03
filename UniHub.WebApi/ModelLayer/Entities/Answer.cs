using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class Answer : BaseEntity
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        //Relation to UserProfile
        public int UserProfileId { get; set; }
        public virtual UsersProfile UserProfile { get; set; }

        //Relation to Post
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        //Relation to File
        public virtual ICollection<File> Files { get; set; }

        //Relation to Comments
        public virtual ICollection<Comment> Comments { get; set; }
        public DateTime? DeletedAt { get; set; }

        // relation to Votes
        public virtual ICollection<AnswerVote> Votes { get; set; }
    }
}