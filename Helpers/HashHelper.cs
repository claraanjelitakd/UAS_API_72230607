using Microsoft.AspNetCore.Identity;

namespace SimpleRESTApi.Helpers
{
    public class HashHelper
    {
        private static PasswordHasher<object> _hasher = new PasswordHasher<object>();

        public static string HashPassword(string password)
        {
            return _hasher.HashPassword(null, password);
        }

        public static bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            var result = _hasher.VerifyHashedPassword(null, hashedPassword, inputPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}