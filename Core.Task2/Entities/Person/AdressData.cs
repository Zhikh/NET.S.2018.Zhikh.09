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
                return this._country;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Country can't be null or empty!");
                }

                this._country = value;
            }
        }
        
        public string State
        {
            get
            {
                return this._state;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("State can't be null or empty!");
                }

                this._state = value;
            }
        }

        public string City
        {
            get
            {
                return this._city;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("City can't be null or empty!");
                }

                this._city = value;
            }
        }
        #endregion

        public string Street
        {
            get
            {
                return this._street;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Street can't be null or empty!");
                }

                this._street = value;
            }
        }
    }
}
