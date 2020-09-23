using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckVersionService.Config.Elements
{
    public class FolderElement : ConfigurationElement
    {
        // Periodicity time to check
        [ConfigurationProperty("every", IsKey = true, IsRequired = true)]
        public string Every
        {
            get
            {
                return (string)base["every"];
            }
            set
            {
                base["every"] = value;
            }
        }

        // Path to folder
        [ConfigurationProperty("path", IsKey = true, IsRequired = true)]
        public string Path
        {
            get
            {
                return (string)base["path"];
            }
            set
            {
                base["path"] = value;
            }
        }
    }
}
