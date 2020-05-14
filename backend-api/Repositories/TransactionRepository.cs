using MongoDB.Driver;
using Data;
using Domain.DefinitionObjects;

namespace Repositories
{
  public class TransactionRepository
  {
    private MongoClient _client;
    private IMongoDatabase _database;
    private IMongoCollection<Transaction> _transactions;

    public TransactionRepository()
    {
      try
      {
        _client = new MongoClient(DatabaseSettings.ConnectionString);
        _database = _client.GetDatabase(DatabaseSettings.DatabaseName);
        _transactions = _database.GetCollection<Transaction>("Transactions");
      }
      catch (MongoClientException ex)
      {
        throw ex;
      }
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
      _transactions.InsertOne(transaction);
      return transaction;
    }

    public void UpdateTransaction(string id, Transaction transactionIn)
    {
      _transactions.ReplaceOne((trans) => trans.Id == id, transactionIn);
    }

    public void RemoveTransaction(Transaction transaction)
    {
      _transactions.DeleteOne((trans) => trans.Id == transaction.Id);
    }

    public void RemoveTransaction(string id)
    {
      _transactions.DeleteOne(tran => tran.Id == id);
    }
  }
}
