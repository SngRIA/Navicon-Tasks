using SimpleInjector;
using System;
using System.Collections.Generic;
using TaskExportLibrary.Exports;
using TaskLibrary.Builders;
using TaskLibrary.DataSources;
using TaskLibrary.Interfaces;
using TaskLibrary.Models;
using TaskLibrary.Saves;
using TaskLibrary.SortCollection;

namespace SecondTask
{
    public class Program
    {
        private static readonly Container _container;
        static Program()
        {
            _container = new Container();

            _container.Options.ResolveUnregisteredConcreteTypes = true;

            _container.Register<IDataSource, MemoryDataSource>();
            _container.Register<ISave, FileSave>();
            _container.Register<ExcelExport>();

            _container.Verify();
        }
        public static void Main(string[] args)
        {
            IDataSource dataSource = _container.GetInstance<MemoryDataSource>();
            IExport export = _container.GetInstance<ExcelExport>();
            FileSave save = _container.GetInstance<FileSave>();

            ICollection<ContactView> contacts = new List<ContactView>();

            ContactViewBuilder builder = new StandartContactViewBuilder();
            foreach (var contact in dataSource.GetContacts())
            {
                builder.CreateContactView();

                builder.BuildContactView();
                builder.BuildContact(contact);

                builder.BuildFormatProp("AddressProp", "st. {0}");

                contacts.Add(builder.BuildContactView());
            }

            contacts = ContactViewSortCollection.Sort(contacts);

            var file = export.Export(contacts);

            save.SetPath($"export")
                .SetFileName("test.xlsx")
                .Save(file.Result);

            Console.ReadKey();
        }
    }
}
