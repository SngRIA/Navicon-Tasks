using CheckVersionService.Models;
using System;
using System.Collections.Generic;

namespace CheckVersionService.Config.Interfaces
{
    interface IConfig
    {
        IEnumerable<Folder> GetFolders();
    }
}
