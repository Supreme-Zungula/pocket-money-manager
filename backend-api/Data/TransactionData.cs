using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data
{
    public class TransactionData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string AccountNo { get; set; }
        public decimal Deposit
        { get; set; }
        public decimal Withdrawal
        { get;  set; }
        public string Reference
        { get; set; }
        public DateTime Date
        { get; set; }
    }
}
