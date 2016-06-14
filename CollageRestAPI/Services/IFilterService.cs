using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollageRestAPI.ViewModels;

namespace CollageRestAPI.Services
{
    internal interface IFilterService
    {
        IEnumerable FilterCourses(CoursesRequestViewModel coursesRequest);
        IEnumerable FilterStudents(StudentsRequestViewModel studentsRequest);
        IEnumerable FilterGrades(GradesRequestViewModel gradesRequest);
    }
}
