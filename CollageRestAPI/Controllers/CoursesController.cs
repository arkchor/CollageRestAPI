using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CollageRestAPI.Controllers.Interfaces;
using CollageRestAPI.Hypermedia;
using CollageRestAPI.Models;
using CollageRestAPI.Repositories;
using CollageRestAPI.Services;
using CollageRestAPI.ViewModels;
using Microsoft.Ajax.Utilities;
using MongoDB.Bson;
using MongoDB.Driver;
using WebGrease.Css.Extensions;

namespace CollageRestAPI.Controllers
{
    //[RoutePrefix("api/courses")]
    public class CoursesController : ApiController, ICourseController
    {
        private readonly IFilterService _filterService = new FilterService();

        /*=======================================
        =========== GET METHODS =================
        =======================================*/
        [HttpGet, Route(WebApiConfig.RoutesTemplates.Courses, Name = "GetCourses")]
        public IHttpActionResult GetCourses([FromUri]CoursesRequestViewModel coursesRequest)
        {
            //System.Diagnostics.Debug.WriteLine(coursesRequest == null ? "### NULL ###" : $"### {coursesRequest.Id} {coursesRequest.CourseName} {coursesRequest.Tutor} ###");
            return Ok(_filterService.FilterCourses(coursesRequest));
        }

        //[HttpGet, Route(WebApiConfig.RoutesTemplates.Courses, Name = "GetCourses")]
        //public IHttpActionResult GetCourses(string id = null, string courseName = null, string tutor = null)
        //{
        //    if (id != null)
        //    {
        //        return Ok(BaseRepository.Instance.CoursesCollection.Single(x => x.Id == id));
        //    }
        //    if (courseName == null && tutor == null)
        //    {
        //        return Ok(BaseRepository.Instance.CoursesCollection);
        //    }
        //    if (tutor == null)
        //    {
        //        return Ok(BaseRepository.Instance.CoursesCollection.Single(x => x.CourseName == courseName));
        //    }
        //    if (courseName == null)
        //    {
        //        return Ok(BaseRepository.Instance.CoursesCollection.Where(course => course.Tutor == tutor).ToList());
        //    }
        //    return Ok(BaseRepository.Instance.CoursesCollection.Where(course => course.CourseName == courseName && course.Tutor == tutor).ToList());
        //}

        //[HttpGet, Route(WebApiConfig.RoutesTemplates.Courses, Name = "GetCourseById")]
        //public IHttpActionResult GetCourseById([FromUri] ObjectId id)
        //{
        //    var course = BaseRepository.Instance.CoursesCollection.Single(x => x.Id == id);
        //    //course.Links = LinkManager.SingleCourseLinks(Url, course.CourseName);
        //    return Ok(course);
        //}

        //[HttpGet, Route(WebApiConfig.RoutesTemplates.Courses, Name = "GetCourseByName")]
        //public IHttpActionResult GetCourseByName(string courseName)
        //{
        //    var course = BaseRepository.Instance.CoursesCollection.Single(x => x.CourseName == courseName);
        //    //course.Links = LinkManager.SingleCourseLinks(Url, course.CourseName);
        //    return Ok(course);
        //}

        [HttpGet, Route(WebApiConfig.RoutesTemplates.CourseStudents, Name = "GetCourseStudents")]
        public IHttpActionResult GetCourseStudents(string courseName, int id = 0)
        {
            var course = BaseRepository.Instance.CoursesCollection.Single(x => x.CourseName == courseName);
            if (id != 0)
            {
                var studentReference =
                    course.StudentsReferences.Single(
                        reference =>
                            reference == new MongoDBRef(DatabaseConfig.StudentsCollectionName, id));
                return Ok(BaseRepository.Instance.Fetch<StudentModel>(studentReference));
            }
            var students = new List<StudentModel>();
            course.StudentsReferences.ForEach(studentReference => students.Add(BaseRepository.Instance.Fetch<StudentModel>(studentReference)));
            //var studentsCollection = BaseRepository.Instance.StudentsCollection;

            //studentsCollection.ForEach(student =>
            //{
            //    if (student.Courses.Contains(course))
            //    {
            //        students.Add(student);
            //    }
            //});

            return Ok(students);
        }

