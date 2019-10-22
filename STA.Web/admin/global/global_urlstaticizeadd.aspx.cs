using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class urlstaticizeadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            txtSavePath.Text = config.Htmlsavepath + "/staticize";
            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            StaticizeInfo info = Contents.GetUrlStaticize(id);
            if (info == null) return;
            txtTitle.Text = info.Title;
            txtEncode.Text = info.Charset;
            txtUrl.Text = info.Url;
            txtSavePath.Text = info.Savepath;
            txtFileName.Text = info.Filename;
            txtSuffix.Text = info.Suffix;
            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
            #endregion
        }
        private StaticizeInfo CreateInfo()
        {
            StaticizeInfo info = new StaticizeInfo();
            if (hidAction.Value == "edit")
                info = Contents.GetUrlStaticize(TypeParse.StrToInt(hidId.Value, 0));
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Title = txtTitle.Text;
            info.Charset = txtEncode.Text;
            info.Url = txtUrl.Text;
            info.Suffix = txtSuffix.Text;
            info.Savepath = txtSavePath.Text;
            info.Filename = txtFileName.Text;

            return info;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            StaticizeInfo info = CreateInfo();
            if (hidAction.Value == "add")
                info.Id = Contents.AddUrlStaticize(info);
            else
                Contents.EditUrlStaticize(info);

            STA.Core.Globals.UrlStaticizeCreate(info);
            InsertLog(2, (hidAction.Value == "add" ? "添加" : "编辑") + "URL静态化", string.Format("ID:{0},名称:{1}", info.Id, info.Title));
            Redirect("global_urlstaticize.aspx");
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