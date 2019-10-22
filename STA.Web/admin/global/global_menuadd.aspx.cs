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
    public partial class menuadd : AdminPage
    {
        public string iconStr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            SetIconJson();
            rblPagetype.AddTableData(ConUtils.GetEnumTable(typeof(PageType)), "name", "id", null);
            rblPagetype.SelectedValue = "1";

            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            MenuInfo info = Menus.GetMenu(id);
            if (info == null) return;
            txtname.Text = info.Name;
            rblsystem.SelectedValue = info.System.ToString();
            rblPagetype.SelectedValue = ((int)info.Pagetype).ToString();
            txticon.Text = info.Icon;
            txturl.Text = info.Url;
            txttarget.Text = info.Target;
            txtorderid.Text = info.Orderid.ToString();
            rblsystem.Enabled = rblPagetype.Enabled = txttarget.Enabled = txturl.Enabled = !(info.System == 1);
            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
            #endregion
        }

        private void SetIconJson()
        {
            foreach (FileItem item in FileUtil.GetFiles(Utils.GetMapPath("../images/icon/"), "png,gif,jpeg,jpg,bmp"))
            {
                iconStr += "\"" + item.Name + "\",";
            }
            if (iconStr.EndsWith(",")) iconStr = iconStr.Substring(0, iconStr.Length - 1);
        }

        private MenuInfo CreateInfo()
        {
            MenuInfo info = new MenuInfo();
            info.Parentid = STARequest.GetQueryInt("pid", 0);
            if (hidAction.Value == "edit")
                info = Menus.GetMenu(TypeParse.StrToInt(hidId.Value, 0));
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Name = txtname.Text;
            info.System = byte.Parse(rblsystem.SelectedValue);
            info.Pagetype = (PageType)TypeParse.StrToInt(rblPagetype.SelectedValue, 1);
            info.Icon = txticon.Text;
            info.Url = txturl.Text;
            info.Target = txttarget.Text;
            info.Orderid = TypeParse.StrToInt(txtorderid.Text);

            return info;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            MenuInfo info = CreateInfo();
            if (hidAction.Value == "add")
                info.Id = Menus.AddMenu(info);
            else
                Menus.EditMenu(info);
            InsertLog(2, (hidAction.Value == "add" ? "添加" : "编辑") + "菜单", string.Format("ID:{0},名称:{1}", info.Id, info.Name));
            Redirect("global_menulist.aspx");
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