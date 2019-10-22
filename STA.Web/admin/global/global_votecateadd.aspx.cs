using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class votecateadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            VotecateInfo info = Votes.GetVotecate(id);
            if (info == null) return;

            txtName.Text = info.Name;
            txtEname.Text = info.Ename;
            txtOrderid.Text = info.Orderid.ToString();

            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
            #endregion
        }
        private VotecateInfo CreateInfo()
        {
            VotecateInfo info = new VotecateInfo();
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Name = txtName.Text;
            info.Ename = txtEname.Text;
            info.Orderid = TypeParse.StrToInt(txtOrderid.Text, 0);
            return info;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            VotecateInfo info = CreateInfo();
            if (hidAction.Value == "add")
                info.Id = Votes.AddVotecate(info);
            else
                Votes.EditVotecate(info);
            InsertLog(2, (hidAction.Value == "add" ? "添加" : "修改") + "投票分类", string.Format("ID:{0},分类名:{1}", info.Id, info.Name));
            Redirect("global_votecates.aspx?msg=" + string.Format("分类 <b>{0}</b> 已成功{1}！", info.Name, hidAction.Value == "add" ? "创建" : "修改"));
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