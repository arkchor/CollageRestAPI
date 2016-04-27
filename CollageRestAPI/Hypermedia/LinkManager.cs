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

        public static List<Link> SingleStudentLinks(UrlHelper urlHelper)
        {
            return new List<Link>
            {
                LinkTemplates.Students.GetStudentByIdLink(urlHelper),
                LinkTemplates.Students.GetStudentGradesLink(urlHelper),
                LinkTemplates.Students.GetStudentCoursesLink(urlHelper)
            };
        }
    }
}