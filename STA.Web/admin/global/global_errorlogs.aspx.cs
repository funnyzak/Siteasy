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
    public partial class errorlogs : AdminPage
    {
        private string logpath = Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/logs/error/");
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "dellogs":
                        Message(FileUtil.DeleteFile(logpath + STARequest.GetFormString("hidValue")));
                        break;
                }
                hidAction.Value = "";
                BuildData();
            }
            if (!IsPostBack)
            {
                BuildData();
            }
        }

        private void BuildData()
        {
            rptData.Visible = true;
            List<FileItem> list = FileUtil.GetFiles(logpath, "config", "err_");
            list.Sort(new FileCreateCompare());
            rptData.DataSource = list;
            rptData.DataBind();
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
        }
        #endregion
    }
}