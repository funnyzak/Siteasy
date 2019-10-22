using log4net;
using System;
using System.Collections;
using System.Text;
using System.Web;

namespace STA.Core
{
    public sealed class LogProvider
    {
        private static ILog _logger;

        public static ILog Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = LogManager.GetLogger(typeof(LogProvider));
                }
                return _logger;
            }
        }

        public static void InfoFormat(string infos, params object[] parms)
        {
            try
            {
                Logger.InfoFormat(infos, parms);
            }
            catch
            {
                throw new Exception("Can't log info");
            }
        }

        /// <summary>
        /// 自定义ErrLog信息
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="additionalContext"></param>
        /// <param name="logHttpInfo"></param>
        public static void ErrLog(Exception exception, string additionalContext, bool logHttpInfo = false)
        {
            var message = String.Empty;
            var stackTrace = String.Empty;
            var s = new StringBuilder();

            if (additionalContext != null)
            {
                s.Append("Additional context:" + additionalContext);
                s.Append("\r\n\r\n");
            }
            if (exception != null)
            {
                message = exception.GetType().Name + ": " + exception.Message;
                if (exception.InnerException != null)
                    message += "\r\n\r\nInner exception: " + exception.InnerException.Message;
                s.Append(message);

                stackTrace = exception.StackTrace ?? String.Empty;
                s.AppendFormat("\r\n\r\nStack trace:{0}\r\n", stackTrace);

                foreach (DictionaryEntry item in exception.Data)
                {
                    s.Append(item.Key);
                    s.Append(": ");
                    s.Append(item.Value);
                    s.Append("\r\n");
                }
            }
            s.Append("\r\n");

            if (logHttpInfo)
            {
                var context = HttpContext.Current;
                if (context != null)
                {
                    lock (context.Request.ServerVariables.AllKeys.SyncRoot)
                    {
                        foreach (var key in context.Request.ServerVariables.AllKeys)
                        {
                            if (context.Request.ServerVariables[key] != String.Empty && key != "ALL_HTTP")
                            {
                                s.Append(key);
                                s.Append(": ");
                                s.Append(context.Request.ServerVariables[key]);
                                s.Append("\r\n");
                            }
                        }
                    }
                }
            }
            try
            {
                Logger.ErrorFormat("{0}\r\n\r\n\r\n", s.ToString());
            }
            catch
            {
                throw new Exception(String.Format("Can't log error: {0}\r\n\r\n{1}\r\n\r\n{2}", message, stackTrace, s));
            }
        }
    }
}
