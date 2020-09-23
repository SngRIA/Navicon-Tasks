using log4net;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskLibrary.Interfaces;
using TaskLibrary.Models;

namespace TaskExportLibrary.Exports
{
    public sealed class ExcelExport : IExport
    {
        private readonly ILog _log;

        public ExcelExport(ILog log)
        {
            _log = log;
        }
        private void FillRowLabels<T>(ExcelWorksheet worksheet)
        {
            int id = default;
            foreach (var prop in typeof(T).GetProperties().OrderBy(p => p.Name))
            {
                worksheet.Cells[1, ++id].Value = prop.Name;
            }
        }

        /// <summary>
        /// Export ExcelPackage
        /// </summary>
        /// <returns>Excel file in byte[]</returns>
        public async Task<byte[]> Export(IEnumerable<ContactView> contacts)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                _log.Debug($"Add new ExcelWorksheet (Contacts) to Worksheets");
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Contacts");

                _log.Debug($"Fill row labels");
                FillRowLabels<ContactView>(worksheet);

                int idRow = 1;
                foreach (var contact in contacts)
                {
                    idRow += 1;
                    int idColumn = default;
                    foreach (var prop in typeof(ContactView).GetProperties().OrderBy(p => p.Name))
                    {
                        worksheet.Cells[idRow, ++idColumn].Value = prop.GetValue(contact);
                    }
                }

                return await excelPackage.GetAsByteArrayAsync();
            }
        }
    }
}
