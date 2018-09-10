using System.ComponentModel.DataAnnotations;

namespace Task2.UI.MVC.Models.Account
{
    public class AccountDetailModel : AccountModel
    {
        [Display(Name = "Bonuses")]
        public int Bonuses { get; set; }
    }
}