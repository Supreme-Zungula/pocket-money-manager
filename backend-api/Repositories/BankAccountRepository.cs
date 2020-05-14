using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using Domain.DefinitionObjects;
using Data;

namespace Repositories
{
    public class BankAccountRepository
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private IMongoCollection<BankAccount> _accounts;

        public BankAccountRepository()
        {
            this._client = new MongoClient(DatabaseSettings.ConnectionString);
            this._database = this._client.GetDatabase(DatabaseSettings.DatabaseName);
            this._accounts = _database.GetCollection<BankAccount>("BankAccounts");
        }

        public List<BankAccount> GetAccounts()
        {
            return _accounts.Find<BankAccount>(acccount => true).ToList();
        }

        public BankAccount GetAccount(Guid acccountID)
        {
           return _accounts.Find<BankAccount>(account => account.AccountNo == acccountID).FirstOrDefault();
        }

        public BankAccount UpdateAccount(BankAccount accountIn)
        {
            BankAccount account = GetAccount(accountIn.AccountNo);
            if (account == null)
            {
                return null;
            }

            _accounts.ReplaceOne(acc => acc.AccountNo == accountIn.AccountNo, accountIn);
            return accountIn;
        }

        public void DeleteAccount(BankAccount account)
        {
            _accounts.DeleteOne(acc => acc.AccountNo == account.AccountNo);
        }

        public void DeleteAccount(Guid accNo)
        {
            _accounts.DeleteOne(account => account.AccountNo == accNo);
        }
    }
}
