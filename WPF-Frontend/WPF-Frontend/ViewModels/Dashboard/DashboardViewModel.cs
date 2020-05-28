using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WPF_Frontend.Event_Helper;
using WPF_Frontend.ViewModels.Application;
using WPF_Frontend.ViewModels.Family;
using WPF_Frontend.ViewModels.Helpers;
using WPF_Frontend.ViewModels.Transactions;
using WPF_Frontend.Views.Family;




using Prism.Commands;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;

namespace WPF_Frontend.ViewModels.Dashboard
{
    public class DashboardViewModel : BindableBase, IPageViewModel
    {
        private List<IPageViewModel> _pageViewModels;
        private IPageViewModel _currentDashViewModel;
        private string _familyname;
        private ICommand _addmember;
        private ICommand _editmember;
        //private ICommand _addmember;


        public static AddMemberView addwindow = new AddMemberView();
  
        public ICommand AddMember
        {
            get
            {
                if (_addmember == null)
                {
                    _addmember = new RelayCommand(param => this.Add(),
                        null);
                }
                return _addmember;
            }
        }

        public ICommand EditMember
        {
            get
            {
                if (_editmember == null)
                {
                    _editmember = new RelayCommand(param => this.Edit(),
                        null);
                }
                return _editmember;
            }
        }

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

        public DashboardViewModel()
        {
            Familyname = DataStore.LastName;
            PageViewModels.Add(new EditMemberViewModel());
            PageViewModels.Add(new AllMembersViewModel());
            PageViewModels.Add(new TransactionsViewModel());

            CurrentDashViewModel = PageViewModels[1];
        }
        
        private void Add()
        {
            if (addwindow == null)
                addwindow = new AddMemberView();
            addwindow.Show();
        }

        private void Edit()
        {
            CurrentDashViewModel = PageViewModels[0];
        }
    }
}
