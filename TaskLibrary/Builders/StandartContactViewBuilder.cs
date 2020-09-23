using System;
using TaskLibrary.Models;

namespace TaskLibrary.Builders
{
    public class StandartContactViewBuilder : ContactViewBuilder
    {
        public override void CreateContactView()
        {
            contactView = new ContactView();
        }
        private string GetShortName()
        {
            return string.Format($"{ contactView.LastName } { contactView.FirstName[0] }.{ contactView.MiddleName[0] }.");
        }

        public override void BuildContact(Contact contact)
        {
            contactView.LastName = contact.LastName;
            contactView.FirstName = contact.FirstName;
            contactView.MiddleName = contact.MiddleName;
            contactView.ShortName = GetShortName();
            contactView.Gender = contact.Gender.ToString();
            contactView.Birthday = contact.Birthday.ToString("dd.MM.yyyy");
            contactView.ITN = contact.ITN;
            contactView.PhoneNumber = contact.PhoneNumber;

            contactView.Country = contact.Address.Country;
            contactView.City = contact.Address.City;
            contactView.AddressProp = contact.Address.AddressProp;
            contactView.AddressType = contact.Address.AddressType.ToString();
        }


        public override void BuildFormatProp(string propName, string format)
        {
            var prop = typeof(ContactView).GetProperty(propName);
            var propValue = prop.GetValue(contactView);

            var convertedData = string.Empty;

            if (propName.Equals("PhoneNumber", StringComparison.OrdinalIgnoreCase))
                convertedData = string.Format(format, Convert.ToDouble(propValue));
            else
                convertedData = string.Format(format, propValue);

            prop.SetValue(contactView, convertedData);
        }

        public override ContactView BuildContactView()
        {
            return contactView;
        }

        public void FormatProp(string propName, string format)
        {
            var prop = typeof(ContactView).GetProperty(propName);

            object propValue = prop.GetValue(contactView).ToString();
            prop.SetValue(contactView, string.Format(format, propValue));
        }
    }
}
