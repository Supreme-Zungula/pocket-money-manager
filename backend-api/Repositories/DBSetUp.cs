using System;
using System.Collections.Generic;
using Data;
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

           //UsersCollection(db);
            TransactionsCollection(db);
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
                    new UserData{FamilyId = ObjectId.GenerateNewId().Increment, FirstName = "Lionel", LastName = "Messi", Role = "admin", Password = "tobehashed", Phone = "0721121122"},
                    new UserData{FamilyId = ObjectId.GenerateNewId().Increment, FirstName = "Tasha", LastName = "Cobbs", Role = "admin", Password = "tobehashed", Phone = "0810022311"},
                    new UserData{FamilyId = ObjectId.GenerateNewId().Increment, FirstName = "Dinelle", LastName = "Stille", Role = "admin", Password = "tobehashed", Phone = "0621172313"}
                };
                foreach(var user in dummyUsers)
                {
                    collection.Save(user);
                }
            }
        }

        public void TransactionsCollection(MongoDatabase db)
        {
            if (db.CollectionExists("Transactions") == false)
                db.CreateCollection("Transactions");

        }

        public void BankAccountsCollection(MongoDatabase db)
        {
            if (db.CollectionExists("BankAccounts") == false)
            {
                db.CreateCollection("BankAccounts");
            }
        }
     
    }
}