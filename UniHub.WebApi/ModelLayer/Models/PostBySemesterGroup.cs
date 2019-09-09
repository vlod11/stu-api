using System.Collections.Generic;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.ModelLayer.Models
{
    public class PostBySemesterGroup
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int Semester { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}