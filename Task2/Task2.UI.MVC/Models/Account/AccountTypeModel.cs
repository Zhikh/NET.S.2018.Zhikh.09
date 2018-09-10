using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task2.UI.MVC.Models.Account
{
    public class AccountTypeModel
    {
        public string Name { get; set; }

        public int DepositCost { get; set; }

        public int WithdrawCost { get; set; }
    }
}