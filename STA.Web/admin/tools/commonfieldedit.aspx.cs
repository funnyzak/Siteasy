using System;
using System.Web;
using System.Data;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;
using System.Xml;

namespace STA.Web.Admin.Tools
{
    public partial class commonfieldedit : AdminPage
    {
        private string field = STARequest.GetQueryString("field");
        private string fieldxmlpath = string.Empty;
        public string fieldname = "参数设置";
        protected void Page_Load(object sender, EventArgs e)
        {
            isShowSysMenu = false;
            fieldxmlpath = Utils.GetMapPath("../xml/field.config");
            if (IsPostBack) return;
            fieldname = XMLUtil.LoadDocument(fieldxmlpath).SelectSingleNode("FieldConfigInfo/" + field).Attributes["name"].Value;
            txtFieldText.Text = XMLUtil.GetNodeValue(fieldxmlpath, field);
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            XmlDocument doc = XMLUtil.LoadDocument(fieldxmlpath);
            XMLUtil.SetNodeValue(doc, field, txtFieldText.Text);
            doc.Save(fieldxmlpath);
            base.RegisterStartupScript("close", "parent.$('#editbox').jqmHide();");
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