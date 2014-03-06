using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reconfig.Domain.Model
{
    public enum Environment
    {
        Development,
        Testing,
        Staging,
        Production,
        Any
    }

    public class Configuration : AggregateRoot, IFromApplication
    {
        public Configuration()
        {
            Version = new Version();
            Sections = new List<ConfigurationSection>();
        }

        [Required]
        public string ApplicationId { get; set; }
        public string ParentId { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        [BsonRepresentation(BsonType.String)]
        public Environment Environment { get; set; }
        public string Url { get; set; }
        public List<ConfigurationSection> Sections { get; set; }
    }
}
