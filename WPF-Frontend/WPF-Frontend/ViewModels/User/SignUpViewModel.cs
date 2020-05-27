using Prism.Mvvm;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Frontend.ApiHelper;
using WPF_Frontend.Event_Helper;
using WPF_Frontend.Models.BankAccount;
using WPF_Frontend.Models.Family;
using WPF_Frontend.ViewModels.Application;
using WPF_Frontend.ViewModels.Helpers;

namespace WPF_Frontend.ViewModels.User
{
    public class SignUpViewModel :  BindableBase, IPageViewModel

    {
        #region Private Members

        private UserModel _user;
        private APIHelper _api;
        private bool _ischecked;
        private ICommand _SubmitCommand;
        private ICommand _logincomand;

        #endregion
        #region Public Properties

        public CheckDuplicate checkDuplicate { get; }

        public ICommand LoginCommand
        {
            get
            {
                return _logincomand ?? (_logincomand = new RelayCommand(x =>
                {
                    Mediator.Notify(ApplicationPage.Login.ToString(), "");
                }));
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
                /*string hashed_password = SecurePasswordHasherHelper.Hash(User.Password);
                User.Password = hashed_password;*/
                await API.RegisterUser(User);
                UserModel user = API.GetUserByPhone(User.Phone);
                if (user != null)
                    await SetBankAccount(user);
                Mediator.Notify(ApplicationPage.Login.ToString(), "");
            }
            else
                MessageBox.Show("Phone Number already taken");
        }

        private async Task SetBankAccount(UserModel user)
        {
            BankAccountModel details = new BankAccountModel()
            {
                Balance = 0.0M,
                CustomerRef = user.Id
            };
            await API.SetBankAccount(details);
        }
    }
}
