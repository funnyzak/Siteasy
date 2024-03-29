﻿using System;
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
    public partial class selects : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "delselect":
                        Del(STARequest.GetFormInt("hidValue", 0));
                        Message();
                        break;
                    case "makefile":
                        MakeCache(STARequest.GetFormInt("hidValue", 0));
                        Message();
                        break;
                }
                hidAction.Value = "";
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
            if (!IsPostBack)
            {
                LoadData(1);
            }
            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { LoadData(pageIndex); };
        }

        private void Del(int id)
        {
            if (id <= 0 || STARequest.GetFormInt("system" + id.ToString(), 0) == 1) return;

            if (Selects.DelSelectType(id) && Selects.DelSelectByEname(STARequest.GetFormString("ename" + id.ToString())))
                InsertLog(2, "删除联动类型", string.Format("ID:{0},名称:{1}", id, STARequest.GetFormString("name" + id.ToString()).Trim()));
        }

        private void LoadData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Selects.GetSelectTypeDataPage(pageIndex, managelistcount, out pageCount, out recordCount);
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

        private void MakeCache(int id)
        {
            if (id == 0) return;
            string ename = STARequest.GetFormString("ename" + id.ToString()).Trim();

            Globals.CreateSelectFile(ename, Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/data/select/"), Selects.GetSelectByWhere(Selects.GetSelectSearchCondition(ename, 0, 0)));
            InsertLog(2, "生成联动缓存文件", string.Format("ID:{0},名称:{1}", id.ToString(), STARequest.GetFormString("name" + id.ToString()).Trim()));
        }

        private void MakeFile_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    MakeCache(id);
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
            this.MakeFileBtn.Click += new EventHandler(this.MakeFile_Click);
        }
        #endregion
    }
}