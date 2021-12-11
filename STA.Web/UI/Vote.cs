using System;
using System.Data;
using System.Web;
using System.Text;
using System.Xml;

using STA.Common;
using STA.Entity;
using STA.Cache;
using STA.Data;
using STA.Core;
using STA.Config;

namespace STA.Web.UI
{
    public class Vote : System.Web.UI.Page
    {
        private DataTable votelist;
        private string vtype = STARequest.GetString("vtype"); //获取方式  ids集合(ids)  标识列(like)
        private string relval = STARequest.GetString("relval"); //相关值
        private string display = STARequest.GetQueryString("display"); //显示方式 html js
        public Vote()
        {
            votelist = vtype == "like" ? Votes.GetVoteByLikeid(relval) : Votes.GetVoteByIds(relval);
            HttpContext.Current.Response.Headers.Add("Content-Type", display == "html" ? "text/html" : "application/javascript");
            HttpContext.Current.Response.Write(display == "html" ? Build() : Utils.HtmlToJs(Build()));
        }


        private string Build()
        {
            GeneralConfigInfo config = GeneralConfigs.GetConfig();
            VoteConfigInfo voteconfig = VoteConfigs.GetConfig();
            StringBuilder build = new StringBuilder(30000);
            int vindex = 1;
            string viewurl = config.Weburl + BaseConfigs.GetSitePath + "/voteshow.aspx?vtype=" + vtype + "&relval=" + relval;
            foreach (DataRow dr in votelist.Rows)
            {
                string posturl = config.Weburl + BaseConfigs.GetSitePath + "/voteshow.aspx?id=" + dr["id"].ToString();
                build.Append("<div class=\"votepre\">\r\n")
                     .Append("<form name=\"voteform\" method=\"post\" action=\"" + posturl + "\" target=\"_blank\">\r\n")
                     .Append(string.Format("<input type=\"hidden\" name=\"voteitem\" value=\"{0}\" max=\"{1}\"/>\r\n", dr["id"], dr["maxvote"]))
                     .Append(string.Format("<input type=\"hidden\" name=\"voteitemname\" value=\"{0}\"/>\r\n", dr["name"]))
                     .Append("<dl>\r\n")
                     .Append("<dt>" + (votelist.Rows.Count > 1 ? (vindex.ToString() + "、") : "") + dr["name"].ToString() + "</dt>\r\n");

                int loop = 0;
                DataTable options = Votes.GetVoteOptionDataTable(TypeParse.StrToInt(dr["id"]), "id,name", "orderid", "desc");

                foreach (DataRow item in options.Rows)
                {
                    string firstcheck = string.Empty;
                    if (loop == 0)
                    {
                        firstcheck = " checked=\"checked\"";
                    }
                    string itype = TypeParse.StrToInt(dr["maxvote"]) > 1 ? "checkbox" : "radio";
                    string idstr = "v_" + dr["id"].ToString() + "_" + item["id"].ToString();
                    build.Append(string.Format("<dd><input type=\"{0}\" value=\"{1}\" name=\"item\" group=\"item_{4}\" id=\"{2}\"" + firstcheck + "/><label for=\"{2}\" title=\"{3}\">{3}</label></dd>\r\n", itype, item["id"], idstr, item["name"], dr["id"]));
                    loop++;
                }
                build.Append("</dl>\r\n");
                build.Append("<div style=\"clear:both\"></div>\r\n");
                if (voteconfig.Infoinput == 1 || dr["isinfo"].ToString() == "1")
                {
                    build.Append("<div class='infofield'>");
                    build.Append("<div class='row clearfix'><div class='txt'>真实姓名：</div><div class='ipt'><input type='text' name='realname' id='realname'></div></div>");
                    build.Append("<div class='row clearfix'><div class='txt'>联系电话：</div><div class='ipt'><input type='text' name='phone' id='phone'></div></div>");
                    build.Append("<div class='row clearfix'><div class='txt'>身份证号：</div><div class='ipt'><input type='text' name='idcard' id='idcard'></div></div>");
                    build.Append("</div>");
                }
                if (voteconfig.Vcode == 1 || dr["isvcode"].ToString() == "1")
                {
                    build.Append("<div class=\"votevcode\"><div class=\"txt\">请输入答案：</div><div class=\"ipt\"><input type=\"text\" name=\"vcode\"/></div><div class=\"cimg\"><a href='javascript:;'><img src=\"" + config.Weburl + BaseConfigs.GetSitePath + "/sta/vcode/mathvcode.aspx?cookiename=vote" + dr["id"].ToString() + "\"/></a></div></div>\r\n");
                    build.Append("<div style=\"clear:both\"></div>\r\n");
                }
                build.Append("<div class=\"votebtn\"><input type=\"submit\" value=\"投票\" /> <input type=\"button\" value=\"查看结果\" onclick=\"window.open('" + viewurl + "');\"/></div>\r\n")
                .Append(TypeParse.StrToInt(dr["maxvote"]) > 1 ? "<script>checkBoxMaxChecked(\"input[group=item_" + dr["id"].ToString() + "]\"," + dr["maxvote"].ToString() + ")</script>\r\n" : "")
                .Append("</form></div>\r\n");
                vindex++;
            }

            return build.ToString();
        }

    }

}