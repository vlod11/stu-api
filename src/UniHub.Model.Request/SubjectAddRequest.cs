using System.ComponentModel.DataAnnotations;

namespace UniHub.Model.Request
{
    public class SubjectAddRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public int FacultyId { get; set; }
        public int TeacherId { get; set; }
    }
}