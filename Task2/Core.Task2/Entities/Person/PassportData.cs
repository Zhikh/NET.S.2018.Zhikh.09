using System;

namespace Core.Task2.Entities
{
    public sealed class PassportData
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
                    throw new ArgumentException(nameof(SerialNumber) + " can't be null or empty!");
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
                    throw new ArgumentException(nameof(IdentityNumber) + " can't be null or empty!");
                }

                _identityNumber = value;
            }
        }
    }
}
