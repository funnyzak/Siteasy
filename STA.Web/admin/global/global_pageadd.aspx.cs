using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;

namespace STA.Web.Admin
{
    public partial class pageadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            txtSavePath.Text = "/page";
            txtFileName.Text = Rand.Number(9);
            DataTable dt = Contents.GetPageLikeIdList();
            foreach (DataRow dr in dt.Rows)
                likeidlist.InnerText += dr["alikeid"].ToString().Trim() + ",";
            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            PageInfo info = Contents.GetPage(id);
            if (info == null) return;
            txtName.Text = info.Name;
            txtAlikeid.Text = info.Alikeid;
            txtPageTitle.Text = info.Seotitle;
            txtDescription.Text = info.Seodescription;
            txtKeywords.Text = info.Seokeywords;
            txtSavePath.Text = info.Savepath;
            txtFileName.Text = info.Filename;
            txtTemplate.Text = info.Template;
            txtOrderid.Text = info.Orderid.ToString();
            txtContent.Text = info.Content;
            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
            #endregion
        }
        private PageInfo CreateInfo()
        {
            PageInfo info = new PageInfo();
            if (hidAction.Value == "edit")
                info = Contents.GetPage(TypeParse.StrToInt(hidId.Value, 0));
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Name = txtName.Text;
            info.Orderid = TypeParse.StrToInt(txtOrderid.Text, 0);
            info.Alikeid = txtAlikeid.Text;
            info.Seotitle = txtPageTitle.Text;
            info.Seokeywords = txtKeywords.Text;
            info.Seodescription = txtDescription.Text;
            info.Savepath = txtSavePath.Text;
            info.Filename = txtFileName.Text;

            //info.Savepath = info.Savepath == "" ? config.Htmlsavepath + "/page" : info.Savepath;
            info.Filename = info.Filename == "" ? Rand.Number(9) : info.Filename;

            info.Template = txtTemplate.Text;
            info.Content = txtContent.Text;
            if (cbIsRemote.Checked && info.Content.Trim() != string.Empty)
                info.Content = STA.Core.Collect.RemoteFile.Remote(info.Content, txtResourceFType.Text.Split(',', '，', ' '), sitepath + filesavepath + "/remote/", config.Colfilestorage == 1);
            if (cbIsFiterDangerousCode.Checked && info.Content.Trim() != string.Empty)
                info.Content = Utils.RemoveUnsafeHtml(info.Content);
            if (cbFiltertags.Checked && txtFiltertags.Text.Trim() != "")
                info.Content = ConUtils.FilterHtmlTags(info.Content, txtFiltertags.Text.Trim());
            return info;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            PageInfo info = CreateInfo();
            if (hidAction.Value == "add")
                info.Id = Contents.AddPage(info);
            else
                Contents.EditPage(info);
            STA.Core.Publish.StaticCreate.CreatePage(info.Id);
            InsertLog(2, (hidAction.Value == "add" ? "添加" : "编辑") + "单页", string.Format("ID:{0},名称:{1}", info.Id, info.Name));
            Redirect("global_pagelist.aspx");
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
        }
        #endregion
    }
}