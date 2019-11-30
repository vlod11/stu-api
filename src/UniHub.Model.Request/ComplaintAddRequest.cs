using System.ComponentModel.DataAnnotations;

namespace UniHub.Model.Request
{
    public class ComplaintAddRequest
    {
        [Required]
        public int PostId { get; set; }
    }
}