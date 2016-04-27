using System;
using System.Collections.Generic;
using CollageRestAPI.Models;
using CollageRestAPI.Providers;
using CollageRestAPI.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollageRestApi.Tests
{
    [TestClass]
    public class AddingToDatabaseTest
    {
        private int GetNewIndexFromIndexProvider()
        {
            return StudentIndexProvider.Instance.CurrentIndex;
        }
        private List<GradeModel> GetExampleGrades(int count)
        {
            var grades = new List<GradeModel>();
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                grades.Add(new GradeModel
                {
                    Id = new Guid(),
                    Value = random.Next(2,5),
                    IssueDateTime = new DateTime(random.Next(2000,2016), random.Next(1,12), random.Next(1,28))
                });
            }
            return grades;
        }
        private CourseModel GetExampleCourse()
        {
            var random = new Random();
            return new CourseModel
            {
                Id = new Guid(),
                CourseName = $"przedmiot{random.Next(1, 100)}",
                Tutor = $"nauczyciel{random.Next(1, 100)}"
            };
        }
        private StudentModel GetExampleStudent()
        {
            var random = new Random();
            return new StudentModel
            {
                Id = GetNewIndexFromIndexProvider(),
                FirstName = $"imie{random.Next(1, 100)}",
                LastName = $"nazwisko{random.Next(1, 100)}",
                BornDate = new DateTime(random.Next(1990, 1995), random.Next(1, 12), random.Next(1, 28))
            };
        }

        [TestMethod]
        public void GettingNewIndexFromIndexProvider()
        {
            var index = GetNewIndexFromIndexProvider();

            Assert.IsNotNull(index);
            Assert.IsInstanceOfType(index, typeof(int));
        }

        [TestMethod]
        public void AddToDatabase()
        {
            var grades = GetExampleGrades(5);
            var student = GetExampleStudent();
            var course = GetExampleCourse();

            grades.ForEach(x =>
            {
                x.Course = course;
                x.Student = student;
            });

            student.Grades.AddRange(grades);
            student.Courses.Add(course);

            course.Grades.AddRange(grades);
            course.Students.Add(student);

            BaseRepository.Instance.CoursesCollection.Add(course);
            BaseRepository.Instance.StudentsCollection.Add(student);
            //List<StudentModel> students = new List<StudentModel>();
            //for (var i = 0; i < 5; i++)
            //{
            //    var grades = new List<GradeModel>();
            //    for (int j = 2; j < 5; j++)
            //    {
            //        grades.Add(new GradeModel {Value = j});
            //    }
            //    var student = new StudentModel
            //    {
            //        FirstName = "imie" + i,
            //        LastName = "nazwisko" + i,
            //        Id = StudentIndexProvider.Instance.CurrentIndex
            //    };
            //    var course = new CourseModel()
            //    {

            //    };
            //    students.Add(student);
            //}         
            //BaseRepository.Instance.StudentsCollection.Add(students);    
        }  
    }
}
