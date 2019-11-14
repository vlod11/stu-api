using System;
using UniHub.WebApi.Models.Enums;

namespace UniHub.WebApi.Models.ModelDto
{
    public class PostProfileDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Semester { get; set; }
        public DateTime LastVisit { get; set; }
        public EPostLocationType PostLocationType { get; set; }
        public int VotesCount { get; set; }
        public EPostValueType PostValueType { get; set; }
        public int GroupId { get; set; }
        public string GroupTitle { get; set; }
        public int UserId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectTitle { get; set; }
        public string TeacherName { get; set; }
        public EPostVoteType UserVote { get; set; }
    }
}