using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;

namespace STA.Web.Admin
{
    public partial class voteitems : AdminPage
    {
        private int id = STARequest.GetQueryInt("id", 0);
        public VotetopicInfo cinfo;
        string action = STARequest.GetString("hidAction");
        protected void Page_Load(object sender, EventArgs e)
        {
            cinfo = Votes.GetVotetopic(id);
            if (cinfo == null) return;
            if (!IsPostBack)
            {
                BindData(1);
            }
            else if (IsPostBack && action != "")
            {
                switch (action)
                {
                    case "deloption":
                        int oid = STARequest.GetFormInt("hidValue", 0);
                        if (oid <= 0) return;
                        InsertLog(2, "删除投票选项", string.Format("ID:{0}", id.ToString()));
                        Votes.DelVoteoption(oid);
                        Message();
                        break;
                }
                hidAction.Value = "";
                BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { BindData(pageIndex); };
        }

        private void Del_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int cid = int.Parse(d);
                    if (cid == 0) continue;
                    Votes.DelVoteoption(cid);
                }
                BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
        }

        private void BindData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Votes.GetVoteoptionDataPage(pageIndex, TypeParse.StrToInt(ViewState["pagesize"], managelistcount), id, out pageCount, out recordCount);
            pGuide.PageCount = pageCount;
            pGuide.RecordCount = recordCount;
            ViewState["pageIndex"] = pGuide.PageIndex = pageIndex;
            rptData.Visible = true;
            rptData.DataSource = dt;
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
            this.DelBtn.Click += new EventHandler(this.Del_Click);
        }
        #endregion
    }
}