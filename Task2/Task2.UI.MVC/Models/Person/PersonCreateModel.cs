using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task2.UI.MVC.Infrastructure.Attributes;

namespace Task2.UI.MVC.Models.Person
{
    internal sealed class PersonCreateModel
    {
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [SerialNumberAttribute(ErrorMessage = "Invalid serial number!")]
        [Display(Name = "Passport serial number")]
        public string SerialNumber { get; set; }

        // TODO: check it
        [Remote("ValidateEmail", "ModelValidation")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}