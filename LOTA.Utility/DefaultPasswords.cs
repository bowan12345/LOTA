using System.Security.Cryptography;
using System.Text;

namespace LOTA.Utility
{
    /// <summary>
    /// Default password and policy constants class - Used for default passwords and security policies when bulk importing users
    /// Note: These passwords should be changed regularly and users must change them on first login
    /// </summary>
    public static class DefaultPasswords
    {
        private static readonly char[] LowercaseChars = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        private static readonly char[] UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private static readonly char[] DigitChars = "0123456789".ToCharArray();
        private static readonly char[] SpecialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?".ToCharArray();

        /// <summary>
        /// Password generation rule prefix (can be overridden by environment variables or configuration)
        /// </summary>
        public const string PasswordPrefix = "weltec";

        /// <summary>
        /// Whether to force new users to change password on first login
        /// </summary>
        public const bool ForcePasswordChangeOnFirstLogin = true;

        /// <summary>
        /// Generate default password based on user type
        /// </summary>
        /// <param name="userType">User type: Student, Tutor</param>
        /// <param name="prefix">Password prefix (optional, defaults to PasswordPrefix)</param>
        /// <returns>Generated password</returns>
        public static string GetDefaultPassword(string userType, string prefix = null)
        {
            var actualPrefix = prefix ?? PasswordPrefix;
            return GenerateDefaultPassword(userType, actualPrefix);
        }

        /// <summary>
        /// Generate default password for Student users
        /// </summary>
        /// <param name="prefix">Password prefix (optional)</param>
        /// <returns>Generated password</returns>
        public static string GetStudentDefaultPassword(string prefix = null)
        {
            return GetDefaultPassword(Roles.Role_Student, prefix);
        }

        /// <summary>
        /// Generate default password for Tutor users
        /// </summary>
        /// <param name="prefix">Password prefix (optional)</param>
        /// <returns>Generated password</returns>
        public static string GetTutorDefaultPassword(string prefix = null)
        {
            return GetDefaultPassword(Roles.Role_Tutor, prefix);
        }


        /// <summary>
        /// Generate password based on rules
        /// </summary>
        /// <param name="prefix">Password prefix (e.g., "weltec")</param>
        /// <param name="minLength">Minimum length (default 12)</param>
        /// <param name="includeSpecialChars">Whether to include special characters (default true)</param>
        /// <returns>Generated password</returns>
        public static string GeneratePassword(string prefix = "weltec", int minLength = 12, bool includeSpecialChars = true)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                prefix = "weltec";

            // Ensure minimum length
            var remainingLength = Math.Max(minLength - prefix.Length, 6);
            
            // Build character set
            var allChars = new List<char>();
            allChars.AddRange(LowercaseChars);
            allChars.AddRange(UppercaseChars);
            allChars.AddRange(DigitChars);
            
            if (includeSpecialChars)
            {
                allChars.AddRange(SpecialChars);
            }

            // Generate random suffix
            var randomSuffix = GenerateRandomString(remainingLength, allChars.ToArray());
            
            // Ensure password contains at least one uppercase letter, one lowercase letter, one digit
            var password = EnsurePasswordComplexity(prefix + randomSuffix, includeSpecialChars);
            
            return password;
        }

        /// <summary>
        /// Generate default password for specified user type
        /// </summary>
        /// <param name="userType">User type: Student, Tutor</param>
        /// <param name="prefix">Password prefix (optional)</param>
        /// <returns>Generated password</returns>
        private static string GenerateDefaultPassword(string userType, string prefix = "weltec")
        {
            return userType?.ToLower() switch
            {
                "student" => GeneratePassword(prefix, 12, true),
                "tutor" => GeneratePassword(prefix, 12, true),
                _ => GeneratePassword(prefix, 12, true)
            };
        }

        /// <summary>
        /// Generate random string
        /// </summary>
        private static string GenerateRandomString(int length, char[] chars)
        {
            var result = new StringBuilder(length);
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[length];
            rng.GetBytes(bytes);

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[bytes[i] % chars.Length]);
            }

            return result.ToString();
        }

        /// <summary>
        /// Ensure password complexity requirements
        /// </summary>
        private static string EnsurePasswordComplexity(string password, bool includeSpecialChars)
        {
            var chars = password.ToCharArray();
            var hasLower = chars.Any(c => char.IsLower(c));
            var hasUpper = chars.Any(c => char.IsUpper(c));
            var hasDigit = chars.Any(c => char.IsDigit(c));
            var hasSpecial = includeSpecialChars && chars.Any(c => SpecialChars.Contains(c));

            // If missing required character types, randomly replace some characters
            if (!hasLower)
            {
                chars[Random.Shared.Next(chars.Length)] = LowercaseChars[Random.Shared.Next(LowercaseChars.Length)];
            }
            if (!hasUpper)
            {
                chars[Random.Shared.Next(chars.Length)] = UppercaseChars[Random.Shared.Next(UppercaseChars.Length)];
            }
            if (!hasDigit)
            {
                chars[Random.Shared.Next(chars.Length)] = DigitChars[Random.Shared.Next(DigitChars.Length)];
            }
            if (includeSpecialChars && !hasSpecial)
            {
                chars[Random.Shared.Next(chars.Length)] = SpecialChars[Random.Shared.Next(SpecialChars.Length)];
            }

            return new string(chars);
        }

        /// <summary>
        /// Validate password complexity
        /// </summary>
        /// <param name="password">Password to validate</param>
        /// <param name="minLength">Minimum length</param>
        /// <param name="requireSpecialChars">Whether special characters are required</param>
        /// <returns>Whether it meets complexity requirements</returns>
        public static bool ValidatePasswordComplexity(string password, int minLength = 6, bool requireSpecialChars = false)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < minLength)
                return false;

            var hasLower = password.Any(c => char.IsLower(c));
            var hasUpper = password.Any(c => char.IsUpper(c));
            var hasDigit = password.Any(c => char.IsDigit(c));
            var hasSpecial = !requireSpecialChars || password.Any(c => SpecialChars.Contains(c));

            return hasLower && hasUpper && hasDigit && hasSpecial;
        }
    }
}
