using System;
using System.Collections.Generic;
using System.Text;

using STA.Data;
using STA.Common;
using STA.Config;
using STA.Entity.Plus;
using System.Data;
using STA.Core;

namespace STA.Page.Plus
{
    public class InfoSearch : PageBase
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int page = STARequest.GetInt("page", 1);
        /// <summary>
        /// 关键字
        /// </summary>
        public string query = Utils.UrlDecode(STARequest.GetString("query")).Trim();

        public int chnid = STARequest.GetInt("chnid", 0);
        /// <summary>
        /// 地区
        /// </summary>
        public string native = STARequest.GetString("native");
        /// <summary>
        /// 信息类型
        /// </summary>
        public string infotype = STARequest.GetString("infotype");


        protected override void PageShow()
        {

        }


        public string Paging(string fields, string orderBy, int pageSize, int pageN, bool showselect, out DataTable datas)
        {
            int pagecount, recordcount;
            string where = Contents.GetContentSearchCondition(5, "", 0, chnid, 0, 2, "", "", "", query);

            string infotypewhere = Globals.GetSqlWhereBySelectValue("infotype", TypeParse.StrToDecimal(infotype));
            string nativewhere = Globals.GetSqlWhereBySelectValue("nativeplace", TypeParse.StrToDecimal(native));
            if (infotypewhere != "")
                where += " and " + infotypewhere;
            if (nativewhere != "")
                where += " and" + nativewhere;

            datas = Contents.GetContentDataPage(fields, "info", page, pageSize, where, orderBy, out pagecount, out recordcount);
            string defurl = string.Format("{0}?native={1}&infotype={2}&chnid={3}&query={4}", cururl.Substring(0, cururl.IndexOf(".aspx") + 5), native, infotype, chnid, Utils.UrlEncode(query));
            string urlformat = defurl + "&page={0}";
            if (pagecount <= 1) return "";
            return Utils.GetDynamicPageNumber(defurl, urlformat, page, pagecount, recordcount, pageN, showselect).ToString();
        }

    }

}
