using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckVersionService.Config.Elements
{
    public class CheckVersionConfig : ConfigurationSection
    {
        [ConfigurationProperty("Folders")]
        [ConfigurationCollection(typeof(FolderCollection))]
        public FolderCollection Folders
        {
            get
            {
                return (FolderCollection)this["Folders"];
            }
        }
    }
}
