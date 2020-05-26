using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WPF_Frontend.Event_Helper;
using WPF_Frontend.ViewModels.Base;
using WPF_Frontend.ViewModels.Dashboard;
using WPF_Frontend.ViewModels.Helpers;
using WPF_Frontend.ViewModels.User;

namespace WPF_Frontend.ViewModels.Application
{
    public class ApplicationViewModel : BindableBase
    {
        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

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

        public ApplicationViewModel()
        {
            // Add available pages and set page
            PageViewModels.Add(new LoginViewModel());
            PageViewModels.Add(new SignUpViewModel());
            PageViewModels.Add(new DashboardViewModel());

            CurrentPageViewModel = PageViewModels[0];

            Mediator.Subscribe(ApplicationPage.Login.ToString(), LoginPage);
            Mediator.Subscribe(ApplicationPage.Register.ToString(), SignUpPage);
            Mediator.Subscribe(ApplicationPage.Dashboard.ToString(), Dashboard);
        }

        /*private BindableBase _currentviewmodel;

        public BindableBase CurrentViewModel
        {
            get 
            {
                if (_currentviewmodel == null)
                    _currentviewmodel = new LoginViewModel();
                return _currentviewmodel; 
            }
            set
            {
                _currentviewmodel = value;
                RaisePropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        public ApplicationViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }
        /*public void GoToPage(ApplicationPage page, BaseViewModel viewModel = null)
        {
            _ = UpdateViewCommand;
            //UpdateViewCommand = new UpdateViewCommand(this);
            // Set the view model
           // CurrentPageViewModel = viewModel;

            // See if page has changed
           *//* var different = CurrentPage != page;

            // Set the current page
            CurrentPage = page;

            if (!different)
                RaisePropertyChanged(nameof(CurrentPage));*//*
        }*/
    }
}
