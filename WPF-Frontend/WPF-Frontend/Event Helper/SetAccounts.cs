using System;
using System.Collections.Generic;
using System.Text;
using WPF_Frontend.Data;
using WPF_Frontend.Models.BankAccount;
using WPF_Frontend.Models.Family;

namespace WPF_Frontend.Event_Helper
{
    public class SetAccounts
    {
        public IEnumerable<FamilyMemberModel> SetBankAccounts(IEnumerable<FamilyMemberModel> familyMembers)
        {
            IEnumerable<BankAccountModel> Accounts = StoredAccounts.BankAccounts;
            if (familyMembers == null || Accounts == null)
                return null;
            foreach (FamilyMemberModel member in familyMembers)
            {
                foreach (BankAccountModel account in Accounts)
                {
                    if (account.CustomerRef == member.Id)
                    {
                        member.Accountno = account.AccountNo;
                        member.Balance = account.Balance;
                    }
                } 
            }
            return familyMembers;
        }

        public UserModel SetBankAccounts(UserModel user)
        {
            IEnumerable<BankAccountModel> Accounts = StoredAccounts.BankAccounts;
            if (Accounts == null)
                return null;
            foreach (BankAccountModel account in Accounts)
            {
                if (account.CustomerRef == user.Id)
                {
                    user.AccNo = account.AccountNo;
                    user.Balance = account.Balance;
                    return user;
                }
            }
            return null;
        }
    }
}
