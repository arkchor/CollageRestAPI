using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CollageRestAPI.Controllers.Interfaces;
using CollageRestAPI.Hypermedia;
using CollageRestAPI.Models;
using CollageRestAPI.Repositories;
using WebGrease.Css.Extensions;

namespace CollageRestAPI.Controllers
{
    [RoutePrefix("api/courses")]
    public class CoursesController : ApiController, ICourseController
    {
        [HttpGet, Route(Name = "GetCoursesCollection")]
        public IHttpActionResult GetCoursesCollection()
        {
            return Ok(BaseRepository.Instance.CoursesCollection);

        }

        [HttpGet, Route("{courseName}", Name = "GetCourseByName")]
        public IHttpActionResult GetCourseByName(string courseName)
        {
            var course = BaseRepository.Instance.CoursesCollection.Single(x => x.CourseName == courseName);
            course.Links = LinkManager.SingleCourseLinks(Url, course.CourseName);

            return Ok(course);
        }

        [HttpGet, Route("{courseName}/students", Name = "GetCourseStudents")]
        public IHttpActionResult GetCourseStudents(string courseName)
        {
            var course = BaseRepository.Instance.CoursesCollection.Single(x => x.CourseName == courseName);
            var students = new List<StudentModel>();
            var studentsCollection = BaseRepository.Instance.StudentsCollection;

            studentsCollection.ForEach(student =>
            {
                if (student.Courses.Contains(course))
                {
                    students.Add(student);
                }
            });

            return Ok(students);
        }

        [HttpGet, Route("{courseName}/grades", Name = "GetCourseGrades")]
        public IHttpActionResult GetCourseGrades(string courseName)
        {
            throw new NotImplementedException();
        }

        [HttpPost, Route(Name = "CreateCourse")]
        public IHttpActionResult CreateCourse(CourseModel courseToCreate)
        {
            courseToCreate.Links = LinkManager.SingleCourseLinks(Url, courseToCreate.CourseName);
            BaseRepository.Instance.CoursesCollection.Add(courseToCreate);

            return Created(LinkTemplates.Courses.GetCourseByNameLink(Url, courseToCreate.CourseName).Href, "");
        }

        public IHttpActionResult CreateGradeForStudent(int id, GradeModel gradeToCreate)
        {
            throw new NotImplementedException();
        }

        [HttpPut, Route(Name = "UpdateCourseByName")]
        public IHttpActionResult UpdateCourse(string courseName, CourseModel courseToUpdate)
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult UpdateGradeForStudent(int id, GradeModel gradeToUpdate)
        {
            throw new NotImplementedException();
        }

        [HttpDelete, Route(Name = "DeleteCourseByName")]
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
