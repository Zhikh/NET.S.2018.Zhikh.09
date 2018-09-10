using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task2.UI.MVC.Infrastructure.Attributes;

namespace Task2.UI.MVC.Models.Person
{
    public class PersonCreateModel : PersonModel
    {
        [SerialNumberAttribute(ErrorMessage = "Invalid serial number!")]
        [Display(Name = "Passport serial number")]
        public string SerialNumber { get; set; }

        // TODO: check it
        // [Remote("ValidateEmail", "ModelValidation")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}