using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.ModelLayer.Requests
{
    public class ComplaintAddRequest
    {
        [Required]
        public int PostId { get; set; }
    }
}