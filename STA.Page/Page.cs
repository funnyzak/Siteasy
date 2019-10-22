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
    public class Page : PageBase
    {
        public int id = STARequest.GetInt("id", 0);
        public int page = STARequest.GetInt("page", 1);
        public PageInfo info;

        public Page()
        {
            info = Contents.GetPage(id);
            if (info == null) return;

            seotitle = info.Seotitle == "" ? string.Format("{0} - {1}", info.Name, config.Webtitle) : info.Seotitle;
            seodescription = info.Seodescription == "" ? seodescription : info.Seodescription;
            seokeywords = info.Seokeywords == "" ? seokeywords : info.Seokeywords;
            location += string.Format(location_format, config.Locationseparator + " ", Urls.Page(id), info.Name);
        }

        protected override void PageShow()
        {

        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <returns></returns>
        public string Paging()
        {
            string paging = string.Empty;
            string urlformat = string.Empty;
            string defurl = string.Empty;
            info.Filename = info.Filename == "" ? id.ToString() : info.Filename;

            if (config.Dynamiced == 0)
            {
                urlformat = sitedir + config.Htmlsavepath + info.Savepath + "/" + info.Filename + "_{0}" + config.Suffix;
                defurl = Urls.Page(config, id);
            }
            else if (config.Dynamiced == 1)
            {
                urlformat = string.Format(sitedir + "/page.aspx?id={0}&", id.ToString()) + "page={0}";
                defurl = sitedir + "/page.aspx?id=" + id.ToString();
            }
            else
            {
                urlformat = sitedir + "/page-" + id.ToString() + "-{0}" + config.Rewritesuffix;
                defurl = sitedir + "/page-" + id.ToString() + config.Rewritesuffix;
            }
            string pageContent, pagingName;
            paging = Utils.GetContentPageNumber(info.Content, page, defurl, urlformat, out pageContent, out pagingName);
            info.Content = pageContent;
            info.Name = pagingName == string.Empty ? info.Name : pagingName;
            return paging;
        }
    }
}
