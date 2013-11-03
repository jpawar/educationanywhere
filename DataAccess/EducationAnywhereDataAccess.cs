using System;
using System.Data.SqlClient;
using System.Linq;

using Models;

namespace DataAccess
{
    public class EducationAnywhereDataAccess : IEducationAnywhereDataAccess
    {
        private readonly EducationAnywhereContext _dataContext;

        public EducationAnywhereDataAccess(EducationAnywhereContext context)
        {
            _dataContext = context;
        }

        public EducationAnywhereDataAccess()
        {
            _dataContext = new EducationAnywhereContext();
        }

        public User RegisterUser(User user)
        {
            var exists = (from c in _dataContext.Users where (c.EmailAddress == user.EmailAddress) select c).Any();

            if (!exists)
            {
                var newUser = _dataContext.Users.Add(user);
                _dataContext.SaveChanges();

                return newUser;
            }

            return null;
        }

        public User SignIn(User user)
        {
            User selectedUser = null;

            try
            {
                var userList =
                    _dataContext.Users.Where(c => c.EmailAddress == user.EmailAddress && c.Password == user.Password)
                                .ToList();

                if (userList.Count == 1)
                {
                    selectedUser = userList[0];
                    selectedUser.Password = "";
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return selectedUser;
        }
    }
}
