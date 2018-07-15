using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Task2
{
    // TODO: add checking for input data
    public sealed class Account: BaseEntity
    {
        public string Number { get; set; }

        public Person Owner { get; set; }

        public decimal InvoiceAmount { get; set; }

        public int Bonuses { get; set; }

        public AccountType BillType { get; set; }

        public AccountHistory BillHistory { get; set; }

        public override bool Equals(object obj)
        {
            var account = obj as Account;
            return account != null &&
                   base.Equals(obj) &&
                   Number == account.Number &&
                   EqualityComparer<Person>.Default.Equals(Owner, account.Owner) &&
                   InvoiceAmount == account.InvoiceAmount &&
                   Bonuses == account.Bonuses &&
                   EqualityComparer<AccountType>.Default.Equals(BillType, account.BillType) &&
                   EqualityComparer<AccountHistory>.Default.Equals(BillHistory, account.BillHistory);
        }

        public override int GetHashCode()
        {
            var hashCode = -482127840;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Number);
            hashCode = hashCode * -1521134295 + EqualityComparer<Person>.Default.GetHashCode(Owner);
            hashCode = hashCode * -1521134295 + InvoiceAmount.GetHashCode();
            hashCode = hashCode * -1521134295 + Bonuses.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<AccountType>.Default.GetHashCode(BillType);
            hashCode = hashCode * -1521134295 + EqualityComparer<AccountHistory>.Default.GetHashCode(BillHistory);
            return hashCode;
        }
    }
}
