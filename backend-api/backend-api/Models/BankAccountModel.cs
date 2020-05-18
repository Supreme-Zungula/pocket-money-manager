using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Domain.DefinitionObjects;
using Data;

namespace backend_api.Models
{
  public class BankAccountModel
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string AccountNo { get; set; }
    public decimal Balance { get; set; }
    public string UserReference { get; set; }

    public static BankAccountModel FromDomain(BankAccount account)
    {
            return new BankAccountModel
            {
                AccountNo = account.AccountNo.ToString(),
                Balance = account.Balance,
                UserReference = account.CustomerRef
            };
    }

    public BankAccount ToDomain()
    {
            return new BankAccount(this.Balance, this.UserReference);
    }
  }
}