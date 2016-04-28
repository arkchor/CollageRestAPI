using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using CollageRestAPI.Hypermedia;
using MongoDB.Bson;
using MongoRepository;

namespace CollageRestAPI.Models
{
    //[DataContract(IsReference = true)]
    [CollectionName(DatabaseConfig.CoursesCollectionName)]
    public class CourseModel : IEntity<ObjectId>
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string CourseName { get; set; }
        public string Tutor { get; set; }
        //[IgnoreDataMember]
        public List<GradeModel> Grades { get; set; } = new List<GradeModel>();
        public List<Link> Links { get; set; }
        //[IgnoreDataMember]
        //public List<StudentModel> Students { get; set; } = new List<StudentModel>();
    }
}