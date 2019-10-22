using System;
using System.Text;
using STA.Core.ScheduledEvents;
using STA.Core.Publish;
using STA.Core;

namespace STA.Event
{
    /// <summary>
    /// 生成Sitemap定时任务
    /// </summary>
    public class SitemapMakeEvent : IEvent
    {
        #region IEvent 成员

        public void Execute(object state)
        {
            ConUtils.MakeSitemap();
        }

        #endregion
    }
}
