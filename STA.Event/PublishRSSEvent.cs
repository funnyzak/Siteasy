using System;
using System.Text;
using STA.Core.ScheduledEvents;
using STA.Core.Publish;

namespace STA.Event
{
    /// <summary>
    /// 生成RSS订阅
    /// </summary>
    public class PublishRSSEvent : IEvent
    {
        #region IEvent 成员

        public void Execute(object state)
        {
            StaticPublish.Reset();
            StaticPublish.rssdt = STA.Data.Contents.GetPublishRssTable("");
            StaticPublish.PublishRss(true);
        }

        #endregion
    }
}
