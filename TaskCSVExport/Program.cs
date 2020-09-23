using log4net;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using TaskExportLibrary.Exports;
using TaskExportLibrary.Factory;
using TaskLibrary.Builders;
using TaskLibrary.DataSources;
using TaskLibrary.Interfaces;
using TaskLibrary.Logger;
using TaskLibrary.Models;
using TaskLibrary.Saves;

namespace TaskCSVExport
{
    public class Program
    {
        private static readonly Container _container;
        private static ILog _log;

        static Program()
        {
            _container = new Container();

            _container.Register<IDataSource, MemoryDataSource>();
            _container.Register<IExportFactory, ExportFactory>();
            _container.Register<FileSave>();
            _container.Register(() => Logger.Log);

            _container.Verify();
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

        public static async Task Main(string[] args)
        {
            _log = _container.GetInstance<ILog>();
            _log.Info("Startup");

            FileSave save = _container.GetInstance<FileSave>();
            IDataSource dataSource = _container.GetInstance<IDataSource>();

            IExportFactory exportFactory = _container.GetInstance<IExportFactory>();
            IExport export = exportFactory.Create("csv");

            ICollection<ContactView> contacts = GetBuildedContacts(dataSource);

            _log.Info("Export contacts");
            var file = await export.Export(contacts);

            _log.Debug("Get [OutExportFileName] from config");
            var fileName = ConfigurationManager.AppSettings.Get("OutExportFileName");

            _log.Info("Save file to export/" + fileName);
            save.SetPath($"export")
                .SetFileName(fileName)
                .Save(file);

            Console.ReadKey();

            _log.Info("Close app");
        }
    }
}
