using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.ModelLayer.Requests
{
    public class RegisterUserRequest
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.-]*$")]
        [StringLength(32, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+=\[{\]};:<>|./?,-]).{6,32}$")]
        public string Password { get; set; }
    }
}