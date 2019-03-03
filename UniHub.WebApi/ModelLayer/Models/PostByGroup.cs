using System.Collections.Generic;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.ModelLayer.Models
{
    public class PostByGroup
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public List<Post> Posts { get; set; }
    }
}