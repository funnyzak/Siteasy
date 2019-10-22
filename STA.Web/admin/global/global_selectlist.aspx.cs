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
    public partial class selectlist : AdminPage
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
                    case "update":
                        UpdateSelect(STARequest.GetFormInt("hidValue", 0));
                        Message();
                        break;
                }
                hidAction.Value = "";
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
            if (!IsPostBack)
            {
                //DataTable typedt = Selects.GetSelectTypeTable();
                //foreach (DataRow dr in typedt.Rows)
                //{
                //    if (dr["ename"].ToString().Trim() == STARequest.GetString("ename"))
                //        ViewState["sign"] = dr["issign"].ToString();

                //    dr["name"] = dr["name"].ToString() + " | " + dr["ename"].ToString();
                //}
                //ddlSelect.AddTableData(typedt, "name", "ename", "全部类型,");
                //ddlSelect.SelectedValue = STARequest.GetString("ename");

                DataTable sdt = Selects.GetSelectByWhere(Selects.GetSelectSearchCondition(STARequest.GetString("ename"), 0, 0));
                ddlCurrent.AddTableData(FormatTable(ConUtils.RemoveTableRow(sdt, "issign", "2")), "sname", "value", "请选择,-1");

                ViewState["condition"] = Selects.GetSelectSearchCondition(STARequest.GetString("ename"), 0, 0);
                LoadData(1);
            }
            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { LoadData(pageIndex); };
        }

        private void Del(int id)
        {
            if (id <= 0) return;

            if (Selects.DelSelect(id))
                InsertLog(2, "删除联动项", string.Format("ID:{0},名称:{1}", id, STARequest.GetFormString("name" + id.ToString())));
        }

        private void UpdateSelect(int id)
        {
            if (id <= 0) return;

            SelectInfo info = Selects.GetSelect(id);
            info.Name = STARequest.GetString("txtName" + id.ToString());
            info.Orderid = TypeParse.StrToInt(STARequest.GetString("txtOrderId" + id.ToString()));
            Selects.EditSelect(info);
        }

        private DataTable FormatTable(DataTable dt)
        {
            dt.Columns.Add("sname");
            dt.Columns.Add("slevel");

            foreach (DataRow dr in dt.Rows)
            {
                float value = TypeParse.StrToFloat(dr["value"].ToString().Trim(), 0);

                if (value % 500 == 0 || TypeParse.ObjToString(ViewState["sign"]) == "1")
                {
                    dr["sname"] = "";
                    dr["slevel"] = "1";
                }
                else if (value % 1 == 0)
                {
                    dr["sname"] = "└ ─";
                    dr["slevel"] = "2";
                }
                else
                {
                    dr["sname"] = "└ ─ ─";
                    dr["slevel"] = "3";
                }
                dr["sname"] += dr["name"].ToString();
            }

            return dt;
        }

        private void LoadData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = FormatTable(Selects.GetSelectDataPage(pageIndex, managelistcount, TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount));
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

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("id") != "")
            {
                string ids = STARequest.GetString("id");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    UpdateSelect(id);
                }
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
                Message();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            float cvalue = TypeParse.StrToFloat(ddlCurrent.SelectedValue, 0);
            double addrange = 0, basenum = 0;
            byte issign = 0;

            float maxvalue, minvalue;
            Globals.SelectRange(cvalue, out maxvalue, out minvalue);

            DataTable sdt = Selects.GetSelectByWhere(Selects.GetSelectSearchCondition(STARequest.GetString("ename"), maxvalue, minvalue));
            int count = sdt.Rows.Count;

            if (cvalue == -1)
            {
                addrange = 500;
                issign = 0;
                if (count > 0)
                    basenum = (TypeParse.StrToInt(sdt.Rows[count - 1]["value"].ToString()) / 500) * 500 + addrange;
                else
                    basenum = 0;
            }
            else if (cvalue % 500 == 0)
            {
                addrange = 1;
                issign = 1;
                if (count > 1)
                    basenum = TypeParse.StrToInt(sdt.Rows[count - 1]["value"].ToString()) / 1 + addrange;
                else
                    basenum = TypeParse.StrToInt(sdt.Rows[0]["value"].ToString()) + addrange;

            }
            else
            {
                addrange = 0.001;
                issign = 2;
                if (count > 1)
                    basenum = double.Parse(sdt.Rows[count - 1]["value"].ToString()) + addrange;
                else
                    basenum = double.Parse(sdt.Rows[0]["value"].ToString()) + addrange;
            }

            foreach (string s in txtNames.Text.Split(','))
            {
                SelectInfo info = new SelectInfo();
                info.Name = s;
                info.Value = basenum.ToString();
                if (issign == 2)
                    info.Value = basenum.ToString("0.000");
                info.Ename = STARequest.GetString("ename");
                info.Orderid = TypeParse.StrToInt(basenum);
                info.Issign = issign;
                Selects.AddSelect(info);
                basenum += addrange;
            }
            LoadData(1);
            Message();
        }

        private void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ename = ddlSelect.SelectedValue;
            if (ename == "")
                Redirect("global_selects.aspx");
            else
                Redirect("global_selectlist.aspx?ename=" + ename + "&name=" + ddlSelect.SelectedItem.Text.Split('|')[0]);
        }

        private void ddlCurrent_SelectedIndexChanged(object sender, EventArgs e)
        {
            float maxvalue, minvalue;
            Globals.SelectRange(TypeParse.StrToFloat(ddlCurrent.SelectedValue, 0), out maxvalue, out minvalue);
            ViewState["condition"] = Selects.GetSelectSearchCondition(STARequest.GetString("ename"), maxvalue, minvalue);
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
            this.SaveBtn.Click += new EventHandler(this.SaveBtn_Click);
            this.BtnAdd.Click += new EventHandler(this.BtnAdd_Click);
            this.ddlSelect.SelectedIndexChanged += new EventHandler(this.ddlSelect_SelectedIndexChanged);
            this.ddlCurrent.SelectedIndexChanged += new EventHandler(this.ddlCurrent_SelectedIndexChanged);
        }
        #endregion
    }
}