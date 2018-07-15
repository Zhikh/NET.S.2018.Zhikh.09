using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Task2
{
    // TODO: add checking for input data
    public sealed class Person
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string SecondName { get; set; }

        public AddressData Address { get; set; }

        public ContactData Contact { get; set; }

        public PassportData Passport { get; set; }

        public ICollection<Bill> Bill { get; set; }
    }
}
