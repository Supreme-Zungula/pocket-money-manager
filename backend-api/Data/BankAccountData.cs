using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Data
{
    public class BankAccountData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string AccountNo { get; set; }
        public decimal Balance { get; set; }
        public string UserReference { get; set; }
    }
}
