using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web;
using System.Web.Http.Routing;
using CollageRestAPI.Models;

namespace CollageRestAPI
{
    public static class LinkTemplates
    {
        public static class Students
        {
            public static Link StudentsCollectionLink(string method) => new Link("students-collection", new UrlHelper().Content("~api/Students"), method);
            public static Link StudentByIdLink(string method) => new Link("students-collection", new UrlHelper().Content("~api/Students"), method);
        }

        public static class Courses
        {
            
        }
    }
}