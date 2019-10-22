using System;
using System.Collections.Generic;
using System.Text;

using STA.Data;
using STA.Common;
using STA.Config;
using STA.Core;
using STA.Payment;
using STA.Entity;
namespace STA.Page.User
{
    public class ContentList : UserBase
    {
        public int typeid = STARequest.GetInt("type", 1);
        public ContypeInfo ctinfo;
        protected override void PageShow()
        {
            if (!IsLogin()) return;

            if (typeid <= 0) typeid = 1;
            ctinfo = ConUtils.GetSimpleContype(typeid);
            if (ctinfo == null)
            {
                ctinfo = new ContypeInfo();
                ctinfo.Id = 1;
                ctinfo.Name = "普通内容";
            }
        }
    }
}
