using System;
using System.Reflection;
using ContactSerializer;
using ContactSerializer.Attributes;
using ContactSerializer.Enums;
using ContactSerializer.Models;

namespace Tasks
{
    class Program
    {
        public static void Main(string[] args)
        {
            Contact contact = new Contact (
                birthday: new DateTime(2000, 10, 10),
                firstName: "FirstName",
                lastName: "LastName",
                middleName: "MiddleName",
                gender: Gender.Male,
                iTN: "123456789",
                phoneNumber: "+79999999999",
                address: new Address("country", "city", "address", AddressType.Actual));

            Console.WriteLine($"ValidateDate - { ValidateDate(contact) }");
            Console.WriteLine($"ValidateFirstName - { ValidateFirstName(contact) }");
            Console.WriteLine($"ValidateLastName - { ValidateLastName(contact) }");
            Console.WriteLine($"ValidateMiddleName - { ValidateMiddleName(contact) }");
            Console.WriteLine($"ValidatePhoneNumber - { ValidatePhoneNumber(contact) }");

            Console.WriteLine(contact);

            
            var contactSerializer = new ContactSerializer.Serializers.ContactSerializer("test.data");
            contactSerializer.Serialize(new[] { contact });

            var contacts = contactSerializer.Deserialize();
            foreach (var contactDeserialized in contacts)
            {
                Console.WriteLine(contactDeserialized);
            }

            Console.ReadKey();
        }

        public static bool ValidateDate(Contact contact)
        {
            Type type = typeof(Contact);
            var prop = type.GetProperty("Birthday");

            DateLimitAttribute dateLimit = (DateLimitAttribute)prop.GetCustomAttribute(typeof(DateLimitAttribute));
            bool result = dateLimit.CheckData(contact.Birthday);

            return result;
        }

        public static bool ValidateFirstName(Contact contact)
        {
            Type type = typeof(Contact);
            var firstName = type.GetProperty("LastName");

            StringLengthLimitAttribute firstNameLimit = (StringLengthLimitAttribute)firstName.GetCustomAttribute(typeof(StringLengthLimitAttribute));
            bool result = firstNameLimit.CheckLength(contact.FirstName.Length);

            return result;
        }

        public static bool ValidateLastName(Contact contact)
        {
            Type type = typeof(Contact);
            var lastName = type.GetProperty("LastName");

            StringLengthLimitAttribute lastNameLimit = (StringLengthLimitAttribute)lastName.GetCustomAttribute(typeof(StringLengthLimitAttribute));
            bool result = lastNameLimit.CheckLength(contact.LastName.Length);

            return result;
        }

        public static bool ValidateMiddleName(Contact contact)
        {
            Type type = typeof(Contact);
            var middleName = type.GetProperty("MiddleName");

            StringLengthLimitAttribute middleNameLimit = (StringLengthLimitAttribute)middleName.GetCustomAttribute(typeof(StringLengthLimitAttribute));
            bool result = middleNameLimit.CheckLength(contact.MiddleName.Length);

            return result;
        }

        public static bool ValidatePhoneNumber(Contact contact)
        {
            Type type = typeof(Contact);
            var prop = type.GetProperty("PhoneNumber");

            RegExAttribute regExp = (RegExAttribute)prop.GetCustomAttribute(typeof(RegExAttribute));
            bool result = regExp.CheckExp(contact.PhoneNumber);

            return result;
        }
    }
}
