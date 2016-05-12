using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CollageRestAPI.Controllers.Interfaces;
using CollageRestAPI.Hypermedia;
using CollageRestAPI.Models;
using CollageRestAPI.Providers;
using CollageRestAPI.Repositories;
using CollageRestAPI.Utils;

namespace CollageRestAPI.Controllers
{
    //[RoutePrefix("api/students")]
    public class StudentsController : ApiController, IStudentsController
    {
        /*=======================================
        =========== GET METHODS =================
        =======================================*/
        //[HttpGet, Route(Name = "GetStudentsCollection")]
        [HttpGet, Route(WebApiConfig.RoutesTemplates.Students, Name = "GetStudentsCollection")]
        public IHttpActionResult GetStudentsCollection(string firstName = null, string lastName = null)
        {
            if (firstName == null && lastName == null)
            {
                return Ok(BaseRepository.Instance.StudentsCollection);
            }
            if (firstName == null)
            {
                return Ok(BaseRepository.Instance.StudentsCollection.Where(student => student.LastName == lastName).ToList());
            }
            if (lastName == null)
            {
                return Ok(BaseRepository.Instance.StudentsCollection.Where(student => student.FirstName == firstName).ToList());
            }
            return Ok(BaseRepository.Instance.StudentsCollection.Where(student => student.FirstName == firstName && student.LastName == lastName).ToList());            
        }

        [HttpGet, Route(WebApiConfig.RoutesTemplates.Students)]
        public IHttpActionResult GetStudentsCollection(DateTime bornDate, int condition = 0)
        {
            var students = BaseRepository.Instance.StudentsCollection.ToList();
            return Ok(students.Where(student => student.BornDate.Date.CompareTo(bornDate.Date) == condition));
            //switch (condition)
            //{
            //    case ComparingUtils.GreaterThan:
            //        return Ok(BaseRepository.Instance.StudentsCollection.Where(student => student.BornDate.CompareTo(bornDate) == condition))
            //        break;
            //    case ComparingUtils.EqualTo:

            //        break;
            //    case ComparingUtils.LessThan:

            //        break;
            //}
        }

        [HttpGet, Route(WebApiConfig.RoutesTemplates.Students, Name = "GetStudentById")]
        public IHttpActionResult GetStudentById(int id)
        {
            var student = BaseRepository.Instance.StudentsCollection.Single(x => x.Id == id);

            return Ok(student);
        }
        //[HttpGet, Route(WebApiConfig.RoutesTemplates.Students+"/filter", Name = "GetStudentsByName")]
        //public IHttpActionResult GetStudentsByName(string firstName = null, string lastName = null)
        //{
        //    if (firstName == null && lastName == null)
        //    {
        //        return Ok(new List<StudentModel>());
        //    }
        //    if (firstName == null)
        //    {
        //        return Ok(BaseRepository.Instance.StudentsCollection.Where(student => student.LastName == lastName).ToList());
        //    }
        //    if (lastName == null)
        //    {
        //        return Ok(BaseRepository.Instance.StudentsCollection.Where(student => student.FirstName == firstName).ToList());
        //    }
        //    return Ok(BaseRepository.Instance.StudentsCollection.Where(student => student.FirstName == firstName && student.LastName == lastName).ToList());
        //}

        [HttpGet, Route(WebApiConfig.RoutesTemplates.StudentGrades, Name = "GetStudentGrades")]
        public IHttpActionResult GetStudentGrades(int id)
        {
            var student = BaseRepository.Instance.StudentsCollection.Single(x => x.Id == id);
            var grades = new List<GradeModel>();
            student.GradesReferences.ForEach(gradeReference => grades.Add(BaseRepository.Instance.Fetch<GradeModel>(gradeReference)));

            return Ok(grades);
        }

        [HttpGet, Route(WebApiConfig.RoutesTemplates.StudentGrades, Name = "GetStudentGradesByCourse")]
        public IHttpActionResult GetStudentGradesByCourse(int id, string courseName)
        {
            var student = BaseRepository.Instance.StudentsCollection.Single(x => x.Id == id);
            var course = BaseRepository.Instance.CoursesCollection.Single(x => x.CourseName == courseName);
            var gradesReferences =
                student.GradesReferences.Where(gradeReference => course.GradesReferences.Contains(gradeReference)).ToList();
            //var gradess = student.Grades.Where(grade => course.Grades.Contains(grade)).ToList();
            var grades = new List<GradeModel>();
            gradesReferences.ForEach(gradeReference => grades.Add(BaseRepository.Instance.Fetch<GradeModel>(gradeReference)));

            return Ok(grades);
        }

