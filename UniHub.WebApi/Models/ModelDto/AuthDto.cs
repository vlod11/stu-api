using System;

namespace UniHub.WebApi.Models.ModelDto
{
    public class AuthDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset ExpireAt { get; set; }
        public UserDto User { get; set; }
    }
}