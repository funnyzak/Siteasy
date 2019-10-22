using System;
using System.Web;
using System.Data;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;
using STA.Control;

namespace STA.Web.Admin.Tools
{
    public partial class fastcontentedit : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            DataTable cdt = Contents.GetChannelDataTable();
            ConUtils.BulidChannelList(STARequest.GetInt("type", 1), cdt, ddlConType);
            if (STARequest.GetInt("id", 0) <= 0) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            #region 加载页面数据
            ContentInfo info = Contents.GetShortContent(id);
            if (info == null) return;
            ddlConType.SelectedValue = info.Channelid.ToString();
            txtTitle.Text = info.Title;
            txtOrderId.Text = info.Orderid.ToString();
            txtSubTitle.Text = info.Subtitle;
            cbP_h.Checked = info.Property.IndexOf("h") >= 0;
            cbP_r.Checked = info.Property.IndexOf("r") >= 0;
            cbP_f.Checked = info.Property.IndexOf("f") >= 0;
            cbP_a.Checked = info.Property.IndexOf("a") >= 0;
            cbP_s.Checked = info.Property.IndexOf("s") >= 0;
            cbP_b.Checked = info.Property.IndexOf("b") >= 0;
            cbP_i.Checked = info.Property.IndexOf("i") >= 0;
            cbP_p.Checked = info.Property.IndexOf("p") >= 0;
            cbP_j.Checked = info.Property.IndexOf("j") >= 0;
            txtTags.Text = ConUtils.DataTableToString(Contents.GetTagsByCid(id), 1, ",");
            #endregion
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            if (ddlConType.SelectedValue.EndsWith("[X]") || ddlConType.SelectedValue == "0")
            {
                Message("请选择有效的内容所属频道！");
                return;
            }
            if (STARequest.GetQueryInt("id", 0) == 0)
            {
                return;
            }
            ContentInfo info = CreateInfo();
            if (Contents.EditContent(info) > 0)
            {
                Contents.AddTag(info.Tags, info.Id);
            }
            ConUtils.InsertLog(2, userid, username, admingroupid, admingroupname, STARequest.GetIP(), "编辑文档属性", string.Format("文档ID:{0},文档名:{1}", info.Id, info.Title));
            Message("您编辑的内容已经成功保存！", 2, "window.parent.SubmitForm('reflush');");
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            isShowSysMenu = false;
            base.OnInit(e);
        }

        private ContentInfo CreateInfo()
        {
            #region 内容实体
            ContentInfo info = info = Contents.GetShortContent(STARequest.GetQueryInt("id", 0));
            if (info == null) return null;
            info.Channelid = TypeParse.StrToInt(ddlConType.SelectedValue, 0);
            info.Channelfamily = ConUtils.GetChannelFamily(info.Channelid, ",");
            info.Channelname = ddlConType.SelectedItem.Text;
            info.Lastedituser = userid;
            info.Channelname = ddlConType.SelectedItem.Text;
            info.Title = txtTitle.Text;
            info.Subtitle = txtSubTitle.Text;
            info.Tags = txtTags.Text;
            info.Property = ",";
            if (cbP_h.Checked)
                info.Property += "h,";
            if (cbP_r.Checked)
                info.Property += "r,";
            if (cbP_f.Checked)
                info.Property += "f,";
            if (cbP_a.Checked)
                info.Property += "a,";
            if (cbP_s.Checked)
                info.Property += "s,";
            if (cbP_b.Checked)
                info.Property += "b,";
            if (cbP_i.Checked)
                info.Property += "i,";
            if (cbP_p.Checked)
                info.Property += "p,";
            if (cbP_j.Checked)
                info.Property += "j,";
            info.Lasteditusername = username;
            info.Orderid = TypeParse.StrToInt(txtOrderId.Text, 0);
            return info;
            #endregion
        }

        private void InitializeComponent()
        {
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
        }
        #endregion
    }
}