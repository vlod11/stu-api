using System.Collections.Generic;

namespace UniHub.WebApi.ModelLayer.ModelDto
{
    public class PostBySemesterGroupDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int Semester { get; set; }
        
        public IEnumerable<PostShortDto> Posts { get; set; }
    }
}