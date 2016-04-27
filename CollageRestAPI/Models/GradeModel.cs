using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using MongoRepository;

namespace CollageRestAPI.Models
{
    //[DataContract(IsReference = true)]
    [CollectionName("grades")]
    public class GradeModel : IEntity<Guid>
    {
        [BsonId]
        public Guid Id { get; set; }
        public double Value { get; set; }
        public DateTime IssueDateTime { get; set; }
        [IgnoreDataMember]
        [BsonIgnore]
        public StudentModel Student { get; set; } = new StudentModel();
        [IgnoreDataMember]
        [BsonIgnore]
        public CourseModel Course { get; set; } = new CourseModel();
    }
}