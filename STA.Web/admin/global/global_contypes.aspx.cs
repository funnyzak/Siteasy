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
    public partial class contypes : AdminPage
    {
        DataTable ndt = new DataTable();
        public string cinfo = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "delcontype":
                        ContypeInfo info = Contents.GetContype(STARequest.GetFormInt("hidValue", 0));
                        if (info.System == 1)
                        {
                            Message("系统模型不可以删除！");
                        }
                        else
                        {
                            Databases.DropTable(info.Extable);
                            InsertLog(2, "删除频道模型", string.Format("名称：{0}", info.Name));
                            Message(ConUtils.DelContype(STARequest.GetFormInt("hidValue", 0)));
                        }
                        break;
                }
                LoadData();
                hidAction.Value = "";
            }
            else if (!IsPostBack)
            {
                RedirectMessage();
                LoadData();
            }
        }

        #region 绑定数据
        private void LoadData()
        {
            DataTable dt = Contents.GetContypeDataTable();
            rptData.Visible = true;
            rptData.DataSource = dt;
            rptData.DataBind();
        }
        #endregion

        void SetConTypeOpen(byte open)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    ContypeInfo cinfo = Contents.GetContype(TypeParse.StrToInt(d));
                    if (cinfo == null) continue;
                    cinfo.Open = open;
                    Contents.EditContype(cinfo);
                    InsertLog(2, (open == 1 ? "开启" : "关闭") + "频道模型", string.Format("ID:{0},模型名称:{1}", d, STARequest.GetFormString("name" + d)));
                }
                LoadData();
                Message();
            }
        }

        void BtnOpen_Click(object sender, EventArgs e)
        {
            SetConTypeOpen(1);
        }

        void BtnClose_Click(object sender, EventArgs e)
        {
            SetConTypeOpen(0);
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.BtnClose.Click += new EventHandler(BtnClose_Click);
            this.BtnOpen.Click += new EventHandler(BtnOpen_Click);
        }
        #endregion
    }
}