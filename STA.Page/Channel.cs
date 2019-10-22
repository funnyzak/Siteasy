using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using STA.Entity;
using STA.Common;
using STA.Core;
using STA.Data;
using STA.Config;

namespace STA.Page
{
    public class Channel : PageBase
    {
        public int id = STARequest.GetInt("id", 0);
        public int page = STARequest.GetInt("page", 1);
        public ChannelInfo info;

        public Channel()
        {
            info = Contents.GetChannel(id);
            if (info == null) return;

            //校验用户是否可以访问频道
            if (!ValidateUserPermission())
                return;

            seotitle = info.Seotitle == "" ? string.Format("{0} - {1}", info.Name, config.Webtitle) : info.Seotitle;
            seodescription = info.Seodescription == "" ? seodescription : info.Seodescription;
            seokeywords = info.Seokeywords == "" ? seokeywords : info.Seokeywords;
            location += GetChannelLocation(info.Id, "");
        }

        protected override void PageShow()
        {

        }

        #region 校验用户是否可以访问频道
        /// <summary>
        /// 校验用户是否可以访问频道
        /// </summary>
        /// <returns></returns>
        private bool ValidateUserPermission()
        {
            // 如果IP访问列表有设置则进行判断
            if (info.Ipaccess.Trim() != "")
            {
                string[] regctrl = Utils.SplitString(info.Ipaccess, "\n");
                if (!Utils.InIPArray(STARequest.GetIP(), regctrl))
                {
                    PageInfo("抱歉, 您无法访问本频道", "/", 2);
                    return false;
                }
            }


            // 如果IP访问列表有设置则进行判断
            if (info.Ipdenyaccess.Trim() != "")
            {
                string[] regctrl = Utils.SplitString(info.Ipdenyaccess, "\n");
                if (Utils.InIPArray(STARequest.GetIP(), regctrl))
                {
                    PageInfo("抱歉, 您被禁止访问本频道", "#", 2);
                    return false;
                }
            }

            if (info.Viewgroup != "")
            {
                if (oluser.Userid <= 0)
                {
                    PageInfo("您没有权限浏览此页面,现在为你转到登陆页...", "/login.aspx?returnurl=" + Utils.UrlEncode(STARequest.GetRawUrl()), 3);
                    return false;
                }

                else if (info.Viewgroup.IndexOf("," + oluser.Groupid + ",") < 0 && oluser.Adminid == 0)
                {
                    PageInfo("您所在的用户组没有权限浏览此页面！", 3);
                    return false;
                }
            }

            return true;
        }
        #endregion

        /// <summary>
        /// 内容分页
        /// </summary>
        /// <returns></returns>
        public string Paging()
        {
            string paging = string.Empty;
            string urlformat = string.Empty;
            string defurl = string.Empty;
            info.Filename = info.Filename == "" ? id.ToString() : info.Filename;

            if (config.Dynamiced == 0 && !ConUtils.IsDynamicedChl(config, id))
            {
                defurl = Urls.Channel(id, 1);
                urlformat = sitedir + config.Htmlsavepath + info.Savepath + "/" + info.Filename + "_{0}" + config.Suffix;
            }
            else
            {
                if (config.Dynamiced == 2)
                {
                    urlformat = sitedir + "/channel-" + id.ToString() + "-{0}" + config.Rewritesuffix;
                    defurl = sitedir + "/channel-" + id.ToString() + config.Rewritesuffix;
                }
                else
                {
                    urlformat += string.Format(sitedir + "/channel.aspx?id={0}&", id) + "page={0}";
                    defurl = string.Format(sitedir + "/channel.aspx?id={0}", id);
                }
            }

            string pageContent, pagingName;
            paging = Utils.GetContentPageNumber(info.Content, page, defurl, urlformat, out pageContent, out pagingName);
            info.Content = pageContent;
            info.Name = pagingName == string.Empty ? info.Name : pagingName;
            return paging;
        }

        public string Paging(int self, string fields, string orderBy, int pageSize, int pageN, out DataTable datas)
        {
            return Paging(self, fields, orderBy, pageSize, pageN, false, out datas);
        }

        public string Paging(int self, string fields, string orderBy, int pageSize, int pageN, bool showselect, out DataTable datas)
        {
            return Paging(self, fields, orderBy, pageSize, pageN, false, false, out datas);
        }
        /// <summary>
        /// 列表文档分页
        /// </summary>
        /// <param name="self">是否调用只调用本频道内容1是 0否</param>
        /// <param name="fields">字段</param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageN">显示页码</param>
        /// <param name="isext">是否调用扩展表</param>
        /// <param name="showselect">页面导航是否显示select</param>
        /// <param name="datas">返回结构集</param>
        /// <returns></returns>
        public string Paging(int self, string fields, string orderBy, int pageSize, int pageN, bool isext, bool showselect, out DataTable datas)
        {
            datas = new DataTable();
            //DataRow[] chl = Caches.GetChannelTable(GeneralConfigs.GetConfig().Cacheinterval * 60).Select("id=" + id.ToString());
            //if (chl.Length <= 0) return "";
            if (info == null) return "";

            string urlformat = siteurl;
            string defurl = siteurl;
            string path = info.Savepath.Trim();
            string filename = info.Filename.Trim();
            filename = filename == "" ? id.ToString() : filename;
            string listrule = info.Listrule.Trim();

            if (config.Dynamiced == 0)
            {
                defurl = Urls.Channel(id, 1);
                urlformat = sitedir + config.Htmlsavepath + Urls.ChlListRuleConver(listrule, path, id, "{0}") + config.Suffix;
            }
            else if (config.Dynamiced == 1)
            {
                urlformat = string.Format(sitedir + "/channel.aspx?id={0}&", id) + "page={0}";
                defurl += string.Format(sitedir + "/channel.aspx?id={0}", id);
            }
            else
            {
                urlformat = sitedir + "/channel-" + id.ToString() + "-{0}" + config.Rewritesuffix;
                defurl = sitedir + "/channel-" + id.ToString() + config.Rewritesuffix;
            }

            orderBy = orderBy == "" ? "addtime desc" : orderBy;
            page = page < 1 ? 1 : page;
            pageN = pageN < 3 ? 5 : pageN;

            string extname = "";
            if (isext)
            {
                DataRow[] tdrs = Caches.GetContypeTable(GeneralConfigs.GetConfig().Cacheinterval * 60).Select("id=" + info.Typeid.ToString());
                if (tdrs.Length > 0)
                {
                    extname = tdrs[0]["ename"].ToString().Trim();
                }
            }

            int pagecount, recordcount;
            datas = Contents.GetContentDataPage(fields, extname, page, pageSize, Contents.GetContentSearchCondition(-1, "", 0, id, self, 2, "", "", "", ""), orderBy, out pagecount, out recordcount);
            if (pagecount <= 1) return "";
            return Utils.GetDynamicPageNumber(defurl, urlformat, page, pagecount, recordcount, pageN, showselect).ToString();
        }

        /// <summary>
        /// 获取子频道
        /// </summary>
        /// <returns></returns>
        protected DataTable SubChlList()
        {
            return base.SubChlList(info.Id);
        }

    }
}
