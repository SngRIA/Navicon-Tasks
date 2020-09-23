using CheckVersionService.Config.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CheckVersionService
{
    public partial class CheckService : ServiceBase
    {
        private ILog _log;
        private IConfig _config;
        public CheckService()
        {
            InitializeComponent();

            CanStop = false;
            CanPauseAndContinue = true;

            ServiceName = "CheckService";
        }

        protected override void OnStart(string[] args)
        {
            foreach(var folder in _config.GetFolders())
            {

            }
        }

        protected override void OnStop()
        {
        }
    }
}
