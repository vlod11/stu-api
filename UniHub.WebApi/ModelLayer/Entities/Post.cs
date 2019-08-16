using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.WebApi.ModelLayer.Entities
{
    public class Post : BaseEntity
    {
        public Post()
        {
            Files = new List<File>();
        }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Semester { get; set; }
        [Required]
        public DateTime GivenAt { get; set; }
        public DateTime LastVisit { get; set; }
        public int VotesCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [Required]
        public int PostLocationTypeId { get; set; }
        [ForeignKey(nameof(PostLocationTypeId))]
        public virtual PostLocationType PostLocationType { get; set; }

        [Required]
        public int PostValueTypeId { get; set; }
        [ForeignKey(nameof(PostValueTypeId))]
        public virtual PostValueType PostValueType { get; set; }

        public int GroupId { get; set; }
        [ForeignKey(nameof(GroupId))]
        public virtual Group Group { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public int SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public virtual Subject Subject { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<PostVote> Votes { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}