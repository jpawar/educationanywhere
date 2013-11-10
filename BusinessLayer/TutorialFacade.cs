using System;
using System.Collections.Generic;

using DataAccess;

using EducationAnywhere.CommonTypes.Interface;

using Models;

namespace EducationAnywhere.BusinessLayer
{
    public class TutorialFacade : ITutorialFacade, IDisposable
    {
        private readonly IEducationAnywhereDataAccess _dataAccess;

        public TutorialFacade()
        {
            _dataAccess = new EducationAnywhereDataAccess();
        }

        public TutorialFacade(IEducationAnywhereDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Course> GetAllCoursesByRole(User user)
        {
            return _dataAccess.GetAllCoursesByRole(user);
        }


        public void UploadTutorial(Tutorial tutorial)
        {

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
