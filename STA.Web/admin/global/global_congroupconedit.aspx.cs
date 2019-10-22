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
    public partial class congroupconedit : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "del":
                        Message(Contents.DelGroupCon(STARequest.GetFormInt("hidValue", 0)));
                        break;
                }
                hidAction.Value = "";
                BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
            if (!IsPostBack)
            {
                RedirectMessage();
                BindData(1);
            }
            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { BindData(pageIndex); };
        }

        private void BindData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Contents.GetConGroupContentDataPage(STARequest.GetInt("id", 0), "id,channelname,addtime,title,typeid", pageIndex, managelistcount, out pageCount, out recordCount);
            pGuide.PageCount = pageCount;
            pGuide.RecordCount = recordCount;
            ViewState["pageIndex"] = pGuide.PageIndex = pageIndex;
            rptData.Visible = true;
            rptData.DataSource = dt;
            rptData.DataBind();
        }

        private void DelContent_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    Contents.DelGroupCon(id);
                }
                BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
        }

        private void SubmitEdit_Click(object sender, EventArgs e)
        {
            #region 编辑排序
            if (STARequest.GetString("hidid") != "")
            {
                string ids = STARequest.GetString("hidid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    Contents.EditGroupCon(id, TypeParse.StrToInt(STARequest.GetString("txtOrderId" + d)));
                }
                BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
                Message("您提交的修改，已生效！");
            }
            #endregion
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SubmitEdit.Click += new EventHandler(this.SubmitEdit_Click);
            this.DelContentBtn.Click += new EventHandler(this.DelContent_Click);
        }
        #endregion
    }
}