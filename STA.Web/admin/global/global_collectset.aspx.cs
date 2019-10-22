using System;
using System.Collections.Generic;

using STA.Common;
using STA.Config;
using STA.Core;

namespace STA.Web.Admin
{
    public partial class collectset : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            rblAutolink.SelectedValue = info.Colautolink.ToString();
            rblTitrplopen.SelectedValue = info.Coltitrplopen.ToString();
            rblTititpos.SelectedValue = info.Coltititpos.ToString();
            rblColfilestorage.SelectedValue = info.Colfilestorage.ToString();
            txtClickrange.Text = info.Colclickrange;
            txtSeorate.Text = info.Colseorate.ToString();
            txtTitkeywords.Text = info.Coltitkeywords;
            txtTitreplace.Text = info.Coltitreplace;
            txtSeocontent.Text = info.Colseocontent;
            txtSeolinks.Text = info.Colseolinks;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            info.Colautolink = TypeParse.StrToInt(rblAutolink.SelectedValue, 0);
            info.Coltitrplopen = TypeParse.StrToInt(rblTitrplopen.SelectedValue, 0);
            info.Colfilestorage = TypeParse.StrToInt(rblColfilestorage.SelectedValue, 0);
            info.Coltititpos = TypeParse.StrToInt(rblTititpos.SelectedValue, 0);
            info.Colclickrange = txtClickrange.Text;
            info.Colseorate = TypeParse.StrToInt(txtSeorate.Text, 1);
            info.Coltitkeywords = txtTitkeywords.Text;
            info.Coltitreplace = txtTitreplace.Text;
            info.Colseocontent = txtSeocontent.Text;
            info.Colseolinks = txtSeolinks.Text;

            info.Colseorate = info.Colseorate < 0 ? 0 : info.Colseorate;

            int temlen = info.Colclickrange.Split(',').Length;
            if (temlen > 2)
            {
                info.Colclickrange = "1";
            }
            else
            {
                if (temlen == 1)
                {
                    if (!Utils.IsInt(info.Colclickrange))
                    {
                        info.Colclickrange = "1";
                    }
                }
                else
                {
                    int tem1 = TypeParse.StrToInt(info.Colclickrange.Split(',')[0], 0);
                    int tem2 = TypeParse.StrToInt(info.Colclickrange.Split(',')[1]);
                    if (tem1 == tem2)
                        info.Colclickrange = tem1.ToString();
                    else if (tem1 > tem2)
                        info.Colclickrange = tem2.ToString() + "," + tem1.ToString();
                }
            }

            ConUtils.InsertLog(2, this.userid, this.username, this.admingroupid, this.admingroupname, STARequest.GetIP(), "采集选项", "");

            Message(GeneralConfigs.SaveConfig(info));
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