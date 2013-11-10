using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

using EducationAnywhere.BusinessLayer;
using EducationAnywhere.CommonTypes.Interface;

using Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EducationAnywhere.Controllers
{
    public class TutorialController : Controller
    {
        private ICourseFacade _courseFacade;
        private ITutorialFacade _tutorialFacade;

        public TutorialController()
        {
            _courseFacade = new CourseFacade();
            _tutorialFacade = new TutorialFacade();
        }

        public TutorialController(ICourseFacade courseFacade, ITutorialFacade tutorialFacade)
        {
            _courseFacade = courseFacade;
            _tutorialFacade = tutorialFacade;
        }

        [HttpGet]
        public string Course()
        {
            var user = Session["UserData"] as User;

            if (user == null)
            {
                throw new HttpRequestException("You are not logged in");
            }

            var courses = _courseFacade.GetAllCoursesByRole(user).Select(x => new { x.Id, x.Subject }).ToList();

            var json = JsonConvert.SerializeObject(courses);

            return json;
        }

        public ActionResult Index()
        {
            var user = Session["UserData"] as User;

            if (user == null)
            {
                throw new HttpRequestException("You are not logged in");
            }

            return View();
        }

        
        // GET: /Tutorial/CourseGrade/{courseId}

        public List<Tutorial> GetCourseGrade(int courseGradeId)
        {
            return new List<Tutorial>();
        }
    }
}
