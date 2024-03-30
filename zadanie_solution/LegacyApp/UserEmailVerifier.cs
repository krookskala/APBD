using System;

namespace LegacyApp
{
    internal class UserEmailVerifier : IUserValidator
    {
        public bool Validate(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            var email = user.EmailAddress;
            return !string.IsNullOrEmpty(email) && email.Contains("@") && email.Contains(".");
        }
    }
}