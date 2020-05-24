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
    public class SignUpViewModel :  BindableBase

    {
        #region Private Members

        private string _title;
        private UserModel _user;
        private APIHelper _api;
        private bool _ischecked;
        private ICommand _SubmitCommand;

        #endregion
        #region Public Properties

        public CheckDuplicate checkDuplicate { get; }
        public string Title
        {
            get { return _title; }
            set {
                SetProperty(ref _title, value);
            }
        }

        public bool IsChecked
        {
            get { return _ischecked; }
            set
            {
                SetProperty(ref _ischecked, value);
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

        public APIHelper API
        {
            get
            {
                return _api;
            }
            set
            {
                _api = value;
                RaisePropertyChanged("API");
            }
        }

        public ICommand SubmitCommand
        {
            get
            {
                if (_SubmitCommand == null)
                {
                    _SubmitCommand = new RelayCommand(async param => await this.SignUp(),
                        null);
                }
                return _SubmitCommand;
            }
        }

        #endregion

        public SignUpViewModel()
        {
            checkDuplicate = new CheckDuplicate();
            Title = "Testing Prism";
            User = new UserModel();
            API = new APIHelper();
        }

        private async Task SignUp()
        {
            if (IsChecked)
                User.Role = "Admin";
            else
                User.Role = "Ordinary";

            if (!checkDuplicate.CheckDuplicateUser(User.Phone))
            {
                string hashed_password = SecurePasswordHasherHelper.Hash(User.Password);
                User.Password = hashed_password;
                await API.RegisterUser(User);
            }
            else
                MessageBox.Show("Phone Number already taken");
        }
    }
}
