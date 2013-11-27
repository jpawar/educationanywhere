using System;
using System.Collections.Generic;
using System.Linq;

using Models;

namespace DataAccess
{
    public interface IEducationAnywhereDataAccess
    {
        User RegisterUser(User user);

        User SignIn(User userData);

        List<CourseTutorial> GetAllCoursesByRole(User user);

        void CreateCourse(Course course);

        void SaveTutorial(Tutorial tutorial);

        List<Course> GetAllCourses(User user);
    }
}
