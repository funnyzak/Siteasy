using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Config;
using STA.Core;
using STA.Data;
using STA.Entity.Plus;
using STA.Cache;

namespace STA.Web.Admin.Plus.ctmvariable
{
    public partial class add : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";

            DataTable dt = STA.Data.Plus.GetVariableLikeidList();
            foreach (DataRow dr in dt.Rows)
                likeidlist.InnerText += dr["likeid"].ToString().Trim() + ",";

            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            VariableInfo info = STA.Data.Plus.GetVariable(id);
            if (info == null) return;
            txtName.Text = info.Name;
            txtLikeid.Text = info.Likeid;
            txtKey.Text = hidkey.Value = info.Key;
            txtDesc.Text = info.Desc;
            txtVal.Text = info.KValue;
            rblsystem.SelectedValue = info.System.ToString();
            if (info.System == 1)
            {
                txtName.Enabled = rblsystem.Enabled = txtKey.Enabled = false;
            }
            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
            #endregion
        }
        private VariableInfo CreateInfo()
        {
            VariableInfo info = new VariableInfo();
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Name = txtName.Text;
            info.Likeid = txtLikeid.Text;
            info.Key = txtKey.Text;
            info.Desc = txtDesc.Text;
            info.KValue = txtVal.Text;
            info.System = byte.Parse(rblsystem.SelectedValue);
            return info;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            VariableInfo info = CreateInfo();
            bool keyexist = STA.Data.Plus.VariableKeyExist(info.Key);
            if ((hidAction.Value == "add" && keyexist) || (hidAction.Value == "edit" && hidkey.Value != info.Key && keyexist))
            {
                Message(string.Format("变量键名 {0} 已经存在,请使用其他键名！", info.Key));
                return;
            }

            if (hidAction.Value == "add")
                info.Id = STA.Data.Plus.AddVariables(info);
            else
                STA.Data.Plus.EditVariable(info);

            Caches.RemoveObject(CacheKeys.PAGE_VARIABLE + "custom_variables");
            InsertLog(2, (hidAction.Value == "add" ? "添加" : "编辑") + "自定义变量", string.Format("ID:{0},名称:{1},键名:{2}", info.Id, info.Name, info.Key));
            Redirect("list.aspx?msg=" + string.Format("变量 <b>{0}</b> 已成功{1}！", info.Name, info.Id == 0 ? "创建" : "修改"));
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