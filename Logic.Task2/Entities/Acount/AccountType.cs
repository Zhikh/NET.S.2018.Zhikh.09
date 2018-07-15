using System;

namespace Logic.Task2
{
    public sealed class AccountType: BaseEntity
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
                    throw new ArgumentException("Name can't be null or empty!");
                }

                _name = value;
            }
        }

        public decimal BalanceCost { get; set; }

        public decimal ReplenishmentCost { get; set; }
    }
}
