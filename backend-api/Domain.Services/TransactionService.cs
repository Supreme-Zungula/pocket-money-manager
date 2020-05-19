
using System.Collections.Generic;
using MongoDB.Driver;
using Domain.DefinitionObjects;
using Repositories;

namespace Domain.Services
{
  public class TransactionService
  {
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

    public Transaction UpdateTransaction(string id, Transaction transactionIn)
    {
      return _repository.UpdateTransaction(id, transactionIn);
    }

    public Transaction RemoveTransaction(Transaction transactionIn)
    {
      return _repository.RemoveTransaction(transactionIn);
    }

    public string RemoveTransaction(string id)
    {
      return _repository.RemoveTransaction(id);
    }

  }
}
