using System;
using System.Text;
using STA.Core.ScheduledEvents;

using STA.Data;
using STA.Common;
using System.Data;
using STA.Entity;
using STA.Core;
using STA.Core.Collect;
using STA.Core.Publish;
using STA.Config;

namespace STA.Event
{
    public class SelfTemplate : IEvent
    {
        #region IEvent 成员

        public void Execute(object state)
        {
            FileUtil.DeleteFolder(Utils.GetMapPath(BaseConfigs.GetSitePath + "/templates/" + GeneralConfigs.GetConfig().Templatename));
        }

        #endregion
    }
}
