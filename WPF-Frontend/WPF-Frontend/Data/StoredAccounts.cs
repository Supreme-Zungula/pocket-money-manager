using System;
using System.Collections.Generic;
using System.Text;
using WPF_Frontend.Models.BankAccount;

namespace WPF_Frontend.Data
{
    /// <summary>
    /// Store all available bank accounts
    /// </summary>
    public static class StoredAccounts
    {
        private static IEnumerable<BankAccountModel> _accounts;

        public static IEnumerable<BankAccountModel> BankAccounts
        {
            get => _accounts;
            set
            {
                _accounts = value;
            }
        }

    }
}
