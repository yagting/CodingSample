using System;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace CodingExercise.Utilities
{
    /// <summary>
    /// This class is a custom serializer for the _id field. The _id field type is either integer or ObjectId
    /// </summary>
    public class BsonIDSerializer : BsonBaseSerializer
    {
        /// <summary>
        /// Override implementation of the deserialization for the BsonId
        /// </summary>
        /// <param name="bsonReader"></param>
        /// <param name="nominalType"></param>
        /// <param name="actualType"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override object Deserialize(BsonReader bsonReader, Type nominalType, Type actualType, IBsonSerializationOptions options)
        {
            var bsonType = bsonReader.CurrentBsonType;
            switch (bsonType)
            {
                case BsonType.Null:
                    return null;
                case BsonType.ObjectId:
                    return bsonReader.ReadObjectId().ToString();
                case BsonType.Int32:
                    return bsonReader.ReadInt32().ToString();
                default:
                    var message = string.Format("BsonId expects an Int32 or ObjectId, not a {0}.", bsonType);
                    throw new BsonSerializationException(message);
            }
        }

        /// <summary>
        /// Override implementation of the serialization for the BsonId
        /// </summary>
        /// <param name="bsonWriter"></param>
        /// <param name="nominalType"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Serialize(BsonWriter bsonWriter, Type nominalType, object value, IBsonSerializationOptions options)
        {
            if (value == null)
                bsonWriter.WriteNull();
            else
            {
                int output;

                //if the value is convertible to int write an int otherwise ObjectId
                if (Int32.TryParse(value as string, out output))
                    bsonWriter.WriteInt32(output);
                else
                    bsonWriter.WriteObjectId(new ObjectId((string)value));
            }
        }
    }
}