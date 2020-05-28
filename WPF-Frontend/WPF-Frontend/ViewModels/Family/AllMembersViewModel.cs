using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using WPF_Frontend.ApiHelper;
using WPF_Frontend.Event_Helper;
using WPF_Frontend.Models.Family;

namespace WPF_Frontend.ViewModels.Family
{
    public class AllMembersViewModel : BindableBase, IPageViewModel
    {
        private APIHelper _api;
        private IEnumerable<FamilyMemberModel> _familyMembersList;

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
            _api = new APIHelper();
            FamilyMembersList = _api.GetAllFamilyMembers(DataStore.FamilyId);
        }
    }
}
