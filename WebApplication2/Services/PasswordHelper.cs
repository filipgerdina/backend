using System.Security.Cryptography;

namespace WebApplication2.Services
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[16];
            rng.GetBytes(salt);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);
            return Convert.ToBase64String(hash) + ":" + Convert.ToBase64String(salt);
        }

        public static bool VerifyPassword(string password, string hashWithSalt)
        {
            var parts = hashWithSalt.Split(':');
            if (parts.Length != 2) return false;
            byte[] hash = Convert.FromBase64String(parts[0]);
            byte[] salt = Convert.FromBase64String(parts[1]);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hashToCheck = pbkdf2.GetBytes(32);
            return hash.SequenceEqual(hashToCheck);
        }
    }

}
