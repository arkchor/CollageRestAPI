using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollageRestAPI.ViewModels
{
    public class StudentsRequestViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}