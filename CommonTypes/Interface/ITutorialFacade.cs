using System.Collections.Generic;

using Models;

namespace EducationAnywhere.CommonTypes.Interface
{
    public interface ITutorialFacade
    {
        //List<Tutorial> GetAllCoursesByRole(User user);

        void SaveTutorial(Tutorial tutorial);
    }
}
