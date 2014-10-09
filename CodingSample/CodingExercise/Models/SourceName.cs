using MongoDB.Bson.Serialization.Attributes;

namespace CodingExercise.Models
{
    [BsonIgnoreExtraElements(true)]
    public class SourceName
    {
        [BsonElement("first")]
        [BsonDefaultValue(null)]
        public string FirstName { get; set; }

        [BsonElement("last")]
        [BsonDefaultValue(null)]
        public string LastName { get; set; }

        [BsonElement("aka")]
        [BsonDefaultValue(null)]
        public string AKA { get; set; }
    }
}