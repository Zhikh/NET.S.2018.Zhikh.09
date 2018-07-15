using System;

namespace Logic.Task2
{
    // TODO: add checking for input data
    public sealed class PassportData: BaseEntity
    {
        private string _serialNumber;
        private string _identityNumber;

        public string SerialNumber
        {
            get
            {
                return _serialNumber;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Serial number can't be null or empty!");
                }

                _serialNumber = value;
            }
        }
        
        public string IdentityNumber
        {
            get
            {
                return _identityNumber;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Identity number can't be null or empty!");
                }

                _identityNumber = value;
            }
        }
    }
}
