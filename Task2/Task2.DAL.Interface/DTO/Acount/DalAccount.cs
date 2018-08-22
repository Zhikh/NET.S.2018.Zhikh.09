using System;
using Task2.DAL.Interface.DTO;

namespace Task2.DAL.Interfaces.DTO
{
    public sealed class DalAccount: IEntity
    {
        private DalPerson _owner;
        private DalAccountType _accountType;

        public int Id { get; set; }

        public string Number { get; set; }

        public DalPerson Owner
        {
            get
            {
                return _owner;
            }

            set
            {
                _owner = value ?? throw new ArgumentException("Owner can't be null!");
            }
        }

        public decimal InvoiceAmount { get; set; }

        public int Bonuses { get; set; }

        public DalAccountType AccountType
        {
            get
            {
                return _accountType;
            }

            set
            {
                _accountType = value ?? throw new ArgumentException("Account type can't be null!");
            }
        }
    }
}
