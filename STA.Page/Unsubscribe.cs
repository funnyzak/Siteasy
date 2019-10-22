using System;
using System.Collections.Generic;
using System.Text;

using STA.Entity;
using STA.Common;
using STA.Core;
using STA.Data;
using STA.Config;
namespace STA.Page
{
    public class Unsubscribe : PageBase
    {
        public string mail = STARequest.GetString("m").Trim();
        public string safecode = STARequest.GetString("s").Trim();
        public int code = 0;
        protected override void PageShow()
        {
            MailsubInfo info = Mails.GetSubmail(mail);
            if (info != null && info.Safecode == safecode)
            {
                code = 1;
                info.Status = 0;
                Mails.EditSubmail(info);
            }
        }
    }
}
