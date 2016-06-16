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
                        course => course.CourseName.ToLower().Contains(coursesRequest.CourseName.ToLower()));
            }

            if (string.IsNullOrWhiteSpace(coursesRequest.CourseName))
            {
                return
                    BaseRepository.Instance.CoursesCollection.Where(
                        course => course.Tutor.ToLower().Contains(coursesRequest.Tutor.ToLower()));
            }

            return BaseRepository.Instance.CoursesCollection.Where(
                course => course.CourseName.ToLower().Contains(coursesRequest.CourseName.ToLower()) && course.Tutor.ToLower().Contains(coursesRequest.Tutor.ToLower()));           
        }

        public IEnumerable FilterStudents(StudentsRequestViewModel studentsRequest)
        {
            if (studentsRequest == null || (studentsRequest.Id == 0
                && string.IsNullOrWhiteSpace(studentsRequest.FirstName) 
                && string.IsNullOrWhiteSpace(studentsRequest.LastName)
                && studentsRequest.BornDate.Equals(DateTime.MinValue)))
            {
                return BaseRepository.Instance.StudentsCollection;
            }

            if (studentsRequest.Id != 0)
            {
                return BaseRepository.Instance.StudentsCollection.Where(student => student.Id == studentsRequest.Id);
            }

            if (string.IsNullOrWhiteSpace(studentsRequest.FirstName) && string.IsNullOrWhiteSpace(studentsRequest.LastName) && studentsRequest.BornDate.Equals(DateTime.MinValue))
            {
                return BaseRepository.Instance.StudentsCollection;
            }          

            //if (string.IsNullOrWhiteSpace(coursesRequest.CourseName) && string.IsNullOrWhiteSpace(coursesRequest.Tutor))
            //{
            //    return BaseRepository.Instance.CoursesCollection;
            //}

            if (string.IsNullOrWhiteSpace(studentsRequest.FirstName) && string.IsNullOrWhiteSpace(studentsRequest.LastName))
            {
                return
                    BaseRepository.Instance.StudentsCollection.Where(student => student.BornDate.Date.CompareTo(studentsRequest.BornDate.Date) == 0);
            }

            if (studentsRequest.BornDate.Equals(DateTime.MinValue) && string.IsNullOrWhiteSpace(studentsRequest.LastName))
            {
                return BaseRepository.Instance.StudentsCollection.Where(student => student.FirstName.ToLower().Contains(studentsRequest.FirstName.ToLower()));
            }

            if (string.IsNullOrWhiteSpace(studentsRequest.FirstName) && studentsRequest.BornDate.Equals(DateTime.MinValue))
            {
                return BaseRepository.Instance.StudentsCollection.Where(student => student.LastName.ToLower().Contains(studentsRequest.LastName.ToLower()));
            }

            if (studentsRequest.BornDate.Equals(DateTime.MinValue))
            {
                return BaseRepository.Instance.StudentsCollection.Where(
                    student => student.FirstName.ToLower().Contains(studentsRequest.FirstName.ToLower()) && student.LastName.ToLower().Contains(studentsRequest.LastName.ToLower()));
            }

            if (string.IsNullOrWhiteSpace(studentsRequest.FirstName))
            {
                return BaseRepository.Instance.StudentsCollection.Where(
                    student => student.LastName.ToLower().Contains(studentsRequest.LastName.ToLower()) && student.BornDate.Date.CompareTo(studentsRequest.BornDate.Date) == 0);
            }

            if (string.IsNullOrWhiteSpace(studentsRequest.LastName))
            {
                return BaseRepository.Instance.StudentsCollection.Where(
                    student => student.FirstName.ToLower().Contains(studentsRequest.FirstName.ToLower()) && student.BornDate.Date.CompareTo(studentsRequest.BornDate.Date) == 0);
            }           

            return BaseRepository.Instance.StudentsCollection.Where(
                student => student.FirstName.ToLower().Contains(studentsRequest.FirstName.ToLower()) 
                && student.LastName.ToLower().Contains(studentsRequest.LastName.ToLower())
                && student.BornDate.Date.CompareTo(studentsRequest.BornDate.Date) == 0);
        }

        public IEnumerable FilterGrades(GradesRequestViewModel gradesRequest)
        {
            throw new NotImplementedException();
        }
    }
}