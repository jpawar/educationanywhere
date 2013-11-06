using System;
using System.Collections.Generic;

using Models;

namespace DataAccess
{
    public interface IEducationAnywhereDataAccess
    {
        User RegisterUser(User user);

        User SignIn(User userData);

        List<Course> GetAllCoursesByRole(User user);

        void CreateCourse(Course course);
    }
}
