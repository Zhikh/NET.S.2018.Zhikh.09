using System;
using Task2.DAL.Interface.DTO;

namespace Task2.DAL.Interfaces.DTO
{
    public sealed class DalAccountType : IEntity
    {
        private string _name;

        public int Id { get; set; }

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
