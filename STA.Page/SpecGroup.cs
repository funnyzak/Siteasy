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
    public class SpecGroup : PageBase
    {
        public int id = STARequest.GetInt("id", 0);
        public int group = STARequest.GetInt("group", 0);
        public ContentInfo info = null;
        public string groupname = "";
        protected override void PageShow()
        {
            info = Contents.GetShortContent(id);

            if (info == null) return;

            DataRow[] drs = Contents.GetSpecgroups(id).Select("id=" + group.ToString());
            if (drs.Length == 0) return;

            groupname = drs[0]["name"].ToString().Trim();

            seotitle = string.Format("{0} - {1}", groupname + " | " + info.Title, config.Webtitle);
            seodescription = info.Seodescription == "" ? seodescription : info.Seodescription;
            seokeywords = (info.Seokeywords == "" ? seokeywords : info.Seokeywords) + "," + groupname;

            base.SetContentLocation(info);
            location += string.Format(location_format, " " + config.Locationseparator + " ", Urls.Content(config, info.Id, info.Typeid, info.Savepath, info.Filename), info.Title);
        }
    }
}
