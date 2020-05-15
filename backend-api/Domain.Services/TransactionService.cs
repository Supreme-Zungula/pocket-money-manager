
using System.Collections.Generic;
using MongoDB.Driver;
using Domain.DefinitionObjects;
using Repositories;

namespace Domain.Services
{
  public class TransactionService
  {
    private readonly IMongoCollection<Transaction> _transactions;
    private TransactionRepository _repository;
    public TransactionService()
    {
      _repository = new TransactionRepository();
    }

    public List<Transaction> GetTransactions()
    {
      return _repository.GetTransactions().Find<Transaction>(trans => true).ToList();
    }

    public Transaction GetTransaction(string id)
    {
      return _repository.GetTransaction(id);
    }
    public Transaction AddTransaction(Transaction transaction)
    {
      _repository.AddTransaction(transaction);
      return transaction;
    }

    public void UpdateTransaction(string id, Transaction transactionIn)
    {
      _repository.UpdateTransaction(id, transactionIn);
    }

    public void RemoveTransaction(Transaction transactionIn)
    {
      _repository.RemoveTransaction(transactionIn);
    }

    public void RemoveTransaction(string id)
    {
      _repository.RemoveTransaction(id);
    }

  }
}
