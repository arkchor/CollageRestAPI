using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CollageRestAPI.Models
{
    //[DataContract(IsReference = true)]
    public class CourseModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public string Tutor { get; set; }
        [IgnoreDataMember]
        public List<GradeModel> Grades { get; set; } = new List<GradeModel>();
        [IgnoreDataMember]
        public List<StudentModel> Students { get; set; } = new List<StudentModel>();
    }
}