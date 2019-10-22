using System;
using System.Data;
using System.Text;

using STA.Entity;
using STA.Common;
using STA.Core;
using STA.Data;
using STA.Config;

namespace STA.Page
{
    public class Tags : PageBase
    {
        public int id = STARequest.GetInt("id", 0);
        public string name = STARequest.GetString("name");
        public TagInfo info = null;
        protected override void PageShow()
        {
            info = ConUtils.GetTag(id);

            if (info.Id <= 0)
            {
                PageInfo("参数有误");
            }

            seotitle = string.Format("{0} - {1}", info.Name + " | 标签", config.Webtitle);

            location += string.Format(location_format, " " + config.Locationseparator + " ", "", "标签:" + info.Name);
        }
    }
}
