using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class flinkadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            ddlLinktype.AddTableData(Contents.GetLinkType(), "name", "id");
            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            LinkInfo info = Contents.GetLink(id);
            if (info == null) return;
            txtName.Text = info.Name;
            ddlLinktype.SelectedValue = info.Typeid.ToString();
            txtImg.Text = info.Logo;
            txtUrl.Text = info.Url;
            txtEmail.Text = info.Email;
            txtDescription.Text = info.Description;
            txtOrderid.Text = info.Orderid.ToString();
            rblStatus.SelectedValue = ((int)info.Status).ToString();
            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
            #endregion
        }
        private LinkInfo CreateInfo()
        {
            LinkInfo info = new LinkInfo();
            if (hidAction.Value == "edit")
                info = Contents.GetLink(TypeParse.StrToInt(hidId.Value, 0));
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Name = txtName.Text;
            info.Typeid = TypeParse.StrToInt(ddlLinktype.SelectedValue, 0);
            info.Url = txtUrl.Text;
            info.Logo = txtImg.Text;
            info.Email = txtEmail.Text;
            info.Description = txtDescription.Text;
            info.Orderid = TypeParse.StrToInt(txtOrderid.Text, 0);
            info.Status = Byte.Parse(TypeParse.StrToInt(rblStatus.SelectedValue, 1).ToString());
            return info;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            LinkInfo info = CreateInfo();
            if (hidAction.Value == "add")
                Contents.AddLink(info);
            else
                Contents.EditLink(info);
            InsertLog(2, (info.Id == 0 ? "添加" : "修改") + "链接", string.Format("ID:{0},链接名:{1}", info.Id, info.Name));
            Redirect("global_flinks.aspx?msg=" + string.Format("链接 <b>{0}</b> 已成功{1}！", info.Name, info.Id == 0 ? "创建" : "修改"));
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