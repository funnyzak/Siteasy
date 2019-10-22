using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Cache;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class contypefieldadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            ContypefieldInfo info = Contents.GetContypeField(id);
            if (info == null) return;
            hidFieldname.Value = txtFieldName.Text = info.Fieldname;
            txtFieldName.Enabled = false;
            ddlFieldType.SelectedValue = info.Fieldtype;
            txtLength.Text = info.Length.ToString();
            rblNull.SelectedValue = info.Isnull.ToString();
            txtDefValue.Text = info.Defvalue;
            txtDesctext.Text = info.Desctext;
            txtTipText.Text = info.Tiptext;
            txtOrderId.Text = info.Orderid.ToString();
            txtVinnertext.Text = info.Vinnertext;
            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
            #endregion
        }

        private ContypefieldInfo CreateInfo()
        {
            ContypefieldInfo info = new ContypefieldInfo();
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Cid = STARequest.GetInt("cid", 0);
            info.Fieldname = hidAction.Value == "add" ? txtFieldName.Text : hidFieldname.Value;
            if (!info.Fieldname.StartsWith("ext_"))
                info.Fieldname = "ext_" + info.Fieldname;
            info.Fieldtype = ddlFieldType.SelectedValue;
            info.Length = TypeParse.StrToInt(txtLength.Text, 20);
            if ((Utils.InArray(info.Fieldtype, "char,nchar") && info.Length > 8000) || (Utils.InArray(info.Fieldtype, "varchar,nvarchar") && info.Length > 4000))
                info.Fieldtype = "ntext";
            info.Isnull = Byte.Parse(rblNull.SelectedValue.ToString());
            info.Defvalue = txtDefValue.Text;
            info.Tiptext = txtTipText.Text;
            info.Desctext = txtDesctext.Text;
            info.Orderid = TypeParse.StrToInt(txtOrderId.Text, 0);
            info.Vinnertext = txtVinnertext.Text;
            return info;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            if (Utils.InArray(ddlFieldType.SelectedValue, "select,radio,checkbox"))
            {
                if (txtDefValue.Text == "")
                {
                    Message("此数据类型必须设置默认值！");
                    return;
                }
            }
            ContypefieldInfo info = CreateInfo();
            ContypeInfo cinfo = Contents.GetContype(STARequest.GetInt("cid", 0));
            bool success = false;
            if (hidAction.Value == "add" && Databases.AddTableField(cinfo.Extable, info.Fieldname, ddlFieldType.SelectedValue, TypeParse.StrToInt(txtLength.Text)))
            {
                success = Contents.AddContypeField(info) > 0;
            }
            else if (hidAction.Value == "edit" && Databases.EditTableField(cinfo.Extable, hidFieldname.Value, ddlFieldType.SelectedValue, TypeParse.StrToInt(txtLength.Text)))
            {
                success = Contents.EditContypeField(info);
            }
            Caches.RemoveObject(CacheKeys.DATATABLE + "contypelist");
            if (success)
            {
                ConUtils.BulidContypeFields(STARequest.GetInt("cid", 0));
                Redirect("global_contypefieldedit.aspx?id=" + STARequest.GetString("cid"));
            }
            else
            {
                Message("操作失败，请检查字段配置是否有误(字段名是否重复、命名是否有特殊字符、或者字段类型转换不允许等)！", 5);
            }
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