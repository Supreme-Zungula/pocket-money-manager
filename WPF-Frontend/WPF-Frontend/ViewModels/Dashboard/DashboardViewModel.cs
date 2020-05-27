using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WPF_Frontend.Event_Helper;
using WPF_Frontend.ViewModels.Family;
using WPF_Frontend.ViewModels.Transactions;

namespace WPF_Frontend.ViewModels.Dashboard
{
    public class DashboardViewModel : BindableBase, IPageViewModel
    {
        private List<IPageViewModel> _pageViewModels;
        private IPageViewModel _currentDashViewModel;
        private string _familyname;

        public string Familyname 
        { 
            get => _familyname; 
            set => _familyname = value;
        }

        public IPageViewModel CurrentDashViewModel
        {
            get
            {
                return _currentDashViewModel;
            }
            set
            {
                _currentDashViewModel = value;
                RaisePropertyChanged("CurrentDashViewModel");
            }
        }

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        /*private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentDashViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        private void AddMember(object obj)
        {
            ChangeViewModel(PageViewModels[3]);
        }

        private void AllMembers(object obj)
        {
            ChangeViewModel(PageViewModels[4]);
        }

        private void Transactions(object obj)
        {
            ChangeViewModel(PageViewModels[5]);
        }*/

        public DashboardViewModel()
        {
            Familyname = DataStore.LastName;
            PageViewModels.Add(new AddMemberViewModel());
            PageViewModels.Add(new AllMembersViewModel());
            PageViewModels.Add(new TransactionsViewModel());

            CurrentDashViewModel = PageViewModels[1];
        }
    }
}
