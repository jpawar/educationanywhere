﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        //http://css.dzone.com/articles/angularjs-file-upload

        [HttpPost]
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
