using System;
using System.Web;
using System.Data;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;


namespace STA.Web.Admin.Tools
{
    public partial class flinkclsadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LinktypeInfo ltinfo = Contents.GetLinkType(STARequest.GetInt("id", 0));
            if (ltinfo == null) return;
            txtName.Text = ltinfo.Name;
            txtOrderid.Text = ltinfo.Orderid.ToString();
            hidAction.Value = "edit";
            hidId.Value = ltinfo.Id.ToString();
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            LinktypeInfo ltinfo = new LinktypeInfo();
            ltinfo.Id = TypeParse.StrToInt(hidId.Value, 0);
            ltinfo.Name = txtName.Text;
            ltinfo.Orderid = TypeParse.StrToInt(txtOrderid.Text, 0);
            if (hidAction.Value == "add")
                ltinfo.Id = Contents.AddLinkType(ltinfo);
            else
                Contents.EditLinkType(ltinfo);
            InsertLog(2, "编辑链接分类", string.Format("ID:{0},名称:{1}", ltinfo.Id.ToString(), ltinfo.Name));
            FlushData();
        }

        private void FlushData()
        {
            base.RegisterStartupScript("cgscript", "window.parent.SubmitForm('flush');");
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            isShowSysMenu = false;
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
        }
        #endregion
    }
}