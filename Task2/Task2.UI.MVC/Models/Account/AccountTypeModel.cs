using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Task2.UI.MVC.Models.Account
{
    public class AccountTypeModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Cost of deposit operation")]
        public int DepositCost { get; set; }

        [Display(Name = "Cost of withdraw operation")]
        public int WithdrawCost { get; set; }
    }
}