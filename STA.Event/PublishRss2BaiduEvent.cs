using System;
using System.Text;
using STA.Core.ScheduledEvents;
using STA.Core.Publish;
using STA.Core;
using STA.Data;

namespace STA.Event
{
    /// <summary>
    /// 生成RSS和百度新闻协议
    /// </summary>
    public class PublishRss2BaiduEvent : IEvent
    {
        #region IEvent 成员

        public void Execute(object state)
        {
            Globals.UrlStaticizeCreate(Contents.GetUrlStaticize(1));
            Globals.UrlStaticizeCreate(Contents.GetUrlStaticize(2));
            Globals.UrlStaticizeCreate(Contents.GetUrlStaticize(3));
        }

        #endregion
    }
}
