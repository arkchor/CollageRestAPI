using System;
using System.Collections.Generic;
using System.Web.Http.Routing;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace CollageRestAPI.Hypermedia
{
    public static class LinkManager
    {
        public static List<Link> InitialLinks(UrlHelper urlHelper)
        {
            return new List<Link>
            {
                LinkTemplates.Students.GetStudentsCollectionLink(urlHelper),
                LinkTemplates.Courses.GetCoursesCollectionLink(urlHelper)
            };
        }

        public static List<Link> SingleStudentLinks(UrlHelper urlHelper, int id)
        {
            return new List<Link>
            {
                LinkTemplates.Students.GetStudentByIdLink(urlHelper, id),
                LinkTemplates.Students.GetStudentGradesLink(urlHelper, id),
                LinkTemplates.Students.GetStudentCoursesLink(urlHelper, id),
                LinkTemplates.Students.DeleteStudentByIdLink(urlHelper, id),
                LinkTemplates.Students.UpdateStudentByIdLink(urlHelper, id)
            };
        }

        public static List<Link> SingleCourseLinks(UrlHelper urlHelper, string name)
        {
            return new List<Link>
            {
                LinkTemplates.Courses.GetCourseByNameLink(urlHelper, name),
                LinkTemplates.Courses.GetCourseGradesLink(urlHelper, name),
                LinkTemplates.Courses.GetCourseStudentsLink(urlHelper, name),
                LinkTemplates.Courses.DeleteCourseByNameLink(urlHelper, name),
                LinkTemplates.Courses.UpdateCourseByNameLink(urlHelper, name)
            };
        }

        public static List<Link> SingleGradeLinks(UrlHelper urlHelper, ObjectId id)
        {
            return new List<Link>
            {
                
            };
        }
    }
}