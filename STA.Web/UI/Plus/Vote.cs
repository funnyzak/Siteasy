using System;
using System.Text;
using System.Web;

using STA.Common;
using STA.Config;
using STA.Data;
using STA.Entity.Plus;

namespace STA.Web.Plus
{
    public class Vote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (STARequest.GetQueryInt("id", 0) != 0)
            {
                int id = STARequest.GetQueryInt("id", 0);
                int utype = STARequest.GetQueryInt("utype", 0);
                if (STARequest.GetQueryString("display") == "html")
                {
                    Response.Write(Build(id));
                }
                else
                {
                    string build = Build(id).Replace("\"", "\\\"");
                    string ret = string.Empty;
                    foreach (string str in build.Split(new string[] { "\r\n" }, StringSplitOptions.None))
                    {
                        ret += string.Format("document.write(\"{0}\")\r\n", str);
                    }
                    Response.Write(ret);
                }
            }
        }

        private string Build(int id)
        {
            string posturl = GeneralConfigs.GetConfig().Weburl + BaseConfigs.GetSitePath + "/plus/vote.aspx?id=" + id.ToString();
            VoteInfo info = STA.Data.Plus.GetStaVote(STARequest.GetQueryInt("id", 0));
            StringBuilder build = new StringBuilder(30000);
            build.Append("<div class=\"votepre\">\r\n")
                 .Append("<form name=\"voteform\" method=\"post\" action=\"" + posturl + "\" target=\"_blank\">\r\n")
                 .Append("<dl>\r\n")
                 .Append("<dt>" + info.Title + "</dt>\r\n");
            int loop = 0;
            foreach (VoteItem item in info.VoteList)
            {
                string firstcheck = string.Empty;
                if (loop == 0)
                {
                    firstcheck = " checked=\"checked\"";
                }
                string itype = info.IsMore == 1 ? "checkbox" : "radio";
                string idstr = "v_" + info.Id.ToString() + "_" + item.Id.ToString();
                build.Append(string.Format("<dd><input type=\"{0}\" value=\"{1}\" name=\"item\" id=\"{2}\"" + firstcheck + "/><label for=\"{2}\" title=\"{3}\">{3}</label></dd>\r\n", itype, item.Id.ToString(), idstr, item.Content));
                loop++;
            }
            build.Append("</dl>\r\n")
                .Append("<div style=\"clear:both\"></div>\r\n")
                .Append("<div class=\"votebtn\"><input type=\"submit\" value=\"投票\" /> <input type=\"button\" value=\"查看结果\" onclick=\"window.open('" + posturl + "');\"/></div>\r\n")
                .Append("</form></div>");
            return build.ToString();
        }
    }
}