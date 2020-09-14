using System;
using Tasks.Enums;
using ContactSerializer.Attributes;

namespace Tasks.Models
{
    [Description("Address")]
    public sealed class Address : ICloneable
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string AddressProp { get; set; }
        public AddressType AddressType { get; set; }

        #region ICloneable

        public object Clone()
        {
            return new Address
            {
                Country = Country,
                City = City,
                AddressProp = AddressProp,
                AddressType = AddressType
            };
        }

        #endregion
    }
}
