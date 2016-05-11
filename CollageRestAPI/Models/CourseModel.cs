using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using CollageRestAPI.Hypermedia;
using CollageRestAPI.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
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
        [IgnoreDataMember]
        public List<MongoDBRef> GradesReferences { get; set; } = new List<MongoDBRef>();
        [IgnoreDataMember]
        public List<MongoDBRef> StudentsReferences { get; set; } = new List<MongoDBRef>();
        //[BsonIgnore]
        public List<Link> Links { get; set; }


        //[BsonIgnore]
        //[IgnoreDataMember]
        //public List<GradeModel> Grades { get; set; } = new List<GradeModel>();
        //public void AddGrades(List<GradeModel> grades)
        //{
        //    Grades.AddRange(grades);
        //    grades.ForEach(grade => GradesReferences.Add(new MongoDBRef(DatabaseConfig.GradesCollectionName, grade.Id)));
        //}
        //[IgnoreDataMember]
        //public List<StudentModel> Students { get; set; } = new List<StudentModel>();

        //public void BeginInit()
        //{
        //    //throw new NotImplementedException();
        //}

        //public void EndInit()
        //{           
        //    var db = BaseRepository.Instance.StudentsCollection.Collection.Database;
        //    GradesReferences.ForEach(gradeReference => Grades.Add(db.FetchDBRefAs<GradeModel>(gradeReference)));
        //}
    }
}