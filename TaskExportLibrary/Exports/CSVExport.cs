using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Interfaces;
using TaskLibrary.Models;

namespace TaskExportLibrary.Exports
{
    public sealed class CSVExport : IExport
    {
        private readonly ILog _log;

        public CSVExport(ILog log)
        {
            _log = log;
        }

        private string ConvertContactViewToString(ContactView contact)
        {
            _log.Info("Convert contact view to string");

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var prop in typeof(ContactView).GetProperties().OrderBy(p => p.Name))
            {
                stringBuilder.Append($"{ prop.GetValue(contact) },");
            }

            _log.Info("Remove ',' from string");
            stringBuilder.Remove(stringBuilder.Length - 1, 1); // remove last ','

            _log.Info("Appent NewLine");
            stringBuilder.Append(Environment.NewLine);

            return stringBuilder.ToString();
        }

        public async Task<byte[]> Export(IEnumerable<ContactView> contacts)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                foreach (var contact in contacts)
                {
                    _log.Info("Call ConvertContactViewToString and get bytes to array");
                    byte[] tempArray = Encoding.Default.GetBytes(ConvertContactViewToString(contact));

                    _log.Info("Write to stream");
                    await stream.WriteAsync(tempArray, 0, tempArray.Length);
                }

                return stream.ToArray();
            }
        }
    }
}
