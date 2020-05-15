using System;
using System.ComponentModel.DataAnnotations;
using Domain.DefinitionObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend_api.Models
{
    public class TransactionModel
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string AccountNo { get; set; }
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
