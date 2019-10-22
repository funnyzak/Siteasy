﻿using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

using STA.Common;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin.Plus
{
    public partial class votelist : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "delvote":
                        Del(STARequest.GetFormInt("hidValue", 0));
                        Message();
                        break;
                }
                hidAction.Value = "";
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
            if (!IsPostBack)
            {
                LoadData(1);
                RedirectMessage();
            }

            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { LoadData(pageIndex); };
        }

        private void Del(int id)
        {
            if (id <= 0) return;
            STA.Data.Plus.DelStaVote(id);
            InsertLog(2, "删除简易投票", string.Format("ID:{0},主题:{1}", id.ToString(), STARequest.GetFormString("name" + id.ToString())));
        }

        private void LoadData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = STA.Data.Plus.GetStaVoteTable(pageIndex, "", out pageCount, out recordCount);

            dt.Columns.Add("totalcount");
            foreach (DataRow dr in dt.Rows)
            {
                dr["totalcount"] = STA.Data.Plus.GetStaVote(TypeParse.StrToInt(dr["id"], 0)).VoteTotalCount.ToString();
            }

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