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
                _dataContext.SaveChanges();

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

            if (user.Role == Enums.Role.Teacher)
            {
                course = _dataContext.Course.ToList();
            }
            else if (user.Role == Enums.Role.Student)
            {
                var courseGrade = (from cg in _dataContext.CourseGrade.Where(cg => cg.Grade == user.Grade) select cg).ToList();
                var courseIds = (from c in courseGrade select c.CourseId).Distinct().ToList();

                course = (from x in _dataContext.Course.Where(x => courseIds.Contains(x.Id)) select x).ToList();
            }

            return course;
        }


        private void AddCourseGrade(CourseGrade courseGrade)
        {
            this._dataContext.CourseGrade.Add(courseGrade);
            this._dataContext.SaveChanges();
        }

        private void AddCourse(Course course)
        {
            this._dataContext.Course.Add(course);
            this._dataContext.SaveChanges();
        }

        public void CreateCourse(Course course)
        {
            var courseExists = (from c in _dataContext.Course.Where(c => c.Subject == course.Subject) select c).ToList();

            //this is new course
            if (courseExists.Count == 0)
            {
                this.AddCourse(course);
                this.AddCourseGrade(new CourseGrade { CourseId = course.Id, Grade = course.Grade });
                return;
            }
            var existingCourseId = courseExists[0].Id;

            try
            {
                var courseGradeExists =
                    (from cg in
                         _dataContext.CourseGrade.Where(cg => cg.CourseId == existingCourseId && cg.Grade == course.Grade)
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
        }
    }
}
