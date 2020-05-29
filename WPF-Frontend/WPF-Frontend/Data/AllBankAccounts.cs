using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using WPF_Frontend.ApiHelper;
using WPF_Frontend.Models.BankAccount;

namespace WPF_Frontend.Data
{
    public class AllBankAccounts : BindableBase
    {
        private IEnumerable<BankAccountModel> _account;
        private readonly APIHelper ApiResult;
        
        private IEnumerable<BankAccountModel> bankAccountsList
        {
            get { return _account; }
            set
            {
                SetProperty(ref _account, value);
            }
        }

        public AllBankAccounts()
        {
            ApiResult = new APIHelper();
            bankAccountsList = ApiResult.GetAllAccounts();
            StoredAccounts.BankAccounts = bankAccountsList;
        }
    }
}
