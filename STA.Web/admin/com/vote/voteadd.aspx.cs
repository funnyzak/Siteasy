using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Config;
using STA.Core;
using STA.Data;
using STA.Entity.Plus;

namespace STA.Web.Admin.Plus
{
    public partial class voteadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndDate.Text = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd");

            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            VoteInfo info = STA.Data.Plus.GetStaVote(id);
            if (info == null) return;
            txtTitle.Text = info.Title;
            txtStartDate.Text = info.StartDate.ToString("yyyy-MM-dd");
            txtEndDate.Text = info.EndDate.ToString("yyyy-MM-dd");
            rblIsMore.SelectedValue = info.IsMore.ToString();
            rblIsView.SelectedValue = info.IsView.ToString();
            rblIsEnable.SelectedValue = info.IsEnable.ToString();
            txtInterval.Text = info.Interval.ToString();
            txtItems.Text = info.Items;
            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
            #endregion
        }
        private VoteInfo CreateInfo()
        {
            VoteInfo info = new VoteInfo();
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Title = txtTitle.Text;
            info.StartDate = TypeParse.StrToDateTime(txtStartDate.Text, DateTime.Now);
            info.EndDate = TypeParse.StrToDateTime(txtEndDate.Text, DateTime.Now.AddYears(20));
            info.IsMore = TypeParse.StrToInt(rblIsMore.SelectedValue, 0);
            info.IsView = TypeParse.StrToInt(rblIsView.SelectedValue, 1);
            info.IsEnable = TypeParse.StrToInt(rblIsEnable.SelectedValue, 1);
            info.Interval = TypeParse.StrToInt(txtInterval.Text, 0);
            info.Items = txtItems.Text;
            info.Items = info.ToString();
            return info;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            VoteInfo info = CreateInfo();
            if (hidAction.Value == "add")
                info.Id = STA.Data.Plus.AddStaVote(info);
            else
                STA.Data.Plus.EditStaVote(info);
            InsertLog(2, (hidAction.Value == "add" ? "添加" : "编辑") + "简易投票", string.Format("ID:{0},主题:{1}", info.Id, info.Title));
            Redirect("votelist.aspx?msg=" + string.Format("投票 <b>{0}</b> 已成功{1}！", info.Title, info.Id == 0 ? "创建" : "修改"));
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