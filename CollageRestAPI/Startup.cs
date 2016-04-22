using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using MongoDB.Driver;
using CollageRestAPI.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

[assembly: OwinStartup(typeof(CollageRestAPI.Startup))]

namespace CollageRestAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DbInit();
            ConfigureAuth(app);
        }

        private void DbInit()
        {
            BsonClassMap.RegisterClassMap<BaseRepository>(x => x.AutoMap());
            BsonClassMap.RegisterClassMap<StudentModel>(x => x.AutoMap());
            BsonClassMap.RegisterClassMap<CourseModel>(x => x.AutoMap());
            BsonClassMap.RegisterClassMap<GradeModel>(x => x.AutoMap());

            var dbClient = BaseRepository.Instance.DbClient;
            var db = BaseRepository.Instance.Db;
            dbClient = new MongoClient("mongodb://localhost");
            db = dbClient.GetDatabase("collagerestapi");          
            //db.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait();
            //System.Diagnostics.Debug.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
        }
    }
}
