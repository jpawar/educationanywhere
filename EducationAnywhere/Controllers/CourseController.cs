using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using EducationAnywhere.BusinessLayer;
using EducationAnywhere.CommonTypes.Interface;
using Models;

using Newtonsoft.Json;

namespace EducationAnywhere.Controllers
{    
    public class CourseController : BaseController
    {
        private ICourseFacade _courseFacade;

        public CourseController()
        {
            _courseFacade = new CourseFacade();
        }

        public CourseController(ICourseFacade courseFacade)
        {
            _courseFacade = courseFacade;
        }

        [System.Web.Mvc.HttpGet]
        public string GetAll()
        {
            var user = IsUserInSession();

            var courses = _courseFacade.GetAllCourses(user).Select(x => new { x.Id, x.Subject }).ToList();
            var json = JsonConvert.SerializeObject(courses);

            return json;
        }

        //
        // GET: /Course

        public ActionResult Index()
        {
            IsUserInSession();
            return View();
        }

        //
        // GET: /Course/Details
        
        [System.Web.Mvc.HttpGet]
        public string Details()
        {
            var user = IsUserInSession();

            var courses = _courseFacade.GetAllCoursesByRole(user);
            var json = JsonConvert.SerializeObject(courses);
            return json;
        }
        

        //
        // GET: /Course/Create

        public ActionResult Create()
        {
            IsUserInSession();
            return View();
        }

        //
        // POST: /Course/Create

        [System.Web.Mvc.HttpPost]        
        public ActionResult Create([FromBody] Course course)
        {
            IsUserInSession();

            if (ModelState.IsValid)
            {
                _courseFacade.CreateCourse(course);
                return RedirectToAction("Index"); //ToDo this is not needed since javascript is handling that refactor later
            }

            return View(course);
        }        
    }
}