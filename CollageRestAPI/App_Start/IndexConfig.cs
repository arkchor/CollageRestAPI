using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoRepository;

namespace CollageRestAPI
{
    [CollectionName("indexconfig")]
    public class IndexConfig : IEntity<ObjectId>
    {
        private static readonly Lazy<IndexConfig> lazy =
        new Lazy<IndexConfig>(() => new IndexConfig());
        public static IndexConfig Instance { get { return lazy.Value; } }

        private IndexConfig()
        {
            Id = ObjectId.Parse("571fe9c98dfd1418185b71d6");
        }

        [BsonId]
        public ObjectId Id { get; set; }
        public int CurrentIndex { get; set; }
    }
}