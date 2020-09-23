using TaskLibrary.Models;

namespace TaskLibrary.Builders
{
    public abstract class ContactViewBuilder
    {
        protected ContactView contactView;

        public ContactView ContactView
        {
            get => contactView;
        }

        public abstract void CreateContactView();

        public abstract void BuildContact(Contact contact);
        public abstract void BuildFormatProp(string propName, string format);

        public abstract ContactView BuildContactView();
    }
}
