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
    public partial class speciallist : AdminPage
    {
        //string action = STARequest.GetString("hidAction");
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        txtPageSize.Text = managelistcount.ToString();
        //        txtStartDate.Text = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
        //        txtEndDate.Text = DateTime.Now.ToShortDateString();
        //        ddlConType.BuildTree(Contents.GetChannelDataTable(), "name", "id");
        //        ddlConType.SelectedValue = STARequest.GetQueryInt("chlid", 0).ToString();
        //        RedirectMessage();

        //        txtKeywords.Text = STARequest.GetQueryString("query");

        //        ViewState["condition"] = Contents.GetContentSearchCondition(0, "", 0, STARequest.GetQueryInt("chlid", 0), -1, "", "", "", txtKeywords.Text);
        //        BindData(1);
        //    }
        //    else if (IsPostBack && action != "")
        //    {
        //        switch (action)
        //        {
        //            case "delcon":
        //                Del(STARequest.GetFormInt("hidValue", 0));
        //                Message();
        //                break;
        //        }
        //        hidAction.Value = "";
        //        BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
        //    }
        //    this.pGuide.OnPageChange += delegate(object send, int pageIndex) { BindData(pageIndex); };
        //}

        //private void Del(int id)
        //{
        //    if (id <= 0) return;
        //    ConUtils.DelContent(config, id, STARequest.GetFormInt("type" + id, 0));
        //    InsertLog(2, "删除专题", string.Format("ID:{0},名称:{1}", id.ToString(), STARequest.GetFormString("name" + id.ToString())));
        //}

        //private void BindData(int pageIndex)
        //{
        //    int pageCount, recordCount;
        //    DataTable dt = Contents.GetContentDataPage(pageIndex, TypeParse.StrToInt(ViewState["pagesize"], managelistcount), TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount);
        //    pGuide.PageCount = pageCount;
        //    pGuide.RecordCount = recordCount;
        //    ViewState["pageIndex"] = pGuide.PageIndex = pageIndex;
        //    rptData.Visible = true;
        //    rptData.DataSource = dt;
        //    rptData.DataBind();
        //}

        //private void Del_Click(object sender, EventArgs e)
        //{
        //    if (STARequest.GetString("cbid") != "")
        //    {
        //        string ids = STARequest.GetString("cbid");
        //        string[] idlist = ids.Split(',');
        //        foreach (string d in idlist)
        //        {
        //            int id = int.Parse(d);
        //            if (id == 0) continue;
        //            Del(id);
        //        }
        //        BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
        //        Message();
        //    }
        //}

        //private void MakeHtml_Click(object sender, EventArgs e)
        //{
        //    if (STARequest.GetString("cbid") != "")
        //    {
        //        string ids = STARequest.GetString("cbid");
        //        string[] idlist = ids.Split(',');
        //        foreach (string d in idlist)
        //        {
        //            int id = int.Parse(d);
        //            if (id == 0) continue;
        //            Core.Publish.StaticCreate.CreateSpecialWithGroup(id);
        //            InsertLog(2, "生成专题HTML", string.Format("ID:{0},名称:{1}", id.ToString(), STARequest.GetFormString("name" + id.ToString())));
        //        }
        //        BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
        //        Message("选择的文件已全部生成静态！");
        //    }
        //}

        //private void BtnSearch_Click(object sender, EventArgs e)
        //{
        //    string searchcondition = Contents.GetContentSearchCondition(0, txtUsers.Text, 0, TypeParse.StrToInt(ddlConType.SelectedValue, 0),
        //              TypeParse.StrToInt(ddlStatus.SelectedValue, -1), ddlProperty.SelectedValue, txtStartDate.Text, txtEndDate.Text, txtKeywords.Text);
        //    ViewState["condition"] = searchcondition;
        //    ViewState["pagesize"] = txtPageSize.Text;
        //    BindData(1);
        //}

        //#region Web 窗体设计器生成的代码
        //protected override void OnInit(EventArgs e)
        //{
        //    InitializeComponent();
        //    base.OnInit(e);
        //}

        //private void InitializeComponent()
        //{
        //    this.DelBtn.Click += new EventHandler(this.Del_Click);
        //    this.btnSearch.Click += new EventHandler(this.BtnSearch_Click);
        //    this.MakeHtmlBtn.Click += new EventHandler(this.MakeHtml_Click);
        //}
        //#endregion
    }
}