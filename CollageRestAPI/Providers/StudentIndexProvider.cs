using CollageRestAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollageRestAPI.Providers
{
    public static class StudentIndexProvider
    {
        //private static readonly Lazy<StudentIndexProvider> lazy =
        //new Lazy<StudentIndexProvider>(() => new StudentIndexProvider());
        //public static StudentIndexProvider Instance { get { return lazy.Value; } }
        //private StudentIndexProvider() { }

        public static int currentIndex;
        public static int GetNewIndex()
        {
            return ++currentIndex;
        }
        public static void SetCurrentIndex()
        {
            var col = BaseRepository.Instance.Db.GetCollection<BsonDocument>("studentindexprovider");
            System.Diagnostics.Debug.WriteLine(col.Find(new BsonDocument()).FirstOrDefault().ToString());
            System.Diagnostics.Debug.WriteLine(col.Find(new BsonDocument()).FirstOrDefault().Elements.Single(x => x.Name == "currentIndex").Value);
            //currentIndex = BaseRepository.Instance.Db.GetCollection("")
        }
    }
}