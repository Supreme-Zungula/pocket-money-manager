using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;
using WPF_Frontend.ApiHelper;
using WPF_Frontend.Data;
using WPF_Frontend.Event_Helper;
using WPF_Frontend.Models.Transactions;
using WPF_Frontend.ViewModels.Helpers;

namespace WPF_Frontend.ViewModels.Transactions
{
    public class TransactionsViewModel : BindableBase, IPageViewModel
    {
        
        private APIHelper _api;
        private IEnumerable<TransactionsModel> _transactionsList;
        private ICommand _removeCommand;
        private TransactionsModel Transaction;

        public ICommand RemoveCommand
        {
            get
            {
                if (_removeCommand == null)
                {
                    _removeCommand = new RelayCommand(async param => await this.Remove(param),
                        null);
                }
                return _removeCommand;
            }
        }

        public IEnumerable<TransactionsModel> TransactionsList
        {
            get { return _transactionsList; }
            set
            {
                _transactionsList = value;
                RaisePropertyChanged("TransactionsList");
            }
        }

        public TransactionsViewModel()
        {
            Transaction = new TransactionsModel();
            _api = new APIHelper();
            TransactionsList = _api.GetTransactionsById(DataStore.AccountNo);
        }

        public async Task Remove(object param)
        {
            Transaction = TransactionsModel.Transaction(param as TransactionsModel);
            await _api.DeleteTransaction(Transaction);

            _ = new AllBankAccounts();
            _ = new AllTransactions();
            TransactionsList = _api.GetTransactionsById(DataStore.AccountNo);
        }
    }
}
