using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniHub.WebApi.Models.Enums;

namespace UniHub.WebApi.Models.Requests
{
    public class PostAddRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
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
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int GroupId { get; set; }

        public IEnumerable<FileInfoRequest> FileInfoRequests { get; set; }
    }
}