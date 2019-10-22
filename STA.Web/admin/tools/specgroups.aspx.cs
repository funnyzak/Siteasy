using System;
using System.Web;
using System.Data;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin.Tools
{
    public partial class specgroups : AdminPage
    {
        private int specid = STARequest.GetQueryInt("id", 0);
        private string action = STARequest.GetFormString("hidAction");
        protected void Page_Load(object sender, EventArgs e)
        {
            isShowSysMenu = false;
            if (!IsPostBack)
            {
                hidAction.Value = "add";
                BindData();
            }
            else if (IsPostBack && action != "")
            {
                switch (action)
                {
                    case "del":
                        Contents.DelSpecgroup(STARequest.GetFormInt("hidValue", 0));
                        break;
                    case "gedit":
                        EditGroup();
                        break;
                }
                BindData();
            }
        }

        private void EditGroup()
        {
            AddBtn.Text = "修改组";
            hidValue.Value = STARequest.GetFormInt("hidValue", 0).ToString();
            txtGroupname.Text = STARequest.GetFormString("name" + hidValue.Value);
            txtOrderid.Text = STARequest.GetFormString("order" + hidValue.Value);
            hidAction.Value = "edit";
        }

        private void BindData()
        {
            DataTable dt = Contents.GetSpecgroups(specid);
            rptData.Visible = true;
            rptData.DataSource = dt;
            rptData.DataBind();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            int orderid = TypeParse.StrToInt(txtOrderid.Text, 0);
            string gname = txtGroupname.Text;
            if (hidAction.Value != "edit")
            {
                Contents.AddSpecgroup(new SpecgroupInfo(0, specid, gname, DateTime.Now, orderid));
                Message(string.Format("内容组 <b>{0}</b> 已成功创建！", gname), true);
            }
            else if (hidAction.Value == "edit" && hidValue.Value != "")
            {
                Contents.EditSpecgroup(new SpecgroupInfo(TypeParse.StrToInt(hidValue.Value, 0), specid, gname, DateTime.Now, orderid));
                Message(string.Format("内容组已成功修改！", gname), true);
            }
            BindData();
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.AddBtn.Click += new EventHandler(this.AddBtn_Click);
        }
        #endregion
    }
}