using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task2.UI.MVC.Models.Person
{
    internal sealed class PersonModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int AccountsCount { get; set; }
    }
}