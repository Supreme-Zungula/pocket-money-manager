using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WPF_Frontend.Event_Helper;
using WPF_Frontend.Models.Transactions;

namespace WPF_Frontend.ViewModels.Transactions
{
    public class InitialiseTransaction
    {
        public async Task<TransactionsModel> InitiateTransaction(decimal value, string accNo)
        {
            TransactionsModel transaction = new TransactionsModel()
            {
                AccountNo = accNo,
                Reference = $"R {value} Payment received from {DataStore.FirstName}.",
                Withdrawal = 0.0M,
                Deposit = value,
                Date = DateTime.Now
            };
            await Task.Delay(2);
            return transaction;
        }

        public async Task<TransactionsModel> InitiateFromTransaction(decimal value, string name, string accNo)
        {
            TransactionsModel transaction = new TransactionsModel()
            {
                AccountNo = DataStore.AccountNo,
                Reference = $"Transfered R {value} to {name}, Account no: {accNo}.",
                Withdrawal = value,
                Deposit = 0.0M,
                Date = DateTime.Now
            };
            await Task.Delay(2);
            return transaction;
        }

        public async Task<TransactionsModel> InitialiseDeposit(decimal value)
        {
            TransactionsModel transaction = new TransactionsModel()
            {
                AccountNo = DataStore.AccountNo,
                Reference = $"Payment: R {value} has been deposited to your account",
                Withdrawal = 0.0M,
                Deposit = value,
                Date = DateTime.Now
            };
            await Task.Delay(2);
            return transaction;
        }

        public async Task<TransactionsModel> InitialiseWithdrawal(decimal value)
        {
            TransactionsModel transaction = new TransactionsModel()
            {
                AccountNo = DataStore.AccountNo,
                Reference = $"Withdrawl: R {value} has been withdrawn from your account",
                Withdrawal = value,
                Deposit = 0.0M,
                Date = DateTime.Now
            };
            await Task.Delay(2);
            return transaction;
        }
    }
}
