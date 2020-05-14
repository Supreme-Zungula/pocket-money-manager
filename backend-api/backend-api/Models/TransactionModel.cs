using System;
using System.ComponentModel.DataAnnotations;
using Domain.DefinitionObjects;
using MongoDB.Bson;

namespace backend_api.Models
{
    public class TransactionModel
    {
        [Key]
        public string Id { get; set; }
        public decimal Deposit
        { get; internal set; }
        public decimal Withdrawal
        { get; internal set; }
        public string Reference
        { get; internal set; }
        public DateTime Date
        { get; internal set; }

        public static TransactionModel FromDomain(Transaction transaction)
        {
            return new TransactionModel
            {
                Id = transaction.Id,
                Deposit = transaction.Deposit,
                Withdrawal = transaction.Withdrawal,
                Reference = transaction.Reference,
                Date = transaction.Date
            };
        }

        public Transaction ToDomain()
        {
            return new Transaction(this.Deposit, this.Withdrawal, this.Reference, this.Date);
        }
        
    }
}
