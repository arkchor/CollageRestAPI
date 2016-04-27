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
    [CollectionName("students")]
    public class StudentModel : IEntity<int>
    {
        //if (Enumerable.Range(1, 999999).Contains(value))
        [BsonId]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        //[IgnoreDataMember]
        public List<GradeModel> Grades { get; set; } = new List<GradeModel>();
        //[IgnoreDataMember]
        public List<CourseModel> Courses { get; set; } = new List<CourseModel>();
    }
}