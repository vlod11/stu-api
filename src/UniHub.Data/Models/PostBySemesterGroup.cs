using System.Collections.Generic;
using UniHub.Data.Entities;

namespace UniHub.Data.Models
{
    public class PostBySemesterGroup
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int Semester { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}