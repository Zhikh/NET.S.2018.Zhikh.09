using System;

namespace Task2.BLL.Interface.Entities
{
    public sealed class AccountType
    {
        #region Private fieds
        private string _name;
        #endregion

        #region Public API
        /// <summary>
        /// Name of account type
        /// </summary>
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

        /// <summary>
        /// Cost of one bonus point for deposit
        /// </summary>
        public decimal BalanceCost { get; set; }

        /// <summary>
        /// Cost of one bonus point for withdraw
        /// </summary>
        public decimal ReplenishmentCost { get; set; }
        #endregion
    }
}
