using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                SetProperty(ref _familyMembersList, value);
                RaisePropertyChanged("FamilyMembersList");
            }
        }

        public string Test { get; set; }

        public AllMembersViewModel()
        {
            Test = "We are testing";
            _api = new APIHelper();
            FamilyMembersList = _api.GetAllFamilyMembers(0);
            FamilyMembersList.Append(new FamilyMemberModel { FirstName = "Tafadzwa", LastName = "Test", Phone = "0987654322", Relationship = "Son" });
        }
    }
}
