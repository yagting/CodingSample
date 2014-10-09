using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Configuration;
using System.Linq;
using MDB = MongoDB.Driver.Builders;

namespace CodingExercise.Models.Repositories
{
    /// <summary>
    /// This class is an implementaion of the IRepository Interface to represent 
    /// a repository in a Mongo DB
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MdbRepository<T> : IMdbRepository<T> where T:class 
    {
        private MongoCollection<T> _dataCollection = null;

        /// <summary>
        /// This is a constructor that initializes a MongoCollection<T>
        /// </summary>
        public MdbRepository()
        {
            var MdbBDatabaseName = ConfigurationManager.AppSettings["MongoDbDatabaseName"] ?? "Bios";
            var collectionName = ConfigurationManager.AppSettings["DeveloperCollectionName"] ?? "Developers";
            var mdb = MongoDBContext.Instance.DBContext(MdbBDatabaseName);

            _dataCollection = mdb.GetCollection<T>(collectionName);
        }

        /// <summary>
        /// Implement the interface's Update method 
        /// </summary>
        /// <param name="entity">Entity is of type ISourceEntity</param>
        public void Update(T entity)
        {
            BsonDocument bsonDoc = entity.ToBsonDocument();

            if (bsonDoc != null)
            {
                _dataCollection.Update(MDB.Query.EQ("_id", bsonDoc["_id"]), MDB.Update.Set("processed", true));
            }
        }

        /// <summary>
        /// Implement the interface's Find method 
        /// </summary>
        /// <param name="entity">Entity is of type ISourceEntity</param>
        public IQueryable<T> Find(Func<T, bool> predicate)
        {
            return _dataCollection.AsQueryable<T>().Where(predicate).AsQueryable();
        }
    }
}