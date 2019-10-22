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
    public partial class specialcontents : AdminPage
    {
        private int id = STARequest.GetQueryInt("id", 0);
        public ContentInfo cinfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            cinfo = Contents.GetShortContent(id);
            if (cinfo == null) return;
            if (!IsPostBack)
            {
                RedirectMessage();
                ddlGroups.AddTableData(Contents.GetSpecgroups(id), "name", "id");
                BindData(1);
            }
            hidAction.Value = "";
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
                    Contents.DelSpeccontent(id, cid);
                }
                BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
        }

        private void MoveBtn_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                if (ddlGroups.SelectedValue == "0")
                {
                    Message("请先选择内容组！");
                    return;
                }
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int cid = int.Parse(d);
                    if (cid == 0) continue;
                    Contents.EditSpeccontent(new SpecontentInfo(id, TypeParse.StrToInt(ddlGroups.SelectedValue, 0), cid));
                }
                BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
                Message();
            }
        }

        private void BindData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Contents.GetSpeccontentDataTable(pageIndex, TypeParse.StrToInt(ViewState["pagesize"], managelistcount), id, 0, TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount);
            pGuide.PageCount = pageCount;
            pGuide.RecordCount = recordCount;
            ViewState["pageIndex"] = pGuide.PageIndex = pageIndex;
            DataTable cgroupdt = Contents.GetSpeconids(id);
            dt.Columns.Add("groupid", Type.GetType("System.String"));
            foreach (DataRow dr in dt.Rows)
            {
                dr["groupid"] = cgroupdt.Select("contentid=" + dr["id"].ToString())[0]["groupid"].ToString();
            }
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
            this.MoveBtn.Click += new EventHandler(this.MoveBtn_Click);
        }
        #endregion
    }
}