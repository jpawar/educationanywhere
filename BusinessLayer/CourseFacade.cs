using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;

using EducationAnywhere.CommonTypes.Interface;

using Models;

namespace EducationAnywhere.BusinessLayer
{
    public class CourseFacade : ICourseFacade, IDisposable
    {
        private readonly IEducationAnywhereDataAccess _dataAccess;

        public CourseFacade()
        {
            _dataAccess = new EducationAnywhereDataAccess();
        }

        public CourseFacade(IEducationAnywhereDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Course> GetAllCoursesByRole(User user)
        {
            return _dataAccess.GetAllCoursesByRole(user);
        }

        public void CreateCourse(Course course)
        {
            _dataAccess.CreateCourse(course);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                var dataAccess = this._dataAccess as IDisposable;

                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
        }
        
    }
}