        [HttpGet, Route(WebApiConfig.RoutesTemplates.StudentGrades, Name = "GetStudentGradesWithValueFilter")]
        public IHttpActionResult GetStudentGradesWithValueFilter(int id, double value, int condition = 0)
        {
            var student = BaseRepository.Instance.StudentsCollection.Single(x => x.Id == id);
            var grades = new List<GradeModel>();
            student.GradesReferences.ForEach(gradeReference =>
            {
                var grade = BaseRepository.Instance.Fetch<GradeModel>(gradeReference);
                if (grade.Value.CompareTo(value) == condition)
                {
                    grades.Add(grade);
                }
                //switch (condition)
                //{
                //    case ComparingUtils.GreaterThan:
                //        if (grade.Value > value)
                //        {
                //            grades.Add(grade);
                //        }
                //        break;
                //    case ComparingUtils.EqualTo:
                //        if (grade.Value == value)
                //        {
                //            grades.Add(grade);
                //        }
                //        break;
                //    case ComparingUtils.LessThan:
                //        if (grade.Value < value)
                //        {
                //            grades.Add(grade);
                //        }
                //        break;
                //}
            });

            return Ok(grades);
        }
        //[HttpGet, Route("{id}/grades", Name = "GetStudentGradeByIssueDate")]
        //public IHttpActionResult GetStudentGradeByIssueDate(int id, [FromBody]DateTime issueDate)
        //{
        //    var student = BaseRepository.Instance.StudentsCollection.Single(x => x.Id == id);
        //    var grade = student.Grades.Where(x => x.IssueDateTime.Date == issueDate.Date && x.IssueDateTime.Hour == issueDate.Hour && x.IssueDateTime.Minute == issueDate.Minute).ToList();

        //    return Ok(grade);
        //}

        [HttpGet, Route(WebApiConfig.RoutesTemplates.StudentCourses, Name = "GetStudentCourses")]
        public IHttpActionResult GetStudentCourses(int id)
        {
            var student = BaseRepository.Instance.StudentsCollection.Single(x => x.Id == id);
            var courses = new List<CourseModel>();
            student.CoursesReferences.ForEach(courseReference => courses.Add(BaseRepository.Instance.Fetch<CourseModel>(courseReference)));

            return Ok(courses);
        }

        /*=======================================
        =========== POST METHODS ================
        =======================================*/
        [HttpPost, Route(WebApiConfig.RoutesTemplates.Students, Name = "CreateStudent")]
        public IHttpActionResult CreateStudent([FromBody]StudentModel studentToCreate)
        {
            studentToCreate.Id = StudentIndexProvider.Instance.CurrentIndex;
            studentToCreate.Links = LinkManager.SingleStudentLinks(Url, studentToCreate.Id);
            BaseRepository.Instance.StudentsCollection.Add(studentToCreate);

            return Created(LinkTemplates.Students.GetStudentByIdLink(Url, studentToCreate.Id).Href, string.Empty);
        }

        /*=======================================
        =========== PUT METHODS =================
        =======================================*/
        [HttpPut, Route(WebApiConfig.RoutesTemplates.Students, Name = "UpdateStudentById")]
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
        //[HttpPut, Route(WebApiConfig.RoutesTemplates.StudentCourses, Name = "UpdateStudentCourse")]
        //public IHttpActionResult UpdateCourses(int id, [FromBody]CourseModel courseToAdd)
        //{
        //    throw new NotImplementedException();
        //}

        /*=======================================
        =========== DELETE METHODS ==============
        =======================================*/
        [HttpDelete, Route(WebApiConfig.RoutesTemplates.Students, Name = "DeleteStudentById")]
        public IHttpActionResult DeleteStudentById(int id)
        {
            BaseRepository.Instance.StudentsCollection.Delete(id);

            return Ok();
        }
        //[HttpGet, Route("{id}", Name = "GetStudentByIdsd")]
        //public IHttpActionResult DeleteCourse(int id, string courseName)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
