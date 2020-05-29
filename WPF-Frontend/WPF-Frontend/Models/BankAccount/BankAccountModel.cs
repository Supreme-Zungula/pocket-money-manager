using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_Frontend.Models.BankAccount
{
    public class BankAccountModel
    {
        public string Id { get; set; }
        public string AccountNo { get; set; }
        public decimal Balance { get; set; }
        public string CustomerRef { get; set; }
    }
}
