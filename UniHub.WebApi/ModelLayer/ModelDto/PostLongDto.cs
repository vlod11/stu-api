using System;
using System.Collections.Generic;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Enums;

namespace UniHub.WebApi.ModelLayer.ModelDto
{
    public class PostLongDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int Semester { get; set; }
        public DateTime GivenAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int PostLocationType { get; set; }
        public int PostValueType { get; set; }
        public int GroupId { get; set; }
        public string GroupTitle { get; set; }
        public int UserId { get; set; }

        public IEnumerable<AnswerDto> Answers { get; set; }
        public IEnumerable<FileDto> Files { get; set; }
    }
}