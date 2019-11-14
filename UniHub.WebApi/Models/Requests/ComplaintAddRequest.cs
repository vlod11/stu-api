using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.Models.Requests
{
    public class ComplaintAddRequest
    {
        [Required]
        public int PostId { get; set; }
    }
}