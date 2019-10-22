using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;

namespace STA.Web.Admin
{
    public partial class wordsfilterset : AdminPage
    {
        string action = STARequest.GetString("hidAction");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData(1);
                GeneralConfigInfo info = GeneralConfigs.GetConfig();
                txtAntispamreplacemen.Text = info.Antispamreplacement;
            }
            else if (IsPostBack && action != "")
            {
                switch (action)
                {
                    case "delword":
                        STA.Data.BanWords.DelWord(STARequest.GetFormInt("hidValue", 0));
                        Message();
                        break;
                }
                hidAction.Value = "";
                BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { BindData(pageIndex); };
        }

        private void BindData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = STA.Data.BanWords.GetWordDataPage("", pageIndex, 10, "", out pageCount, out recordCount);
            pGuide.PageCount = pageCount;
            pGuide.RecordCount = recordCount;
            ViewState["pageIndex"] = pGuide.PageIndex = pageIndex;

            rptData.Visible = true;
            rptData.DataSource = dt;
            rptData.DataBind();
        }


        private void SaveInfo_Click(object sender, EventArgs e)
        {
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            info.Antispamreplacement = txtAntispamreplacemen.Text;
            ConUtils.InsertLog(2, this.userid, this.username, this.admingroupid, this.admingroupname, STARequest.GetIP(), "词语过滤", "");

            string banwords = txtWords.Text;
            if (banwords != "")
            {
                String[] badwords = banwords.Split('\n');
                String[] filterwords;
                WordInfo winfo = new WordInfo();
                winfo.Uid = userid;
                winfo.Username = username;

                #region 根据radiobuttonlist选择条件
                if (rblFilter.SelectedValue == "0")
                {
                    //清空当前词语表,再插入
                    string find = "";
                    string replacement = "";
                    string banWordsIdList = "";
                    foreach (DataRow dr in STA.Core.BanWords.GetBanWordList().Rows)
                    {
                        banWordsIdList += dr["id"].ToString() + ",";
                    }
                    if (banWordsIdList != "")
                        STA.Data.BanWords.DelWords(banWordsIdList.TrimEnd(','));

                    for (int i = 0; i < badwords.Length; i++)
                    {
                        filterwords = badwords[i].Split('=');

                        find = filterwords[0].ToString().Replace("\r", "").Trim();

                        if (!GetReplacement(badwords, filterwords, ref find, ref replacement))
                        {
                            continue;
                        }

                        winfo.Find = find;
                        winfo.Replacement = replacement;

                        STA.Data.BanWords.AddWord(winfo);
                    }

                }

                if (rblFilter.SelectedValue == "1")
                {
                    //使用新的设置覆盖已经存在的词语
                    string find = "";
                    string replacement = "";

                    for (int i = 0; i < badwords.Length; i++)
                    {
                        filterwords = badwords[i].Split('=');

                        find = filterwords[0].ToString().Replace("\r", "").Trim();

                        if (!GetReplacement(badwords, filterwords, ref find, ref replacement))
                        {
                            continue;
                        }
                        STA.Core.BanWords.UpdateBadWords(find, replacement);
                    }
                }

                if (rblFilter.SelectedValue == "2")
                {
                    //不导入已经存在的词语
                    string find = "";
                    string replacement = "";

                    DataTable dt = STA.Core.BanWords.GetBanWordList();

                    for (int i = 0; i < badwords.Length; i++)
                    {

                        filterwords = badwords[i].Split('=');

                        find = filterwords[0].ToString().Replace("\r", "").Trim();

                        if (!GetReplacement(badwords, filterwords, ref find, ref replacement))
                        {
                            continue;
                        }

                        DataRow[] arrRow = dt.Select("find='" + find + "'");

                        if (arrRow.Length == 0)
                        {
                            winfo.Find = find;
                            winfo.Replacement = replacement;
                            STA.Data.BanWords.AddWord(winfo);
                        }
                    }
                }
                #endregion
            }
            txtWords.Text = "";
            BindData(1);
            Message(GeneralConfigs.SaveConfig(info));
        }

        private static bool GetReplacement(String[] badwords, String[] filterwords, ref string find, ref string replacement)
        {
            if (find == "")
            {
                return false;
            }

            if (filterwords.Length == 2)
            {

                replacement = filterwords[1].ToString() != "" ? filterwords[1].ToString() : "**";
            }
            else if (filterwords.Length < 2)
            {
                replacement = "**";
            }
            else
            {
                replacement = filterwords[filterwords.Length - 1];

                filterwords.SetValue("", filterwords.Length - 1);

                find = string.Join("=", filterwords);
                find = find.Remove(find.Length - 2);
            }

            if (replacement == string.Empty)
            {
                replacement = "**";
            }
            return true;
        }


        void DelBtn_Click(object sender, EventArgs e)
        {
            #region 删除词语
            foreach (string str in STARequest.GetString("cbid").Split(','))
            {
                STA.Data.BanWords.DelWord(TypeParse.StrToInt(str, 0)); ;
            }
            BindData(1);
            Message();
            #endregion
        }

        void BtnSaveWords_Click(object sender, EventArgs e)
        {
            #region 保存词语
            foreach (string str in STARequest.GetString("iid").Split(','))
            {
                WordInfo info = STA.Data.BanWords.GetWord(TypeParse.StrToInt(str));
                if (info == null) continue;
                info.Find = STARequest.GetFormString("txtFind" + str);
                info.Replacement = STARequest.GetFormString("txtReplacement" + str);
                STA.Data.BanWords.EditWord(info);
            }
            BindData(TypeParse.StrToInt(ViewState["pageIndex"], 1));
            Message();
            #endregion
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
            this.BtnSaveWords.Click += new EventHandler(BtnSaveWords_Click);
            this.DelBtn.Click += new EventHandler(DelBtn_Click);
        }
        #endregion
    }
}