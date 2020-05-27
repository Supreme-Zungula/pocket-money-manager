using System;
using System.Collections.Generic;
using System.Text;
using WPF_Frontend.Models.Family;
using WPF_Frontend.ViewModels.User;

namespace WPF_Frontend.ViewModels.Helpers
{
    /// <summary>
    /// Check any duplicate entries
    /// </summary>
    public class CheckDuplicate
    {
        private readonly AllUsers allUsers = new AllUsers();
        public bool CheckDuplicateUser(string phone)
        {
            bool result = false;
            IEnumerable<UserModel> Users = allUsers.UsersList;
            foreach(var user in Users)
            {
                if (user.Phone == phone)
                    result = true;
            }
            return result;
        }
    }
}
