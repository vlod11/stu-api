using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.ModelLayer.Requests
{
    public class LoginUserRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

//TODO: password validation
        [Required]
        [StringLength(32, MinimumLength = 6)]
        public string Password { get; set; }
    }
}