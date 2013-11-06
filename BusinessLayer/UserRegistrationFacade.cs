using System.Net;
using System.Security.Authentication;

using DataAccess;

using EducationAnywhere.CommonTypes.Interface;

using Models;

namespace EducationAnywhere.BusinessLayer
{
    public class UserRegistrationFacade : IUserRegistrationFacade
    {
        private readonly IEducationAnywhereDataAccess _dataAccess;

        public UserRegistrationFacade()
        {
            _dataAccess = new EducationAnywhereDataAccess();
        }

        public UserRegistrationFacade(IEducationAnywhereDataAccess dataAccess )
        {
            _dataAccess = dataAccess;
        }

        public User RegisterUser(User user)
        {
            return _dataAccess.RegisterUser(user);
        }

        public User SignIn(User userData)
        {
            User user = _dataAccess.SignIn(userData);

            if (user == null)
            {
                throw new InvalidCredentialException("Incorrect Credentials");
            }
            
            return user;
        }
    }

    
}
