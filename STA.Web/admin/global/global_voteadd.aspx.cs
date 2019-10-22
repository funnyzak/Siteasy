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
    public partial class voteadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            ddlVotecates.AddTableData(Votes.GetVoteCateTable(), "name", "id");
            likeidlist.InnerText = ConUtils.DataTableToString(Votes.VoteLikeIds(), 0, ",");
            txtEndtime.Text = DateTime.Now.AddYears(3).ToString("yyyy-MM-dd HH:mm:ss");

            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            VotetopicInfo info = Votes.GetVotetopic(id);
            if (info == null) return;

            txtName.Text = info.Name;
            ddlVotecates.SelectedValue = info.Cateid.ToString();
            rblType.SelectedValue = info.Type.ToString();
            rblVtype.SelectedValue = info.Maxvote > 1 ? "2" : "1";
            txtMaxvote.Text = info.Maxvote.ToString();
            txtDesc.Text = info.Desc;
            txtOrderid.Text = info.Orderid.ToString();
            txtLikeid.Text = info.Likeid;
            txtImg.Text = info.Img;
            txtEndtext.Text = info.Endtext;
            txtVoted.Text = info.Voted;
            rblIsinfo.SelectedValue = info.Isinfo.ToString();
            rblIslogin.SelectedValue = info.Islogin.ToString();
            rblIsvcode.SelectedValue = info.Isvcode.ToString();

            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
            #endregion
        }
        private VotetopicInfo CreateInfo()
        {
            VotetopicInfo info = new VotetopicInfo();
            if (hidAction.Value == "edit")
                info = Votes.GetVotetopic(TypeParse.StrToInt(hidId.Value, 0));
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Name = txtName.Text;
            info.Cateid = TypeParse.StrToInt(ddlVotecates.SelectedValue, 0);
            info.Catename = ddlVotecates.SelectedItem.Text;
            info.Type = byte.Parse(rblType.SelectedValue);
            info.Desc = txtDesc.Text;
            info.Endtext = txtEndtext.Text;
            info.Voted = txtVoted.Text;
            info.Likeid = txtLikeid.Text;
            info.Endtime = TypeParse.StrToDateTime(txtEndtime.Text, DateTime.Now.AddDays(7f));
            info.Img = txtImg.Text;
            info.Maxvote = TypeParse.StrToInt(rblVtype.SelectedValue, 1);
            info.Maxvote = info.Maxvote == 2 ? TypeParse.StrToInt(txtMaxvote.Text, 2) : 1;
            info.Orderid = TypeParse.StrToInt(txtOrderid.Text, 0);
            info.Isvcode = TypeParse.StrToInt(rblIsvcode.SelectedValue, 0);
            info.Isinfo = TypeParse.StrToInt(rblIsinfo.SelectedValue, 0);
            info.Islogin = TypeParse.StrToInt(rblIslogin.SelectedValue, 0);

            return info;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            Save();
            Redirect("global_votelist.aspx");
        }

        private void NextStep_Click(object sender, EventArgs e)
        {
            Redirect("global_voteitems.aspx?id=" + Save().ToString());
        }

        private int Save()
        {
            VotetopicInfo info = CreateInfo();
            if (hidAction.Value == "add")
                info.Id = Votes.AddVotetopic(info);
            else
                Votes.EditVotetopic(info);
            InsertLog(2, (hidAction.Value == "add" ? "添加" : "编辑") + "投票主题", string.Format("ID:{0}", info.Id));
            return info.Id;

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
            this.NextStep.Click += new EventHandler(this.NextStep_Click);
        }
        #endregion
    }
}