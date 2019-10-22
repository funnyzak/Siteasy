using System;
using System.Web;
using System.Data;
using System.Text;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;
namespace STA.Web.Admin
{
    public partial class runsql : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void ExecuteSql_Click(object sender, EventArgs e)
        {
            #region 运行指定的SQL语句
            if (!IsFounder(userid))
            {
                Message(base.notfoundermsg);
                return;
            }
            tbdata.InnerHtml = "";
            BtnExport.Visible = false;
            ViewState["ssql"] = "";

            string sqlText = txtSqlString.Text;
            if (sqlText == string.Empty)
            {
                Message("请您输入SQL语句!");
                return;
            }
            string msg = Databases.RunSql(sqlText);
            if (msg != string.Empty)
            {
                Message(msg);
                return;
            }
            if (sqlText.ToUpper().StartsWith("SELECT") || sqlText.ToUpper().StartsWith("--SELECT"))
            {
                try
                {
                    DataTable dt = DbHelper.ExecuteDataset(sqlText).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        BtnExport.Visible = true;
                        ViewState["ssql"] = sqlText;

                        StringBuilder sbtr = new StringBuilder();
                        sbtr.Append("<div class=\"bar\">\r\n");
                        sbtr.Append("&nbsp;&nbsp;查询数据结果:" + dt.Rows.Count.ToString() + "行</div>\r\n");
                        sbtr.Append(" <div class=\"con\">\r\n<table class=\"list\">\r\n<tr>\r\n");
                        foreach (DataColumn col in dt.Columns)
                        {
                            sbtr.Append("<th>" + col.ColumnName.ToLower() + "</th>\r\n");
                        }
                        sbtr.Append("</tr>\r\n");

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sbtr.Append("<tr>\r\n");
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                sbtr.Append("<td>" + dt.Rows[i][j] + "</td>\r\n");
                            }
                            sbtr.Append("</tr>");
                        }
                        sbtr.Append("</table></div></div>");
                        tbdata.InnerHtml = sbtr.ToString();
                    }
                    else
                    {
                        Message("没有查到符合条件的记录！");
                    }
                }
                catch
                {
                    Message("执行查询失败！");
                    return;
                }
            }
            else
            {
                Message("您输入的SQL语句，已成功执行！");
            }
            ConUtils.InsertLog(1, this.userid, this.username, this.admingroupid, this.admingroupname, STARequest.GetIP(), "执行SQL语句", sqlText);
            #endregion
        }

        void BtnExport_Click(object sender, EventArgs e)
        {
            if (TypeParse.ObjToString(ViewState["ssql"]) != "")
            {
                Utils.CreateExecl(DbHelper.ExecuteDataset(ViewState["ssql"].ToString()).Tables[0], "查询结果", "查询结果导出_" + Rand.RamTime());
            }
            else
            {
                Message("没有可导出的数据;");
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
            this.ExecuteSql.Click += new EventHandler(this.ExecuteSql_Click);
            this.BtnExport.Click += new EventHandler(BtnExport_Click);
        }
        #endregion
    }
}