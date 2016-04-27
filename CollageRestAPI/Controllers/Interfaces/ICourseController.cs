using System;
using System.Web.Http;
using CollageRestAPI.Models;

namespace CollageRestAPI.Controllers.Interfaces
{
    interface ICourseController
    {
        //=== GET METHODS ===//
        IHttpActionResult GetCoursesCollection();
        IHttpActionResult GetCourseByName(string courseName);
        IHttpActionResult GetCourseStudents(string courseName);
        IHttpActionResult GetCourseGrades(string courseName);

        //=== POST METHODS ===//
        IHttpActionResult CreateCourse(CourseModel courseToCreate);
        IHttpActionResult CreateGradeForStudent(int id, GradeModel gradeToCreate);

        //=== PUT METHODS ===//
        IHttpActionResult UpdateCourse(string courseName, CourseModel courseToUpdate);
        IHttpActionResult UpdateGradeForStudent(int id, GradeModel gradeToUpdate);

        //=== DELETE METHODS ===//
        IHttpActionResult DeleteCourse(string courseName);
        IHttpActionResult DeleteGradeForStudent(int id, DateTime dateOfIssue);
    }
}
