using System;
using System.Xml;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin.Tools
{
    public partial class likeset : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            try
            {
                List<FileItem> flist = FileUtil.GetFiles(Utils.GetMapPath("../themes"), "xml");
                foreach (FileItem fitem in flist)
                {
                    XmlDocument doc = XMLUtil.LoadDocument(fitem.FullName);
                    ddlTheme.Items.Add(new ListItem(doc.SelectSingleNode("Theme/themename").InnerText, doc.SelectSingleNode("Theme/ename").InnerText));
                }
            }
            catch
            {
                ddlTheme.Items.Add(new ListItem("简单灰", "grey"));
            }
            LikesetInfo info = ConUtils.GetLikeset(userid);
            hidtheme.Value = info.Systemstyle;
            ddlTheme.SelectedValue = info.Systemstyle;
            rblTipMsg.SelectedValue = info.Msgtip.ToString();
            txtManageCount.Text = info.Managelistcount.ToString();
            txtOraylay.Text = info.Overlay.ToString();
            txtFastmenucount.Text = info.Fastmenucount.ToString();
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            LikesetInfo info = new LikesetInfo();
            info.Uid = userid;
            info.Systemstyle = ddlTheme.SelectedValue;
            info.Msgtip = TypeParse.StrToInt(rblTipMsg.SelectedValue, 1);
            info.Managelistcount = TypeParse.StrToInt(txtManageCount.Text);
            info.Managelistcount = info.Managelistcount <= 0 ? 20 : info.Managelistcount;
            info.Overlay = TypeParse.StrToInt(txtOraylay.Text, 60);
            info.Overlay = info.Overlay < 0 || info.Overlay > 100 ? 60 : info.Overlay;
            info.Fastmenucount = TypeParse.StrToInt(txtFastmenucount.Text, 15);
            info.Fastmenucount = info.Fastmenucount < 0 || info.Fastmenucount > 15 ? 15 : info.Fastmenucount;
            ConUtils.UpdateLikeset(info);
            Caches.RemoveSetCache();
            if (hidtheme.Value != info.Systemstyle)
                RegisterClientScriptBlock("top.location.href = top.location.href;");
            else
                Message("您的偏好设置已经生效！");
        }

        private void UpdatePwd_Click(object sender, EventArgs e)
        {
            string pwd = txtPwd.Text;
            string npwd = txtNpwd.Text;
            string npwd2 = txtNpwd2.Text;
            UserInfo uinfo = Users.GetUser(userid);
            if (uinfo != null)
            {
                if (uinfo.Password == Utils.MD5(pwd))
                {
                    if (npwd != npwd2)
                    {
                        Message("修改失败,两次输入的新密码不相同！", 2);
                    }
                    else
                    {
                        uinfo.Password = Utils.MD5(npwd2);
                        Users.EditUser(uinfo);
                        Message("密码已成功修改,下次请使用新密码登录！", 2);
                    }
                }
                else
                {
                    Message("修改失败,原密码输入有误！");
                }
            }
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
            this.UpdatePwd.Click += new EventHandler(this.UpdatePwd_Click);
        }
        #endregion
    }
}