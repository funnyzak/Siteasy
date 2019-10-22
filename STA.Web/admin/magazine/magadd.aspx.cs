using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class magazineadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            likeidlist.InnerText = ConUtils.DataTableToString(Contents.MagazineLikeIds(), 0, ",");

            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            MagazineInfo info = Contents.GetMagazine(id);
            if (info == null) return;
            txtRation.Text = info.Ratio;
            txtName.Text = info.Name;
            txtLikeid.Text = info.Likeid;
            txtOrderid.Text = info.Orderid.ToString();
            txtImg.Text = info.Cover;
            txtDesc.Text = info.Description;
            txtClick.Text = info.Click.ToString();
            rblStatus.SelectedValue = ((int)info.Status).ToString();
            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
            #endregion
        }
        private MagazineInfo CreateInfo()
        {
            MagazineInfo info = new MagazineInfo();
            if (hidAction.Value == "edit")
                info = Contents.GetMagazine(TypeParse.StrToInt(hidId.Value, 0));
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Name = txtName.Text;
            info.Cover = txtImg.Text;
            info.Likeid = txtLikeid.Text;
            info.Ratio = txtRation.Text;
            info.Description = txtDesc.Text;
            info.Click = TypeParse.StrToInt(txtClick.Text);
            info.Updatetime = DateTime.Now;
            info.Orderid = TypeParse.StrToInt(txtOrderid.Text, 0);
            info.Status = Byte.Parse(TypeParse.StrToInt(rblStatus.SelectedValue, 1).ToString());
            return info;
        }

        private void NextStep_Click(object sender, EventArgs e)
        {
            Redirect("magups.aspx?id=" + Save().ToString());
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            Save();
            Redirect("maglist.aspx");
        }

        private int Save()
        {
            MagazineInfo info = CreateInfo();
            if (hidAction.Value == "add")
                info.Id = Contents.AddMagazine(info);
            else
                Contents.EditMagazine(info);
            InsertLog(2, (info.Id == 0 ? "添加" : "修改") + "杂志", string.Format("ID:{0},杂志名:{1}", info.Id, info.Name));
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