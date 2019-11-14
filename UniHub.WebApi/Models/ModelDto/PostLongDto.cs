using System;
using System.Collections.Generic;
using UniHub.WebApi.Models.Enums;

namespace UniHub.WebApi.Models.ModelDto
{
    public class PostLongDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int Semester { get; set; }
        public DateTime GivenAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int VotesCount { get; set; }
        public EPostLocationType PostLocationType { get; set; }
        public EPostValueType PostValueType { get; set; }
        public int GroupId { get; set; }
        public string GroupTitle { get; set; }
        public int UserId { get; set; }
        public EPostVoteType UserVoteType { get; set; }

        public IEnumerable<AnswerDto> Answers { get; set; }
        public IEnumerable<FileDto> Files { get; set; }
    }
}