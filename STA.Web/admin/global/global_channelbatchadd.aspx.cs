using System;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;

namespace STA.Web.Admin
{
    public partial class channelbatchadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            ddrChannels.BuildTree(Contents.GetChannelDataTable(), "name", "id");

            DataTable cdt = ConUtils.RemoveTableRow(Contents.GetContypeDataTable(), "id", "0");

            ddlConType.AddTableData(ConUtils.RemoveTableRow(cdt, "open", "0"), "name", "id", "");
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            int parentid = TypeParse.StrToInt(ddrChannels.SelectedValue, 0);
            int typeid = TypeParse.StrToInt(ddlConType.SelectedValue, 0);
            int tempnum = 0, temparentid = 3;
            MatchCollection channels = Regex.Matches(txtChannels.Text, @"((\-{0,10})*)([^\(\)\r\n]+)(\((\w+)\))?");
            foreach (Match chl in channels)
            {
                int placenum = chl.Groups[1].Length;
                ChannelInfo info = new ChannelInfo();
                info.Name = chl.Groups[3].Value;
                info.Ctype = 1;
                info.Listrule = "{@channelpath}/list_{@channelid}_{@page}";
                info.Conrule = "{@channelpath}/{@year02}/{@month}/{@day}/{@hour}/{@contentid}";
                info.Typeid = short.Parse(typeid.ToString());
                info.Filename = chl.Groups[5].Value;
                info.Parentid = parentid;
                if (placenum > 0 && ((tempnum + 1) == placenum))
                    info.Parentid = temparentid;
                temparentid = Contents.AddChannel(info);
                tempnum = placenum;
            }
            Message(string.Empty, "global_channelist.aspx");
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