using System;

namespace LegacyApp
{
    internal class UserCreditVerifier : IUserValidator
    {
        public bool Validate(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            return !user.HasCreditLimit || user.CreditLimit >= 500;
        }
    }
}