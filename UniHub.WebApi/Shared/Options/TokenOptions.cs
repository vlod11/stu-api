namespace UniHub.WebApi.Shared.Options
{
    public class TokenOptions
    {
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string IssuerSecurityKey { get; set; }
    }
}
