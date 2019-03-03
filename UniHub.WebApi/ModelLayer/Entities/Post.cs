using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Enums;

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
        public EPostLocationType PostLocationType { get; set; }
        [Required]
        public EPostValueType PostValueType { get; set; }
        [Required]
        public DateTime GivenAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastVisit { get; set; }

        //Relation to Group
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        //Relation to UserProfile
        public int UserProfileId { get; set; }
        public virtual UsersProfile UserProfile { get; set; }

        //Relation to File
        public virtual ICollection<File> Files { get; set; }

        //Relation to Comments
        public virtual ICollection<Comment> Comments { get; set; }

        //Relation to Subject
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public DateTime? DeletedAt { get; set; }

        // relation to Votes
        public virtual ICollection<PostVote> Votes { get; set; }

        // relation to Answers
        public virtual ICollection<Answer> Answers { get; set; }
    }
}