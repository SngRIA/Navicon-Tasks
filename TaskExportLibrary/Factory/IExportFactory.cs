using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Interfaces;

namespace TaskExportLibrary.Factory
{
    public interface IExportFactory
    {
        IExport Create(string exportFormat);
    }
}
