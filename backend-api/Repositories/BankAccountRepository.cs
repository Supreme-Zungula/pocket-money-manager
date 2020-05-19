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

    public BankAccount GetAccount(string acccountID)
    {
            var result = _accounts.Find<BankAccount>(account =>  account.Id == acccountID).FirstOrDefault();
            return result;
    }
    public BankAccount AddAccount(BankAccount newAccount)
    {
      _accounts.InsertOne(newAccount);
      return newAccount;
    }
    public BankAccount UpdateAccount(string accountId, BankAccount accountIn)
    {
            BankAccount account = GetAccount(accountId);
            if (account == null)
            {
              return null;
            }

            accountIn.Id = account.Id;
            _accounts.ReplaceOne(acc => acc.Id == accountId, accountIn);
            return accountIn;
    }

    public void DeleteAccount(BankAccount account)
    {
      _accounts.DeleteOne(acc => acc.AccountNo == account.AccountNo);
    }

    public BankAccount DeleteAccount(string accountId)
    {
      var accountFound = GetAccount(accountId);

      if (accountFound != null)
        _accounts.DeleteOne(account => account.Id == accountId);
      return accountFound;
    }
  }
}
