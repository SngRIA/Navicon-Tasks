using System;
using System.Collections.Generic;
using TaskLibrary.Models;
using TaskLibrary.Enums;
using TaskLibrary.Interfaces;

namespace TaskLibrary.DataSources
{
    public sealed class MemoryDataSource : IDataSource
    {
        public IEnumerable<Contact> GetContacts()
        {
            return new[] { 
                new Contact(
                    birthday: new DateTime(1999, 10, 10),
                    firstName: "BFirstName",
                    lastName: "BLastName",
                    middleName: "BMiddleName",
                    gender: Gender.Male,
                    iTN: "123456781",
                    phoneNumber: "+79999999991",
                    address: new Address("country2", "city2", "address2", AddressType.Actual)),
                
                new Contact(
                    birthday: new DateTime(1999, 10, 10),
                    firstName: "BFirstName",
                    lastName: "BAastName",
                    middleName: "BAiddleName",
                    gender: Gender.Male,
                    iTN: "123456782",
                    phoneNumber: "+79999999992",
                    address: new Address("country3", "city3", "address3", AddressType.Actual)),

                new Contact(
                    birthday: new DateTime(2000, 10, 10),
                    firstName: "AFirstName",
                    lastName: "ALastName",
                    middleName: "AMiddleName",
                    gender: Gender.Male,
                    iTN: "123456789",
                    phoneNumber: "+79999999999",
                    address: new Address("country", "city", "address", AddressType.Actual))
            };
        }
    }
}
