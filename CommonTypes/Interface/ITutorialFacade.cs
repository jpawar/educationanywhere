using System.Collections.Generic;

using Models;

namespace EducationAnywhere.CommonTypes.Interface
{
    public interface ITutorialFacade
    {
        List<Course> GetAllCoursesByRole(User user);

        void UploadTutorial(Tutorial tutorial);
    }
}
