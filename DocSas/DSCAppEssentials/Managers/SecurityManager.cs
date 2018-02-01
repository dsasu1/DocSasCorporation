using System;
using System.Collections.Generic;
using System.Text;
using BCrypt;

namespace DSCAppEssentials.Managers
{
    public class SecurityManager
    {
        /// <summary>
        /// Hashes the password.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>System.String.</returns>
        public static string HashPassword(string input, string salt)
        {
            return BCryptHelper.HashPassword(input, salt);
        }

        /// <summary>
        /// Verifies the password.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="hash">The hash.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool VerifyPassword(string input, string hash)
        {
            return BCryptHelper.CheckPassword(input, hash);

        }

        /// <summary>
        /// Generates the salt.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GenerateSalt()
        {
            return BCryptHelper.GenerateSalt(4);

        }
    }
}
