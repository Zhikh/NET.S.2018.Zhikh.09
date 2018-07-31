using System;
using System.Text.RegularExpressions;
using Task2.DAL.Interface.DTO;

namespace Task2.DAL.Interfaces.DTO
{
    public sealed class DalContactData : IEntity
    {
        private readonly string _emailPattern = @"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})";

        private string _email;

        public int Id { get; set; }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(Email) + " can't be null or empty!");
                }

                if (!Regex.IsMatch(value, _emailPattern, RegexOptions.IgnoreCase))
                {
                    throw new ArgumentException(nameof(Email) + " doesn't correct!");
                }

                _email = value;
            }
        }
    }
}
