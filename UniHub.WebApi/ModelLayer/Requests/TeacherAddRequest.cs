using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.ModelLayer.Requests
{
    public class TeacherAddRequest
    {
        [StringLength(50, MinimumLength = 1)]
        public string FirstName { get; set; }
        [StringLength(50, MinimumLength = 1)]
        public string MiddleName { get; set; }
        [StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }
        public int UniversityId { get; set; }
    }
}