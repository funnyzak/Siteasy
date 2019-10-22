using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class congroupadd : AdminPage
    {
        public string gname = "内容";
        private int type = STARequest.GetInt("type", 0);
        protected void Page_Load(object sender, EventArgs e)
        {
            isShowSysMenu = false;
            if (type == 1) gname = "频道";
            if (IsPostBack) return;
            hidAction.Value = "add";
            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            CongroupInfo info = Contents.GetConGroup(id);
            if (info == null) return;
            txtName.Text = info.Name;
            txtDescText.Text = info.Desctext;
            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
            #endregion
        }
        private CongroupInfo CreateInfo()
        {
            CongroupInfo info = new CongroupInfo();
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Type = byte.Parse(type.ToString());
            info.Name = txtName.Text;
            info.Desctext = txtDescText.Text;
            return info;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            if (txtName.Text == string.Empty) return;
            CongroupInfo info = CreateInfo();
            if (hidAction.Value == "add")
                Contents.AddConGroup(info);
            else
                Contents.EditConGroup(info);
            string script = "<script>window.parent.location.href=window.parent.location.href; </script>";
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "cgscript", script);
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
        }
        #endregion
    }
}