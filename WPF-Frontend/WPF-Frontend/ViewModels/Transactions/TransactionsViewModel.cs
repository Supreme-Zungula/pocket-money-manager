using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using WPF_Frontend.ApiHelper;
using WPF_Frontend.Event_Helper;
using WPF_Frontend.Models.Transactions;

namespace WPF_Frontend.ViewModels.Transactions
{
    public class TransactionsViewModel : BindableBase, IPageViewModel
    {
        private APIHelper _api;
        private IEnumerable<TransactionsModel> _transactionsList;

        public IEnumerable<TransactionsModel> TransactionsList
        {
            get { return _transactionsList; }
            set
            {
                _transactionsList = value;
                RaisePropertyChanged("FamilyMembersList");
            }
        }

        public TransactionsViewModel()
        {
            _api = new APIHelper();
            TransactionsList = _api.GetTransactionsById(DataStore.ID);
        }
    }
}
