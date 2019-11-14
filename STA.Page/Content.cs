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
    public class Content : PageBase
    {
        public int id = STARequest.GetQueryInt("id", 0);
        public int page = STARequest.GetQueryInt("page", 1);
        public ContentInfo info;

        public Content()
        {
            if (config.Updateclick == 1)
                Contents.UpdateContentClick(id, false);

            info = ConUtils.GetContent(id);
            if (info == null) return;

            //校验用户是否可以访问内容
            if (!ValidateUserPermission())
                return;

            seotitle = info.Seotitle == "" ? string.Format("{0} - {1}", info.Title, config.Webtitle) : info.Seotitle;
            seodescription = info.Seodescription == "" ? (info.Title + "," + seodescription) : info.Seodescription;
            seokeywords = info.Seokeywords == "" ? (info.Title + "," + seokeywords) : info.Seokeywords;
            base.SetContentLocation(info);
        }

        protected override void PageShow()
        {

        }


        #region 校验用户是否可以访问内容
        /// <summary>
        /// 校验用户是否可以访问内容
        /// </summary>
        /// <returns></returns>
        private bool ValidateUserPermission()
        {
            //如果放入回收站
            if (info.Orderid < -1000)
            {
                PageInfo("页面不存在", sitedir + "/", 1);
                return false;
            }

            ChannelInfo chlinfo = ConUtils.GetSimpleChannel(info.Channelid);
            if (chlinfo != null)
            {
                if (chlinfo.Id > 0)
                {
                    // 如果IP访问列表有设置则进行判断
                    if (chlinfo.Ipaccess.Trim() != "")
                    {
                        string[] regctrl = Utils.SplitString(chlinfo.Ipaccess, "\n");
                        if (!Utils.InIPArray(STARequest.GetIP(), regctrl))
                        {
                            PageInfo("抱歉, 您无法访问本页面", "/", 2);
                            return false;
                        }
                    }


                    // 如果IP访问列表有设置则进行判断
                    if (chlinfo.Ipdenyaccess.Trim() != "")
                    {
                        string[] regctrl = Utils.SplitString(chlinfo.Ipdenyaccess, "\n");
                        if (Utils.InIPArray(STARequest.GetIP(), regctrl))
                        {
                            PageInfo("抱歉, 您被禁止访问本页面", "#", 2);
                            return false;
                        }
                    }
                }
            }

            if (info.Status != 2)
            {
                PageInfo("该页面暂不能显示！");
                return false;
            }

            if (chlinfo != null && chlinfo.Viewcongroup != "")
            {
                if (oluser.Userid <= 0)
                {
                    PageInfo("您没有权限浏览此页面,现在为你转到登陆页...", "/login.aspx?returnurl=" + Utils.UrlEncode(STARequest.GetRawUrl()), 3);
                    return false;
                }

                else if (chlinfo.Viewcongroup.IndexOf("," + oluser.Groupid + ",") < 0 && oluser.Adminid == 0)
                {
                    PageInfo("您所在的用户组没有权限浏览此页面！", 3);
                    return false;
                }
            }

            if (info.Credits > 0)
            {
                if (userid <= 0)
                {
                    PageInfo(string.Format("查看该内容需要 {0} 积分,请先登录", info.Credits), sitedir + "/login.aspx?returnurl=" + Utils.UrlEncode(cururl));
                    return false;
                }
                else
                {
                    //登录用户积分处理逻辑
                }
            }

            return true;
        }
        #endregion

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
           
            if (config.Dynamiced == 0 && !ConUtils.IsDynamicedCon(config, id))
            {
                urlformat = sitedir + config.Htmlsavepath + info.Savepath + "/" + info.Filename + "_{0}" + config.Suffix;
                defurl = Urls.Content(id, info.Typeid, info.Savepath, info.Filename);
            }
            else
            {
                DataRow[] ctype = Caches.GetContypeTable(GeneralConfigs.GetConfig().Cacheinterval * 60).Select("id=" + info.Typeid.ToString());
                string ename = ctype[0]["ename"].ToString().Trim();
                if (config.Dynamiced == 2)
                {
                    urlformat = sitedir + "/" + ename + "-" + id.ToString() + "-{0}" + config.Rewritesuffix;
                    defurl = sitedir + "/" + ename + "-" + id.ToString() + config.Rewritesuffix;
                }
                else
                {
                    urlformat = string.Format(sitedir + "/{0}.aspx?id={1}&", ename, id) + "page={0}";
                    defurl = sitedir + "/" + ename + ".aspx?id=" + id.ToString();
                }
            }
            string pageContent, pagingName;
            paging = Utils.GetContentPageNumber(info.Content, page, defurl, urlformat, out pageContent, out pagingName);
            info.Content = pageContent;
            info.Title = pagingName == string.Empty ? info.Title : pagingName;
            return paging;
        }

        protected DataTable SpecConGroup()
        {
            return Contents.GetSpecgroups(info.Id);
        }

        /// <summary>
        /// 获取上一篇 下一篇
        /// </summary>
        /// <param name="self">是否本频道</param>
        /// <param name="type">prev上一篇</param>
        /// <returns></returns>
        protected ContentInfo PrevCon(bool self, string type)
        {
            DataTable dt = Caches.GetSimpleContentTable(GeneralConfigs.GetConfig().Cacheinterval * 60);
            DataRow[] drs = dt.Select(string.Format("id {0} {1} {2}", type == "prev" ? "<" : ">", id, (self && info.Channelid > 0) ? (" AND channelid = " + info.Channelid) : ""), "id " + ((type == "prev") ? "desc" : "asc"));
            if (drs.Length == 0) return null;
            ContentInfo cinfo = new ContentInfo();
            cinfo.Id = TypeParse.StrToInt(drs[0]["id"]);
            cinfo.Title = drs[0]["title"].ToString();
            cinfo.Img = drs[0]["img"].ToString();
            cinfo.Typeid = short.Parse(drs[0]["typeid"].ToString());
            cinfo.Channelid = TypeParse.StrToInt(drs[0]["channelid"]);
            cinfo.Filename = drs[0]["filename"].ToString();
            cinfo.Savepath = drs[0]["savepath"].ToString();
            return cinfo;
        }
    }
}
