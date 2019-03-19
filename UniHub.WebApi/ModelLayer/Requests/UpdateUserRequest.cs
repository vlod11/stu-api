using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.ModelLayer.Requests
{
    public class UpdateUserRequest
    {
        [RegularExpression(@"^[a-zA-Z0-9_.-]*$")]
        [StringLength(50, MinimumLength = 3)]
        public string NewUsername { get; set; }
        [Url]
        public string NewAvatar { get; set; }
        public string NewPassword { get; set; }
        public string CurrentPassword { get; set; }
    }
}