using System;
using System.Web.Http;
using CollageRestAPI.Models;

namespace CollageRestAPI.Controllers.Interfaces
{
    interface IStudentsController
    {
        //=== GET METHODS ===//
        IHttpActionResult GetStudents(int id = 0, string firstName = null, string lastName = null);
        IHttpActionResult GetStudents(DateTime bornDate, int condition = 0);
        //IHttpActionResult GetStudentById(int id);
        //IHttpActionResult GetStudentsByName(string firstName, string lastName);
        //IHttpActionResult GetStudentsByFirstName(string firstName);
        //IHttpActionResult GetStudentsByLastName(string lastName);
        IHttpActionResult GetStudentGrades(int id);
        IHttpActionResult GetStudentGradesByCourse(int id, string courseName);
        IHttpActionResult GetStudentGradesWithValueFilter(int id, double value, int condition = 0);
        //IHttpActionResult GetStudentGradeByIssueDate(int id, DateTime issueDate);
        IHttpActionResult GetStudentCourses(int id);

        //=== POST METHODS ===//
        IHttpActionResult CreateStudent(StudentModel studentToCreate);

        //=== PUT METHODS ===//
        IHttpActionResult UpdateStudentById(int id, StudentModel studentToUpdate);
        //IHttpActionResult UpdateCourses(int id, CourseModel courseToAdd);

        //=== DELETE METHODS ===//
        IHttpActionResult DeleteStudentById(int id);
        //IHttpActionResult DeleteCourse(int id, string courseName);
    }
}
