using System;

namespace UniHub.Model.Read.ModelDto
{
    public class AuthDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset ExpireAt { get; set; }
        public UserDto User { get; set; }
    }
}