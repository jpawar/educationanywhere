using Models;

namespace DataAccess
{
    public interface IEducationAnywhereDataAccess
    {
        User RegisterUser(User user);

        User SignIn(User userData);
    }
}
