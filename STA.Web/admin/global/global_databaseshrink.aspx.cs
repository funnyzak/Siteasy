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
    public partial class databaseshrink : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Databases.IsShrinkData())
            {
                if (!Page.IsPostBack)
                {
                    strDbName.Text = Databases.GetDbName();
                }
            }
            else
            {
                Response.Write("<script>alert('您所使用的数据库不支持此功能');</script>");
                Response.Write("<script>history.go(-1)</script>");
                Response.End();
            }
        }

        public void ShrinkDateBase()
        {
            # region 收缩数据库函数
            string msg = ConUtils.ShrinkDataBase(strDbName.Text, size.Text);
            Message(msg == "yes" ? "数据日已成功收缩！" : msg);
            #endregion
        }


        private void ClearLog_Click(object sender, EventArgs e)
        {
            #region 清除数据日志

            if (this.CheckCookie())
            {
                if (!base.IsFounder(userid))
                {
                    NoFounderMessage();
                    return;
                }
                string msg = ConUtils.ClearDBLog(strDbName.Text);
                Message(msg == "yes" ? "数据日志已全部清除！" : msg);
            }

            #endregion
        }

        private void ShrinkDB_Click(object sender, EventArgs e)
        {
            #region 收缩数据库

            if (this.CheckCookie())
            {
                if (!base.IsFounder(userid))
                {
                    NoFounderMessage();
                    return;
                }

                Thread t = new Thread(new ThreadStart(ShrinkDateBase));
                t.Start();
                Message("此操作可能耗费一定时间，您可以先进行其他操作！", 5);
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
            this.ClearLog.Click += new EventHandler(this.ClearLog_Click);
            this.ShrinkDB.Click += new EventHandler(this.ShrinkDB_Click);
        }

        #endregion
    }
}