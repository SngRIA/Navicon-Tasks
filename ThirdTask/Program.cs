using log4net;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskExportLibrary.Exports;
using TaskLibrary.Builders;
using TaskLibrary.DataSources;
using TaskLibrary.Interfaces;
using TaskLibrary.Logger;
using TaskLibrary.Models;
using TaskLibrary.Saves;

namespace ThirdTask
{
    public class Program
    {
        private static readonly Container _container;
        private static readonly ILog _log;
        static Program()
        {
            _container = new Container();

            _container.Register<IDataSource, MemoryDataSource>();
            _container.Register<FileSave>();
            _container.Register<IExport, ExcelExport>();
            _container.Register<ILog>(() => Logger.Log);

            _container.Verify();

            _log = _container.GetInstance<ILog>();

        }
        private static ICollection<ContactView> GetBuildedContacts(IDataSource dataSource)
        {
            ICollection<ContactView> contacts = new List<ContactView>();

            ContactViewBuilder builder = new StandartContactViewBuilder();
            foreach (var contact in dataSource.GetContacts())
            {
                builder.CreateContactView();

                builder.BuildContact(contact);
                builder.BuildFormatProp("PhoneNumber", "{0:+#(###) ###-####}");

                contacts.Add(builder.BuildContactView());
            }

            return contacts;
        }

        private static void RequestFileName()
        {
            Console.WriteLine("Enter file name:");
        }
        private static string GetFileName()
        {
            RequestFileName();

            var fileName = Console.ReadLine();

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("fileName is empty");

            return fileName;
        }
        public static async Task Main(string[] args)
        {
            IDataSource dataSource = _container.GetInstance<IDataSource>();
            IExport export = _container.GetInstance<IExport>();
            FileSave save = _container.GetInstance<FileSave>();

            ICollection<ContactView> contacts = GetBuildedContacts(dataSource);

            var file = await export.Export(contacts);

            var fileName = GetFileName();

            save.SetPath($"export")
                .SetFileName($"{fileName}.xlsx")
                .Save(file);
        }
    }
}
