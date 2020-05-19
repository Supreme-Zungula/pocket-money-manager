using Domain.DefinitionObjects;
using Domain.Services;
using NUnit.Framework;
using System;
using MongoDB.Bson;

namespace TransactionUnitTests
{
    public class Tests
    {
        private TransactionService _transactionService;

        [SetUp]
        public void Setup()
        {
            _transactionService = new TransactionService();
        }

        [Test, Order(1)]
        public void addTransactionTest()
        {
            Transaction transaction = new Transaction
            {
                Deposit = 123,
                Withdrawal = 0,
                Reference = "account deposit",
                Date = DateTime.Now
            };

            var trans = _transactionService.AddTransaction(transaction);
            Assert.IsInstanceOf(typeof(Transaction), trans);
        }

        [Test, Order(2)]
        public void getAllTransactionsTest()
        {
            var transactions = _transactionService.GetTransactions();
            Assert.IsNotNull(transactions);
        }

        // Get transaction by ID that exists.
        [Test, Order(3)]
        public void getTransactionByIdValidTest()
        {
            
            Transaction transaction = new Transaction
            {
                Deposit = 123,
                Withdrawal = 0,
                Reference = "account deposit",
                Date = DateTime.Now
            };
            
            var addTrans = _transactionService.AddTransaction(transaction);
            var returnedTrans = _transactionService.GetTransaction(addTrans.Id);
            Assert.AreEqual(addTrans.Id, returnedTrans.Id);
        }

        // Test getting Transaction by ID that does not exist.
        [Test, Order(4)]
        public void getTransactionByIdInvalidTest()
        {
            var id = ObjectId.GenerateNewId();
            var trans = _transactionService.GetTransaction(id.ToString());
            Assert.IsNull(trans);
        }

        [Test, Order(5)]
        public void updateTransactionValidTest()
        {
            Transaction transaction = new Transaction
            {
                Deposit = 1200,
                Withdrawal = 0,
                Reference = "Cash deposit",
                Date = DateTime.Now
            };

            var addTrans = _transactionService.AddTransaction(transaction);
            transaction.Deposit = 0;
            transaction.Withdrawal = -1200;
            transaction.Reference = "Online purchase";
            transaction.Date = DateTime.Now;

            _transactionService.UpdateTransaction(transaction.Id, transaction);
            var updateTrans = _transactionService.GetTransaction(transaction.Id);
            Assert.AreEqual(addTrans.Withdrawal, updateTrans.Withdrawal);
            Assert.AreEqual(transaction.Reference, updateTrans.Reference);
        }
        
        [Test, Order(5)]
        public void updateTransactionInvalidTest()
        {
            Transaction transaction = new Transaction
            {
                Deposit = 1200,
                Withdrawal = 0,
                Reference = "Cash deposit",
                Date = DateTime.Now
            };

            var addTrans = _transactionService.AddTransaction(transaction);
            transaction.Deposit = 0;
            transaction.Withdrawal = -1200;
            transaction.Reference = "Online purchase";
            transaction.Date = DateTime.Now;
            var invalidId = ObjectId.GenerateNewId().ToString();

            _transactionService.UpdateTransaction(invalidId, transaction);
            var updateTrans = _transactionService.GetTransaction(invalidId);
            Assert.IsNull(updateTrans);
        }

        [Test, Order(6)]
        public void deleteAccountByValidIdTest()
        {
            Transaction transaction = new Transaction
            {
                Deposit = 1200,
                Withdrawal = 0,
                Reference = "Cash deposit",
                Date = DateTime.Now
            };

            var addTrans = _transactionService.AddTransaction(transaction);
            _transactionService.RemoveTransaction(transaction.Id);
            var deletedAccount = _transactionService.GetTransaction(transaction.Id);

            Assert.IsNull(deletedAccount);
        }
        [Test, Order(7)]
        public void deleteAccountByInvalidIdTest()
        {
            Transaction transaction = new Transaction
            {
                Deposit = 1200,
                Withdrawal = 0,
                Reference = "Cash deposit",
                Date = DateTime.Now
            };

            var addTrans = _transactionService.AddTransaction(transaction);
            _transactionService.RemoveTransaction(ObjectId.GenerateNewId().ToString());
            var deletedAccount = _transactionService.GetTransaction(transaction.Id);

            Assert.IsNotNull(deletedAccount);
            Assert.IsInstanceOf(typeof(Transaction), deletedAccount);
        }
    }
    
}