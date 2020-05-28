using Prism.Mvvm;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Frontend.ApiHelper;
using WPF_Frontend.Event_Helper;
using WPF_Frontend.Models.Family;
using WPF_Frontend.ViewModels.Dashboard;
using WPF_Frontend.ViewModels.Helpers;

namespace WPF_Frontend.ViewModels.Family
{
    public class AddMemberViewModel : BindableBase, IPageViewModel
    {
        private FamilyMemberModel _member;
        private UserModel _user;
        private APIHelper _api;
        private bool _ischecked;
        private ICommand _addCommand;
        private ICommand _cancelCommand;


        #region Private Members
        public CheckDuplicate checkDuplicate { get; }
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(async param => await this.SignUp(),
                        null);
                }
                return _addCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(param => this.ClosePopup(),
                        null);
                }
                return _cancelCommand;
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

        public FamilyMemberModel Member
        {
            get
            {
                return _member;
            }
            set
            {
                _member = value;
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
        #endregion

        public AddMemberViewModel()
        {
            Member = new FamilyMemberModel();
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
                User.FamilyId = DataStore.FamilyId;
                Member = Member.ToFamilyMember(User, Member.Relationship);
                await API.RegisterUser(User);
                await API.AddMember(Member);
                ClosePopup();
            }
            else
                MessageBox.Show("Phone Number already taken");
        }

        private void ClosePopup()
        {
            DashboardViewModel.addwindow.Close();
            DashboardViewModel.addwindow = null;
        }
    }
}