        [HttpGet, Route(WebApiConfig.RoutesTemplates.CourseGrades, Name = "GetCourseGrades")]
        public IHttpActionResult GetCourseGrades(string courseName, string id = null)
        {
            var course = BaseRepository.Instance.CoursesCollection.Single(x => x.CourseName == courseName);
            if (id != null)
            {
                var gradeReference =
                    course.GradesReferences.Single(
                        reference =>
                            reference == new MongoDBRef(DatabaseConfig.GradesCollectionName, new ObjectId(id)));
                return Ok(BaseRepository.Instance.Fetch<GradeModel>(gradeReference));
            }          
            var grades = new List<GradeModel>();
            course.GradesReferences.ForEach(gradeReference => grades.Add(BaseRepository.Instance.Fetch<GradeModel>(gradeReference)));

            return Ok(grades);
        }

        /*=======================================
        =========== POST METHODS ================
        =======================================*/
        [HttpPost, Route(WebApiConfig.RoutesTemplates.Courses, Name = "CreateCourse")]
        public IHttpActionResult CreateCourse([FromBody] CourseModel courseToCreate)
        {
            courseToCreate.Links = LinkManager.SingleCourseLinks(Url, courseToCreate.CourseName);
            BaseRepository.Instance.CoursesCollection.Add(courseToCreate);

            return Created(LinkTemplates.Courses.GetCourseByNameLink(Url, courseToCreate.CourseName).Href, "");
        }

        [HttpPost, Route(WebApiConfig.RoutesTemplates.CourseGrades, Name = "CreateGradeForStudent")]
        public IHttpActionResult CreateGradeForStudent(int id, string courseName, [FromBody] GradeModel gradeToCreate)
        {
            var course = BaseRepository.Instance.CoursesCollection.Single(x => x.CourseName == courseName);
            var student = BaseRepository.Instance.StudentsCollection.Single(x => x.Id == id);
            gradeToCreate.CourseReference = new MongoDBRef(DatabaseConfig.CoursesCollectionName, new ObjectId(course.Id));
            gradeToCreate.StudentReference = new MongoDBRef(DatabaseConfig.StudentsCollectionName, student.Id);
            BaseRepository.Instance.GradesCollection.Add(gradeToCreate);
            gradeToCreate.Links = LinkManager.SingleGradeLinks(Url, new ObjectId(gradeToCreate.Id));
            BaseRepository.Instance.GradesCollection.Update(gradeToCreate);
            course.GradesReferences.Add(new MongoDBRef(DatabaseConfig.GradesCollectionName, new ObjectId(gradeToCreate.Id)));
            student.GradesReferences.Add(new MongoDBRef(DatabaseConfig.GradesCollectionName, new ObjectId(gradeToCreate.Id)));
            BaseRepository.Instance.StudentsCollection.Update(student);
            BaseRepository.Instance.CoursesCollection.Update(course);

            return Created(LinkTemplates.Courses.GetCourseGradeByIdLink(Url, course.CourseName, gradeToCreate.Id).Href, "");
        }

        //[HttpPost, Route(WebApiConfig.RoutesTemplates.CourseGrades, Name = "CreateGradeForStudent")]
        //public IHttpActionResult CreateGradeForStudentByCourseName(int id, string courseName, [FromBody] GradeModel gradeToCreate)
        //{
        //    var course = BaseRepository.Instance.CoursesCollection.Single(x => x.CourseName == courseName);
        //    var student = BaseRepository.Instance.StudentsCollection.Single(x => x.Id == id);
        //    gradeToCreate.CourseReference = new MongoDBRef(DatabaseConfig.CoursesCollectionName, new ObjectId(course.Id));
        //    gradeToCreate.StudentReference = new MongoDBRef(DatabaseConfig.StudentsCollectionName, student.Id);
        //    BaseRepository.Instance.GradesCollection.Add(gradeToCreate);
        //    gradeToCreate.Links = LinkManager.SingleGradeLinks(Url, new ObjectId(gradeToCreate.Id));
        //    BaseRepository.Instance.GradesCollection.Update(gradeToCreate);
        //    course.GradesReferences.Add(new MongoDBRef(DatabaseConfig.GradesCollectionName, new ObjectId(gradeToCreate.Id)));
        //    student.GradesReferences.Add(new MongoDBRef(DatabaseConfig.GradesCollectionName, new ObjectId(gradeToCreate.Id)));
        //    BaseRepository.Instance.StudentsCollection.Update(student);
        //    BaseRepository.Instance.CoursesCollection.Update(course);

