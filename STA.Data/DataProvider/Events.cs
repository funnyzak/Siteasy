using System;

namespace STA.Data
{
    public class Events
    {
        public static void SetLastExecuteScheduledEventDateTime(string key, string servername, DateTime lastexecuted)
        {
            DatabaseProvider.GetInstance().SetLastExecuteScheduledEventDateTime(key, servername, lastexecuted);
        }

        public static DateTime GetLastExecuteScheduledEventDateTime(string key, string servername)
        {
            return DatabaseProvider.GetInstance().GetLastExecuteScheduledEventDateTime(key,servername);
        }
    }
}
