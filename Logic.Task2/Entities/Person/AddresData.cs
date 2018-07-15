using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Task2
{
    // TODO: add checking for input data
    public sealed class AddressData: BaseEntity
    {
        // TODO: AdressType?
        // public AdressType { get; set; }
        #region AdressType
        public string Country { get; set; }
        
        public string State { get; set; }
        
        public string City { get; set; }
        #endregion

        public string Street { get; set; }
    }
}
