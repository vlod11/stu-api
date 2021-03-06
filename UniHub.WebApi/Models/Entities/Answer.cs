using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.WebApi.Models.Entities
{
    public class Answer : BaseEntity
    {
        [Required]
        public string Description { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime ModifiedAtUtc { get; set; }

        public DateTime? DeletedAtUtc { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<AnswerVote> Votes { get; set; }
    }
}