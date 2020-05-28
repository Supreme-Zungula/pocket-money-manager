using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Frontend.ApiHelper;
using WPF_Frontend.Event_Helper;
using WPF_Frontend.Models.Family;
using WPF_Frontend.ViewModels.Helpers;

namespace WPF_Frontend.ViewModels.Family
{
    class EditMemberViewModel : BindableBase, IPageViewModel
    {
        private APIHelper _api;
        private IEnumerable<FamilyMemberModel> _membersList;
        private bool _popupOpen;
        private ICommand _editClicked;
        private ICommand _update;
        private ICommand _cancel;

        private FamilyMemberModel _member;

        public FamilyMemberModel Member
        {
            get => _member;
            set
            {
                _member = value;
                RaisePropertyChanged(nameof(Member));
            }
        }

        public bool PopupOpen
        {
            get { return _popupOpen; }
            set
            {
                SetProperty(ref _popupOpen, value);
                RaisePropertyChanged(nameof(PopupOpen));
            }
        }

        public ICommand EditClicked
        {
            get
            {
                if (_editClicked == null)
                {
                    _editClicked = new RelayCommand(param => this.PopUp(param),
                        null);
                }
                return _editClicked;
            }
        }

        public ICommand UpdateCommand
        {
            get
            {
                if (_update == null)
                {
                    _update = new RelayCommand(async param => await this.Update(),
                        null);
                }
                return _update;
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

        public IEnumerable<FamilyMemberModel> MembersList
        {
            get 
            {
                if(_membersList == null)
                    _membersList = _api.GetAllFamilyMembers(DataStore.FamilyId);
               return _membersList;
            }
            set
            {
                _membersList = value;
                RaisePropertyChanged("MembersList");
            }
        }

        public EditMemberViewModel()
        {
            Member = new FamilyMemberModel();
            _api = new APIHelper();
            MembersList = _api.GetAllFamilyMembers(DataStore.FamilyId);
        }

        private void PopUp(object param)
        {
            Member = Member.ToFamilyMember(param as FamilyMemberModel);
            PopupOpen = true;
        }

        private async Task Update()
        {
            Console.WriteLine(Member);
            await _api.UpdateMember(Member);

        }

        private void Cancel()
        {
            PopupOpen = false;
        }
    }
}
