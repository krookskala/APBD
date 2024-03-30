using System;

namespace LegacyApp
{
    public class UserAgeVerifier : IUserValidator
    {
        public bool Validate(User user)
        {
            if (user == null || user.DateOfBirth == default)
            {
                return false;
            }

            var age = CalculateAge(user.DateOfBirth);
            return age >= 21;
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) 
            {
                age--;
            }
            return age;
        }
    }
}