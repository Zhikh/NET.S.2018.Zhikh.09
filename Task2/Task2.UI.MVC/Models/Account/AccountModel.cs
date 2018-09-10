using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Task2.UI.MVC.Models.Person;

namespace Task2.UI.MVC.Models.Account
{
    public class AccountModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput]
        [Display(Name = "Account number")]
        public string Number { get; set; }

        [Display(Name = "Balance")]
        public decimal Balance { get; set; }

        [Display(Name = "Owner")]
        public PersonModel Owner { get; set; }
    }
}