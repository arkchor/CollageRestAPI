using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CollageRestAPI.Controllers.Interfaces;
using CollageRestAPI.Hypermedia;
using CollageRestAPI.Models;
using CollageRestAPI.Providers;
using CollageRestAPI.Repositories;

namespace CollageRestAPI.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController, IStudentsController
    {
        /*=======================================
        =========== GET METHODS =================
        =======================================*/
        [HttpGet, Route(Name = "GetStudentsCollection")]
        public IHttpActionResult GetStudentsCollection()
        {
            return Ok(BaseRepository.Instance.StudentsCollection);
        }

        [HttpGet, Route("{id}", Name = "GetStudentById")]
        public IHttpActionResult GetStudentById(int id)
        {
            var student = BaseRepository.Instance.StudentsCollection.Single(x => x.Id == id);
            //student.Links = new List<Link>
            //{
            //    LinkTemplates.Students.GetStudentByIdLink(Url.Link("GetStudentById", new {id = student.Id})),
            //    LinkTemplates.Students.GetStudentGradesLink(Url.Link("GetStudentGrades", new {id = student.Id})),
            //    LinkTemplates.Students.GetStudentCoursesLink(Url.Link("GetStudentCourses", new {id = student.Id}))
            //};
            student.Links = LinkManager.SingleStudentLinks(Url, student.Id);

            return Ok(student);
        }
        [HttpGet, Route("{id}/grades", Name = "GetStudentGrades")]
        public IHttpActionResult GetStudentGrades(int id)
        {
            var student = BaseRepository.Instance.StudentsCollection.Single(x => x.Id == id);

            return Ok(student.Grades);
        }
        [HttpGet, Route("{id}/grades/{courseName}", Name = "GetStudentGradesByCourse")]
        public IHttpActionResult GetStudentGradesByCourse(int id, string courseName)
        {
            var student = BaseRepository.Instance.StudentsCollection.Single(x => x.Id == id);
            var course = BaseRepository.Instance.CoursesCollection.Single(x => x.CourseName == courseName);
            var grades = student.Grades.Where(grade => course.Grades.Contains(grade)).ToList();

            return Ok(grades);
        }
        //[HttpGet, Route("{id}/grades", Name = "GetStudentGradeByIssueDate")]
        //public IHttpActionResult GetStudentGradeByIssueDate(int id, [FromBody]DateTime issueDate)
        //{
        //    var student = BaseRepository.Instance.StudentsCollection.Single(x => x.Id == id);
        //    var grade = student.Grades.Where(x => x.IssueDateTime.Date == issueDate.Date && x.IssueDateTime.Hour == issueDate.Hour && x.IssueDateTime.Minute == issueDate.Minute).ToList();

        //    return Ok(grade);
        //}

        [HttpGet, Route("{id}/courses", Name = "GetStudentCourses")]
        public IHttpActionResult GetStudentCourses(int id)
        {
            var student = BaseRepository.Instance.StudentsCollection.Single(x => x.Id == id);

            return Ok(student.Courses);
        }

        /*=======================================
        =========== POST METHODS ================
        =======================================*/
        [HttpPost, Route(Name = "CreateStudent")]
        public IHttpActionResult CreateStudent([FromBody]StudentModel studentToCreate)
        {
            studentToCreate.Id = StudentIndexProvider.Instance.CurrentIndex;
            studentToCreate.Links = LinkManager.SingleStudentLinks(Url, studentToCreate.Id);
            BaseRepository.Instance.StudentsCollection.Add(studentToCreate);

            return Created(LinkTemplates.Students.GetStudentByIdLink(Url, studentToCreate.Id).Href,"");
        }

        /*=======================================
        =========== PUT METHODS =================
        =======================================*/
        [HttpPut, Route("{id}", Name = "UpdateStudentById")]
        public IHttpActionResult UpdateStudentById(int id, [FromBody]StudentModel studentToUpdate)
        {
            //var student = BaseRepository.Instance.StudentsCollection.Single(x => x.Id == id);
            //System.Diagnostics.Debug.WriteLine($"============ {studentToUpdate.Id} =============");
            studentToUpdate.Links = LinkManager.SingleStudentLinks(Url, id);
            if (studentToUpdate.Id == 0)
            {
                studentToUpdate.Id = id;
            }
            BaseRepository.Instance.StudentsCollection.Update(studentToUpdate);
            //student.Links = LinkManager.SingleStudentLinks(Url);

            return Ok();
        }
        [HttpGet, Route("{id}/course", Name = "GetStudentByIdd")]
        public IHttpActionResult UpdateCourses(int id, [FromBody]CourseModel courseToAdd)
        {
            throw new NotImplementedException();
        }

        /*=======================================
        =========== DELETE METHODS ==============
        =======================================*/
        [HttpDelete, Route("{id}", Name = "DeleteStudentById")]
        public IHttpActionResult DeleteStudentById(int id)
        {
            BaseRepository.Instance.StudentsCollection.Delete(id);

            return Ok();
        }
        [HttpGet, Route("{id}", Name = "GetStudentByIdsd")]
        public IHttpActionResult DeleteCourse(int id, string courseName)
        {
            throw new NotImplementedException();
        }
    }
}
