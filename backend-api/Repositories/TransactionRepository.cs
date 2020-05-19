using MongoDB.Driver;
using Data;
using Domain.DefinitionObjects;
using System;

namespace Repositories
{
  public class TransactionRepository
  {
    private MongoClient _client;
    private IMongoDatabase _database;
    private IMongoCollection<Transaction> _transactions;

    public TransactionRepository()
    {

      _client = new MongoClient(DatabaseSettings.ConnectionString);
      _database = _client.GetDatabase(DatabaseSettings.DatabaseName);
      _transactions = _database.GetCollection<Transaction>("Transactions");

    }

    public IMongoCollection<Transaction> GetTransactions()
    {
      return _transactions;
    }

    public Transaction GetTransaction(string id)
    {
      return _transactions.Find<Transaction>(tran => tran.Id == id).FirstOrDefault();
    }

    public Transaction AddTransaction(Transaction transaction)
    {
      transaction.Date = DateTime.Now;
      _transactions.InsertOne(transaction);
      return transaction;
    }

    public Transaction UpdateTransaction(string id, Transaction transactionIn)
    {
      var result = _transactions.ReplaceOne((trans) => trans.Id == id, transactionIn);

      if (result.IsAcknowledged)
      {
        return transactionIn;
      }
      return null;
    }

    public Transaction RemoveTransaction(Transaction transaction)
    {
      var result = _transactions.DeleteOne((trans) => trans.Id == transaction.Id);
      if (result.DeletedCount == 1)
      {
        return transaction;
      }

      return null;
    }

    public string RemoveTransaction(string id)
    {
      var result = _transactions.DeleteOne(tran => tran.Id == id);

      if (result.DeletedCount == 1)
      {
        return id;
      }

      return null;
    }

    public static TransactionData FromDomain(Transaction transaction)
    {
      return new TransactionData
      {
        Id = transaction.Id,
        AccountNo = transaction.AccountNo,
        Deposit = transaction.Deposit,
        Withdrawal = transaction.Withdrawal,
        Reference = transaction.Reference,
        Date = transaction.Date
      };
    }

    public static Transaction ToDomain(TransactionData transaction)
    {
      return new Transaction
      {
        Id = transaction.Id,
        AccountNo = transaction.AccountNo,
        Deposit = transaction.Deposit,
        Withdrawal = transaction.Withdrawal,
        Reference = transaction.Reference,
        Date = transaction.Date
      };
    }
  }
}
