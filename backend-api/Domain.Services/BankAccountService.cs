using System;
using System.Collections.Generic;
using System.Text;
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

        public IEnumerable<BankAccountData> GetAccounts()
        {
            List<BankAccountData> bankAccounts = new List<BankAccountData>();

            var accounts = _repository.GetAccounts();
            foreach (var account in accounts)
            {
                bankAccounts.Add(
                    new BankAccountData
                    {
                        AccountNo = account.AccountNo.ToString(),
                        Balance = account.Balance,
                        UserReference = account.CustomerRef
                    }
                );
            }
            return bankAccounts;
        }

        public BankAccount GetAccount(Guid accountNo)
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
    }
}
