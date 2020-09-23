using System.Collections.Generic;
using System.Threading.Tasks;
using TaskLibrary.Models;

namespace TaskLibrary.Interfaces
{
    public interface IExport
    {
        Task<byte[]> Export(IEnumerable<ContactView> contacts);
    }
}
