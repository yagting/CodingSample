using MongoDB.Bson.Serialization.Attributes;

namespace CodingExercise.Models
{
    [BsonIgnoreExtraElements(true)]
    public class SourceAward
    {
        /// <summary>
        /// This field map to the award property mapped to the "name" key in the BsonDocument
        /// </summary>
        [BsonDefaultValue(null)]
        [BsonElement("award")]
        public string AwardName { get; set; }

        /// <summary>
        /// This field map to the by property mapped to the "name" key in the BsonDocument
        /// </summary>
        [BsonDefaultValue(null)]
        [BsonElement("by")]
        public string AwardBy { get; set; }

        /// <summary>
        /// This field map to the year property mapped to the "name" key in the BsonDocument
        /// </summary>
        [BsonDefaultValue(null)]
        [BsonElement("year")]
        public int AwardYear { get; set; }
    }
}