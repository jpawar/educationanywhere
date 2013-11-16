using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

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


        public List<Course> GetAllCoursesByRole(User user)
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
            int courseGradeId  = FindCourseId(tutorial.Subject, tutorial.Grade);

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

            return courseGradeId;
        }
    }
}
