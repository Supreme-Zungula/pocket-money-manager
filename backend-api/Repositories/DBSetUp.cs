using System;
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

           //UsersCollection(db);
            TransactionsCollection(db);
        }

        public void UsersCollection(MongoDatabase db)
        {
            if (db.CollectionExists("Users") == false)
                db.CreateCollection("Users");
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