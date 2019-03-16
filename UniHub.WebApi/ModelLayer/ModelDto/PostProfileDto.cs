using System;
using UniHub.WebApi.ModelLayer.Enums;

namespace UniHub.WebApi.ModelLayer.ModelDto
{
    public class PostProfileDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Semester { get; set; }
        public DateTime LastVisit { get; set; }
        public int PostLocationType { get; set; }
        public int PostValueType { get; set; }
        public int GroupId { get; set; }
        public string GroupTitle { get; set; }
        public int UserProfileId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectTitle { get; set; }
        public string TeacherName { get; set; }
    }
}