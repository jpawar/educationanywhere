using System;

using EducationAnywhere.BusinessLayer;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Models;

namespace UnitTests
{
    [TestClass]
    public class BusinessLayerTest
    {
        [TestMethod]
        public void UserRegistration()
        {
            var user = new User();
            user.EmailAddress = "test@yahoo.com";
            user.Grade = "2";
            user.Name = "Test User";
            user.Password = "TestPassword";
            user.Role = Enums.Role.Student.ToString();

            
            var userRegistrationFacade = new UserRegistrationFacade();
            var newUser = userRegistrationFacade.RegisterUser(user);

            Assert.IsTrue(newUser.Id > 0);
        }

        [TestMethod]
        public void CreateCourse()
        {
            var courseFacade = new CourseFacade();
            var course = new Course() { Grade = "2", Subject = "English" };
            courseFacade.CreateCourse(course);

            Assert.IsTrue(course.Id > 0);
        }

        [TestMethod]
        public void GetAllCourses()
        {
            var user = new User();
            user.EmailAddress = "test@yahoo.com";
            user.Grade = "2";
            user.Name = "Test User";
            user.Role = "1";

            var courseFacade = new CourseFacade();
            var courses = courseFacade.GetAllCourses(user);

            Assert.IsTrue(courses.Count > 0);
        }

        [TestMethod]
        public void SaveTutorial()
        {
            var tutorialFacade = new TutorialFacade();

            var tut = new Tutorial();
            tut.Description = "Learn Grammar";
            tut.FullFilePath = "/uploads/Grammar.mp4";
            tut.Grade = "2";
            tut.Subject = "1"; // denotes english subject
            

            tutorialFacade.SaveTutorial(tut);

            Assert.IsTrue(tut.Id > 0);

        }
    }
}
