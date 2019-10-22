using System;
using System.Text;
using STA.Core.ScheduledEvents;
using STA.Core.Publish;

namespace STA.Event
{
    /// <summary>
    /// 生成全站
    /// </summary>
    public class PublishSiteEvent : IEvent
    {
        #region IEvent 成员

        public void Execute(object state)
        {
            STA.Core.Publish.StaticPublish.Reset();
            STA.Core.Publish.StaticPublish.OnKeyPublish(false);
            STA.Core.Publish.StaticPublish.OnKeyPublish(true);
        }

        #endregion
    }
}
