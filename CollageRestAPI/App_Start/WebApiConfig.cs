using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace CollageRestAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static class RoutingTemplates
        {
            //Root
            public static string ApiRoot => "~/api";
            //Students
            public static string StudentsAll => "~/api/students/all";
            public static string StudentById => "~/api/students/{id}";
            public static string StudentGradesAll => "~/api/students/{id}/grades/all";
            public static string StudentGradesByCourse => "~/api/students/{id}/grades/{courseName}";
            public static string StudentGradeByIssueDate => "~/api/students/{id}/grades";
            //Courses
            public static string Courses => "~/api/courses";
        }
    }
}
