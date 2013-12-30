using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    public class TutorialController : BaseController
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
            var user = IsUserInSession();
            IsAuthorized();

            var courses = _courseFacade.GetAllCourses(user).Select(x => new { x.Id, x.Subject }).ToList();

            var json = JsonConvert.SerializeObject(courses);

            return json;
        }

        [System.Web.Mvc.HttpPost]
        public void Save([FromBody] Tutorial tutorial)
        {
            IsUserInSession();
            IsAuthorized();
            //var path = Path.Combine(Server.MapPath("~/uploads"), tutorial.FullFilePath);

            var path = "/uploads/" + tutorial.FullFilePath;
            tutorial.FullFilePath = path;
            _tutorialFacade.SaveTutorial(tutorial);
        }

        public ActionResult Index()
        {
            IsUserInSession();

            if (IsAuthorized())
            {
                return View();                
            }

            return View("Error");
        }

        //http://css.dzone.com/articles/angularjs-file-upload

        [System.Web.Mvc.HttpPost]
        public ContentResult Upload(HttpPostedFileBase file)
        {
            IsUserInSession();
            IsAuthorized();

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
            IsUserInSession();
            IsAuthorized();
            return new List<Tutorial>();
        }       
    }
}
