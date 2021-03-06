using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.Models.Requests.Authorization
{
    public class RegisterUserRequest
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.]*$")]
        [StringLength(32, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // TODO: return validation
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+=\[{\]};:<>|./?,-]).{6,32}$")]
        [StringLength(32, MinimumLength = 6)]
        public string Password { get; set; }
    }
}