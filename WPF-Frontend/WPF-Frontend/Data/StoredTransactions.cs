using System;
using System.Collections.Generic;
using System.Text;
using WPF_Frontend.Models.Transactions;

namespace WPF_Frontend.Data
{
    public static class StoredTransactions
    {
        private static IEnumerable<TransactionsModel> _transactions;

        public static IEnumerable<TransactionsModel> Transactions
        {
            get => _transactions;
            set
            {
                _transactions = value;
            }
        }
    }
}
