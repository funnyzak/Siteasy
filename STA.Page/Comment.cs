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
    public class Comment : PageBase
    {
        public int id = STARequest.GetInt("id", 0);
        public ContentInfo cinfo = null;
        protected override void PageShow()
        {
            cinfo = SimpleContent(id);
            if (cinfo.Id <= 0)
            {
                PageInfo("访问的信息不存在", sitedir + "/");
            }
            else if (cinfo.Iscomment == 0 || config.Opencomment == 0)
            {
                PageInfo(string.Format("<b>{0}</b>的评论已关闭", cinfo.Title), sitedir + "/");
            }
        }
    }
}
