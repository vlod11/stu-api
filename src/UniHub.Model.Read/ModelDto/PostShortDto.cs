using System;
using UniHub.Common.Enums;

namespace UniHub.Model.Read.ModelDto
{
    public class PostShortDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Semester { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }

        public int PointsCount { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime GivenAt { get; set; }
        public int PostLocationType { get; set; }
        public int PostValueType { get; set; }
        public int UserId { get; set; }
        public EPostVoteType UserVote { get; set; }
        public bool IsUnlocked { get; set; }
    }
}