using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Reconfig.Domain.Model
{
    public abstract class AggregateRoot
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Version Version { get; set; }
    }
}
