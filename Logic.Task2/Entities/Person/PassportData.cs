using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Task2
{
    // TODO: add checking for input data
    public sealed class PassportData: BaseEntity
    {
        public string SerialNumber { get; set; }
        
        public string IdentityNumber { get; set; }
    }
}
