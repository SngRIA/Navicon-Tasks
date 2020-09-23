using log4net;
using TaskExportLibrary.Exports;
using TaskLibrary.Interfaces;

namespace TaskExportLibrary.Factory
{
    public sealed class ExportFactory : IExportFactory
    {
        private readonly ILog _log;
        public ExportFactory(ILog log)
        {
            _log = log;
        }
        public IExport Create(string exportFormat)
        {
            IExport export;

            _log.Info("Selected export format - " + exportFormat);
            switch (exportFormat)
            {
                case "csv":
                    _log.Debug("Create CSVExport");
                    export = new CSVExport(_log);
                    break;
                case "xlsx":
                    _log.Debug("Create CSVExport");
                    export = new ExcelExport(_log);
                    break;
                default:
                    _log.Warn("Wrong exportFormat, select CSVExport");
                    export = new CSVExport(_log);
                    break;
            }

            return export;
        }
    }
}
