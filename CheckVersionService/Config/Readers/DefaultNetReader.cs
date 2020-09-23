using CheckVersionService.Config.Elements;
using CheckVersionService.Config.Interfaces;
using CheckVersionService.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace CheckVersionService.Configs
{
    public class DefaultNetReader : IConfig
    {
        private CheckVersionConfig _versionConfig;
        private ILog _log;
        public DefaultNetReader(ILog log)
        {
            _versionConfig = (CheckVersionConfig)ConfigurationManager.GetSection("CheckVersion");
            _log = log;
        }

        public IEnumerable<Folder> GetFolders()
        {
            foreach (FolderElement folder in _versionConfig.Folders)
            {
                if (DateTime.TryParse(folder.Every, out DateTime time))
                {
                    yield return new Folder(folder.Path, time);
                }
                else
                {
                    _log.Warn("Config value [Folders.Folder.Every] has wrong format: " + folder.Every);
                }
            }
        }
    }
}
