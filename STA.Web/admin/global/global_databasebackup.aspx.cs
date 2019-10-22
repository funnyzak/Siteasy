using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;
using System.Text;

namespace STA.Web.Admin
{
    public partial class databasebackup : AdminPage
    {
        private static string backuppath = Utils.GetMapPath(BaseConfigs.GetSitePath + "/sta/data/db_back/");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Databases.IsBackupDatabase() == true)
            {
                if (!IsPostBack)
                {
                    LoadDataBaseConnectString();
                    BulidData();
                }
                else
                {
                    if (STARequest.GetString("hidAction") == "downback")
                    {
                        Utils.ResponseFile(backuppath + STARequest.GetString("hidValue") + ".config", STARequest.GetString("hidValue") + ".bak", "application/octet-stream");
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('您所使用的数据库不支持在线备份!');history.go(-1);</script>");
                Response.End();
            }
        }

        private void BulidData()
        {
            rptData.Visible = true;
            rptData.DataSource = FileUtil.GetFiles(backuppath, "config");
            rptData.DataBind();
        }

        private void LoadDataBaseConnectString()
        {
            #region 绑定数据库链接串信息
            foreach (string info in BaseConfigs.GetDBConnectString.Split(';'))
            {
                if (info.ToLower().IndexOf("data source") >= 0 || info.ToLower().IndexOf("server") >= 0)
                {
                    ServerName.Text = info.Split('=')[1].Trim();
                    continue;
                }
                if (info.ToLower().IndexOf("user id") >= 0 || info.ToLower().IndexOf("uid") >= 0)
                {
                    UserName.Text = info.Split('=')[1].Trim();
                    continue;
                }
                if (info.ToLower().IndexOf("password") >= 0 || info.ToLower().IndexOf("pwd") >= 0)
                {
                    Password.Text = info.Split('=')[1].Trim();
                    continue;
                }

                if (info.ToLower().IndexOf("initial catalog") >= 0 || info.ToLower().IndexOf("database") >= 0)
                {
                    strDbName.Text = info.Split('=')[1].Trim();
                    break;
                }
            }
            #endregion
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (!IsFounder(userid))
            {
                NoFounderMessage();
                return;
            }
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    FileUtil.DeleteFile(backuppath + d);
                    InsertLog(1, "删除数据备份文件", "文件名：" + d);
                }
                BulidData();
                Message();
            }
        }

        public bool BackUPDB(string ServerName, string UserName, string Password, string strDbName, string strFileName)
        {
            #region 数据库的备份的代码
            string message = Databases.BackUpDatabase(backuppath, ServerName, UserName, Password, strDbName, strFileName);
            if (message != "")
            {
                Message("备份数据库失败,原因:" + message + "!");
                return false;
            }
            return true;
            #endregion
        }

        public bool RestoreDB(string ServerName, string UserName, string Password, string strDbName, string strFileName)
        {
            #region 数据库的恢复的代码
            string message = Databases.RestoreDatabase(backuppath, ServerName, UserName, Password, strDbName, strFileName);
            if (message != string.Empty)
            {
                Message("恢复数据库失败,原因:" + message + "!");
                return false;
            }
            return true;
            #endregion
        }

        #region 异步建立备份或恢复的代理

        private delegate bool delegateBackUpDatabase(string ServerName, string UserName, string Password, string strDbName, string strFileName);

        //异步建立索引并进行填充的代理
        private delegateBackUpDatabase aysncallback;

        public void CallBack(IAsyncResult e)
        {
            aysncallback.EndInvoke(e);
        }
        #endregion

        private void BackBtn_Click(object sender, EventArgs e)
        {
            #region 开始备份数据
            if (!IsFounder(userid))
            {
                NoFounderMessage();
                return;
            }
            aysncallback = new delegateBackUpDatabase(BackUPDB);
            AsyncCallback myCallBack = new AsyncCallback(CallBack);
            string name = backupname.Text.Replace(" ", "_");
            name = name == "" ? Rand.RamTime() : name;
            aysncallback.BeginInvoke(ServerName.Text, UserName.Text, Password.Text, strDbName.Text, name, myCallBack, this.username); //
            InsertLog(1, "备份数据库", "文件名：" + name + ".bak");
            Message("当前操作可能需要执行一段时间，您可以先进行其它操作，稍后查看备份文件...", 20);
            #endregion
        }

        private void RestoreBtn_Click(object sender, EventArgs e)
        {

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
            this.BackBtn.Click += new EventHandler(this.BackBtn_Click);
            this.RestoreBtn.Click += new EventHandler(this.RestoreBtn_Click);
        }
        #endregion
    }
}