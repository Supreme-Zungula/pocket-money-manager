using Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class GetDB
    {
        //get the latest database, returns a collection
        public IMongoCollection<UserData> Connect()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            //MongoServer server = client.GetServer();
            var db = client.GetDatabase("PocketMoneyDB");
            
            return db.GetCollection<UserData>("Users");
        }
    }
}
