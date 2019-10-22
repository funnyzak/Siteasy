using System;
using System.Collections.Generic;
using System.Text;

using STA.Data;
using STA.Common;
using STA.Config;
using STA.Core;
using STA.Entity;
namespace STA.Page
{
    public class Userinfo : PageBase
    {
        public int id = STARequest.GetInt("id", 0);
        protected override void PageShow()
        {
            //PageInfo("没有权限查看此页面", "/", 2);
        }
    }
}
