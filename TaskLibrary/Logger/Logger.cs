using log4net;
using log4net.Config;

namespace TaskLibrary.Logger
{
    public sealed class Logger
    {
        public static readonly ILog Log = LogManager.GetLogger(typeof(Logger));

        static Logger()
        {
            XmlConfigurator.Configure();
        }
    }
}
