using System;
using System.Text;
using STA.Core.ScheduledEvents;
using STA.Core.Publish;
using STA.Core;
using STA.Data;
using STA.Common;
using System.Data;

namespace STA.Event
{
    public class PublishUrlStaticEvent : IEvent
    {
        #region IEvent 成员

        public void Execute(object state)
        {
            int pagecount, recordcount;
            DataTable dt = Contents.GetUrlStaticizeDataPage(1, 100, out pagecount, out recordcount);
            foreach (DataRow dr in dt.Rows)
            {
                Globals.UrlStaticizeCreate(Contents.GetUrlStaticize(TypeParse.StrToInt(dr["id"])));
            }
        }

        #endregion
    }
}
