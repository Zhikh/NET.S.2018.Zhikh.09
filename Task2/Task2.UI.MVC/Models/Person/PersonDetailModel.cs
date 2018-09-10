using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task2.UI.MVC.Models.Account;

namespace Task2.UI.MVC.Models.Person
{
    public sealed class PersonDetailModel : PersonCreateModel
    {
        public IEnumerable<AccountModel> Accounts;
    }
}