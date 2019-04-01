using System.Collections.Generic;

namespace UniHub.WebApi.ModelLayer.ModelDto
{
    public class PostCardDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public IEnumerable<PostShortDto> Posts { get; set; }
    }
}