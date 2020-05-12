using System;
using System.Collections.Generic;
using System.Text;
using Domain.DefinitionObjects;
using MongoDB.Bson;
using MongoDB.Driver;


namespace Repositories
{
    public class DBSetUp : ISetCollections
    {
        [Obsolete("Use the new API instead.")]
        public DBSetUp()
        {
            MongoClient client = new MongoClient();
            MongoServer server = client.GetServer();
            MongoDatabase db = server.GetDatabase("PocketMoneyDB");

            UsersCollection(db);
        }

        public void UsersCollection(MongoDatabase db)
        {
            if (db.CollectionExists("Users") == false)
                db.CreateCollection("Users");

            //for testing
            MongoCollection<User> collection = db.GetCollection<User>("Users");
            User p = new User { FamilyId = ObjectId.GenerateNewId().Increment, FirstName = "NewID", LastName = "Testings", Role = "father" };
            collection.Save(p);
        }
    }
}