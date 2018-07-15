using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Task2
{
    // TODO: add checking for input data
    public sealed class Person: BaseEntity
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string SecondName { get; set; }

        public AddressData Address { get; set; }

        public ContactData Contact { get; set; }

        public PassportData Passport { get; set; }

        public ICollection<Account> Account { get; set; }

        public override bool Equals(object obj)
        {
            var person = obj as Person;
            return person != null &&
                   base.Equals(obj) &&
                   FirstName == person.FirstName &&
                   LastName == person.LastName &&
                   SecondName == person.SecondName &&
                   EqualityComparer<AddressData>.Default.Equals(Address, person.Address) &&
                   EqualityComparer<ContactData>.Default.Equals(Contact, person.Contact) &&
                   EqualityComparer<PassportData>.Default.Equals(Passport, person.Passport) &&
                   EqualityComparer<ICollection<Account>>.Default.Equals(Account, person.Account);
        }

        public override int GetHashCode()
        {
            var hashCode = 681110399;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SecondName);
            hashCode = hashCode * -1521134295 + EqualityComparer<AddressData>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + EqualityComparer<ContactData>.Default.GetHashCode(Contact);
            hashCode = hashCode * -1521134295 + EqualityComparer<PassportData>.Default.GetHashCode(Passport);
            hashCode = hashCode * -1521134295 + EqualityComparer<ICollection<Account>>.Default.GetHashCode(Account);
            return hashCode;
        }
    }
}
