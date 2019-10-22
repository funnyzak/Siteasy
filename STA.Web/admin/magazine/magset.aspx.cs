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
    public partial class magazineset : AdminPage
    {
        public MagazineInfo info = new MagazineInfo();
        public DataTable list = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            info = Contents.GetMagazine(STARequest.GetInt("id", 0));
            if (!IsPostBack)
            {
                if (info == null)
                {
                    Redirect("maglist.aspx");
                    return;
                }
                info.Ratio += ",";
                list = ConUtils.GetMagazineList(info.Content);
                filetext.InnerHtml = "杂志名称：" + GetDescString(info.Name)
                                    + "创建时间：" + info.Addtime.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>"
                    //+ "杂志页数：共" + list.Rows.Count.ToString() + "页<br/>"
                                    + "阅读尺寸：" + string.Format("宽：{0}&nbsp;高：{1}", info.Ratio.Split(',')[0], info.Ratio.Split(',')[1]);
            }
            else
            {
                info.Content = STARequest.GetFormString("content");
                info.Pages = ConUtils.GetMagazineList(info.Content).Rows.Count;
                info.Updatetime = DateTime.Now;
                Contents.EditMagazine(info);
                InsertLog(2, "杂志内容编辑", string.Format("ID:{0},杂志:{1}", info.Id, info.Name));
                Redirect("maglist.aspx?msg=" + string.Format("杂志 <b>{0}</b> 已成功编辑！", info.Name));

            }
        }

        private string GetDescString(string text)
        {
            return "<span title=\"" + text + "\">" + Utils.GetUnicodeSubString(text, 40, "..") + "</span><br/>";
        }


        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {

        }
        #endregion
    }
}