        //    return Created(LinkTemplates.Courses.GetCourseGradeByIdLink(Url, courseName, gradeToCreate.Id).Href, "");
        //}

        /*=======================================
        =========== PUT METHODS =================
        =======================================*/
        [HttpPut, Route(WebApiConfig.RoutesTemplates.Courses, Name = "UpdateCourseByName")]
        public IHttpActionResult UpdateCourse([FromBody]CourseModel courseToUpdate)
        {
            //System.Diagnostics.Debug.WriteLine(courseToUpdate.GradesReferences.Count);
            //System.Diagnostics.Debug.WriteLine(courseToUpdate.StudentsReferences.Count);
            var courseToUpdateFromDb =
                BaseRepository.Instance.CoursesCollection.Single(course => course.Id == courseToUpdate.Id);

            courseToUpdate.GradesReferences = courseToUpdateFromDb.GradesReferences;
            courseToUpdate.StudentsReferences = courseToUpdateFromDb.StudentsReferences;

            courseToUpdate.Links = LinkManager.SingleCourseLinks(Url, courseToUpdate.CourseName);
            BaseRepository.Instance.CoursesCollection.Update(courseToUpdate);

            return Ok();
        }

        //public IHttpActionResult UpdateCourse(string courseName, [FromBody]CourseModel courseToUpdate)
        //{
        //    courseToUpdate.Links = LinkManager.SingleCourseLinks(Url, courseToUpdate.CourseName);
        //    BaseRepository.Instance.CoursesCollection.Update(courseToUpdate);

        //    return Ok();
        //}

        [HttpPut, Route(WebApiConfig.RoutesTemplates.CourseGrades, Name = "UpdateGradeForStudent")] //TODO
        public IHttpActionResult UpdateGradeForStudent([FromBody]GradeModel gradeToUpdate)
        {
            System.Diagnostics.Debug.WriteLine(gradeToUpdate.ToString());
            System.Diagnostics.Debug.WriteLine(gradeToUpdate.Id);
            System.Diagnostics.Debug.WriteLine(gradeToUpdate.Id.GetType());
            System.Diagnostics.Debug.WriteLine(gradeToUpdate.Value);
            System.Diagnostics.Debug.WriteLine(gradeToUpdate.IssueDateTime);
            System.Diagnostics.Debug.WriteLine(gradeToUpdate.CourseReference);
            System.Diagnostics.Debug.WriteLine(gradeToUpdate.StudentReference);

            var gradeToUpdateFromDb =
                BaseRepository.Instance.GradesCollection.Single(grade => grade.Id == gradeToUpdate.Id);

            gradeToUpdate.CourseReference = gradeToUpdateFromDb.CourseReference;
            gradeToUpdate.StudentReference = gradeToUpdateFromDb.StudentReference;

            //BaseRepository.Instance.GradesCollection.U
            BaseRepository.Instance.GradesCollection.Update(gradeToUpdate);

            return Ok();
        }

        [HttpPut, Route(WebApiConfig.RoutesTemplates.CourseStudents, Name = "RegisterStudentForCourse")]//TODO
        public IHttpActionResult RegisterStudentForCourse(int id, string courseName, bool unregister = false)
        {
            var course = BaseRepository.Instance.CoursesCollection.Single(x => x.CourseName == courseName);
            var student = BaseRepository.Instance.StudentsCollection.Single(x => x.Id == id);

            if (unregister)
            {              
                course.StudentsReferences.Remove(new MongoDBRef(DatabaseConfig.StudentsCollectionName, student.Id));
                student.CoursesReferences.Remove(new MongoDBRef(DatabaseConfig.CoursesCollectionName, new ObjectId(course.Id)));
            }
            else
            {
                course.StudentsReferences.Add(new MongoDBRef(DatabaseConfig.StudentsCollectionName, student.Id));
                student.CoursesReferences.Add(new MongoDBRef(DatabaseConfig.CoursesCollectionName, new ObjectId(course.Id)));
            }
            BaseRepository.Instance.CoursesCollection.Update(course);
            BaseRepository.Instance.StudentsCollection.Update(student);

            return Ok();
        }
        //[HttpPut, Route(WebApiConfig.RoutesTemplates.Courses, Name = "UnregisterStudentFromCourse")]//TODO
        //public IHttpActionResult UnregisterStudentFromCourse(int id, string courseName)
        //{
        //    throw new NotImplementedException();
        //}

