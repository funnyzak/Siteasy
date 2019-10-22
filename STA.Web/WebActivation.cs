using log4net.Config;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(STA.Web.WebActivation), "Initialize")]

namespace STA.Web
{
    public sealed class WebActivation
    {
        public static void Initialize()
        {
            lock (_syncRoot)
            {
                if (!_isInitialized)
                {
                    //DynamicModuleUtility.RegisterModule(typeof(HttpLoggingModule));
                    XmlConfigurator.Configure(); //读取log4net配置
                    _isInitialized = true;
                }
            }
        }

        private static readonly object _syncRoot = new object();
        private static bool _isInitialized;

    }
}
