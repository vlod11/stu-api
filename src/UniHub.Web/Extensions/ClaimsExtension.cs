using System;
using System.Linq;
using System.Security.Claims;
using UniHub.Common.Enums;
using UniHub.Common.Options;

namespace UniHub.Web.Extensions
{
    public static class ClaimsExtension
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            Claim accountClaim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == SetOfKeysForClaims.UserId);

            if (accountClaim == null || !int.TryParse(accountClaim.Value, out int userId))
            {
                throw new ArgumentNullException(nameof(claimsPrincipal), "Not found.");
            }

            return userId;
        }

        public static string GetUsername(this ClaimsPrincipal claimsPrincipal)
        {
            Claim accountClaim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == SetOfKeysForClaims.Username);

            if (accountClaim == null)
            {
                throw new ArgumentNullException(nameof(claimsPrincipal), "Not found.");
            }

            return accountClaim.Value;
        }

        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            Claim accountClaim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == SetOfKeysForClaims.EmailClaimKey);

            if (accountClaim == null)
            {
                throw new ArgumentNullException(nameof(claimsPrincipal), "Not found.");
            }

            return accountClaim.Value;
        }

        public static ERoleType GetRoleType(this ClaimsPrincipal claimsPrincipal)
        {
            Claim accountClaim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
            return (ERoleType)Enum.Parse(typeof(ERoleType), accountClaim.Value);
        }
    }
}