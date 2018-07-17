using System;

namespace Core.Task2.Entities
{
    public sealed class AccountType
    {
        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(Name) + "can't be null or empty!");
                }

                _name = value;
            }
        }

        public decimal BalanceCost { get; set; }

        public decimal ReplenishmentCost { get; set; }
    }
}
