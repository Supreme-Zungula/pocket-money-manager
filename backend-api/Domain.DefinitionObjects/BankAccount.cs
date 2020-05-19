using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.DefinitionObjects
{
  public class BankAccount
  {
    private decimal _balance;
    private string _accountNo;
    private string _customerRef;
    private List<Transaction> _transactions = new List<Transaction>();

    public BankAccount(string accountNo, decimal balance, string customerRef, Transaction transaction)
    {
            _accountNo = accountNo;
            _balance = balance;
            _customerRef = customerRef;
            _transactions.Add(transaction);
    }
    public BankAccount(decimal balance, string customerRef)
    {
            _accountNo = Guid.NewGuid().ToString();
            _balance = balance;
            _customerRef = customerRef;

            Transaction transaction = new Transaction
            {
                AccountNo = this.AccountNo.ToString(),
                Deposit = balance,
                Withdrawal = 0,
                Reference = "Account creation",
                Date = DateTime.Now
            };

            _transactions.Add( transaction );
    }
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string AccountNo
    {
      get { return _accountNo; }
      internal set { _accountNo = value; }
    }
    
    public decimal Balance
    {
      get { return _balance; }
      internal set { _balance = value; }
    }
    public string CustomerRef
    {
      get { return _customerRef; }
      set { _customerRef = value; }
    }
    public bool CanWithdraw(decimal amount)
    {
      return (Balance >= amount);
    }
    public void Withdraw(decimal amount, string reference)
    {
      if (CanWithdraw(amount))
      {
        Balance -= amount;
        _transactions.Add(
          new Transaction 
          { 
            AccountNo = this.AccountNo.ToString(),
            Deposit = 0m, 
            Withdrawal = amount, 
            Reference =reference,
            Date = DateTime.Now 
          }
        );
      }
      else
      {
        throw new InsufficientFundsException();
      }
    }
    public void Deposit(decimal amount, string reference)
    {
      Balance += amount;
      Transaction transaction =  new Transaction
        {
          AccountNo = this.AccountNo.ToString(),
          Deposit = amount,
          Withdrawal = 0m,
          Reference = reference,
          Date = DateTime.Now
        };

      _transactions.Add(transaction);
    }
    public IEnumerable<Transaction> GetTransactions()
    {
      return _transactions;
    }
  }

  public class InsufficientFundsException : ApplicationException
  {
  } 
}
