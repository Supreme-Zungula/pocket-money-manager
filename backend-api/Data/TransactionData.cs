using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data
{
    public class TransactionData
    {
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
