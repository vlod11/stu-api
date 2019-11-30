using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniHub.Data.Entities
{
    public class Post : BaseEntity
    {
        public Post(string title = null, string description = null, int semester = default, DateTime givenAt = default,
            DateTime lastVisit = default, int votesCount = default, DateTime createdAtUtc = default,
            DateTime modifiedAtUtc = default, DateTime? deletedAtUtc = default, int postLocationTypeId = default,
            PostLocationType postLocationType = null, int postValueTypeId = default, PostValueType postValueType = null,
            int groupId = default, Group @group = null, int userId = default, User user = null, int subjectId = default,
            Subject subject = null)
        {
            Title = title;
            Description = description;
            Semester = semester;
            GivenAt = givenAt;
            LastVisit = lastVisit;
            VotesCount = votesCount;
            CreatedAtUtc = createdAtUtc;
            ModifiedAtUtc = modifiedAtUtc;
            DeletedAtUtc = deletedAtUtc;
            PostLocationTypeId = postLocationTypeId;
            PostLocationType = postLocationType;
            PostValueTypeId = postValueTypeId;
            PostValueType = postValueType;
            GroupId = groupId;
            Group = @group;
            UserId = userId;
            User = user;
            SubjectId = subjectId;
            Subject = subject;
        }
        
        public Post()
        {
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
        public DateTime CreatedAtUtc { get; set; }
        public DateTime ModifiedAtUtc { get; set; }
        public DateTime? DeletedAtUtc { get; set; }

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

        public virtual ICollection<UserAvailablePost> UserAvailablePosts { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}