using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using WPF_Frontend.ApiHelper;
using WPF_Frontend.Models.Transactions;

namespace WPF_Frontend.Data
{
    public class AllTransactions : BindableBase
    {
        private IEnumerable<TransactionsModel> _transaction;
        private readonly APIHelper ApiResult;

        private IEnumerable<TransactionsModel> TransactionsList
        {
            get { return _transaction; }
            set
            {
                SetProperty(ref _transaction, value);
            }
        }

        public AllTransactions()
        {
            ApiResult = new APIHelper();
            TransactionsList = ApiResult.GetAllTransactions();
            StoredTransactions.Transactions = TransactionsList;
        }
    }
}
