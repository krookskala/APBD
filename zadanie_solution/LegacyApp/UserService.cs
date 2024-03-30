using System;
using System.Collections.Generic;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserCreditService _userCreditService;
        private readonly IEnumerable<IUserValidator> _validators;

        public UserService(IClientRepository clientRepository, IUserCreditService userCreditService, IEnumerable<IUserValidator> validators)
        {
            _validators = new List<IUserValidator>
            {
                new UserEmailVerifier(),
                new UserAgeVerifier(),
                new UserCreditVerifier()
            };
            
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _userCreditService = userCreditService ?? throw new ArgumentNullException(nameof(userCreditService));
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            var client = _clientRepository.GetById(clientId);
            if (client == null)
            {
                throw new ArgumentException($"Client with ID {clientId} does not exist.");
            }

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = email,
                DateOfBirth = dateOfBirth,
                Client = client
            };

            foreach (var validator in _validators)
            {
                if (!validator.Validate(user))
                {
                    return false;
                }
            }

            if (!user.HasCreditLimit || user.CreditLimit >= 500)
            {
                UserDataAccess.AddUser(user);
                return true;
            }

            return false;
        }
    }
}