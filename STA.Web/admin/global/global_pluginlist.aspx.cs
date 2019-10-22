using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class pluginlist : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                int id = STARequest.GetFormInt("hidValue", 0);
                switch (action)
                {
                    case "delplugin":
                        Del(STARequest.GetFormInt("hidValue", 0));
                        Message("扩展已成功删除!");
                        break;
                    case "setup":
                        PluginInfo info = Plugins.GetPlugin(id);
                        if (info.Setup == 1)
                        {
                            Message("扩展已安装,无需重复安装！");
                            return;
                        }
                        bool success = Globals.InstallPlugin(info, false);
                        InsertLog(2, "安装扩展", string.Format("ID:{0},名称:{1}", id, STARequest.GetFormString("name" + id.ToString())));
                        Message(success ? "扩展安装完成!" : "扩展安装失败,请检测扩展包是否有误！");
                        break;
                    case "unsetup":
                        Globals.UnInstallPlugin(Plugins.GetPlugin(id));
                        InsertLog(2, "卸载扩展", string.Format("ID:{0},名称:{1}", id, STARequest.GetFormString("name" + id.ToString())));
                        Message("扩展卸载完成！");
                        break;
                }
                hidAction.Value = "";
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
            if (!IsPostBack)
                LoadData(1);
            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { LoadData(pageIndex); };
        }


        private void Del(int id)
        {
            if (id <= 0) return;

            Globals.UnInstallPlugin(Plugins.GetPlugin(id));
            Plugins.DelPlugin(id);
            InsertLog(2, "删除扩展", string.Format("ID:{0},名称:{1}", id, STARequest.GetFormString("name" + id.ToString())));
        }

        private void LoadData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Plugins.GetPluginDataPage(pageIndex, managelistcount, out pageCount, out recordCount);
            pGuide.PageCount = pageCount;
            pGuide.RecordCount = recordCount;
            ViewState["pageIndex"] = pGuide.PageIndex = pageIndex;
            rptData.Visible = true;
            rptData.DataSource = dt;
            rptData.DataBind();
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    Del(id);
                }
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
                Message();
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
            this.DelBtn.Click += new EventHandler(this.DelBtn_Click);
        }
        #endregion
    }
}