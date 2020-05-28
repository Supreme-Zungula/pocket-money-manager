using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WPF_Frontend.Event_Helper;
using WPF_Frontend.ViewModels.Base;
using WPF_Frontend.ViewModels.Dashboard;
using WPF_Frontend.ViewModels.Family;
using WPF_Frontend.ViewModels.Helpers;
using WPF_Frontend.ViewModels.Transactions;
using WPF_Frontend.ViewModels.User;

namespace WPF_Frontend.ViewModels.Application
{
    public class ApplicationViewModel : BindableBase
    {
        #region Private members
        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels; 
        #endregion

        #region Public properties
        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                _currentPageViewModel = value;
                RaisePropertyChanged("CurrentPageViewModel");
            }
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        } 
        #endregion

        #region Private change view methods
        private void LoginPage(object obj)
        {
            ChangeViewModel(PageViewModels[0]);
        }

        private void SignUpPage(object obj)
        {
            ChangeViewModel(PageViewModels[1]);
        }

        private void Dashboard(object obj)
        {
            ChangeViewModel(PageViewModels[2]);
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
        }

        private void EditMember(object obj)
        {
            ChangeViewModel(PageViewModels[6]);
        }
        #endregion

        public ApplicationViewModel()
        {
            // Add available pages and set page
            PageViewModels.Add(new LoginViewModel());
            PageViewModels.Add(new SignUpViewModel());
            PageViewModels.Add(new DashboardViewModel());
            PageViewModels.Add(new AddMemberViewModel());
            PageViewModels.Add(new AllMembersViewModel());
            PageViewModels.Add(new TransactionsViewModel());
            PageViewModels.Add(new AddMemberViewModel());

            CurrentPageViewModel = PageViewModels[0];

            Mediator.Subscribe(ApplicationPage.Login.ToString(), LoginPage);
            Mediator.Subscribe(ApplicationPage.Register.ToString(), SignUpPage);
            Mediator.Subscribe(ApplicationPage.Dashboard.ToString(), Dashboard);
            Mediator.Subscribe(ApplicationPage.AddMember.ToString(), AddMember);
            Mediator.Subscribe(ApplicationPage.AllMembers.ToString(), AllMembers);
            Mediator.Subscribe(ApplicationPage.Transactions.ToString(), Transactions);
            Mediator.Subscribe(ApplicationPage.EditMember.ToString(), EditMember);
        }
    }
}
