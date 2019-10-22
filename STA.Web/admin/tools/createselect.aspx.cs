using System;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin.Tools
{
    public partial class createselect : AdminPage
    {
        string tagname = Utils.UrlDecode(STARequest.GetString("name"));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            SelecttypeInfo info = Selects.GetSelectType(id);
            if (info == null) return;

            txtEname.Text = info.Ename.Trim();
            txtName.Text = info.Name.Trim();

            if(info.System==1)
                base.RegisterStartupScript("close", "parent.$('#editbox').jqmHide();parent.SAlert(\"系统联动类型不能修改！\")");

            hidAction.Value = "edit";
            hidId.Value = STARequest.GetInt("id", 0).ToString();
        }

        private SelecttypeInfo Create()
        {
            SelecttypeInfo info = new SelecttypeInfo();
            if (hidAction.Value == "edit")
                info = Selects.GetSelectType(TypeParse.StrToInt(hidId.Value));
            info.Name = txtName.Text;
            info.Ename = txtEname.Text;
            return info;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtEname.Text != "")
            {
                bool success = false;

                SelecttypeInfo info = Create();
                if (hidAction.Value == "add")
                {
                    info.Id = Selects.AddSelectType(info);
                    success = info.Id > 0;
                }
                else
                    success = Selects.EditSelectType(info);

                if (!success)
                    base.RegisterStartupScript("close", "parent.$('#editbox').jqmHide();parent.SAlert(\"标识已存在，已取消操作！\")");
                else
                    base.RegisterStartupScript("close", "parent.$('#editbox').jqmHide();parent.SAlert(\"操作已成功执行！\", 1, function(){parent.SubmitForm('flush')})"); ;

                InsertLog(2, (info.Id == 0 ? "添加" : "修改") + "联动类型", string.Format("ID{0},联动名:{1}", info.Id, info.Name));
            }
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            isShowSysMenu = false;
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
        }
        #endregion
    }
}