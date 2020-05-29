using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_Frontend.Models.Transactions
{
    public class TransactionsModel
    {
        public string Id { get; set; }
        public string AccountNo { get; set; }
        public decimal Deposit
        { get; set; }
        public decimal Withdrawal
        { get; set; }
        public string Reference
        { get; set; }
        public DateTime Date
        { get; internal set; }

        public static TransactionsModel Transaction(TransactionsModel trans)
        {
            return new TransactionsModel
            {
                AccountNo = trans.AccountNo,
                Deposit = trans.Deposit,
                Date = trans.Date,
                Withdrawal = trans.Withdrawal,
                Reference = trans.Reference,
                Id = trans.Id
            };
        }
    }
}
