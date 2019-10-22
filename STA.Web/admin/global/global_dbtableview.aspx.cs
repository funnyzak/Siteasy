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
    public partial class dbtableview : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tablestruct.Visible = false;
                ltbTables.AddTableData(Contents.GetAllTableName(), "name", "name", null);
            }
        }

        private void ltbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = Contents.GetTableField(ltbTables.SelectedValue);
            dt.Columns.Add("property", Type.GetType("System.String"));
            foreach (DataRow dr in dt.Rows)
            {
                string temp = dr["is_nullable"].ToString() == "True" ? "nullable" : "";
                temp += (temp != "" ? "," : "") + (dr["is_identity"].ToString() == "True" ? "identity" : "");
                dr["property"] = temp.EndsWith(",") ? temp.Substring(0, temp.Length - 1) : temp;
            }
            tablestruct.Visible = true;
            spantablename.InnerText = ltbTables.SelectedValue;
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
            this.ltbTables.SelectedIndexChanged += new EventHandler(this.ltbTables_SelectedIndexChanged);
        }

        #endregion
    }
}