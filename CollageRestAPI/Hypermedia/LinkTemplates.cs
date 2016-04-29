using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;

namespace CollageRestAPI.Hypermedia
{
    public static class LinkTemplates
    {
        public static class Students
        {
            public static Link GetStudentByIdLink(UrlHelper urlHelper, int studentId)
                => new Link("GetStudentById", urlHelper.Link("GetStudentById", new { id = studentId }), "GET");
            public static Link GetStudentGradesLink(UrlHelper urlHelper, int studentId)
                => new Link("GetStudentGrades", urlHelper.Link("GetStudentGrades", new { id = studentId }), "GET");
            public static Link GetStudentCoursesLink(UrlHelper urlHelper, int studentId)
                => new Link("GetStudentCourses", urlHelper.Link("GetStudentCourses", new { id = studentId }), "GET");

            public static Link CreateStudentLink(UrlHelper urlHelper, int studentId)
                => new Link("CreateStudent", urlHelper.Link("CreateStudent", new { id = studentId }), "POST");

            public static Link UpdateStudentByIdLink(UrlHelper urlHelper, int studentId)
                => new Link("UpdateStudentById", urlHelper.Link("UpdateStudentById", new { id = studentId }), "PUT");

            public static Link DeleteStudentByIdLink(UrlHelper urlHelper, int studentId)
                => new Link("DeleteStudentById", urlHelper.Link("DeleteStudentById", new { id = studentId }), "DELETE");
        }

        public static class Courses
        {

        }
    }
}