using System;
using System.Web;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;
using System.Data;
using System.Text;
using STA.Cache;

namespace STA.Web.Admin
{
    public partial class channeladd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            powerset.Text = GetPowerSet();
            hidAction.Value = "add";

            DataTable cdt = ConUtils.RemoveTableRow(Contents.GetContypeDataTable(), "id", "0");
            ddlConType.AddTableData(STARequest.GetQueryString("action").Equals("edit") ? cdt : ConUtils.RemoveTableRow(cdt, "open", "0"), "name", "id", "");

            ddlConType.SelectedValue = "1";
            hidParentId.Value = STARequest.GetQueryString("parent");
            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            ChannelInfo info = Contents.GetChannel(id);
            if (info == null) return;
            //ddlConType.Enabled = false;
            hidId.Value = info.Id.ToString();
            ddlConType.SelectedValue = info.Typeid.ToString();
            hidParentId.Value = info.Parentid.ToString();
            txtName.Text = info.Name;
            txtSavePath.Text = info.Savepath;
            txtFileName.Text = info.Filename;
            rblCtype.SelectedValue = info.Ctype.ToString();
            txtCoverTem.Text = info.Covertem;
            txtListTem.Text = info.Listem;
            txtConTem.Text = info.Contem;
            txtConRule.Text = info.Conrule;
            txtListRule.Text = info.Listrule;
            txtPageTitle.Text = info.Seotitle;
            txtKeywords.Text = info.Seokeywords;
            txtDescription.Text = info.Seodescription;
            cbMoreSite.Checked = info.Moresite == 1;
            txtImg.Text = info.Img;
            txtListcount.Text = info.Listcount.ToString();
            txtSiteUrl.Text = info.Siteurl;
            txtContent.Text = info.Content;
            cbIsPost.Checked = info.Ispost == 1;
            cbIsHidden.Checked = info.Ishidden == 1;
            txtIpaccess.Text = info.Ipaccess;
            txtIpdenyaccess.Text = info.Ipdenyaccess;
            txtOrderId.Text = info.Orderid.ToString();
            hidViewchl.Value = info.Viewgroup;
            hidViewchlcons.Value = info.Viewcongroup;
            hidAction.Value = "edit";
            #endregion
        }
        private string GetPowerSet()
        {
            DataTable udts = Users.GetUserGroupTable();
            StringBuilder pstr = new StringBuilder();
            foreach (DataRow udr in udts.Rows)
            {
                if (TypeParse.StrToInt(udr["system"]) == 1) continue;

                pstr.Append("<tr>\r\n\t<td></td>")
                    .AppendFormat("\r\n\t<td><input type=\"checkbox\" id=\"u{0}\" value=\"{0}\" onclick=\"selByCheckValue({0},this.checked)\"/></td>", udr["id"].ToString())
                    .AppendFormat("\r\n\t<td><label for=\"u{0}\">{1}</label></td>", udr["id"].ToString(), udr["name"].ToString())
                    .AppendFormat("\r\n\t<td><input type=\"checkbox\" name=\"viewchl\" value=\"{0}\"/></td>", udr["id"].ToString())
                    .AppendFormat("\r\n\t<td><input type=\"checkbox\" name=\"viewchlcons\" value=\"{0}\"/></td>", udr["id"].ToString())
                    .Append("\r\n</tr>");
            }
            return pstr.ToString();
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            ChannelInfo info = CreateInfo();
            if (hidAction.Value == "add")
            {
                info.Id = Contents.AddChannel(info);
            }
            else
            {
                Contents.EditChannel(info);
            }
            if (cbInherit.Checked) ConUtils.ChannelInherit(config, info);

            ConUtils.InsertLog(2, userid, username, admingroupid, admingroupname, STARequest.GetIP(), (hidAction.Value == "add" ? "添加" : "编辑") + "频道", string.Format("频道ID:{0},频道名:{1}", info.Id, info.Name));
            Caches.RemoveObject(CacheKeys.DATATABLE + "allchannel");
            Redirect("global_channelist.aspx?msg=" + Utils.UrlEncode(string.Format("<b>{0}</b> 已成功{1}！", info.Name, hidAction.Value == "add" ? "添加" : "修改")));
        }

        private ChannelInfo CreateInfo()
        {
            ChannelInfo info = new ChannelInfo();
            info.Id = TypeParse.StrToInt(hidId.Value, 0);
            info.Name = txtName.Text;
            info.Typeid = short.Parse(ddlConType.SelectedValue);
            info.Parentid = TypeParse.StrToInt(hidParentId.Value, 0);
            info.Savepath = txtSavePath.Text;
            info.Filename = txtFileName.Text;

            //info.Savepath = info.Savepath == "" ? config.Htmlsavepath + "/channel" : info.Savepath;

            info.Ctype = byte.Parse(rblCtype.SelectedValue);
            info.Covertem = txtCoverTem.Text;
            info.Listem = txtListTem.Text;
            info.Contem = txtConTem.Text;
            info.Conrule = txtConRule.Text;
            info.Listrule = txtListRule.Text;
            info.Img = txtImg.Text;
            info.Seotitle = txtPageTitle.Text;
            info.Seokeywords = txtKeywords.Text;
            info.Listcount = TypeParse.StrToInt(txtListcount.Text, 0);
            info.Seodescription = txtDescription.Text;
            info.Moresite = byte.Parse((cbMoreSite.Checked ? 1 : 0).ToString());
            info.Siteurl = txtSiteUrl.Text;
            info.Content = txtContent.Text;
            if (cbIsRemote.Checked && info.Content.Trim() != string.Empty)
                info.Content = STA.Core.Collect.RemoteFile.Remote(info.Content, txtResourceFType.Text.Split(',', '，', ' '), sitepath + filesavepath + "/remote/", config.Colfilestorage == 1);
            if (IsFiterDangerousCode.Checked && info.Content.Trim() != string.Empty)
                info.Content = Utils.RemoveUnsafeHtml(info.Content);
            if (cbFiltertags.Checked && txtFiltertags.Text.Trim() != "")
                info.Content = ConUtils.FilterHtmlTags(info.Content, txtFiltertags.Text.Trim());
            info.Ispost = byte.Parse((cbIsPost.Checked ? 1 : 0).ToString());
            info.Ishidden = byte.Parse((cbIsHidden.Checked ? 1 : 0).ToString());
            info.Orderid = TypeParse.StrToInt(txtOrderId.Text, 0);
            info.Viewgroup = STARequest.GetString("viewchl");
            info.Viewcongroup = STARequest.GetString("viewchlcons");
            info.Ipdenyaccess = txtIpdenyaccess.Text;
            info.Ipaccess = txtIpaccess.Text;

            if (info.Viewcongroup.Trim() != "")
                info.Viewcongroup = "," + info.Viewcongroup + ",";
            if (info.Viewgroup.Trim() != "")
                info.Viewgroup = "," + info.Viewgroup + ",";

            return info;
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