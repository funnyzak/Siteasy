using System;
using System.Collections.Generic;
using System.Text;

using STA.Data;
using STA.Common;
using STA.Config;
using STA.Core;
using STA.Entity;
using System.Data;
using System.Collections;
namespace STA.Page.User
{
    public class ContentAdd : UserBase
    {
        public int id = STARequest.GetInt("id", 0);
        public string action = STARequest.GetString("action");
        public int typeid = STARequest.GetInt("type", 1);
        public ContypeInfo ctinfo = null;
        public ContentInfo cinfo = null;
        //List<ContypefieldInfo> list = null;
        public string title = STARequest.GetString("title");
        public string author = STARequest.GetString("author");
        public int channelid = STARequest.GetInt("channelid", 0);
        public string tags = STARequest.GetString("tags");
        public string img = STARequest.GetString("img");
        public string content = STARequest.GetString("content");
        public string vcode = STARequest.GetString("vcode");
        private string cookiename = "staconadd";
        public ContentAdd()
        {
            seotitle = string.Format("{0}{1} - 用户管理中心 - {2}", action == "add" ? "发表" : "编辑", ctinfo.Name, config.Webtitle);
        }

        protected override void PageShow()
        {
            if (!IsLogin()) return;

            if (id > 0 && action != "add")
            {
                cinfo = ConUtils.GetContent(id);
            }

            if (cinfo != null && !ispost)
            {
                id = cinfo.Id;
                title = cinfo.Title;
                content = cinfo.Content;
                typeid = cinfo.Typeid;
                author = cinfo.Author;
                img = cinfo.Img;
                tags = ConUtils.DataTableToString(Contents.GetTagsByCid(id), 1, ",");
                channelid = cinfo.Channelid;
            }

            if (typeid <= 0) typeid = 1;
            ctinfo = ConUtils.GetSimpleContype(typeid);
            if (ctinfo == null)
            {
                ctinfo = new ContypeInfo();
                ctinfo.Id = 1;
                ctinfo.Name = "普通内容";
            }

            if (cinfo == null)
            {
                cinfo = new ContentInfo();
                action = "add";
            }

            if (!ConUtils.IsCrossSitePost())
            {
                SubmitContent();
            }
            //list = Contents.GetContypeFieldList(ctinfo.Id);
        }

        void SubmitContent()
        {
            if (vcode == "" || vcode.ToLower() != Utils.GetCookie(cookiename).ToLower())
            {
                AddErrLine("验证码输入有误!");
                return;
            }

            cinfo = CreateInfo();
            bool success = false;
            if (cinfo.Id <= 0)
            {
                cinfo.Id = Contents.AddContent(cinfo);
                success = cinfo.Id > 0;
            }
            else
            {
                success = Contents.EditContent(cinfo) > 0;
            }
            if (success)
            {
                Contents.EditContent(ConUtils.EditContentPath(cinfo));
                STA.Core.Publish.StaticCreate.CreateContent(cinfo.Id);
                Contents.AddTag(cinfo.Tags, cinfo.Id);
                PageInfo(string.Format("<b>{0}</b>已成功保存！{1}", cinfo.Title, cinfo.Status != 2 ? "请耐心等待管理员审核！" : ""), siteurl + "/" + ctinfo.Listmod + "?type=" + typeid.ToString());
            }
        }

        private ContentInfo CreateInfo()
        {
            #region 内容实体
            ChannelInfo chlinfo = ConUtils.GetSimpleChannel(channelid);
            if (chlinfo != null)
            {
                cinfo.Channelid = channelid;
                cinfo.Channelfamily = ConUtils.GetChannelFamily(channelid, ",");
                cinfo.Channelname = chlinfo.Name;
            }
            cinfo.Lastedituser = userid;
            cinfo.Updatetime = DateTime.Now;
            cinfo.Title = Utils.HtmlEncode(title);
            cinfo.Author = Utils.HtmlEncode(author);
            cinfo.Img = Utils.RemoveHtml(img);
            cinfo.Lasteditusername = username;
            cinfo.Tags = Utils.RemoveHtml(tags);
            cinfo.Content = Utils.RemoveUnsafeHtml(content);

            cinfo.Status = byte.Parse((user.Adminid > 0 ? 2 : 1).ToString());
            //viewgroup
            cinfo.Iscomment = 1;
            //cinfo.Credits = TypeParse.StrToInt(txtCredits.Text);
            if (cinfo.Id <= 0)
            {
                cinfo.Adduser = userid;
                cinfo.Addusername = username;
                cinfo.Typeid = byte.Parse(typeid.ToString());
            }

            ContypeInfo tyInfo = ConUtils.GetSimpleContype(typeid);
            cinfo.Typename = tyInfo.Name;
            //cinfo.Ext = new Hashtable();
            //foreach (string s in tyInfo.Fields.Split(','))
            //{
            //    cinfo.Ext.Add(s, STARequest.GetString(s));
            //}
            return cinfo;
            #endregion
        }

        public string ChlSelects(string id, int type, string selectedval)
        {
            DataTable dt = SubChlList(-1);
            dt = ConUtils.GetChlsForContribute(type, dt);
            SelectTreeList sel = new SelectTreeList(id, selectedval);
            return sel.BuildTree(dt, "name", "id");
        }
    }
}
