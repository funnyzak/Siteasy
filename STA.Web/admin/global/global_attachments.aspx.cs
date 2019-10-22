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
    public partial class attachments : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "delattachment":
                        DelAttachment(STARequest.GetFormInt("hidValue", 0));
                        Message();
                        break;
                }
                hidAction.Value = "";
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
            if (!IsPostBack)
            {
                txtPageSize.Text = managelistcount.ToString();
                txtStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                LoadData(1);
            }
            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { LoadData(pageIndex); };
        }

        private void LoadData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Contents.GetAttachmentDataPage("", pageIndex, TypeParse.StrToInt(ViewState["pagesize"], managelistcount), TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount);
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
                    DelAttachment(id);
                }
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
        }

        private void EmptyBtn_Click(object sender, EventArgs e)
        {
            if (!IsFounder(userid))
            {
                Message(base.notfoundermsg);
                return;
            }
            int pageCount = 100000, recordCount = 100000;
            DataTable cdt = Contents.GetAttachmentDataPage("", 1, 10000000, TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount);
            foreach (DataRow cdr in cdt.Rows)
            {
                FileUtil.DeleteFile(Utils.GetMapPath(cdr["filename"].ToString().Trim()));
                Contents.DelAttachment(TypeParse.StrToInt(cdr["id"]));
                InsertLog(2, "删除附件", string.Format("ID:{0},名称:{1}", cdr["id"].ToString(), cdr["attachment"].ToString()));
            }
            Message("已经全部删除！");
            LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
        }

        private void DelAttachment(int id)
        {
            ConUtils.DelAttachment(id, STARequest.GetFormString("path" + id.ToString()).Trim());
            InsertLog(2, "删除附件", string.Format("ID:{0}", id.ToString()));
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchcondition = Data.DatabaseProvider.GetInstance().GetAttachSearchCondition(txtStartDate.Text, txtEndDate.Text, txtUsers.Text.Trim().Replace(" ", ",").Replace("，", ","),
                                     txtFiletype.Text.Trim().Replace(" ", ",").Replace("，", ","), TypeParse.StrToInt(txtMinsize.Text, 0) * 1024, TypeParse.StrToInt(txtMaxsize.Text, 0) * 1024, txtKeywords.Text);
            ViewState["condition"] = searchcondition;
            ViewState["pagesize"] = txtPageSize.Text;
            LoadData(1);
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
            this.btnSearch.Click += new EventHandler(this.BtnSearch_Click);
            this.EmptyBtn.Click += new EventHandler(this.EmptyBtn_Click);
        }
        #endregion
    }
}