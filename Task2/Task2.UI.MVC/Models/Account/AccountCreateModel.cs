using System.ComponentModel.DataAnnotations;

namespace Task2.UI.MVC.Models.Account
{
    public class AccountCreateModel : AccountModel
    {
        [Display(Name = "Account type")]
        public AccountTypeModel AccountType { get; set; }
    }
}