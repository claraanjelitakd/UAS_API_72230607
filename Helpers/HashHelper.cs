using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAS_POS_CLARA.Helpers
{
    public static class HashHelper
    {
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty", nameof(password));
            }
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
        public static bool VerifyPassword(string inputPassword, string hashedPassword)
{
    var hashOfInput = HashPassword(inputPassword);
    return hashOfInput == hashedPassword;
}
    }
}