using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class Answer : BaseEntity
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public int UserProfileId { get; set; }
        [ForeignKey(nameof(UserProfileId))]
        public virtual UsersProfile UserProfile { get; set; }

        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<AnswerVote> Votes { get; set; }
    }
}