        /*=======================================
        =========== DELETE METHODS ==============
        =======================================*/
        [HttpDelete, Route(WebApiConfig.RoutesTemplates.Courses, Name = "DeleteCourseByName")] //TODO
        public IHttpActionResult DeleteCourse(string courseId)
        {
            var courseObjectId = new ObjectId(courseId);
            //BaseRepository.Instance.GradesCollection.ForEach(grade => grade.CourseReference.RemoveAll(reference => reference.Id.AsObjectId == gradeObjectId));
            //BaseRepository.Instance.StudentsCollection.ForEach(student => student.GradesReferences.RemoveAll(reference => reference.Id.AsObjectId == gradeObjectId));
            //BaseRepository.Instance.GradesCollection.ForEach(grade =>
            //{
            //    if (grade.CourseReference.Id.AsObjectId == courseObjectId)
            //    {
            //        BaseRepository.Instance.GradesCollection.Delete(new ObjectId(grade.Id));
            //    }              
            //} );

            var gradesIds = new List<ObjectId>();

            BaseRepository.Instance.GradesCollection.ForEach(grade =>
            {
                if (grade.CourseReference.Id.AsObjectId == courseObjectId)
                {
                    gradesIds.Add(new ObjectId(grade.Id));
                }
            });

            BaseRepository.Instance.StudentsCollection.ForEach(student =>
            {
                student.GradesReferences.RemoveAll(reference => gradesIds.Contains(reference.Id.AsObjectId));
                BaseRepository.Instance.StudentsCollection.Update(student);
            });

            BaseRepository.Instance.GradesCollection.Delete(grade => grade.CourseReference.Id.AsObjectId == courseObjectId);         

            BaseRepository.Instance.CoursesCollection.Delete(courseObjectId);

            return Ok();
        }
        [HttpDelete, Route(WebApiConfig.RoutesTemplates.CourseGrades, Name = "DeleteGradeForStudentByDateOfIssue")] //TODO
        public IHttpActionResult DeleteGradeForStudentByDateOfIssue(int id, DateTime dateOfIssue)
        {
            throw new NotImplementedException();
        }
        [HttpDelete, Route(WebApiConfig.RoutesTemplates.CourseGrades, Name = "DeleteGradeForStudent")]
        public IHttpActionResult DeleteGradeForStudent(string gradeId)
        {
            var gradeObjectId = new ObjectId(gradeId);
            //BaseRepository.Instance.CoursesCollection.ForEach(course => course.GradesReferences.ForEach(reference =>
            //{
            //    System.Diagnostics.Debug.WriteLine(reference.Id.AsObjectId);
            //    System.Diagnostics.Debug.WriteLine(gradeObjectId);
            //    System.Diagnostics.Debug.WriteLine(reference.Id.AsObjectId == gradeObjectId);

            //}));

            //BaseRepository.Instance.CoursesCollection.ForEach(course =>
            //{
            //    for (int i = course.GradesReferences.Count - 1; i >= 0; i--)
            //    {
            //        if (course.GradesReferences[i].Id.AsObjectId == gradeObjectId)
            //        {
            //            course.GradesReferences.RemoveAt(i);
            //        }
            //    }
            //});

            //BaseRepository.Instance.StudentsCollection.ForEach(student =>
            //{
            //    for (int i = student.GradesReferences.Count - 1; i >= 0; i--)
            //    {
            //        if (student.GradesReferences[i].Id.AsObjectId == gradeObjectId)
            //        {
            //            student.GradesReferences.RemoveAt(i);
            //        }
            //    }
            //});

            BaseRepository.Instance.CoursesCollection.ForEach(course =>
            {
                System.Diagnostics.Debug.WriteLine(course.Id.GetType());
                course.GradesReferences.RemoveAll(reference => reference.Id.AsObjectId == gradeObjectId);
                BaseRepository.Instance.CoursesCollection.Update(course);
            });
            BaseRepository.Instance.StudentsCollection.ForEach(student =>
            {
                student.GradesReferences.RemoveAll(reference => reference.Id.AsObjectId == gradeObjectId);
                BaseRepository.Instance.StudentsCollection.Update(student);
            });   
            BaseRepository.Instance.GradesCollection.Delete(gradeObjectId);

            return Ok();
        }
    }
}
