using System;
using System.Collections.Generic;
using System.Linq;
using TaskLibrary.Enums;
using TaskLibrary.Interfaces;
using TaskLibrary.Models;

namespace ThirdTask.DataSources
{
    public class RandomDataSource : IDataSource
    {
        private static Random random = new Random();

        private List<string> _firstNames;
        private List<string> _lastNames;
        private List<string> _middleNames;
        public RandomDataSource()
        {
            _firstNames = new List<string>
            {
                "Мирослав", "Модест", "Исаак", "Влас", "Кассиан"
            };

            _lastNames = new List<string>
            {
                "Быков", "Власов", "Некрасов", "Семёнов", "Стрелков"
            };

            _middleNames = new List<string>
            {
                "Арсеньевич", "Глебович", "Оскарович", "Альвианович", "Мартынович"
            };
        }

        private string SelectRandomItem(IEnumerable<string> items)
        {
            if (items == null)
                throw new ArgumentNullException();

            var indexItem = random.Next(items.Count());

            return items.ElementAt(indexItem);
        }

        public Contact GetRandomContact()
        {
            return new Contact
            {
                FirstName = SelectRandomItem(_firstNames),
                LastName = SelectRandomItem(_lastNames),
                MiddleName = SelectRandomItem(_middleNames),
                Gender = Gender.Male,
                Birthday = new DateTime(2000, 1, 1),
                ITN = "123456789",
                PhoneNumber = "+79999999991",
                Address = new Address("country", "city", "address", AddressType.Actual)
            };
        }

        public IEnumerable<Contact> GetContacts()
        {
            return GetContacts(10);
        }

        public IEnumerable<Contact> GetContacts(int needCount)
        {
            ICollection<Contact> contacts = new List<Contact>();
            for (int i = 0; i < needCount; i++)
            {
                contacts.Add(GetRandomContact());
            }
            return contacts;
        }
    }
}
