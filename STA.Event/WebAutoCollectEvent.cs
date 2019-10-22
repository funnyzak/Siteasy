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
    /// <summary>
    /// 网页采集发布定时任务
    /// </summary>
    public class WebAutoCollectEvent : IEvent
    {
        #region IEvent 成员

        public void Execute(object state)
        {
            int pageCount, recordCount;
            DataTable dt = STA.Data.Collects.GetWebCollectDataTable(1, 20, "", out pageCount, out recordCount);
            foreach (DataRow dr in dt.Rows)
            {
                WebcollectInfo webinfo = STA.Data.Collects.GetWebCollect(TypeParse.StrToInt(dr["id"]));
                WebCollect.CollectPre(webinfo, 0);
                WebCollect.Collect();
            }

            StaticPublish.Reset();
            StaticPublish.condt = Contents.GetContentDataPage(1, 500, "", out pageCount, out recordCount);
            StaticPublish.chldt = Caches.GetChannelTable(GeneralConfigs.GetConfig().Cacheinterval * 60);
            StaticPublish.PublishContent(true);
            StaticPublish.PublishChannel(true);
            StaticPublish.PublishIndex(true);
        }

        #endregion
    }
}
