using System;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Models;
using DataAccess;

namespace EducationAnywhere.Controllers
{
    public class CourseController : Controller
    {
        private EducationAnywhereContext db = new EducationAnywhereContext();

        //
        // GET: /Course

        public ActionResult Index()
        { 
            var user = Session["UserData"] as User;

            if (user == null)
            {
                throw new HttpRequestException("You are not logged in");
            }

            var course = db.Course.ToList();
            return View(course);
        }

        //
        // GET: /Course/Details/5

        public ActionResult Details(int id = 0)
        {
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

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
                var courseExists = (from c in db.Course.Where(c => c.Subject == course.Subject) select c).ToList();

                //this is new course
                if (courseExists.Count == 0)
                {
                    this.AddCourse(course);
                    this.AddCourseGrade(new CourseGrade { CourseId = course.Id, Grade = course.Grade });
                    return RedirectToAction("Index");
                }
                var existingCourseId = courseExists[0].Id;

                try
                {
                    var courseGradeExists =
                        (from cg in
                             db.CourseGrade.Where(cg => cg.CourseId == existingCourseId && cg.Grade == course.Grade)
                         select cg).ToList();

                    //this is new grade for existing course
                    if (courseGradeExists.Count == 0)
                    {
                        var courseGrade = new CourseGrade { CourseId = existingCourseId, Grade = course.Grade };
                        this.AddCourseGrade(courseGrade);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return RedirectToAction("Index");
            }

            return View(course);
        }

        private void AddCourseGrade(CourseGrade courseGrade)
        {
            this.db.CourseGrade.Add(courseGrade);
            this.db.SaveChanges();
        }

        private void AddCourse(Course course)
        {
            this.db.Course.Add(course);
            this.db.SaveChanges();
        }

        //
        // GET: /Course/Edit/5

        public ActionResult Edit(int id = 0)
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}