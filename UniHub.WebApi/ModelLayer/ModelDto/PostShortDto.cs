using System;
using UniHub.WebApi.ModelLayer.Enums;

namespace UniHub.WebApi.ModelLayer.ModelDto
{
    public class PostShortDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Semester { get; set; }
        public string Description { get; set; }
        public int PointsCount { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int PostLocationType { get; set; }
        public int PostValueType { get; set; }
        public int UserId { get; set; }
    }
}