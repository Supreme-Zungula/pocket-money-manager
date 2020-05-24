using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Frontend.ApiHelper;
using WPF_Frontend.Models.User;
using WPF_Frontend.ViewModels.Helpers;

namespace WPF_Frontend.ViewModels.User
{
    public class LoginViewModel : BindableBase
    {
        private UserModel _user;
        private ICommand _logincommand;
        private APIHelper _api;
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
        }

        public void Login()
        {
            UserModel result = _api.LoginUser(User.Phone);
            if(SecurePasswordHasherHelper.Verify(User.Password, result.Password))
            {
                //go to dashboard
                MessageBox.Show("Go to dashboard");
            }
            else
            {
                //login failed
                MessageBox.Show("login failed");
            }
        }
    }
}
