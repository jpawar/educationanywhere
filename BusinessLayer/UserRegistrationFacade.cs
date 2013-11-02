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
    }

    
}
