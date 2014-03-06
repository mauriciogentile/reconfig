using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Reconfig.Storage.Mongo.Extensions
{
    public static class MongoDbDriverExtensions
    {
        public static T FindById<T>(this MongoCollection<T> collection, string id)
            where T : class
        {
            try
            {
                var t = collection.FindOneById(BsonValue.Create(new ObjectId(id)));
                return t;
            }
            catch (FormatException)
            {
                //invalid id
                return null;
            }
            catch (ArgumentOutOfRangeException)
            {
                // Document not found
                return null;
            }
        }

        public static bool Exists<T>(this MongoCollection<T> collection, string id)
        {
            return Exists(collection, new QueryDocument("_id", new ObjectId(id)));
        }

        public static bool Exists<T>(this MongoCollection<T> collection, IMongoQuery query)
        {
            try
            {
                var cursor = collection.Find(query);
                cursor.SetLimit(1);
                return cursor.Any();
            }
            catch (ArgumentOutOfRangeException)
            {
                // Document not found
                return false;
            }
        }

        public static void Remove<T>(this MongoCollection<T> collection, string id)
        {
            collection.Remove(new QueryDocument("_id", new ObjectId(id)));
        }

        static public QueryDocument ToQueryDocument(this string from)
        {
            if (string.IsNullOrWhiteSpace(from)) return null;

            using (var reader = BsonReader.Create(from))
            {
                var doc = BsonSerializer.Deserialize<BsonDocument>(reader);
                var query = new QueryDocument(doc.Elements);
                return query;
            }
        }
    }
}