using System;
using System.Collections.Generic;
using System.Text;

using STA.Data;
using STA.Common;
using STA.Config;
using STA.Entity.Plus;

namespace STA.Page.Plus
{
    public class Download : PageBase
    {
        public int id = STARequest.GetInt("id", 0);
        public string downurl = STARequest.GetString("downurl");
        protected override void PageShow()
        {
            if (downurl != "" && id > 0)
            {
                Contents.UpdateSoftDownloadCount(id);
                Response.Redirect(downurl);
            }
        }


    }

}
