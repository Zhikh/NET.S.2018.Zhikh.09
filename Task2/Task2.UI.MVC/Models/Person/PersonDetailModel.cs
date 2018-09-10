using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Task2.UI.MVC.Models.Account;

namespace Task2.UI.MVC.Models.Person
{
    public sealed class PersonDetailModel : PersonCreateModel
    {
        [Display(Name = "Accounts")]
        public IEnumerable<AccountModel> Accounts;
    }
}