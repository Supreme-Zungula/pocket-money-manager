using Data;
using Domain.DefinitionObjects;
using Domain.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;
using System.Collections;

namespace BankAccountUnitTests
{
    public class Tests
    {
        private BankAccountService _bankAccountService;
        [SetUp]
        public void Setup()
        {
            _bankAccountService = new BankAccountService();
        }

        [Test, Order(1)]
        public void createAccountTest()
        {
            var account1 = _bankAccountService.CreateAccount(new BankAccount(100, "user1"));
            var account2 =  _bankAccountService.CreateAccount(new BankAccount(1000, "user2"));

            Assert.IsNotNull(account1);
            Assert.IsNotNull(account2);
            Assert.IsInstanceOf(typeof(BankAccount), account1);
            Assert.IsInstanceOf(typeof(BankAccount), account2);           
        }

        [Test, Order(2)]
        public void getAllAccountsTest()
        {
            var accounts = _bankAccountService.GetAccounts();
            Assert.IsNotNull(accounts);
            Assert.IsInstanceOf(typeof (IEnumerable), accounts);
        }

        
        [Test, Order(3)]
        public void getAccountByIdValidTest()
        {
            BankAccount account = _bankAccountService.CreateAccount(new BankAccount(100, "TestUser"));
            var returnedAccount = _bankAccountService.GetAccount(account.Id);

            Assert.IsNotNull(returnedAccount);
            Assert.IsInstanceOf(typeof(BankAccount), returnedAccount);
            Assert.AreEqual(account.Id, returnedAccount.Id);
        }

        [Test, Order(4)]
        public void getAccountByIdInvalidTest()
        {
            var account = _bankAccountService.GetAccount(ObjectId.GenerateNewId().ToString());
            Assert.IsNull(account);
        }

        [Test, Order(5)]
        public void updateAccountValidIdTest()
        {
            BankAccount account = _bankAccountService.CreateAccount(new BankAccount(100, "TestRef"));
            account.Balance = 2000;
            account.CustomerRef = "updatedRef";
            var updatedAccount = _bankAccountService.UpdateAccount(account.Id, account);

            Assert.IsNotNull(updatedAccount);
            Assert.IsInstanceOf(typeof(BankAccount), updatedAccount);
            Assert.AreEqual(2000, updatedAccount.Balance);
            Assert.AreEqual("updatedRef", updatedAccount.CustomerRef);
        }

        [Test, Order(6)]
        public void updateAccountInvalidIdTest()
        {
            BankAccount account = _bankAccountService.CreateAccount(new BankAccount(100, "TestRef"));
            account.Balance = 2000;
            account.CustomerRef = "updatedRef";
            var updatedAccount = _bankAccountService.UpdateAccount(ObjectId.GenerateNewId().ToString(), account);

            Assert.IsNull(updatedAccount);
        }

        [Test, Order(7)]
        public void deleteAccountByValidIdTest()
        {
            BankAccount account = _bankAccountService.CreateAccount(new BankAccount(100, "TestRef"));
            var addedAccount = _bankAccountService.GetAccount(account.Id);
            
            Assert.IsNotNull(addedAccount);
            Assert.AreEqual(account.Id, addedAccount.Id);

            _bankAccountService.DeleteAccount(account.Id);
            var deletedAccount = _bankAccountService.GetAccount(account.Id);

            Assert.IsNull(deletedAccount);
        }

        [Test, Order(8)]
        public void deleteAccountByInvalidIdTest()
        {
            BankAccount account = _bankAccountService.CreateAccount(new BankAccount(100, "TestRef"));
            var addedAccount = _bankAccountService.GetAccount(account.Id);

            Assert.IsNotNull(addedAccount);
            Assert.AreEqual(account.Id, addedAccount.Id);

            _bankAccountService.DeleteAccount(ObjectId.GenerateNewId().ToString());
            var returnedAccount = _bankAccountService.GetAccount(account.Id);

            Assert.IsNotNull(returnedAccount);
            Assert.AreEqual(account.Id, returnedAccount.Id);
        }
    }
}