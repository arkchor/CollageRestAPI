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
using MongoDB.Bson;
using MongoDB.Driver;
using WebGrease.Css.Extensions;

namespace CollageRestAPI.Controllers
{
    //[RoutePrefix("api/courses")]
    public class CoursesController : ApiController, ICourseController
    {
        /*=======================================
        =========== GET METHODS =================
        =======================================*/
        [HttpGet, Route(WebApiConfig.RoutesTemplates.Courses, Name = "GetCoursesCollection")]
        public IHttpActionResult GetCoursesCollection()
        {
            return Ok(BaseRepository.Instance.CoursesCollection);
        }

        [HttpGet, Route(WebApiConfig.RoutesTemplates.Courses, Name = "GetCourseById")]
        public IHttpActionResult GetCourseByName(ObjectId id)
        {
            var course = BaseRepository.Instance.CoursesCollection.Single(x => x.Id == id);
            //course.Links = LinkManager.SingleCourseLinks(Url, course.CourseName);
            return Ok(course);
        }

        [HttpGet, Route(WebApiConfig.RoutesTemplates.Courses, Name = "GetCourseByName")]
        public IHttpActionResult GetCourseByName(string courseName)
        {
            var course = BaseRepository.Instance.CoursesCollection.Single(x => x.CourseName == courseName);
            //course.Links = LinkManager.SingleCourseLinks(Url, course.CourseName);
            return Ok(course);
        }

        [HttpGet, Route(WebApiConfig.RoutesTemplates.CourseStudents, Name = "GetCourseStudents")]
        public IHttpActionResult GetCourseStudents(string courseName)
        {
            var course = BaseRepository.Instance.CoursesCollection.Single(x => x.CourseName == courseName);
            var students = new List<StudentModel>();
            course.StudentsReferences.ForEach(studentReference => students.Add(BaseRepository.Instance.Fetch<StudentModel>(studentReference)));
            //var studentsCollection = BaseRepository.Instance.StudentsCollection;

            //studentsCollection.ForEach(student =>
            //{
            //    if (student.Courses.Contains(course))
            //    {
            //        students.Add(student);
            //    }
            //});

            return Ok(students);
        }

        [HttpGet, Route(WebApiConfig.RoutesTemplates.CourseGrades, Name = "GetCourseGrades")]
        public IHttpActionResult GetCourseGrades(string courseName)
        {
            var course = BaseRepository.Instance.CoursesCollection.Single(x => x.CourseName == courseName);
            var grades = new List<GradeModel>();
            course.GradesReferences.ForEach(gradeReference => grades.Add(BaseRepository.Instance.Fetch<GradeModel>(gradeReference)));

            return Ok(grades);
        }

        /*=======================================
        =========== POST METHODS ================
        =======================================*/
        [HttpPost, Route(WebApiConfig.RoutesTemplates.Courses, Name = "CreateCourse")]
        public IHttpActionResult CreateCourse([FromBody] CourseModel courseToCreate)
        {
            courseToCreate.Links = LinkManager.SingleCourseLinks(Url, courseToCreate.CourseName);
            BaseRepository.Instance.CoursesCollection.Add(courseToCreate);

            return Created(LinkTemplates.Courses.GetCourseByNameLink(Url, courseToCreate.CourseName).Href, "");
        }

        [HttpPost, Route(WebApiConfig.RoutesTemplates.CourseGrades, Name = "CreateGradeForStudent")]
        public IHttpActionResult CreateGradeForStudent(int id, string courseName, [FromBody] GradeModel gradeToCreate)
        {
            var course = BaseRepository.Instance.CoursesCollection.Single(x => x.CourseName == courseName);
            var student = BaseRepository.Instance.StudentsCollection.Single(x => x.Id == id);
            gradeToCreate.CourseReference = new MongoDBRef(DatabaseConfig.CoursesCollectionName, course.Id);
            gradeToCreate.StudentReference = new MongoDBRef(DatabaseConfig.StudentsCollectionName, student.Id);
            BaseRepository.Instance.GradesCollection.Add(gradeToCreate);
            gradeToCreate.Links = LinkManager.SingleGradeLinks(Url, gradeToCreate.Id);
            BaseRepository.Instance.GradesCollection.Update(gradeToCreate);
            course.GradesReferences.Add(new MongoDBRef(DatabaseConfig.GradesCollectionName, gradeToCreate.Id));
            student.GradesReferences.Add(new MongoDBRef(DatabaseConfig.GradesCollectionName, gradeToCreate.Id));

            return Created(LinkTemplates.Courses.GetCourseGradeByIdLink(Url, courseName, gradeToCreate.Id).Href, "");
        }

        /*=======================================
        =========== PUT METHODS =================
        =======================================*/
        [HttpPut, Route(WebApiConfig.RoutesTemplates.Courses, Name = "UpdateCourseByName")]
        public IHttpActionResult UpdateCourse(string courseName, CourseModel courseToUpdate)
        {
            courseToUpdate.Links = LinkManager.SingleCourseLinks(Url, courseToUpdate.CourseName);
            BaseRepository.Instance.CoursesCollection.Update(courseToUpdate);

            return Ok();
        }
        [HttpPut, Route(WebApiConfig.RoutesTemplates.Courses, Name = "UpdateCourseByName")] //TODO
        public IHttpActionResult UpdateGradeForStudent(int id, GradeModel gradeToUpdate)
        {
            throw new NotImplementedException();
        }
        [HttpPut, Route(WebApiConfig.RoutesTemplates.Courses, Name = "UpdateCourseByName")]//TODO
        public IHttpActionResult RegisterStudentForCourse(int id, string courseName)
        {
            throw new NotImplementedException();
        }
        [HttpPut, Route(WebApiConfig.RoutesTemplates.Courses, Name = "UpdateCourseByName")]//TODO
        public IHttpActionResult UnregisterStudentFromCourse(int id, string courseName)
        {
            throw new NotImplementedException();
        }

        /*=======================================
        =========== DELETE METHODS ==============
        =======================================*/
        [HttpDelete, Route(WebApiConfig.RoutesTemplates.Courses, Name = "DeleteCourseByName")] //TODO
        public IHttpActionResult DeleteCourse(string courseName)
        {
            throw new NotImplementedException();
        }
        [HttpDelete, Route(WebApiConfig.RoutesTemplates.Courses, Name = "DeleteCourseByName")] //TODO
        public IHttpActionResult DeleteGradeForStudent(int id, DateTime dateOfIssue)
        {
            throw new NotImplementedException();
        }
    }
}
