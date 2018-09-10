using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task2.UI.MVC.Models.Person;

namespace Task2.UI.MVC.Models.Account
{
    public sealed class AccountModel
    {
        public string Number { get; set; }

        public string Balance { get; set; }

        public PersonModel Owner { get; set; }
        
    }
}