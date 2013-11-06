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
        List<Course> GetAllCoursesByRole(User user);

        void CreateCourse(Course course);
    }
}
