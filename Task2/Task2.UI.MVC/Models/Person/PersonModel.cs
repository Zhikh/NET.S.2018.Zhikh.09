using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Task2.UI.MVC.Models.Person
{
    public class PersonModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }
    }
}