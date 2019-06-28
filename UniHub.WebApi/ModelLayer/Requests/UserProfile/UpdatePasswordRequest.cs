using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.ModelLayer.Requests.User
{
    public class UpdatePasswordRequest
    {
        // TODO: return validation
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+=\[{\]};:<>|./?,-]).{6,32}|$")]
        public string NewPassword { get; set; }
        public string CurrentPassword { get; set; }
    }
}