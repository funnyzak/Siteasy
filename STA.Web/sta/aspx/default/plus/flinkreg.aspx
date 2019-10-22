<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.Plus.Flinkreg" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="STA.Common" %>
<%@ Import namespace="STA.Data" %>
<%@ Import namespace="STA.Core" %>
<%@ Import namespace="STA.Entity" %>
<%@ Import namespace="STA.Config" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Siteasy CMS Template Engine at 2019/10/22 21:38:16.
		本页面代码由Siteasy CMS模板引擎生成于 2019/10/22 21:38:16. 
	*/

	base.OnInit(e);


	int item__loop__id=0;
	int gp__loop__id=0;
	int im__loop__id=0;
	int i__loop__id=0;
	int citem__loop__id=0;
	int oitem__loop__id=0;
	int ty__loop__id=0;


	DataTable list;
	DataTable pht;
	DataTable ls;
	DataTable lts;

	templateBuilder.Capacity = 220000;
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>申请友链 - ");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("</title>\r\n<meta name=\"description\" content=\"申请友链,");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\"/>\r\n<meta name=\"keywords\" content=\"申请友链,");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\"/>\r\n<meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\" />\r\n<link rel=\"shortcut icon\" href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/favicon.ico\" type=\"image/x-icon\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/css/stacms.css\" type=\"text/css\" />\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/jquery.js\"></");
	templateBuilder.Append("script>\r\n</head>\r\n<body>          \r\n<div class=\"vt_wrapper\">\r\n	");
	templateBuilder.Append("	<div class=\"header\">\r\n    	<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\"  title=\"");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\" target=\"_self\"><img title=\"");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\" src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/logo.png\" /></a>\r\n    </div>");

	templateBuilder.Append("\r\n    <div class=\"votetit\">\r\n    	<div class=\"left s14 cgrey\">申请友情链接</div>\r\n        <div class=\"right lh25\"><a href=\"/flink.aspx\" class=\"s13 clightgrey normal\">查看链接</a></div>\r\n    </div>\r\n    <div class=\"votecon\">\r\n    <form action=\"\" method=\"post\">\r\n    <div class=\"vform\">\r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">链接类型：</div>\r\n            <div class=\"left radio\">\r\n            ");
	list = GetSqlTable("select id,name from tbprefix_linktypes order by orderid desc");

	templateBuilder.Append("\r\n            <select name=\"typeid\">\r\n                ");
	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	templateBuilder.Append("\r\n                    <option value=\"" + item["id"].ToString().Trim() + "\">" + item["name"].ToString().Trim() + "</option>\r\n                ");
	}	//end loop

	templateBuilder.Append("\r\n            </select>\r\n            </div>\r\n        </div>\r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">网站名称：</div>\r\n            <div class=\"input left\">\r\n                <input type=\"text\" class=\"txt\" id=\"name\" name=\"name\" value=\"" + STARequest.GetString("name") + "\"/>\r\n            </div>\r\n        </div>\r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">网站链接：</div>\r\n            <div class=\"input left\">\r\n                <input type=\"text\" class=\"txt\" id=\"url\" name=\"url\" value=\"" + STARequest.GetString("url") + "\"/> <span class=\"clightgrey\">格式如：http://www.stacms.com/ </span>\r\n            </div>\r\n        </div>\r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">网站LOGO：</div>\r\n            <div class=\"input left\">\r\n                <input type=\"text\" class=\"txt\" id=\"logo\" name=\"logo\" value=\"" + STARequest.GetString("logo") + "\"/> <span class=\"clightgrey\">键入大小88*31的LOGO地址</span>\r\n            </div>\r\n        </div>\r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">联系邮件：</div>\r\n            <div class=\"input left\">\r\n                <input type=\"text\" class=\"txt\" id=\"email\" name=\"email\" value=\"" + STARequest.GetString("email") + "\"/>\r\n            </div>\r\n        </div>\r\n        <div class=\"row clearfix\" style=\"height:auto;\">\r\n            <div class=\"field left s13\">网站描述：</div>\r\n            <div class=\"input left\" style=\"width:750px;\">\r\n                <textarea class=\"msg\" name=\"description\" id=\"description\">" + STARequest.GetString("description") + "</textarea>\r\n            </div>\r\n        </div> \r\n        ");
	if (config.Vcodemods.IndexOf("4")>=0)
	{

	templateBuilder.Append("\r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">验证码：</div>\r\n            <div class=\"input left\">\r\n                <input type=\"text\" class=\"txt\" id=\"vcode\" name=\"vcode\" style=\"width:100px;\"/> <span><img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/vcode/validatecode.aspx?cookiename=staflinkreg&live=0\" id=\"vimg\" height=\"26\" style=\"cursor:pointer;\" /></span>\r\n            </div>\r\n        </div>\r\n        ");
	}	//end if

	templateBuilder.Append("\r\n        <div class=\"row2\">\r\n            <input type=\"button\" id=\"btnsub\" name=\"btnsub\" class=\"com\" value=\"提交友链\" onclick=\"$('form').get(0).submit();\"/>&nbsp;&nbsp;\r\n            <input type=\"button\" id=\"Button1\" class=\"com\" value=\"重置信息\" onclick=\"$('form').get(0).reset();\"/>\r\n        </div>\r\n    </div>\r\n            </form>\r\n    </div>\r\n    ");
	templateBuilder.Append("    <div class=\"vt_footer\">\r\n        Copyright &copy; 2007-2011 <a href=\"http://www.stacms.com\" title=\"站易内容管理系统\" target=\"_blank\" class=\"cdarkgrey\">STACMS</a> 中视网维 版权所有\r\n    </div>");

	templateBuilder.Append("\r\n</div>\r\n    <script type=\"text/javascript\">\r\n        $(\"#vimg,#chgcode\").click(function () { $(\"#vimg\").attr(\"src\", \"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/vcode/validatecode.aspx?cookiename=staflinkreg&live=0&date=\" + new Date()); });\r\n    </");
	templateBuilder.Append("script>\r\n</body>\r\n</html>\r\n");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
