using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using MongoRepository;

namespace CollageRestAPI.Models
{
    //[DataContract(IsReference = true)]
    [CollectionName("courses")]
    public class CourseModel : IEntity<Guid>, ISupportInitialize
    {
        [BsonId]
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public string Tutor { get; set; }
        [IgnoreDataMember]
        [BsonIgnore]
        public List<GradeModel> Grades { get; set; } = new List<GradeModel>();
        [IgnoreDataMember]
        public List<StudentModel> Students { get; set; } = new List<StudentModel>();

        public void BeginInit(){}

        public void EndInit()
        {
            foreach (var student in Students)
            {
                student.Courses.Add(this);
                student.Grades.ForEach(x =>
                {
                    x.Course = this;
                    x.Student = student;
                });
                Grades.AddRange(student.Grades);
            }
        }
    }
}