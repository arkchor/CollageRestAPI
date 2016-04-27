using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CollageRestAPI.Models;
using CollageRestAPI.Repositories;
using MongoRepository;

namespace CollageRestAPI.Controllers
{
    [RoutePrefix("api/Course")]
    public class CoursesController : ApiController
    {
        /*=======================================
        =========== GET METHODS =================
        =======================================*/

        // GET api/Course
        public MongoRepository<CourseModel, Guid> GetCourses()
        {
            return BaseRepository.Instance.CoursesCollection;
        }

        // GET api/Course/courseName
        [Route("{courseName}")]
        public CourseModel GetCourse(string courseName)
        {
            return BaseRepository.Instance.CoursesCollection.Single(x => x.CourseName == courseName);
        }

        // GET api/Course/courseName/Grades
        [Route("{courseName}/Grades")]
        public List<GradeModel> GetGrades(string courseName)
        {
            return BaseRepository.Instance.CoursesCollection.Single(x => x.CourseName == courseName).Grades;
        }

        // GET api/Course/courseName/issueYear/issueMonth/issueDay (eg. /Grades/2016/01/03 )
        //[Route("{courseName}/{issueYear}/{issueMonth}/{issueDay}")]
        //public List<GradeModel> GetGradesByDay(string courseName, int issueYear, int issueMonth, int issueDay)
        //{
        //    var course = BaseRepository.Instance.CoursesCollection.Find(x => x.CourseName == courseName);
        //    DateTime incomingData = new DateTime(issueYear, issueMonth, issueDay);
        //    return course.Grades.Where(x => x.IssueDateTime == incomingData).ToList();
        //}

        /*=======================================
        =========== POST METHODS ================
        =======================================*/

        // POST api/Course
        public HttpResponseMessage PostCourses([FromBody]List<CourseModel> coursesToCreate)
        {
            BaseRepository.Instance.CoursesCollection.Add(coursesToCreate);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Url.Content("~/api/Course"));
            return response;
            //return Request.CreateResponse(HttpStatusCode.Created);
        }

        // POST api/Course/courseName/id
        //[Route("{courseName}/{id}")]
        //public HttpResponseMessage PostGrades(string courseName, int id, [FromBody]List<GradeModel> gradesToAdd)
        //{
        //    var course = BaseRepository.Instance.CoursesCollection.Single(x => x.CourseName == courseName);
        //    var student = BaseRepository.Instance.StudentsCollection.Single(x => x.Id == id);
        //    gradesToAdd.ForEach(x => x.Student = student);
        //    course.Grades.AddRange(gradesToAdd);
        //    student.Grades.AddRange(gradesToAdd);
        //    return Request.CreateResponse(HttpStatusCode.Created);
        //}

        /*=======================================
        =========== PUT METHODS =================
        =======================================*/

        // PUT api/Course/courseName
        //[Route("{courseName}")]
        //public HttpResponseMessage PutCourse(string courseName, [FromBody]CourseModel courseToUpdate)
        //{
        //    int courseIndex = BaseRepository.Instance.CoursesCollection.FindIndex(x => x.CourseName == courseName);
        //    BaseRepository.Instance.CoursesCollection[courseIndex] = courseToUpdate;
        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}

        //PUT api/Course/courseName/issueYear/issueMonth/issueDay(eg. /Grades/2016/01/03 )
        //[Route("{courseName}/{issueYear}/{issueMonth}/{issueDay}")]
        //public HttpResponseMessage PutGrade(string courseName, int issueYear, int issueMonth, int issueDay, [FromBody]GradeModel gradeToUpdate)
        //{
        //    var course = BaseRepository.Instance.CoursesCollection.Find(x => x.CourseName == courseName);
        //    DateTime incomingData = new DateTime(issueYear, issueMonth, issueDay);
        //    int gradeIndex = course.Grades.FindIndex(x => x.IssueDateTime == incomingData);
        //    course.Grades[gradeIndex] = gradeToUpdate;
        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}

        /*=======================================
        =========== DELETE METHODS ==============
        =======================================*/

        // DELETE api/Course/courseName
        //[Route("{courseName}")]
        //public HttpResponseMessage DeleteCourse(string courseName)
        //{
        //    var coursesList = BaseRepository.Instance.CoursesCollection;
        //    coursesList.Remove(coursesList.Single(x => x.CourseName == courseName));
        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}

        // DELETE api/Course/courseName/issueYear/issueMonth/issueDay (eg. /Grades/2016/01/03 )
        //[Route("{courseName}/{issueYear}/{issueMonth}/{issueDay}")]
        //public HttpResponseMessage DeleteGrade(string courseName, int issueYear, int issueMonth, int issueDay)
        //{
        //    var course = BaseRepository.Instance.CoursesCollection.Find(x => x.CourseName == courseName);
        //    DateTime incomingData = new DateTime(issueYear, issueMonth, issueDay);
        //    course.Grades.Remove(course.Grades.Single(x => x.IssueDateTime == incomingData));
        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}
    }
}
