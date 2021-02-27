using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace socialize_api.Services
{
    /// <summary>
    /// Handles the password validations.
    /// </summary>
    public static class PasswordService
    {
        #region Methods
        /// <summary>
        /// Generates the hash.
        /// </summary>
        /// <param name="salt">The salt</param>
        /// <param name="password">The password string.</param>
        /// <returns>The hashed password string.</returns>
        public static string GeneratePasswordHash(byte[] salt, string password)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        /// <summary>
        /// Generates the salt.
        /// </summary>
        /// <returns>The salt.</returns>
        public static string GenerateSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        /// <summary>
        /// Validates the user's entered password with saved hash.
        /// </summary>
        /// <param name="salt">The salt.</param>
        /// <param name="hash">The hash.</param>
        /// <param name="password">The password entered by user.</param>
        /// <returns>True if password matches, else False.</returns>
        public static bool Validate(string salt, string hash, string password)
        {
            return hash == GeneratePasswordHash(Convert.FromBase64String(salt), password);
        }
        #endregion Methods
    }
}
