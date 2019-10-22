using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;
using STA.Plugin.Mail;
using System.Text.RegularExpressions;

namespace STA.Web.Admin
{
    public partial class attachmentset : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            rblAttachsaveway.SelectedValue = info.Attachsaveway.ToString();
            rblAttachnameway.SelectedValue = info.Attachnameway.ToString();
            txtAttachsavepath.Text = info.Attachsavepath;
            txtAttachimgtype.Text = info.Attachimgtype;
            txtThumbsize.Text = info.Thumbsize;
            txtAttachbigfilepath.Text = info.Attachbigfilepath;
            txtAttachimgmaxsize.Text = info.Attachimgmaxsize.ToString();
            txtAttachmediatype.Text = info.Attachmediatype;
            txtAttachmediamaxsize.Text = info.Attachmediamaxsize.ToString();
            txtAttachsoftmaxsize.Text = info.Attachsoftmaxsize.ToString();
            txtAttachsofttype.Text = info.Attachsofttype;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(txtAttachsavepath.Text, @"^(\/\w+)+$"))
            {
                Message("图片/上传文件保存路径设置有误！");
                return;
            }

            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            info.Thumbsize = txtThumbsize.Text;
            if (info.Thumbsize != "0" && (info.Thumbsize.Split(',').Length != 2 || !Utils.IsNumericList(info.Thumbsize))) info.Thumbsize = "240,180";
            info.Attachsaveway = TypeParse.StrToInt(rblAttachsaveway.SelectedValue, 2);
            info.Attachnameway = TypeParse.StrToInt(rblAttachnameway.SelectedValue, 2);
            info.Attachsavepath = txtAttachsavepath.Text;
            info.Attachimgtype = txtAttachimgtype.Text;
            info.Attachbigfilepath = txtAttachbigfilepath.Text;
            info.Attachimgmaxsize = TypeParse.StrToInt(txtAttachimgmaxsize.Text, 2);
            info.Attachmediatype = txtAttachmediatype.Text;
            info.Attachmediamaxsize = TypeParse.StrToInt(txtAttachmediamaxsize.Text, 10);
            info.Attachsofttype = txtAttachsofttype.Text;
            info.Attachsoftmaxsize = TypeParse.StrToInt(txtAttachsofttype.Text, 10);
            Message(GeneralConfigs.SaveConfig(info));
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