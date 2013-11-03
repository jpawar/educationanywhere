using Models;

namespace EducationAnywhere.CommonTypes.Interface
{
    public interface IUserRegistrationFacade
    {
        User RegisterUser(User user);

        User SignIn(User userData);
    }
}
