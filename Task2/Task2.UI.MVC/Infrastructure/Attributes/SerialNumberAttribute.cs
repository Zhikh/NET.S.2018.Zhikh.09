using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task2.UI.MVC.Infrastructure.Attributes
{
    public class SerialNumberAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            // TODO: realize
            return false;
        }
    }
}