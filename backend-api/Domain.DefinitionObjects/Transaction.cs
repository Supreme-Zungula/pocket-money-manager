using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.DefinitionObjects
{
    public class Transaction
    {
        public Transaction (decimal deposit, decimal withdrawal, string reference, DateTime date)
        {
            this.Deposit = deposit;
            this.Withdrawal = withdrawal;
            this.Reference = reference;
            this.Date = date;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public decimal Deposit
        { get; internal set; }
        public decimal Withdrawal
        { get; internal set; }
        public string Reference
        { get; internal set; }
        public DateTime Date
        { get; internal set; }
    }
}
