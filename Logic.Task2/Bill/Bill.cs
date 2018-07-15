using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Task2
{
    // TODO: add checking for input data
    public sealed class Bill
    {
        // TODO: type (string or int?)
        public int Number { get; set; }

        public Person Owner { get; set; }

        public decimal InvoiceAmount { get; set; }

        public int Bonuses { get; set; }

        public BillType BillType { get; set; }

        public BillHistory BillHistory { get; set; }
    }
}
