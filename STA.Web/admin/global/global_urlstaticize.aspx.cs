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
    public partial class urlstaticize : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "del":
                        Del(STARequest.GetFormInt("hidValue", 0));
                        Message();
                        break;
                    case "make":
                        Make(STARequest.GetFormInt("hidValue", 0));
                        Message();
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

            Globals.UrlStaticizeDelete(Contents.GetUrlStaticize(id));
            InsertLog(2, "删除URL静态化", string.Format("ID:{0},名称:{1}", id, STARequest.GetFormString("name" + id.ToString())));
        }

        private void Make(int id)
        {
            if (id <= 0) return;
            Globals.UrlStaticizeCreate(Contents.GetUrlStaticize(id));
            InsertLog(2, "URL静态化生成", string.Format("ID:{0},名称:{1}", id.ToString(), STARequest.GetFormString("name" + id.ToString())));
        }

        private void LoadData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Contents.GetUrlStaticizeDataPage(pageIndex, managelistcount, out pageCount, out recordCount);
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

        private void MakeHtml_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    Make(int.Parse(d));
                }
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
                Message("选择的已全部生成静态！");
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
            this.MakeHtmlBtn.Click += new EventHandler(this.MakeHtml_Click);
        }
        #endregion
    }
}