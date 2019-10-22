using System;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin.Tools
{
    public partial class collectpre : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            int count = TypeParse.StrToInt(txtCount.Text, 0);
            if (count <= 0) count = 1000000000;
            if (STARequest.GetString("type") == "db")
            {
                DbcollectInfo dbinfo = Collects.GetDbCollect(STARequest.GetQueryInt("id", 0));
                if (dbinfo != null)
                {
                    DataTable cdt = Databases.DbCollectDataTableBySet(dbinfo, count);
                    if (cdt.Rows.Count > 0)
                    {
                        dbinfo.Addtime = DateTime.Now;
                        Collects.EditDbCollect(dbinfo);
                        STA.Core.Collect.DbCollect.dbinfo = dbinfo;
                        STA.Core.Collect.DbCollect.cltdt = cdt;
                        InsertLog(2, "数据库信息采集", string.Format("ID:{0},规则名:{1},采集数量：{2}", dbinfo.Id, dbinfo.Name, cdt.Rows.Count));
                        Redirect("statusprogress.aspx?type=数据库采集&action=dbcollect");
                    }
                    else
                    {
                        Message("没有查询到可采集的记录！");
                    }
                }
                else
                {
                    Message("采集规则不存在！");
                }
            }
            else
            {
                WebcollectInfo webinfo = Collects.GetWebCollect(STARequest.GetQueryInt("id", 0));
                if (webinfo != null)
                {
                    STA.Core.Collect.WebCollect.CollectPre(webinfo, count);
                    if (STA.Core.Collect.WebCollect.totalcount > 0)
                    {
                        webinfo.Addtime = DateTime.Now;
                        Collects.EditWebCollect(webinfo);
                        InsertLog(2, "站点信息采集", string.Format("ID:{0},规则名:{1},采集数量：{2}", webinfo.Id, webinfo.Name, STA.Core.Collect.WebCollect.totalcount));
                        Redirect("statusprogress.aspx?type=站点采集&action=webcollect");
                    }
                    else
                    {
                        Message("没有可采集的页面！");
                    }
                }
                else
                {
                    Message("采集规则不存在！");
                }
            }

        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            isShowSysMenu = false;
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
        }
        #endregion
    }
}