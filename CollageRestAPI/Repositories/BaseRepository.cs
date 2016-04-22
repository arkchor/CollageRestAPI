using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollageRestAPI.Models
{
    public sealed class BaseRepository
    {
        public MongoClient DbClient { get; set; }
        public IMongoDatabase Db { get; set; }

        private static readonly Lazy<BaseRepository> lazy =
        new Lazy<BaseRepository>(() => new BaseRepository());
        public static BaseRepository Instance { get { return lazy.Value; } }
        private BaseRepository(){}

        public List<StudentModel> Students { get; set; } = new List<StudentModel>();
        public List<CourseModel> Courses { get; set; } = new List<CourseModel>();
    }
}