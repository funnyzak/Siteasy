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
    public partial class dbcollectdd : AdminPage
    {
        public string cfields = "";
        private string removeconfields = "id,typeid,typename,title,channelfamily,channelid,channelname,extchannels,adduser,addusername,lastedituser,"
                                        + "lasteditusername,status,viewgroup,iscomment,ishtml,commentcount";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            ddlConType.BuildTree(Contents.GetChannelDataTable(), "name", "id");
            rblStatus.AddTableData(ConUtils.GetEnumTable(typeof(ConStatus)), "name", "id", null);
            rblStatus.SelectedValue = "0";

            DataTable cdt = ConUtils.RemoveTableRow(Contents.GetTableField(BaseConfigs.GetTablePrefix + "contents"), "name", removeconfields);
            cdt.Columns.Add("ntext");
            foreach (DataRow dr in cdt.Rows)
            {
                dr["ntext"] = dr["name"].ToString() + "[" + dr["type"].ToString() + "]";
            }
            cfields = Utils.DataTableToJSON(cdt).ToString();
            lbConfields.AddTableData(cdt, "ntext", "name", null);

            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            LoadData(STARequest.GetInt("id", 0));
        }

        private void LoadData(int id)
        {
            DbcollectInfo info = Collects.GetDbCollect(id);
            if (info == null) return;
            ddlConType.SelectedValue = info.Channelid.ToString();
            txtName.Text = info.Name;
            rblStatus.SelectedValue = info.Constatus.ToString();
            rblDbtype.SelectedValue = info.Dbtype.ToString();
            txtDatasource.Text = info.Datasource;
            txtUserid.Text = info.Userid;
            txtPassword.Text = info.Password;
            txtDbname.Text = info.Dbname;
            hidtbname.Value = info.Tbname;
            hidprimarykey.Value = info.Primarykey;
            hidorderbykey.Value = info.Orderkey;
            hidrepeatkey.Value = info.Repeatkey;
            rblSortby.SelectedValue = info.Sortby;
            txtWhere.Text = info.Where;

            foreach (DataRow dr in ConUtils.GetCollectMatchList(info.Matchs).Rows)
            {
                matchs.InnerText += dr["sname"].ToString().Trim() + ":" + dr["name"].ToString().Trim() + ",";
            }

            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
        }
        private DbcollectInfo CreateInfo()
        {
            DbcollectInfo info = new DbcollectInfo();
            if (hidAction.Value == "edit")
                info = Collects.GetDbCollect(TypeParse.StrToInt(hidId.Value));
            info.Name = txtName.Text;
            info.Channelid = TypeParse.StrToInt(ddlConType.SelectedValue);
            info.Channelname = ddlConType.SelectedItem.Text;
            info.Constatus = byte.Parse(rblStatus.SelectedValue);
            info.Dbtype = byte.Parse(rblDbtype.SelectedValue);
            info.Datasource = txtDatasource.Text;
            info.Userid = txtUserid.Text;
            info.Password = txtPassword.Text;
            info.Dbname = txtDbname.Text;
            info.Tbname = STARequest.GetFormString("ddlTbnames");
            info.Primarykey = STARequest.GetFormString("ddlPrimarykey");
            info.Orderkey = STARequest.GetFormString("ddlOrderbykey");
            info.Repeatkey = STARequest.GetFormString("ddlRepeatkey");
            info.Sortby = rblSortby.SelectedValue;
            info.Where = txtWhere.Text;

            info.Matchs = "";
            foreach (string item in matchs.InnerText.Split(','))
            {
                if (item.Trim() == "") continue;
                info.Matchs += string.Format("<item name=\"{0}\" sname=\"{1}\"/>\r\n", item.Split(':')[1], item.Split(':')[0]);
            }

            return info;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            DbcollectInfo info = CreateInfo();
            if (hidAction.Value == "add")
                info.Id = Collects.AddDbCollect(info);
            else
                Collects.EditDbCollect(info);
            InsertLog(2, (hidAction.Value == "add" ? "添加" : "编辑") + "数据库采集规则", string.Format("ID:{0},规则{1}", info.Id, info.Name));
            Redirect("global_dbcollect.aspx?msg=" + string.Format("数据库采集规则 <b>{0}</b> 已成功{1}！", info.Name, info.Id == 0 ? "创建" : "修改"));
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