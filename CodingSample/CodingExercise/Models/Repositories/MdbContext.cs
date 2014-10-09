using System.Configuration;
using MongoDB.Driver;

namespace CodingExercise.Models.Repositories
{
    /// <summary>
    /// This is a singleton implementation of the MongoDB database context. 
    /// </summary>
    internal sealed class MongoDBContext
    {
        private static readonly MongoDBContext _instance = new MongoDBContext();
        private static readonly MongoServer _dbServer = null;

        /// <summary>
        /// Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
        /// </summary>
        static MongoDBContext()
        {
            string _mdbConnection = ConfigurationManager.ConnectionStrings["MongoDBConnection"].ConnectionString;
            var client = new MongoClient(_mdbConnection);

            _dbServer = client.GetServer();
        }

        /// <summary>
        /// Set a private constructor to prevent instantiation of this class
        /// </summary>
        private MongoDBContext() { }

        /// <summary>
        /// Returns a single instance of this class
        /// </summary>
        public static MongoDBContext Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Expose a property that returns a MongoDatabase object
        /// </summary>
        /// <param name="databaseName">The name of the Mongo database to return</param>
        /// <returns></returns>
        public MongoDatabase DBContext(string databaseName)
        {
            return _dbServer.GetDatabase(databaseName);
        }
    }
}