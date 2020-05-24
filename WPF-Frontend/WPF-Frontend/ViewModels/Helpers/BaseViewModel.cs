using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WPF_Frontend.ViewModels.Base
{
    /// <summary>
    /// Track any changes from the UI and the code behind
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /*public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }*/
    }
}