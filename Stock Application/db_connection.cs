using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Application
{
    /// <summary>
    /// Class handels the connection to the MongoDB instance running on a Win10 host at Amazon cloud server
    /// </summary>
    public static class db_connection
    {
        private static String connectionString = "mongodb://ec2-35-164-218-97.us-west-2.compute.amazonaws.com";
        private static MongoClient Client = new MongoDB.Driver.MongoClient(connectionString);
        private static IMongoDatabase dbtemp = Client.GetDatabase("ClientDBS");
        public static IMongoCollection<BsonDocument> customerTable = dbtemp.GetCollection<BsonDocument>("CustomerTBL");
        public static IMongoCollection<BsonDocument> shareTable = dbtemp.GetCollection<BsonDocument>("SharesTBL");
    }
}
