﻿using System;
using System.Text.RegularExpressions;

namespace Logic.Task2
{
    public sealed class ContactData: BaseEntity
    {
        private readonly string _emailPattern = @"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})";

        private string _email;
        private string _phone;

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
                    throw new ArgumentException("Email can't be null or empty!");
                }

                if(!Regex.IsMatch(value, _emailPattern, RegexOptions.IgnoreCase))
                {
                    throw new ArgumentException("Email doesn't correct!");
                }

                _email = value;
            }
        }

        public string ContactPhone
        {
            get
            {
                return _phone;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Phone number can't be null or empty!");
                }
                _phone = value;
            }
        }
    }
}
