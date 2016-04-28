using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using CollageRestAPI.Hypermedia;
using CollageRestAPI.Repositories;
using MongoDB.Driver;
using MongoRepository;

namespace CollageRestAPI.Models
{
    //[DataContract(IsReference = true)]
    [CollectionName(DatabaseConfig.StudentsCollectionName)]
    public class StudentModel : IEntity<int>, ISupportInitialize
    {
        //if (Enumerable.Range(1, 999999).Contains(value))
        [BsonId]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }

        [IgnoreDataMember]
        [BsonIgnore]
        public List<GradeModel> Grades { get; set; } = new List<GradeModel>();
        public List<MongoDBRef> GradesReferences { get; set; } = new List<MongoDBRef>();
        public void AddGrades(List<GradeModel> grades)
        {
            grades.ForEach(grade => grade.StudentReference = new MongoDBRef(DatabaseConfig.StudentsCollectionName, Id));
            BaseRepository.Instance.GradesCollection.Add(grades);
            Grades.AddRange(grades);
            grades.ForEach(grade => GradesReferences.Add(new MongoDBRef(DatabaseConfig.GradesCollectionName, grade.Id)));
        }

        [IgnoreDataMember]
        [BsonIgnore]
        public List<CourseModel> Courses { get; set; } = new List<CourseModel>();
        public List<MongoDBRef> CoursesReferences { get; set; } = new List<MongoDBRef>();
        public void AddCourses(List<CourseModel> courses)
        {
            BaseRepository.Instance.CoursesCollection.Add(courses);
            Courses.AddRange(courses);
            courses.ForEach(course => CoursesReferences.Add(new MongoDBRef(DatabaseConfig.CoursesCollectionName, course.Id)));
        }

        public List<Link> Links { get; set; } = new List<Link>();


        public void BeginInit()
        {
            //throw new NotImplementedException();
        }

        public void EndInit()
        {
            var db = BaseRepository.Instance.StudentsCollection.Collection.Database;
            GradesReferences.ForEach(gradeReference => Grades.Add(db.FetchDBRefAs<GradeModel>(gradeReference)));
        }
    }
}