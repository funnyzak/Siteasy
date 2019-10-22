using System;
using System.Text;
using STA.Core.ScheduledEvents;
using STA.Core;
using STA.Config;
using STA.Data;
using STA.Common;

namespace STA.Event
{

    public class BackDBEvent : IEvent
    {
        #region IEvent 成员

        public void Execute(object state)
        {
            foreach (string info in BaseConfigs.GetDBConnectString.Split(';'))
            {
                if (info.ToLower().IndexOf("initial catalog") >= 0 || info.ToLower().IndexOf("database") >= 0)
                {
                    Databases.BackUpDatabase(Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/data/db_back/"), "", "", "", info.Split('=')[1].Trim(), Rand.RamTime());
                    break;
                }
            }
        }

        #endregion
    }
}
