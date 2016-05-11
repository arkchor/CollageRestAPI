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
        }

        public static class RoutesTemplates
        {
            //Root
            public const string ApiRoot = "~/api";
            //Students
            public const string Students = ApiRoot + "/students";
            public const string StudentGrades = Students + "/grades";
            public const string StudentCourses = Students + "/courses";
            //public const string StudentById = Students + "/{id}";
            //public static string StudentGradesAll => "~/api/students/{id}/grades/all";
            //public static string StudentGradesByCourse => "~/api/students/{id}/grades/{courseName}";
            //public static string StudentGradeByIssueDate => "~/api/students/{id}/grades";
            //Courses
            public const string Courses = ApiRoot + "/courses";
            public const string CourseStudents = Courses + "/students";
            public const string CourseGrades = Courses + "/grades";
        }

        public static class RoutesNames
        {
            //Students
            public const string GetStudentsCollection = "GetStudentsCollection";
        }
    }
}
