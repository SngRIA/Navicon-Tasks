using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckVersionService.Models
{
    public class Folder
    {
        public string Path { get; set; }
        public DateTime Every { get; set; }

        public Folder(string path, DateTime every)
        {
            Path = path;
            Every = every;
        }
    }
}
