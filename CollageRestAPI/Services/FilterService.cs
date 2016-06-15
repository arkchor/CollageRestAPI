using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CollageRestAPI.Models;
using CollageRestAPI.Repositories;
using CollageRestAPI.ViewModels;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;

namespace CollageRestAPI.Services
{
    public class FilterService : IFilterService
    {
        public IEnumerable FilterCourses(CoursesRequestViewModel coursesRequest)
        {
            if (coursesRequest == null || (string.IsNullOrWhiteSpace(coursesRequest.CourseName) && string.IsNullOrWhiteSpace(coursesRequest.Tutor)))
            {
                return BaseRepository.Instance.CoursesCollection;
            }   
            if (!string.IsNullOrWhiteSpace(coursesRequest.Id))
            {
                return BaseRepository.Instance.CoursesCollection.Where(course => course.Id == coursesRequest.Id);
            }

            //if (string.IsNullOrWhiteSpace(coursesRequest.CourseName) && string.IsNullOrWhiteSpace(coursesRequest.Tutor))
            //{
            //    return BaseRepository.Instance.CoursesCollection;
            //}

            if (string.IsNullOrWhiteSpace(coursesRequest.Tutor))
            {
                return
                    BaseRepository.Instance.CoursesCollection.Where(
                        course => course.CourseName.Contains(coursesRequest.CourseName));
            }

            if (string.IsNullOrWhiteSpace(coursesRequest.CourseName))
            {
                return
                    BaseRepository.Instance.CoursesCollection.Where(
                        course => course.Tutor.Contains(coursesRequest.Tutor));
            }

            return BaseRepository.Instance.CoursesCollection.Where(course => course.CourseName.Contains(coursesRequest.CourseName) && course.Tutor.Contains(coursesRequest.Tutor));           
        }

        public IEnumerable FilterStudents(StudentsRequestViewModel studentsRequest)
        {
            throw new NotImplementedException();
        }

        public IEnumerable FilterGrades(GradesRequestViewModel gradesRequest)
        {
            throw new NotImplementedException();
        }
    }
}