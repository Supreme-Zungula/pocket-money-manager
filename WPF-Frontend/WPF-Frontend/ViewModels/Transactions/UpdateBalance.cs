using System;
using System.Collections.Generic;
using System.Text;
using WPF_Frontend.Data;
using WPF_Frontend.Event_Helper;
using WPF_Frontend.Models.BankAccount;

namespace WPF_Frontend.ViewModels.Transactions
{
    public class UpdateBalance
    {
        public BankAccountModel GetObject(string reference, decimal balance)
        {
            BankAccountModel bankAccount = new BankAccountModel();
            IEnumerable<BankAccountModel> Accounts = StoredAccounts.BankAccounts;
            foreach (BankAccountModel acc in Accounts)
            {
                if (acc.CustomerRef == reference)
                {
                    bankAccount.Id = acc.Id;
                    bankAccount.CustomerRef = acc.CustomerRef;
                    bankAccount.Balance = acc.Balance + balance;
                    bankAccount.AccountNo = acc.AccountNo;

                    return bankAccount;
                }
            }
            return bankAccount;
        }

        public BankAccountModel GetObjectSender(string reference, decimal balance)
        {
            BankAccountModel bankAccount = new BankAccountModel();
            IEnumerable<BankAccountModel> Accounts = StoredAccounts.BankAccounts;
            foreach (BankAccountModel acc in Accounts)
            {
                if (acc.CustomerRef == reference)
                {
                    bankAccount.Id = acc.Id;
                    bankAccount.CustomerRef = acc.CustomerRef;
                    bankAccount.Balance = acc.Balance - balance;
                    bankAccount.AccountNo = acc.AccountNo;

                    return bankAccount;
                }
            }
            return bankAccount;
        }

        public BankAccountModel Deposit(string reference, decimal balance)
        {
            BankAccountModel bankAccount = new BankAccountModel();
            IEnumerable<BankAccountModel> Accounts = StoredAccounts.BankAccounts;
            foreach (BankAccountModel acc in Accounts)
            {
                if (acc.CustomerRef == reference)
                {
                    bankAccount.Id = acc.Id;
                    bankAccount.CustomerRef = acc.CustomerRef;
                    bankAccount.Balance = acc.Balance + balance;
                    bankAccount.AccountNo = acc.AccountNo;

                    return bankAccount;
                }
            }
            return bankAccount;
        }

        public BankAccountModel Withdraw(string reference, decimal balance)
        {
            BankAccountModel bankAccount = new BankAccountModel();
            IEnumerable<BankAccountModel> Accounts = StoredAccounts.BankAccounts;
            foreach (BankAccountModel acc in Accounts)
            {
                if (acc.CustomerRef == reference)
                {
                    bankAccount.Id = acc.Id;
                    bankAccount.CustomerRef = acc.CustomerRef;
                    bankAccount.Balance = acc.Balance - balance;
                    bankAccount.AccountNo = acc.AccountNo;

                    return bankAccount;
                }
            }
            return bankAccount;
        }

        public void NewBalance(string reference)
        {
            BankAccountModel bankAccount = new BankAccountModel();
            IEnumerable<BankAccountModel> Accounts = StoredAccounts.BankAccounts;
            foreach (BankAccountModel acc in Accounts)
            {
                if (acc.CustomerRef == reference)
                    DataStore.Balance = acc.Balance;
            }
        }
    }
}
