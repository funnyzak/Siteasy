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
    public class Magview : PageBase
    {
        public int id = STARequest.GetInt("id", 0);
        public MagazineInfo info;

        protected override void PageShow()
        {
            info = Contents.GetMagazine(id);
            if (info == null || info.Status == 0)
            {
                PageInfo("不可浏览", weburl);
                return;
            }

            Contents.UpdateMagazineClick(info.Id, false);
        }
    }
}
