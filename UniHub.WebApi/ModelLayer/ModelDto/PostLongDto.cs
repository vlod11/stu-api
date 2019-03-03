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
        public EPostLocationType PostLocationType { get; set; }
        public EPostValueType PostValueType { get; set; }

        // relation to Answers
        public List<AnswerDto> Answers { get; set; }
    }
}