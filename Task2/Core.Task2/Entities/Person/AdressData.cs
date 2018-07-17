using System;

namespace Core.Task2.Entities
{
    public sealed class AdressData
    {
        private string _country;
        private string _state;
        private string _city;
        private string _street;

        // TODO: AdressType?
        // public AdressType { get; set; }
        #region AdressType
        public string Country
        {
            get
            {
                return _country;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(Country) + " can't be null or empty!");
                }

                _country = value;
            }
        }
        
        public string State
        {
            get
            {
                return _state;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(State) + " can't be null or empty!");
                }

                _state = value;
            }
        }

        public string City
        {
            get
            {
                return _city;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(City) + " can't be null or empty!");
                }

                _city = value;
            }
        }
        #endregion

        public string Street
        {
            get
            {
                return _street;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(Street) + " can't be null or empty!");
                }

                _street = value;
            }
        }
    }
}
