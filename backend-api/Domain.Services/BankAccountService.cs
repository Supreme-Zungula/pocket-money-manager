using System.Collections.Generic;
using Data;
using Repositories;
using Domain.DefinitionObjects;

namespace Domain.Services
{
    public class BankAccountService
    {
        private readonly BankAccountRepository _repository;
        private readonly TransactionService _transactionService;

        public BankAccountService() 
        {
            _repository = new BankAccountRepository();
            _transactionService = new TransactionService();
        }

        public IEnumerable<BankAccount> GetAccounts()
        {
            var accounts = _repository.GetAccounts();

            return accounts;
        }

        public BankAccount GetAccount(string accountNo)
        {
            return _repository.GetAccount(accountNo);
        }

        public BankAccount CreateAccount(BankAccount account)
        {
            IEnumerable<Transaction> transactions = account.GetTransactions();
            foreach(Transaction tran in transactions)
            {
                _transactionService.AddTransaction(tran);
            }

            return _repository.AddAccount(account);
        }

        public BankAccount UpdateAccount(string id, BankAccount account)
        {
            return  _repository.UpdateAccount(id, account);
        }

        public BankAccount DeleteAccount(string accountNo)
        {
            return _repository.DeleteAccount(accountNo);
        }

    }
}
