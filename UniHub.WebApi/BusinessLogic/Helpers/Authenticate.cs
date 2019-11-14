using CryptoHelper;

namespace UniHub.WebApi.BusinessLogic.Helpers
{
    public static class Authenticate
    {
        public static string Hash(string inputValue)
        {
            return Crypto.HashPassword(inputValue);
        }

        public static bool Verify(string inputValue, string hashedValue)
        {
            if (string.IsNullOrEmpty(inputValue))
            {
                return false;
            }

            return Crypto.VerifyHashedPassword(hashedValue, inputValue);
        }
    }
}