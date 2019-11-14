using System.ComponentModel.DataAnnotations;

namespace UniHub.WebApi.Models.Requests.UserProfile
{
    public class UpdateUserInfoRequest
    {
        [RegularExpression(@"^[a-zA-Z0-9_.-]{5,32}|$")]
        public string NewUsername { get; set; }
        [RegularExpression(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?|$")]
        public string NewAvatar { get; set; }
    }
}