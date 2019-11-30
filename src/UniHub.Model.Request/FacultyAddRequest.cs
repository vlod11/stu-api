using System.ComponentModel.DataAnnotations;

namespace UniHub.Model.Request
{
    public class FacultyAddRequest
    {
        [Required]
        [StringLength(7, MinimumLength = 3)]
        public string ShortTitle { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FullTitle { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        [Required]
        public int UniversityId { get; set; }
    }
}