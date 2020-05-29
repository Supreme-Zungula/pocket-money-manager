using Prism.Mvvm;
using System;
using System.Windows;
using System.Windows.Input;
using WPF_Frontend.ApiHelper;
using WPF_Frontend.Data;
using WPF_Frontend.Event_Helper;
using WPF_Frontend.Models.Family;
using WPF_Frontend.ViewModels.Application;
using WPF_Frontend.ViewModels.Helpers;

namespace WPF_Frontend.ViewModels.User
{
    public class LoginViewModel : BindableBase, IPageViewModel
    {
        #region Private Memmbers

        private SetAccounts setAccounts;
        private UserModel _user;
        private ICommand _logincommand;
        private ICommand _registercommand;
        private APIHelper _api;

        #endregion

        public ICommand RegisterCommand
        {
            get
            {
                return _registercommand ?? (_registercommand = new RelayCommand(x =>
                {
                    Mediator.Notify(ApplicationPage.Register.ToString(), "");
                }));
            }
        }

        public UserModel User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                RaisePropertyChanged("User");
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                if (_logincommand == null)
                {
                    _logincommand = new RelayCommand(param => this.Login(),
                        null);
                }
                return _logincommand;
            }
        }

        public LoginViewModel()
        {
            _api = new APIHelper();
            User = new UserModel();
            setAccounts = new SetAccounts();
        }

        private void Login()
        {
            try
            {
                UserModel result = _api.LoginUser(User.Phone);
                if (result == null)
                    throw new Exception("User Not Found");
                else if (User.Password == result.Password)//SecurePasswordHasherHelper.Verify(User.Password, result.Password))
                {
                    //set session data
                    //go to dashboard
                    _ = new AllBankAccounts();
                    _ = new AllTransactions();
                    result = setAccounts.SetBankAccounts(result);
                    _ = new CreateData(result);
                    Mediator.Notify(ApplicationPage.Dashboard.ToString(), "");
                }
                else
                {
                    //login failed
                    MessageBox.Show("login failed");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("User Not Found");
            }
        }
    }
}
