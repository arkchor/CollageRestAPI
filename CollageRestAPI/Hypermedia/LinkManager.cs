using System;
using System.Collections.Generic;
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

        public static List<Link> SingleStudentLinks(int id)
        {
            return new List<Link>
            {
                LinkTemplates.Students.GetStudentByIdLink(id),
                LinkTemplates.Students.GetStudentGradesLink(id),
                LinkTemplates.Students.GetStudentCoursesLink(id)
            };
        }
    }
}