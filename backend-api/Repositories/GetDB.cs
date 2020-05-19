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
        public IMongoCollection<UserData> GetUserCollection()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("PocketMoneyDB");
            
            return db.GetCollection<UserData>("Users");
        }

        //for transactions
        /*public IMongoCollection<TransactionData> GetTransactionsCollection()
        {
        }
        */

        //for family members
        public IMongoCollection<FamilyMemberData> GetFamilyMembersCollection()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("PocketMoneyDB");

            return db.GetCollection<FamilyMemberData>("FamilyMembers");
        }

    }
}
