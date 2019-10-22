using System;
using System.Collections.Generic;

using STA.Common;
using STA.Config;
using STA.Core;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

namespace STA.Web.Admin
{
    public partial class baseset : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            rblWebBrowerModel.SelectedValue = info.Dynamiced.ToString();
            txtReWriteSuffix.Text = info.Rewritesuffix;
            txtCookiesName.Text = info.Domaincookie;
            txtTemplatesavedir.Text = info.Templatesavedirname;
            txtPageListNum.Text = info.Listinfocount.ToString();
            txtIndexLinkName.Text = info.Indexlinkname;
            txtLocationBlank.Text = info.Locationseparator;
            rblAdminlogs.SelectedValue = info.Adminlogs.ToString();
            ddlSuffix.SelectedValue = info.Suffix;
            txtHtmlSavePath.Text = info.Htmlsavepath;
            rblClosed.SelectedValue = info.Closed.ToString();
            txtClosereason.Text = info.Closedreason;
            rblWeblang.SelectedValue = info.Weblang;
            rblOpentran.SelectedValue = info.Opentran.ToString();

        }



        private void SaveInfo_Click(object sender, EventArgs e)
        {
            GeneralConfigInfo info = GeneralConfigs.GetConfig();

            //设置检测模版文件夹
            string templatedir = txtTemplatesavedir.Text.Trim();
            templatedir = templatedir == "" ? config.Templatesavedirname : templatedir;
            string currenttplpath = Utils.GetMapPath(baseconfig.Sitepath + "/templates/" + config.Templatename + "/" + info.Templatesavedirname);
            string targettplpath = Utils.GetMapPath(baseconfig.Sitepath + "/templates/" + config.Templatename + "/" + templatedir);
            if (!FileUtil.DirExists(currenttplpath) && !FileUtil.DirExists(targettplpath))
            {
                Message("当前模版文件夹不存在,请先设置与实际模版相同目录的文件夹名!", 3);
                return;
            }
            else
            {
                if (info.Templatesavedirname != templatedir)
                    FileUtil.MoveFolder(currenttplpath, targettplpath);
            }
            info.Opentran = TypeParse.StrToInt(rblOpentran.SelectedValue, 0);
            info.Weblang = rblWeblang.SelectedValue;
            info.Rewritesuffix = txtReWriteSuffix.Text;
            info.Dynamiced = TypeParse.StrToInt(rblWebBrowerModel.Text, 1);
            info.Domaincookie = txtCookiesName.Text;
            info.Suffix = ddlSuffix.SelectedValue;
            info.Templatesavedirname = templatedir;
            info.Listinfocount = TypeParse.StrToInt(txtPageListNum.Text, 20);
            if (info.Listinfocount <= 0) info.Listinfocount = 20;
            info.Indexlinkname = txtIndexLinkName.Text;
            info.Locationseparator = txtLocationBlank.Text;
            info.Adminlogs = TypeParse.StrToInt(rblAdminlogs.SelectedValue, 1);
            info.Htmlsavepath = txtHtmlSavePath.Text;
            
            info.Closed = TypeParse.StrToInt(rblClosed.SelectedValue, 1);
            info.Closedreason = txtClosereason.Text;

            ConUtils.InsertLog(2, this.userid, this.username, this.admingroupid, this.admingroupname, STARequest.GetIP(), "基本设置", "");

            //Urls.ReSetUrl();
            Emails.ReSetISmtpMail();
            Globals.CreateJsConfigFile("", info);

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