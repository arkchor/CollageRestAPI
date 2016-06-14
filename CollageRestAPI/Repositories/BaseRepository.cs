using System;
using System.Collections.Generic;
using CollageRestAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoRepository;

namespace CollageRestAPI.Repositories
{
    public sealed class BaseRepository
    {
        //public MongoClient DbClient { get; set; }
        //public IMongoDatabase Db { get; set; }
        private readonly MongoDatabase _db;

        private static readonly Lazy<BaseRepository> Lazy =
        new Lazy<BaseRepository>(() => new BaseRepository());
        public static BaseRepository Instance => Lazy.Value;

        private BaseRepository()
        {
            _db = StudentsCollection.Collection.Database;
        }

        public T Fetch<T>(MongoDBRef reference)
        {
            return _db.FetchDBRefAs<T>(reference);
        }

        //public List<StudentModel> StudentsCollection { get; set; } = new List<StudentModel>();
        //public List<CourseModel> CoursesCollection { get; set; } = new List<CourseModel>();

        public MongoRepository<StudentModel, int> StudentsCollection { get; set; } = new MongoRepository<StudentModel, int>();
        public MongoRepository<CourseModel, string> CoursesCollection { get; set; } = new MongoRepository<CourseModel, string>();
        public MongoRepository<GradeModel, string> GradesCollection { get; set; } = new MongoRepository<GradeModel, string>();
        public MongoRepository<IndexConfig, ObjectId> CurrentIndexConfig { get; set; } = new MongoRepository<IndexConfig, ObjectId>();
    }
}