using System;
using System.Web.Http;
using CollageRestAPI.Models;
using MongoDB.Bson;

namespace CollageRestAPI.Controllers.Interfaces
{
    interface ICourseController
    {
        //=== GET METHODS ===//
        IHttpActionResult GetCourses(string id = null, string courseName = null, string tutor = null);
        //IHttpActionResult GetCourseByName(string courseName);
        //IHttpActionResult GetCourseById(ObjectId id);
        IHttpActionResult GetCourseStudents(string courseName);
        IHttpActionResult GetCourseGrades(string courseName, string id = null);

        //=== POST METHODS ===//
        IHttpActionResult CreateCourse(CourseModel courseToCreate);
        IHttpActionResult CreateGradeForStudent(int id, string courseName, GradeModel gradeToCreate);

        //=== PUT METHODS ===//
        IHttpActionResult UpdateCourse(string courseName, CourseModel courseToUpdate);
        IHttpActionResult UpdateGradeForStudent(int id, GradeModel gradeToUpdate);
        IHttpActionResult RegisterStudentForCourse(int id, string courseName, bool unregister = false);
        //IHttpActionResult UnregisterStudentFromCourse(int id, string courseName);

        //=== DELETE METHODS ===//
        IHttpActionResult DeleteCourse(string courseName);
        IHttpActionResult DeleteGradeForStudent(int id, DateTime dateOfIssue);
    }
}
