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
        IHttpActionResult CreateGradeForStudent(int id, string courseName, GradeModel gradeToCreate);

        //=== PUT METHODS ===//
        IHttpActionResult UpdateCourse(string courseName, CourseModel courseToUpdate);
        IHttpActionResult UpdateGradeForStudent(int id, GradeModel gradeToUpdate);
        IHttpActionResult RegisterStudentForCourse(int id, string courseName);
        IHttpActionResult UnregisterStudentFromCourse(int id, string courseName);

        //=== DELETE METHODS ===//
        IHttpActionResult DeleteCourse(string courseName);
        IHttpActionResult DeleteGradeForStudent(int id, DateTime dateOfIssue);
    }
}
