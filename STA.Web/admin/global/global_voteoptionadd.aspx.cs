using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;

namespace STA.Web.Admin
{
    public partial class voteoptionadd : AdminPage
    {
        private int tid = STARequest.GetQueryInt("tid", 0);
        public VotetopicInfo cinfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            cinfo = Votes.GetVotetopic(tid);
            if (cinfo == null) return;

            if (IsPostBack) return;
            txtTopicname.Text = cinfo.Name;
            hidAction.Value = "add";
            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            VoteoptionInfo info = Votes.GetVoteoption(id);
            if (info == null) return;

            txtName.Text = info.Name;
            txtDesc.Text = info.Desc;
            txtOrderid.Text = info.Orderid.ToString();
            txtImg.Text = info.Img;
            txtCount.Text = info.Count.ToString();
            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
            #endregion
        }
        private VoteoptionInfo CreateInfo()
        {
            VoteoptionInfo info = new VoteoptionInfo();
            info.Topicid = cinfo.Id;
            info.Topicname = cinfo.Name;
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Name = txtName.Text;
            info.Desc = txtDesc.Text;
            info.Img = txtImg.Text;
            info.Count = TypeParse.StrToInt(txtCount.Text, 0);
            info.Orderid = TypeParse.StrToInt(txtOrderid.Text, 0);
            return info;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            VoteoptionInfo info = CreateInfo();
            if (hidAction.Value == "add")
                info.Id = Votes.AddVoteoption(info);
            else
                Votes.EditVoteoption(info);
            InsertLog(2, (hidAction.Value == "add" ? "添加" : "编辑") + "投票选项", string.Format("ID:{0}", info.Id));

            Redirect("global_voteitems.aspx?id=" + tid.ToString());
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