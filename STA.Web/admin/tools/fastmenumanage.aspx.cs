using System;
using System.IO;
using System.Xml;
using System.Web;

using STA.Common;
using STA.Config;
using STA.Core;
using System.Data;

namespace STA.Web.Admin.Tools
{
    public partial class fastmenumanage : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "delmenu":
                        DelMenu(STARequest.GetInt("hidValue", 0));
                        base.RegisterClientScriptBlock("top.LoadFastMenuData();");
                        break;
                }
                hidAction.Value = "";
                BindData();
            }
            if (!IsPostBack)
                BindData();
        }


        private void DelMenu(int id)
        {
            string filename = ConUtils.UserLikeXmlPath(userid, baseconfig);
            XmlDocument doc = XMLUtil.LoadDocument(filename);
            XmlNodeList items = doc.SelectNodes("data/fastmenu/item");
            if (items.Count > 0 && id < items.Count)
            {
                doc.SelectSingleNode("data/fastmenu").RemoveChild(items.Item(id));
                XMLUtil.SaveDocument(ConUtils.UserLikeXmlPath(userid, baseconfig), doc);
            }
        }

        protected void BindData()
        {
            DataTable menudt = new DataTable();
            menudt.Columns.Add("id", Type.GetType("System.Int32"));
            menudt.Columns.Add("name", Type.GetType("System.String"));
            menudt.Columns.Add("url", Type.GetType("System.String"));
            menudt.Columns.Add("target", Type.GetType("System.String"));

            XmlDocument doc = XMLUtil.LoadDocument(ConUtils.UserLikeXmlPath(userid, baseconfig));
            int loop = 0;
            foreach (XmlNode node in doc.SelectNodes("data/fastmenu/item"))
            {
                DataRow menudr = menudt.NewRow();
                menudr["id"] = loop;
                menudr["name"] = TypeParse.ObjToString(node.Attributes["name"].Value);
                menudr["url"] = TypeParse.ObjToString(node.Attributes["url"].Value);
                menudr["target"] = TypeParse.ObjToString(node.Attributes["target"].Value);
                menudt.Rows.Add(menudr);
                loop++;
            }
            rptData.Visible = true;
            rptData.DataSource = menudt;
            rptData.DataBind();
        }
    }
}