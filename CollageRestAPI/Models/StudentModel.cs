using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CollageRestAPI.Models
{
    //[DataContract(IsReference = true)]
    public class StudentModel
    {
        //if (Enumerable.Range(1, 999999).Contains(value))
        [BsonId]
        public int Index { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        [IgnoreDataMember]
        public List<GradeModel> Grades { get; set; } = new List<GradeModel>();
        [IgnoreDataMember]
        public List<CourseModel> Courses { get; set; } = new List<CourseModel>();
    }
}