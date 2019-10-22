using System;
using System.Web;
using System.IO;
using System.Data;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;
using System.Xml;

namespace STA.Web.Admin
{
    public partial class templatelist : AdminPage
    {
        private string tplpath = Utils.GetMapPath("../../templates/");
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "deltpl":
                        DelTpl(STARequest.GetString("hidValue"));
                        Message();
                        break;
                    case "maketpl":
                        MakeTpl(STARequest.GetString("hidValue"));
                        Message("模版已全部生成！");
                        break;
                    case "usetpl":
                        string pathname = STARequest.GetFormString("name" + STARequest.GetString("hidValue"));
                        config.Templatename = pathname;
                        GeneralConfigs.SaveConfig(config);
                        break;
                }
                hidAction.Value = "";
                BuildData();
            }
            if (!IsPostBack)
                BuildData();
        }

        private void DelTpl(string id)
        {
            if (id == "" && id == "0") return;
            string pathname = STARequest.GetFormString("name" + id.ToString());
            string tplname = STARequest.GetFormString("tname" + id.ToString());
            if (pathname == "default") return;

            if (pathname == templatename)
            {
                config.Templatename = "default";
                GeneralConfigs.SaveConfig(config);
            }

            FileUtil.DeleteFolder(tplpath + pathname, true);
            InsertLog(1, "删除模版", string.Format("模版名：{0},路径:{1}", tplname, pathname));
        }

        private void MakeTpl(string id)
        {
            if (id == "") return;
            string pathname = STARequest.GetFormString("name" + id.ToString());
            string tplname = STARequest.GetFormString("tname" + id.ToString());
            Templates tpls = new Templates();
            tpls.MakeTplAspx(pathname);
            InsertLog(1, "生成模版", string.Format("模版名：{0},路径:{1}", tplname, pathname));
        }

        private void BuildData()
        {
            GeneralConfigs.ResetConfig();
            DataTable dt = Globals.BuildTemplates(tplpath);
            dt.Columns.Add("use");
            foreach (DataRow dr in dt.Rows)
            {
                dr["use"] = string.Format("<img src=\"../images/{0}.gif\" style=\"vertical-align:middle;\">", dr["pathname"].ToString() == GeneralConfigs.GetConfig().Templatename ? "tempuse" : "tempnouse");
            }
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
                    DelTpl(d);
                }
                BuildData();
                Message();
            }
        }

        private void MakeBtn_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    MakeTpl(d);
                }
                BuildData();
                Message("模版已全部生成！");
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
            this.MakeBtn.Click += new EventHandler(this.MakeBtn_Click);
        }
        #endregion
    }
}