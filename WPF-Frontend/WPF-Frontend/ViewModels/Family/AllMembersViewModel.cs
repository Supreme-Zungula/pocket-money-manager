using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Frontend.ApiHelper;
using WPF_Frontend.Data;
using WPF_Frontend.Event_Helper;
using WPF_Frontend.Models.BankAccount;
using WPF_Frontend.Models.Family;
using WPF_Frontend.Models.Transactions;
using WPF_Frontend.ViewModels.Helpers;
using WPF_Frontend.ViewModels.Transactions;

namespace WPF_Frontend.ViewModels.Family
{
    public class AllMembersViewModel : BindableBase, IPageViewModel
    {
        private APIHelper _api;
        private IEnumerable<FamilyMemberModel> _familyMembersList;
        private FamilyMemberModel _member;
        private ICommand _topup;
        private bool _popupOpen;
        private string _amount;
        private ICommand _sendcommand;
        private ICommand _cancel;
        private InitialiseTransaction initTransanction;
        private SetAccounts NewList;
        private UpdateBalance updateBalance;
        private static string accNo { get; set; }


        public string Amount
        {
            get => _amount;
            set
            {
                SetProperty(ref _amount, value);
                RaisePropertyChanged(nameof(Amount));
            }
        }

        public FamilyMemberModel Member
        {
            get => _member;
            set
            {
                _member = value;
                RaisePropertyChanged(nameof(Member));
            }
        }

        public static string reference { get; set; }
        public static string name { get; set; }

        public bool PopupOpen
        {
            get { return _popupOpen; }
            set
            {
                SetProperty(ref _popupOpen, value);
                RaisePropertyChanged(nameof(PopupOpen));
            }
        }

        public ICommand TopUpCommand
        {
            get
            {
                if (_topup == null)
                {
                    _topup = new RelayCommand(param => this.PopUp(param),
                        null);
                }
                return _topup;
            }
        }

        public ICommand SendCommand
        {
            get
            {
                if (_sendcommand == null)
                {
                    _sendcommand = new RelayCommand(async param => await this.Send(),
                        null);
                }
                return _sendcommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (_cancel == null)
                {
                    _cancel = new RelayCommand(param => this.Cancel(),
                        null);
                }
                return _cancel;
            }
        }

        public IEnumerable<FamilyMemberModel> FamilyMembersList
        {
            get { return _familyMembersList; }
            set
            {
                _familyMembersList = value;
                RaisePropertyChanged("FamilyMembersList");
            }
        }

        public AllMembersViewModel()
        {
            Amount = "0.0";
            NewList = new SetAccounts();
            Member = new FamilyMemberModel();
            updateBalance = new UpdateBalance();
            initTransanction = new InitialiseTransaction();
            _api = new APIHelper();
            FamilyMembersList = _api.GetAllFamilyMembers(DataStore.FamilyId);
            FamilyMembersList = NewList.SetBankAccounts(FamilyMembersList);
        }

        private void PopUp(object param)
        {
            Member = Member.ToFamilyMember(param as FamilyMemberModel);
            accNo = Member.Accountno;
            reference = Member.Id;
            name = Member.FirstName;
            PopupOpen = true;
        }

        private async Task Send()
        {
            decimal.TryParse(Amount, out decimal value);
            if (Decimal.Compare(DataStore.Balance, value) > 0)
            {
                TransactionsModel Fromtransaction = await initTransanction.InitiateFromTransaction(value, name, accNo);
                TransactionsModel Totransaction = await initTransanction.InitiateTransaction(value, accNo);
                await _api.SendTransaction(Fromtransaction);
                await _api.SendTransaction(Totransaction);

                BankAccountModel acc = updateBalance.GetObject(reference, value);
                BankAccountModel sender = updateBalance.GetObjectSender(DataStore.ID, value);
                await _api.UpdateAccount(acc);
                await _api.UpdateAccount(sender);
            }
            else
                MessageBox.Show("You have insufficient funds to perform this transaction");

        }

        /*public async Task<TransactionsModel> InitiateTransaction(decimal value)
        {
            TransactionsModel transaction = new TransactionsModel()
            {
                AccountNo = accNo,
                Reference = $"R {value} Payment received from {DataStore.FirstName}.",
                Withdrawal = value,
                Deposit = 0.0M,
                Date = DateTime.Now
            };
            await Task.Delay(2);
            return transaction;
        }

        public async Task<TransactionsModel> InitiateFromTransaction(decimal value)
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
        }*/

        private void Cancel()
        {
            PopupOpen = false;
        }
    }
}
