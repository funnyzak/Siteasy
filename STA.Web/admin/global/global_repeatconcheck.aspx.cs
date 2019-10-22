using System;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;
using System.Threading;

namespace STA.Web.Admin
{
    public partial class repeatconcheck : AdminPage
    {
        string action = STARequest.GetString("hidAction");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                uirecords.Visible = false;
                DataTable cdt = ConUtils.RemoveTableRow(Contents.GetContypeDataTable(), "id", "0");

                ddlConType.AddTableData(ConUtils.RemoveTableRow(cdt, "open", "0"), "name", "id", "");
            }
            else if (IsPostBack && action != "")
            {
                switch (action)
                {
                    case "delcon":
                        int id = STARequest.GetFormInt("hidValue", 0);
                        InsertLog(2, "删除重复文档", string.Format("ID:{0},名称:{1}", id.ToString(), STARequest.GetFormString("name" + id.ToString())));
                        Message(Contents.PutContentRecycle(id));
                        break;
                }
                hidAction.Value = "";
                CheckRepepeat();
            }
        }

        private void Del_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    Contents.PutContentRecycle(id);
                    InsertLog(2, "删除重复文档", string.Format("ID:{0},名称:{1}", id.ToString(), STARequest.GetFormString("name" + id.ToString())));
                }
                Message();
            }
        }

        private void CheckCon_Click(object sender, EventArgs e)
        {
            if (CheckRepepeat() == 0)
                Message(ddlConType.SelectedItem.Text + "里没有重复的文档！", 2);
        }

        private int CheckRepepeat()
        {
            int typeid = TypeParse.StrToInt(ddlConType.SelectedValue, 1);
            DataTable dt = Contents.RepepeatConTitleCheck(typeid);
            if (dt == null || dt.Rows.Count == 0)
            {
                uirecords.Visible = false;
            }
            else
            {
                uirecords.Visible = true;
                rptData.Visible = true;
                rptData.DataSource = dt;
                rptData.DataBind();
            }
            return dt == null ? 0 : dt.Rows.Count;
        }

        #region Web 窗体设计器生成的代码

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.CheckCon.Click += new EventHandler(this.CheckCon_Click);
            this.DelBtn.Click += new EventHandler(this.Del_Click);
        }

        #endregion
    }
}