using System;
using System.Data;
using System.Text;

using STA.Common;
using STA.Core;
using STA.Data;
using STA.Config;

namespace STA.Page
{
    public class Search : PageBase
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int page = STARequest.GetInt("page", 1);
        /// <summary>
        /// 页容量
        /// </summary>
        public int persize = STARequest.GetInt("persize", 30);
        /// <summary>
        /// 频道ID
        /// </summary>
        public int chlid = STARequest.GetInt("chlid", 0);
        /// <summary>
        /// 模型ID
        /// </summary>
        public int typeid = STARequest.GetInt("typeid", -1);
        /// <summary>
        /// 缓存ID
        /// </summary>
        public int searchid = STARequest.GetInt("searchid", 0);
        /// <summary>
        /// 排序方式
        /// </summary>
        public int ordertype = STARequest.GetInt("ordertype", 1);
        /// <summary>
        /// 搜索几天内的内容
        /// </summary>
        public int durday = STARequest.GetInt("durdate", 0);
        /// <summary>
        /// 排序字段
        /// </summary>
        public int order = STARequest.GetInt("order", 0);
        /// <summary>
        /// 搜索类型
        /// </summary>
        public int searchtype = STARequest.GetInt("searchtype", 1);
        /// <summary>
        /// 关键字
        /// </summary>
        public string query = Utils.UrlDecode(STARequest.GetString("query")).Trim();

        /// <summary>
        /// 扩展表标识
        /// </summary>
        //public string ext = STARequest.GetString("ext");

        public string defurl = "";
        protected override void PageShow()
        {
            if (config.Opensearch != 1 && oluser.Adminid <= 0)
            {
                PageInfo("抱歉,搜索功能暂不开放使用", "back");
                return;
            }

            //if (Utils.GetStringLength(query) <= 2)
            //{
            //    PageInfo("关键字不能小于两个字节", "back");
            //}

            if (Utils.InArray(query, config.Forbidswords))
            {
                PageInfo(string.Format("“{0}”不是可搜索的关键字", query), "back");
                return;
            }

            if (config.Searchinterval > 0 && query != "")
            {
                int Interval = Utils.StrDateDiffSeconds(oluser.Lastsearchtime, config.Searchinterval);
                if (Interval < 0)
                {
                    PageInfo("系统规定搜索间隔为" + config.Searchinterval + "秒, 您还需要等待 " + (Interval * -1) + " 秒", "back");
                    return;
                }
                else
                {
                    oluser.Lastsearchtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    oluser = UserUtils.UpdateOnlineUserInfo(oluser, oluser.Expires);
                }

            }

            cururl = cururl.Substring(0, cururl.IndexOf(".aspx") + 5);

            seotitle = string.Format("关键字“{0}”搜索 - {1}", query, config.Webtitle);
            seokeywords += "," + query;
            location += string.Format(location_format, "&nbsp;" + config.Locationseparator + "&nbsp;", cururl, "站内搜索");

            if (persize < 1 || persize > 100) persize = 20;
            if (page < 1) page = 1;
        }


        public string Paging(string fields, int pageN, out DataTable condata, out int pagecount, out int recordcount)
        {
            condata = new DataTable();
            fields = fields == "" ? "*" : fields;
            pageN = pageN <= 1 ? 7 : pageN;
            pagecount = recordcount = 0;

            if (!ispost && query != "")
            {
                if (searchid <= 0)
                    searchid = Searchs.Search(chlid, query, durday, typeid, order, ordertype, searchtype);

                if (searchid > 0)
                {
                    condata = Searchs.GetSearchCacheList(fields, searchid, persize, page, order, ordertype, typeid, out recordcount, out pagecount);
                    string defurl = string.Format("{0}?searchtype={1}&order={2}&ordertype={3}&persize={4}&durdate={5}&typeid={6}&searchid={7}&chlid={8}&query={9}", cururl, searchtype, order, ordertype, persize, durday, typeid, searchid, chlid, Utils.UrlEncode(query));
                    string urlformat = defurl + "&page={0}";
                    if (pagecount <= 1) return "";
                    return Utils.GetDynamicPageNumber(defurl, urlformat, page, pagecount, recordcount, pageN, false).ToString();
                }
            }
            return "";
        }

        public string LightKeyWord(string str, string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return str;
            return str.Replace(keyword, "<font color=\"#ff0000\">" + keyword + "</font>");
        }

    }
}
