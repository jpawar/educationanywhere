using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using EducationAnywhere.BusinessLayer;
using EducationAnywhere.CommonTypes.Interface;
using Models;

namespace EducationAnywhere.Controllers
{
    public class CourseController : Controller
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

        //
        // GET: /Course

        public ActionResult Index()
        { 
            var user = Session["UserData"] as User;

            if (user == null)
            {
                throw new HttpRequestException("You are not logged in");
            }

            var course = _courseFacade.GetAllCoursesByRole(user);

            return View(course);
        }

        

        //
        // GET: /Course/Details/5
        
        /*
        public ActionResult Details(int id = 0)
        {
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }
        */

        //
        // GET: /Course/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Course/Create

        [System.Web.Mvc.HttpPost]        
        public ActionResult Create([FromBody] Course course)
        {
            if (ModelState.IsValid)
            {
                _courseFacade.CreateCourse(course);
                return RedirectToAction("Index"); //ToDo this is not needed since javascript is handling that refactor later
            }

            return View(course);
        }

        

        //
        // GET: /Course/Upload/5
        /*
        
        public ActionResult Upload(int id = 0)
        {
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        //
        // POST: /Course/Edit/5

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        //
        // GET: /Course/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        //
        // POST: /Course/Delete/5

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Course.Find(id);
            db.Course.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        */
        
    }
}