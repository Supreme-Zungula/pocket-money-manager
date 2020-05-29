using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Frontend.ApiHelper;
using WPF_Frontend.Data;
using WPF_Frontend.Event_Helper;
using WPF_Frontend.Models.BankAccount;
using WPF_Frontend.Models.Transactions;
using WPF_Frontend.ViewModels.Helpers;
using WPF_Frontend.ViewModels.Transactions;

namespace WPF_Frontend.ViewModels.User
{
    public class ProfileViewModel : BindableBase, IPageViewModel
    {
        private APIHelper Api;
        private UpdateBalance updateBalance;
        private InitialiseTransaction initTransaction;
        private string _visibilityState;
        private string _depositvisibilityState;
        private string _amount;
        private decimal _balance;
        private ICommand _withdraw;
        private ICommand _deposit;
        private ICommand _depositcommand;
        private ICommand _withdrawcommand;


        public decimal Balance 
        {
            get => _balance;
            set
            {
                _balance = value;
                RaisePropertyChanged(nameof(Balance));
            }
        }
        public string VisibilityState
        {
            get => _visibilityState;
            set
            {
                _visibilityState = value;
                RaisePropertyChanged(nameof(VisibilityState));
            }
        }

        public string DepositVisibilityState
        {
            get => _depositvisibilityState;
            set
            {
                _depositvisibilityState = value;
                RaisePropertyChanged(nameof(DepositVisibilityState));
            }
        }

        public ICommand Withdraw
        {
            get
            {
                if (_withdraw == null)
                {
                    _withdraw = new RelayCommand(param => this.WithdrawCard(),
                        null);
                }
                return _withdraw;
            }
        }

        public ICommand DepositCommand
        {
            get
            {
                if (_depositcommand == null)
                {
                    _depositcommand = new RelayCommand(async param => await this.DoDeposit(param),
                        null);
                }
                return _depositcommand;
            }
        }

        public ICommand WithdrawCommand
        {
            get
            {
                if (_withdrawcommand == null)
                {
                    _withdrawcommand = new RelayCommand(async param => await this.DoWithdrawal(param),
                        null);
                }
                return _withdrawcommand;
            }
        }

        public ICommand Deposit
        {
            get
            {
                if (_deposit == null)
                {
                    _deposit = new RelayCommand(param => this.DepositeCard(),
                        null);
                }
                return _deposit;
            }
        }

        public string Amount
        {
            get => _amount;
            set
            {
                SetProperty(ref _amount, value);
                RaisePropertyChanged(nameof(Amount));
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountNo { get; set; }
        
        public ProfileViewModel()
        {
            Api = new APIHelper();
            updateBalance = new UpdateBalance();
            initTransaction = new InitialiseTransaction();
            VisibilityState = "Hidden";
            DepositVisibilityState = "Hidden";
            FirstName = DataStore.FirstName;
            LastName = DataStore.LastName;
            AccountNo = DataStore.AccountNo;
            Balance = DataStore.Balance;
        }

        private void WithdrawCard()
        {
            if (VisibilityState == "Hidden")
            {
                if(DepositVisibilityState == "Visible")
                    DepositVisibilityState = "Hidden";
                VisibilityState = "Visible";
            }
            else if (VisibilityState == "Visible")
                VisibilityState = "Hidden";
        }

        private void DepositeCard()
        {
            if (DepositVisibilityState == "Visible")
                DepositVisibilityState = "Hidden";
            else if (DepositVisibilityState == "Hidden")
            {
                if(VisibilityState == "Visible")
                    VisibilityState = "Hidden";
                DepositVisibilityState = "Visible";
            }
        }

        private async Task DoDeposit(object param)
        {
            decimal.TryParse(Amount, out decimal value);
            TransactionsModel deposit = await initTransaction.InitialiseDeposit(value);
            BankAccountModel bankAccount = updateBalance.Deposit(DataStore.ID, value);

            await Api.SendTransaction(deposit);
            await Api.UpdateAccount(bankAccount);

            _ = new AllBankAccounts();
            _ = new AllTransactions();
            updateBalance.NewBalance(DataStore.ID);
            Balance = DataStore.Balance;
        }

        private async Task DoWithdrawal(object param)
        {
            decimal.TryParse(Amount, out decimal value);
            if(Decimal.Compare(DataStore.Balance, value) > 0)
            {
                BankAccountModel bankAccount = updateBalance.Withdraw(DataStore.ID, value);
                TransactionsModel deposit = await initTransaction.InitialiseWithdrawal(value);

                await Api.SendTransaction(deposit);
                await Api.UpdateAccount(bankAccount);

                _ = new AllBankAccounts();
                _ = new AllTransactions();
                updateBalance.NewBalance(DataStore.ID);
                Balance = DataStore.Balance;
            }
            else
                MessageBox.Show("You have insufficient funds to perform this transaction");
        }
    }
}
