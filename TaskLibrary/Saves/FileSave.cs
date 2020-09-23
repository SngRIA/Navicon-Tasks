using log4net;
using System.Configuration;
using System.IO;
using TaskLibrary.Interfaces;

namespace TaskLibrary.Saves
{
    public class FileSave : ISave
    {
        private string _path;
        private string _fileName;
        private readonly ILog _log;

        public FileSave(ILog log)
        {
            _log = log;
        }

        public bool Save(byte[] data)
        {
            bool result = false;

            if (data.Length == 0)
            {
                _log.Error("ArgumentException: data is empty");
                return result;
            }

            var path = $"{ _path }\\{ _fileName }";

            if (!bool.TryParse(ConfigurationManager.AppSettings.Get("NeedReWriteFile"), out bool needReWriteFile))
            {
                _log.Warn("Can't load NeedReWriteFile from config");
            }

            if (File.Exists(path) && needReWriteFile == true)
            { 
                _log.Debug($"File { path } rewrite");
                File.WriteAllBytes(path, data);
                result = true;
            }
            else
            {
                _log.Error($"File { path } exist");
                result = false;
            }

            return result;
        }
        public ISave SetFileName(string fileName)
        {
            if (Directory.Exists(fileName))
            {
                _log.Error("ArgumentException: fileName is empty");
                return this;
            }

            _fileName = fileName;
            return this;
        }
        public ISave SetPath(string path)
        {
            if (!Directory.Exists(path))
            {
                _log.Error($"DirectoryNotFoundException: path - { path }");
                return this;
            }

            _path = path;
            return this;
        }
    }
}
