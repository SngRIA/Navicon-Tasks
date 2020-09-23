using System.Collections.Generic;
using TaskLibrary.Models;

namespace TaskLibrary.Interfaces
{
    public interface IDataSource
    {
        IEnumerable<Contact> GetContacts();
    }
}
