using System.Collections.Generic;
using UniHub.WebApi.Models.Entities;

namespace UniHub.WebApi.Models.Models
{
    public class PostBySemesterGroup
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int Semester { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}