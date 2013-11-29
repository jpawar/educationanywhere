using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;

using Models;

namespace DataAccess
{
    public class EducationAnywhereDataAccess : IEducationAnywhereDataAccess
    {
        private readonly EducationAnywhereContext _dataContext;

        public EducationAnywhereDataAccess(EducationAnywhereContext context)
        {
            _dataContext = context;
        }

        public EducationAnywhereDataAccess()
        {
            _dataContext = new EducationAnywhereContext();
        }

        public User RegisterUser(User user)
        {
            var exists = (from c in _dataContext.Users where (c.EmailAddress == user.EmailAddress) select c).Any();

            if (!exists)
            {
                var newUser = _dataContext.Users.Add(user);
                try
                {
                    _dataContext.SaveChanges();

                }
                catch (Exception e)
                {

                }

                return newUser;
            }

            return null;
        }

        public User SignIn(User user)
        {
            User selectedUser = null;

            try
            {
                var userList =
                    _dataContext.Users.Where(c => c.EmailAddress == user.EmailAddress && c.Password == user.Password)
                                .ToList();

                if (userList.Count == 1)
                {
                    selectedUser = userList[0];
                    selectedUser.Password = "";
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return selectedUser;
        }

        public List<CourseTutorial> GetAllCoursesByRole(User user)
        {
            List<CourseTutorial> courseTutorialList = null;

            var courseGrade = new List<CourseGrade>();

            var role = (Enums.Role)(Int32.Parse(user.Role));

            if (role == Enums.Role.Student)
            {
                courseGrade = _dataContext.CourseGrade.Where(cg => cg.Grade == user.Grade).OrderBy(cg => cg.CourseId).ToList();

                courseTutorialList = PopulateCourseTutorials(courseGrade);
            }
            else
            {
                courseGrade = _dataContext.CourseGrade.OrderBy(cg => cg.CourseId).ToList();
                courseTutorialList = PopulateCourseTutorials(courseGrade);
            }

            return courseTutorialList;
        }

        private List<CourseTutorial> PopulateCourseTutorials(List<CourseGrade> courseGrade)
        {
            var courseTutorialList = new List<CourseTutorial>();

            var currentSubject = string.Empty;
            var courseTutorial = new CourseTutorial();

            foreach (var cg in courseGrade)
            {
                var course = _dataContext.Course.Find(cg.CourseId);
                if (currentSubject != course.Subject)
                {
                    courseTutorial = new CourseTutorial();
                    courseTutorial.Course = course;                    
                    courseTutorialList.Add(courseTutorial);
                    
                    currentSubject = course.Subject;
                }

                var tutorialList = _dataContext.Tutorial.Where(t => t.CourseGradeId == cg.Id).ToList();

                if (tutorialList.Count > 0)
                {
                    foreach (var tut in tutorialList)
                    {
                        tut.Grade = cg.Grade;
                        tut.FullFilePath = "http://localhost:58879" + tut.FullFilePath;
                        courseTutorial.Tutorials.Add(tut);
                    }
                }
            }

            return SetTheSortedOrder(courseTutorialList);
            
        }

        private List<CourseTutorial> SetTheSortedOrder(List<CourseTutorial> courseTutorialList)
        {
            List<CourseTutorial> sortedCourseTutorialList = courseTutorialList.OrderBy(x => x.Course.Subject).ToList();

            foreach (var courseTutorial in sortedCourseTutorialList)
            {
                var tut = courseTutorial.Tutorials.OrderBy(t => t.Grade).ToList();
                courseTutorial.Tutorials = tut;
            }

            return sortedCourseTutorialList;
        }

        //public IQueryable GetAllCoursesByRole(User user)
        //{
        //    //var tutorialList = new List<Tutorial>();

        //    IQueryable tutorials;

        //    var role = (Enums.Role)(Int32.Parse(user.Role));

        //    if (role == Enums.Role.Student)
        //    {
        //        //tutorials = from t in _dataContext.Tutorial
        //        //    join cg in _dataContext.CourseGrade on t.CourseGradeId equals cg.Id
        //        //    join c in _dataContext.Course on cg.CourseId equals c.Id
        //        //    select t;
        //        tutorials = (from c in _dataContext.Course
        //                    join cg in _dataContext.CourseGrade on c.Id equals cg.CourseId
        //                    join t in _dataContext.Tutorial on cg.Id equals t.CourseGradeId
        //                    where cg.Grade == user.Grade
        //                    select new
        //                    {
        //                        Description = t.Description,
        //                        FullFilePath = t.FullFilePath,
        //                        Grade = cg.Grade,
        //                        Subject = c.Subject
        //                    }).OrderBy(t => t.Subject).GroupBy(t => t.Grade);


        //    }
        //    else
        //    {
        //        tutorials = (from c in _dataContext.Course
        //                    join cg in _dataContext.CourseGrade on c.Id equals cg.CourseId
        //                    join t in _dataContext.Tutorial on cg.Id equals t.CourseGradeId
        //                    select new 
        //                        {
        //                            Description = t.Description,
        //                            FullFilePath = t.FullFilePath,
        //                            Grade = cg.Grade,
        //                            Subject = c.Subject
        //                        }).OrderBy(t => t.Subject).GroupBy(t => t.Grade);
        //    }

        //    return tutorials;
        //}


        public List<Course> GetAllCourses(User user)
        {
            var course = new List<Course>();

            var role = (Enums.Role)(Int32.Parse(user.Role));
            ;
            if (role == Enums.Role.Teacher)
            {
                course = _dataContext.Course.ToList();
            }
            else if (role == Enums.Role.Student)
            {
                var courseGrade = (from cg in _dataContext.CourseGrade.Where(cg => cg.Grade == user.Grade) select cg).ToList();
                var courseIds = (from c in courseGrade select c.CourseId).Distinct().ToList();

                course = (from x in _dataContext.Course.Where(x => courseIds.Contains(x.Id)) select x).ToList();
            }

            return course;
        }


        private void AddCourseGrade(CourseGrade courseGrade)
        {
            _dataContext.CourseGrade.Add(courseGrade);
            _dataContext.SaveChanges();
        }

        private void AddCourse(Course course)
        {
            _dataContext.Course.Add(course);
            _dataContext.SaveChanges();
        }

        public void CreateCourse(Course course)
        {
            var courseExists = (from c in _dataContext.Course.Where(c => c.Subject == course.Subject) select c).ToList();

            //is new course
            if (courseExists.Count == 0)
            {
                AddCourse(course);
                AddCourseGrade(new CourseGrade { CourseId = course.Id, Grade = course.Grade });
                return;
            }
            var existingCourseId = courseExists[0].Id;

            try
            {
                var courseGradeExists =
                    (from cg in
                         _dataContext.CourseGrade.Where(cg => cg.CourseId == existingCourseId && cg.Grade == course.Grade)
                     select cg).ToList();

                //is new grade for existing course
                if (courseGradeExists.Count == 0)
                {
                    var courseGrade = new CourseGrade { CourseId = existingCourseId, Grade = course.Grade };
                    AddCourseGrade(courseGrade);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveTutorial(Tutorial tutorial)
        {
            int courseGradeId = FindCourseId(tutorial.Subject, tutorial.Grade);

            if (courseGradeId > 0)
            {
                tutorial.CourseGradeId = courseGradeId;
                _dataContext.Tutorial.Add(tutorial);
                _dataContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Unable to find course and grade");
            }
        }

        private int FindCourseId(string course, string grade)
        {
            int courseGradeId = 0;

            int courseId = int.Parse(course);

            var courseGrade = (from cg in _dataContext.CourseGrade where (cg.CourseId == courseId && cg.Grade == grade) select cg).ToList();

            if (courseGrade.Count > 0)
            {
                courseGradeId = courseGrade[0].Id;
            }
            else
            {
                var cg = new CourseGrade() { CourseId = int.Parse(course), Grade = grade };
                _dataContext.CourseGrade.Add(cg);
                _dataContext.SaveChanges();
                courseGradeId = cg.Id;
            }

            return courseGradeId;
        }
    }
}
