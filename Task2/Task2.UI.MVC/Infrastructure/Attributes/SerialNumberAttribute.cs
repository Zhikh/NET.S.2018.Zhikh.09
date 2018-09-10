using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Task2.UI.MVC.Infrastructure.Attributes
{
    public class SerialNumberAttribute: ValidationAttribute
    {
        private const string PATTERN = @"[^\d\s\p{P}\]{2}\d{6}";

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var input = value as string;

            if (input == null)
            {
                return false;
            }

            return Regex.IsMatch(input, PATTERN);
        }
    }
}