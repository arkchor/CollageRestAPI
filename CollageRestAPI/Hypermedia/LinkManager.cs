using System;
using System.Collections.Generic;
using System.Web.Http.Routing;
using System.Linq;
using System.Web;

namespace CollageRestAPI.Hypermedia
{
    public static class LinkManager
    {
        public static List<Link> InitialLinks()
        {
            return null;
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
    }
}