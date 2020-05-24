using System;
using System.Collections.Generic;
using System.Text;
using WPF_Frontend.ViewModels.Base;
using WPF_Frontend.ViewModels.Helpers;

namespace WPF_Frontend.ViewModels.Application
{
    public class ApplicationViewModel : BaseViewModel
    {
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Login;
        public BaseViewModel CurrentPageViewModel { get; set; }

        public void GoToPage(ApplicationPage page, BaseViewModel viewModel = null)
        {
            // Set the view model
            CurrentPageViewModel = viewModel;

            // See if page has changed
            var different = CurrentPage != page;

            // Set the current page
            CurrentPage = page;

            if (!different)
                NotifyPropertyChanged(nameof(CurrentPage));
        }
    }
}
