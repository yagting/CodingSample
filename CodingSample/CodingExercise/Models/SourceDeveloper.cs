using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CodingExercise.Utilities;

namespace CodingExercise.Models
{
    [BsonIgnoreExtraElements(true)]
    public class SourceDeveloper : ISourceEntity
    {
        [BsonId]
        [BsonSerializer(typeof(BsonIDSerializer))]
        [BsonElement("_id")]
        public string ID { get; set; }

        [BsonElement("name")]
        public SourceName Name { get; set; }

        [BsonDefaultValue(null)]
        [BsonElement("title")]
        public string Title { get; set; }

        [BsonDefaultValue(null)]
        [BsonElement("birth")]
        public Nullable<DateTime> Birth { get; set; }

        [BsonDefaultValue(null)]
        [BsonElement("death")]
        public Nullable<DateTime> Death { get; set; }

        [BsonDefaultValue(null)]
        [BsonElement("awards")]
        public List<SourceAward> Awards { get; set; }

        [BsonDefaultValue(null)]
        [BsonElement("contribs")]
        public List<string> Contribs { get; set; }

        [BsonDefaultValue(null)]
        [BsonElement("processed")]
        public Nullable<bool> Processed { get; set; }
    }
}