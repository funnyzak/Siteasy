using System;
using System.Collections.Generic;

using STA.Common;
using STA.Config;
using STA.Core;
using STA.Plugin.Translator;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

namespace STA.Web.Admin
{
    public partial class otherset : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            txtSitemapconcount.Text = info.Sitemapconcount.ToString();
            txtOrdernorule.Text = info.Ordernorule;
            txtRssconcount.Text = info.Rssconcount.ToString();
            txtInvoicerate.Text = info.Invoicerate.ToString();
            txtOrderbackday.Text = info.Orderbackday.ToString();
            rblwithWeburl.SelectedValue = info.Withweburl.ToString();
            try
            {
                SetTranslatorPlugIn(HttpRuntime.BinDirectory);
            }
            catch
            {
                ddlTranslator.Items.Clear();
                try
                {
                    SetTranslatorPlugIn(Utils.GetMapPath("/bin/"));
                }
                catch
                {
                    ddlTranslator.Items.Add(new ListItem("Google语言翻译器", "STA.Plugin.Translate.GoogleTranslator,STA.Plugin.dll"));
                }
            }

            try
            {
                ddlTranslator.SelectedValue = info.TranPluginNameSpace + "," + info.TranDllFileName;
            }
            catch
            {
                throw new Exception("邮件配置出错！");
            }
        }

        public void SetTranslatorPlugIn(string filepath)
        {
            #region 获取翻译插件

            DirectoryInfo dirinfo = new DirectoryInfo(filepath);
            foreach (FileSystemInfo file in dirinfo.GetFileSystemInfos())
            {
                if (file != null)
                {
                    if (file.Extension.ToLower().Equals(".dll"))
                    {
                        try
                        {
                            Assembly a = Assembly.LoadFrom(HttpRuntime.BinDirectory + file);

                            foreach (Module m in a.GetModules())
                            {
                                foreach (Type t in m.FindTypes(Module.FilterTypeName, "*"))
                                {
                                    foreach (object arr in t.GetCustomAttributes(typeof(TranslatorAttribute), true))
                                    {
                                        TranslatorAttribute sea = (TranslatorAttribute)arr;
                                        ddlTranslator.Items.Add(new ListItem(sea.PlugInName, t.FullName + "," + sea.DllFileName));
                                    }
                                }
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }

            #endregion
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            info.Sitemapconcount = TypeParse.StrToInt(txtSitemapconcount.Text, 100);
            info.Sitemapconcount = info.Sitemapconcount < 1 ? 100 : info.Sitemapconcount;
            info.Rssconcount = TypeParse.StrToInt(txtRssconcount.Text, 50);
            info.Rssconcount = info.Rssconcount <= 0 ? 50 : info.Rssconcount;
            if (info.Withweburl.ToString() != rblwithWeburl.SelectedValue)
                Caches.RemoveUrlCache();
            info.Ordernorule = txtOrdernorule.Text;
            info.Invoicerate = TypeParse.StrToInt(txtInvoicerate.Text, 0);
            if (info.Invoicerate < 0 || info.Invoicerate > 100)
                info.Invoicerate = 0;
            info.Orderbackday = TypeParse.StrToInt(txtOrderbackday.Text, 0);
            if (info.Orderbackday < 1 || info.Orderbackday > 100)
                info.Orderbackday = 3;
            info.Withweburl = TypeParse.StrToInt(rblwithWeburl.SelectedValue, 1);

            info.TranPluginNameSpace = ddlTranslator.SelectedValue.Split(',')[0];
            info.TranDllFileName = ddlTranslator.SelectedValue.Split(',')[1];

            ConUtils.InsertLog(2, this.userid, this.username, this.admingroupid, this.admingroupname, STARequest.GetIP(), "其他选项", "");

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