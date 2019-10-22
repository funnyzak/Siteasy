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
    public partial class dbfieldreplace : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltbTables.AddTableData(Contents.GetAllTableName(), "name", "name", null);
            }
        }

        private void ltbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            ltbFields.Visible = true;
            DataTable fdt = Contents.GetTableField(ltbTables.SelectedValue);
            fdt.Columns.Add("ctext", Type.GetType("System.String"));
            foreach (DataRow dr in fdt.Rows)
            {

                if (dr["is_identity"].ToString().Trim() == "True")
                {
                    dr["ctext"] = dr["name"].ToString() + "(identity)";
                    txtPrimarykey.Text = dr["name"].ToString();
                }
                else
                {
                    dr["ctext"] = dr["name"].ToString();
                }

            }
            ltbFields.AddTableData(fdt, "ctext", "name", null);
        }

        private void Replace_Click(object sender, EventArgs e)
        {
            if (Utils.GetCookie("replacefield") != txtVcode.Text)
            {
                Message("安全验证码输入有误！");
                return;
            }

            string field = ltbFields.SelectedValue;
            int way = TypeParse.StrToInt(rblWay.SelectedValue, 1);
            string priamykey = txtPrimarykey.Text;

            if (field == "")
            {
                Message("请选择要替换的字段！", 2);
                return;
            }
            if (way == 2 && priamykey == "")
            {
                Message("正则替换，请设置主键字段！", 2);
                return;
            }

            Utils.ClearCookie("replacefield");
            string tablename = ltbTables.SelectedValue;
            string fieldname = ltbFields.SelectedValue;
            string msg = "";
            string source = txtSource.Text;
            string target = txtTarget.Text;
            if (way == 1)
            {
                msg = Databases.ReplaceTableField(tablename, txtWhere.Text, fieldname, source, target);
                if (msg.StartsWith("替换"))
                    Message(msg, 2);
                else
                    Message("替换失败:" + msg, 3);
            }
            else
            {
                try
                {
                    DataTable fdt = Databases.GetFieldListTable(tablename, txtWhere.Text, priamykey + "," + fieldname);
                    foreach (DataRow dr in fdt.Rows)
                    {
                        string fieldvalue = dr[fieldname].ToString();
                        string targetvalue = Regex.Replace(fieldvalue, source, target);
                        Databases.UpdateTableFieldByPrimaryKey(tablename, fieldname, targetvalue, priamykey, dr[priamykey].ToString());
                    }
                    Message("替换操作成功执行,操作行数 <b>" + fdt.Rows.Count.ToString() + "</b> 行！");
                }
                catch (Exception ex)
                {
                    Message(ex.Message.Replace("'", " ").Replace("\n", " ").Replace("\\", "/"));
                }
            }
            InsertLog(1, "字段批量替换", string.Format("表:{0},字段:{1},把:{2} 替换为:{3}", tablename, fieldname, source, target));

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
            this.Replace.Click += new EventHandler(this.Replace_Click);
        }

        #endregion
    }
}