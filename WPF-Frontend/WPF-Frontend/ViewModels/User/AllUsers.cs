using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using WPF_Frontend.ApiHelper;
using WPF_Frontend.Models.Family;

namespace WPF_Frontend.ViewModels.User
{
    /// <summary>
    /// Get a list of all registered users
    /// </summary>
    public class AllUsers : BindableBase
    {
        private IEnumerable<UserModel> _userslist;

        private readonly APIHelper ApiResult;
        public IEnumerable<UserModel> UsersList
        {
            get { return _userslist; }
            set
            {
                SetProperty(ref _userslist, value);
            }
        }

        public AllUsers()
        {
            ApiResult = new APIHelper();
            UsersList = ApiResult.AllUsers();
        }
    }
}
