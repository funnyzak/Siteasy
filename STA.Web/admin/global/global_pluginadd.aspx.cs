using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class pluginadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            PluginInfo info = Plugins.GetPlugin(id);
            if (info == null) return;
            txtName.Text = info.Name;
            txtEmail.Text = info.Email;
            txtAuthor.Text = info.Author;
            txtPubtime.Text = info.Pubtime.ToString("yyyy-MM-dd");
            txtOfficesite.Text = info.Officesite;
            txtMenu.Text = info.Menu;
            txtDescription.Text = info.Description;
            txtDbcreate.Text = info.Dbcreate;
            txtDbdelete.Text = info.Dbdelete;
            txtPackage.Text = info.Package;
            txtFilelist.Text = info.Filelist.Replace("@separator@", "\r\n"); //Utils.SplitString(info.Filelist,"@separator@");

            hidSetup.Value = info.Setup.ToString();
            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
            #endregion
        }
        private PluginInfo CreateInfo()
        {
            PluginInfo info = new PluginInfo();
            if (hidAction.Value == "edit")
                info = Plugins.GetPlugin(TypeParse.StrToInt(hidId.Value, 0));
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Name = txtName.Text;
            info.Email = txtEmail.Text;
            info.Author = txtAuthor.Text;
            info.Pubtime = TypeParse.StrToDateTime(txtPubtime.Text);
            info.Officesite = txtOfficesite.Text;
            info.Menu = txtMenu.Text;
            info.Description = txtDescription.Text;
            info.Dbcreate = txtDbcreate.Text;
            info.Dbdelete = txtDbdelete.Text;
            info.Package = txtPackage.Text;
            info.Filelist = txtFilelist.Text.Replace("\r\n", "@separator@");
            return info;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            PluginInfo info = CreateInfo();
            if (hidAction.Value == "add")
                info.Id = Plugins.AddPlugin(info);
            else
                Plugins.EditPlugin(info);
            InsertLog(2, (hidAction.Value == "add" ? "添加" : "编辑") + "扩展信息", string.Format("ID:{0},名称:{1}", info.Id, info.Name));
            Redirect("global_pluginlist.aspx");
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