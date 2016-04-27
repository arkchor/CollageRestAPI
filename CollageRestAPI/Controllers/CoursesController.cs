using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CollageRestAPI.Controllers.Interfaces;
using CollageRestAPI.Models;

namespace CollageRestAPI.Controllers
{
    [RoutePrefix("api/courses")]
    public class CoursesController : ApiController, ICourseController
    {
        public IHttpActionResult GetCoursesCollection()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult GetCourseByName(string courseName)
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult GetCourseStudents(string courseName)
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult GetCourseGrades(string courseName)
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult CreateCourse(CourseModel courseToCreate)
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult CreateGradeForStudent(int id, GradeModel gradeToCreate)
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult UpdateCourse(string courseName, CourseModel courseToUpdate)
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult UpdateGradeForStudent(int id, GradeModel gradeToUpdate)
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult DeleteCourse(string courseName)
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult DeleteGradeForStudent(int id, DateTime dateOfIssue)
        {
            throw new NotImplementedException();
        }
    }
}
