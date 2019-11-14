using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.Models.Requests.Authorization
{
    public class LoginUserRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}