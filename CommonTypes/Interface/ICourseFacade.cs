using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models;

namespace EducationAnywhere.CommonTypes.Interface
{
    public interface ICourseFacade
    {
        IQueryable GetAllCoursesByRole(User user);

        void CreateCourse(Course course);

        List<Course> GetAllCourses(User user);
    }
}
