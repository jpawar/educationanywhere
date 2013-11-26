using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using EducationAnywhere.BusinessLayer;
using EducationAnywhere.CommonTypes.Interface;

using Models;

using Newtonsoft.Json;

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

        [System.Web.Mvc.HttpGet]
        public string Course()
        {
            var user = Session["UserData"] as User;

            if (user == null)
            {
                throw new HttpRequestException("You are not logged in");
            }

            var courses = _courseFacade.GetAllCourses(user).Select(x => new { x.Id, x.Subject }).ToList();

            var json = JsonConvert.SerializeObject(courses);

            return json;
        }

        [System.Web.Mvc.HttpPost]
        public void Save([FromBody] Tutorial tutorial)
        {
            var user = Session["UserData"] as User;

            if (user == null)
            {
                throw new HttpRequestException("You are not logged in");
            }

            var path = Path.Combine(Server.MapPath("~/uploads"), tutorial.FullFilePath);
            tutorial.FullFilePath = path;
            _tutorialFacade.SaveTutorial(tutorial);
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

        //http://css.dzone.com/articles/angularjs-file-upload

        [System.Web.Mvc.HttpPost]
        public ContentResult Upload(HttpPostedFileBase file)
        {
            var filename = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/uploads"), filename);
            file.SaveAs(path);

            return new ContentResult
            {
                ContentType = "text/plain",
                Content = filename,
                ContentEncoding = Encoding.UTF8
            };
        }

        // GET: /Tutorial/CourseGrade/{courseId}

        public List<Tutorial> GetCourseGrade(int courseGradeId)
        {
            return new List<Tutorial>();
        }
    }
}
