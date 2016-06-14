using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollageRestAPI.ViewModels
{
    public class CoursesRequestViewModel
    {
        public string Id { get; set; }
        public string CourseName { get; set; }
        public string Tutor { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}