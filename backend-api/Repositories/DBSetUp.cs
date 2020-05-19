using System;
using System.Collections.Generic;
using System.Text;
using Data;
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
            FamilyMembersCollection(db);
        }

        public void FamilyMembersCollection(MongoDatabase db)
        {
            if (db.CollectionExists("FamilyMembers") == false)
                db.CreateCollection("FamilyMembers");
        }

        public void UsersCollection(MongoDatabase db)
        {
            if (db.CollectionExists("Users") == false)
            {
                db.CreateCollection("Users");
                MongoCollection<UserData> collection = db.GetCollection<UserData>("Users");
                List<UserData> dummyUsers = new List<UserData>() 
                {
                    new UserData{FamilyId = ObjectId.GenerateNewId().Increment, FirstName = "Lionel", LastName = "Messi", Role = "Father", Password = "tobehashed", Phone = "0721121122"},
                    new UserData{FamilyId = ObjectId.GenerateNewId().Increment, FirstName = "Tasha", LastName = "Cobbs", Role = "Mother", Password = "tobehashed", Phone = "0810022311"},
                    new UserData{FamilyId = ObjectId.GenerateNewId().Increment, FirstName = "Dinelle", LastName = "Stille", Role = "Daughter", Password = "tobehashed", Phone = "0621172313"}
                };
                foreach(var user in dummyUsers)
                {
                    collection.Save(user);
                }
            }
        }
    }
}