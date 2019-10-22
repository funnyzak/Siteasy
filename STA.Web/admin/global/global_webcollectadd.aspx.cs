using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;
using System.Collections;

namespace STA.Web.Admin
{
    public partial class webcollectadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            ddlConType.BuildTree(Contents.GetChannelDataTable(), "name", "id");
            rblStatus.AddTableData(ConUtils.GetEnumTable(typeof(ConStatus)), "name", "id", null);
            rblStatus.SelectedValue = "1";
            rblCletype.AddTableData(ConUtils.GetEnumTable(typeof(CollectType)), "name", "id", null);
            rblCletype.SelectedValue = "3";

            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            WebcollectInfo info = Collects.GetWebCollect(id);
            if (info == null) return;
            ddlConType.SelectedValue = info.Channelid.ToString();
            txtName.Text = info.Name;
            rblStatus.SelectedValue = info.Constatus.ToString();
            txtEncode.Text = info.Encode;
            rblCletype.SelectedValue = ((int)info.Collecttype).ToString();
            txtCurl.Text = info.Curl;
            txtHosturl.Text = info.Hosturl;
            txtCurls.Text = info.Curls;
            txtClisturl.Text = info.Clisturl;
            txtPagestart.Text = info.Clistpage.Split(',')[0];
            txtPageend.Text = info.Clistpage.Trim().Split(',')[1];
            cbDownImg.Checked = info.Property.IndexOf("m") >= 0;
            cbFilterRepeat.Checked = info.Property.IndexOf("r") >= 0;
            cbFilterempty.Checked = info.Property.IndexOf("c") >= 0;
            cbCSort.Checked = info.Property.IndexOf("d") >= 0; ;
            cbFilterpage.Checked = info.Property.IndexOf("p") >= 0;
            cbFirstimg.Checked = info.Property.IndexOf("a") >= 0;
            chbFilter.SetSelectByID(info.Filter);
            txtConfilter.Text = info.Confilter;

            Hashtable set = ConUtils.GetCollectRuleSet(WebcollectInfo.ats, info.Setting);
            txtSTurllist.Text = set["urllist"].ToString();
            txtSTurl.Text = set["url"].ToString();
            txtSTtitle.Text = set["title"].ToString();
            txtSTsource.Text = set["source"].ToString();
            txtSTauthor.Text = set["author"].ToString();
            txtSTaddtime.Text = set["addtime"].ToString();
            txtSTcontent.Text = set["content"].ToString();
            txtSTconpage.Text = set["conpage"].ToString();
            txtSTconpageurl.Text = set["conpageurl"].ToString();

            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
            #endregion
        }
        private WebcollectInfo CreateInfo()
        {
            WebcollectInfo info = new WebcollectInfo();
            if (hidAction.Value == "edit")
                info = Collects.GetWebCollect(TypeParse.StrToInt(hidId.Value));
            info.Name = txtName.Text;
            info.Channelid = TypeParse.StrToInt(ddlConType.SelectedValue);
            info.Channelname = ddlConType.SelectedItem.Text;
            info.Constatus = byte.Parse(rblStatus.SelectedValue);
            info.Hosturl = txtHosturl.Text;
            info.Collecttype = (CollectType)TypeParse.StrToInt(rblCletype.SelectedValue, 3);
            info.Curl = txtCurl.Text;
            info.Clisturl = txtClisturl.Text;
            info.Clistpage = TypeParse.StrToInt(txtPagestart.Text, 1).ToString() + "," + TypeParse.StrToInt(txtPageend.Text, 2).ToString();
            info.Curls = txtCurls.Text;
            info.Encode = txtEncode.Text;
            info.Confilter = txtConfilter.Text;
            info.Property = cbDownImg.Checked ? "m" : "";
            if (cbFilterRepeat.Checked)
                info.Property += ",r";
            if (cbFilterempty.Checked)
                info.Property += ",c";
            if (cbCSort.Checked)
                info.Property += ",d";
            if (cbFilterpage.Checked)
                info.Property += ",p";
            if (cbFirstimg.Checked)
                info.Property += ",a";
            info.Attrs = WebcollectInfo.ats;
            info.Filter = chbFilter.GetSelectString();
            info.Setting = "";
            foreach (string attr in WebcollectInfo.ats.Split(','))
            {
                info.Setting += string.Format("setting_{0}_start_{1}_setting_{0}_end&", attr, STARequest.GetFormString("txtST" + attr)); ;
            }

            return info;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            WebcollectInfo info = CreateInfo();
            if (hidAction.Value == "add")
                info.Id = Collects.AddWebCollect(info);
            else
                Collects.EditWebCollect(info);
            InsertLog(2, (hidAction.Value == "add" ? "添加" : "编辑") + "站点采集规则", string.Format("ID:{0},规则{1}", info.Id, info.Name));
            Redirect("global_webcollect.aspx?msg=" + string.Format("站点采集规则 <b>{0}</b> 已成功{1}！", info.Name, info.Id == 0 ? "创建" : "修改"));
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