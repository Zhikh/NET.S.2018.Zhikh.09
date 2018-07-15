using System;

namespace Logic.Task2
{
    public sealed class AccountType
    {
        private string _name;

        public string Name
        {
            get
            {
                return this._name;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name can't be null or empty!");
                }

                this._name = value;
            }
        }

        public decimal BalanceCost { get; set; }

        public decimal ReplenishmentCost { get; set; }
    }
}
