using System;
using System.Collections.Generic;
using CollageRestAPI.Models;
using MongoDB.Driver;
using MongoRepository;

namespace CollageRestAPI.Repositories
{
    public sealed class BaseRepository
    {
        public MongoClient DbClient { get; set; }
        public IMongoDatabase Db { get; set; }

        private static readonly Lazy<BaseRepository> lazy =
        new Lazy<BaseRepository>(() => new BaseRepository());
        public static BaseRepository Instance { get { return lazy.Value; } }
        private BaseRepository(){}

        public List<StudentModel> StudentsCollection { get; set; } = new List<StudentModel>();
        public List<CourseModel> CoursesCollection { get; set; } = new List<CourseModel>();

        public MongoRepository<StudentModel, int> StudentsCollectiond { get; set; } = new MongoRepository<StudentModel, int>();
        public MongoRepository<CourseModel, Guid> CoursesCollectiond { get; set; } = new MongoRepository<CourseModel, Guid>();
    }
}