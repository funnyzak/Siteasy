<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.Plus.Advisory" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="STA.Common" %>
<%@ Import namespace="STA.Data" %>
<%@ Import namespace="STA.Core" %>
<%@ Import namespace="STA.Entity" %>
<%@ Import namespace="STA.Config" %>

<%@ Import namespace="STA.Entity.Plus" %>
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
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>留言&建议 - ");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("</title>\r\n<meta name=\"description\" content=\"咨询建议,");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\"/>\r\n<meta name=\"keywords\" content=\"咨询建议,");
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

	templateBuilder.Append("\r\n    <div class=\"votetit\">\r\n    	我们会认真考虑来自您的评论、建议、批评，并在我们力所能及的情况下尽快回复您。\r\n    </div>\r\n    <div class=\"votecon\">\r\n    <form action=\"\" method=\"post\">\r\n    <div class=\"vform\">\r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">留言类型：</div>\r\n            <div class=\"radio left\">\r\n                ");
	                string[] mtypes = Variable("advisory_msgtype").Split(',');
	                

	ty__loop__id=0;
	foreach(string ty in mtypes)
	{
		ty__loop__id++;


	if (ty__loop__id==1)
	{

	templateBuilder.Append("\r\n                <input type=\"radio\" name=\"qtype\" value=\"");
	templateBuilder.Append(ty.ToString());
	templateBuilder.Append("\" id=\"ty");
	templateBuilder.Append(ty__loop__id.ToString());
	templateBuilder.Append("\" checked=\"checked\"/><label for=\"ty");
	templateBuilder.Append(ty__loop__id.ToString());
	templateBuilder.Append("\">");
	templateBuilder.Append(ty.ToString());
	templateBuilder.Append("</label>\r\n                ");
	}
	else
	{

	templateBuilder.Append("\r\n                <input type=\"radio\" name=\"qtype\" value=\"");
	templateBuilder.Append(ty.ToString());
	templateBuilder.Append("\" id=\"Radio1\"/><label for=\"ty");
	templateBuilder.Append(ty__loop__id.ToString());
	templateBuilder.Append("\">");
	templateBuilder.Append(ty.ToString());
	templateBuilder.Append("</label>\r\n                ");
	}	//end if


	                ty__loop__id++;
	                

	}	//end loop

	templateBuilder.Append("\r\n            </div>\r\n        </div>\r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">留言标题：</div>\r\n            <div class=\"input left\">\r\n                <input type=\"text\" class=\"txt\" id=\"title\" name=\"title\" value=\"" + STARequest.GetString("title") + "\"/>\r\n            </div>\r\n        </div>\r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">联&nbsp;系&nbsp;人：</div>\r\n            <div class=\"input left\">\r\n                <input type=\"text\" class=\"txt\" id=\"uname\" name=\"uname\" value=\"" + STARequest.GetString("uname") + "\"/>\r\n            </div>\r\n        </div> \r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">邮件地址：</div>\r\n            <div class=\"input left\">\r\n                <input type=\"text\" class=\"txt\" id=\"email\" name=\"email\" value=\"" + STARequest.GetString("email") + "\"/>\r\n            </div>\r\n        </div> \r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">联系电话：</div>\r\n            <div class=\"input left\">\r\n                <input type=\"text\" class=\"txt\" id=\"phone\" name=\"phone\" value=\"" + STARequest.GetString("phone") + "\"/>\r\n            </div>\r\n        </div> \r\n        <div class=\"row clearfix\" style=\"height:auto;padding-bottom:0;\">\r\n            <div class=\"field left s13\">留言&建议内容：</div>\r\n            <div class=\"input left\" style=\"width:750px;\">\r\n                <textarea class=\"msg\" name=\"message\" id=\"message\"></textarea>\r\n            </div>\r\n        </div> \r\n        <div class=\"row2\">\r\n            <input type=\"button\" id=\"btnsub\" name=\"btnsub\" class=\"com\" value=\"提交建议\" onclick=\"$('form').get(0).submit();\"/>\r\n        </div>\r\n    </div>\r\n            </form>\r\n    </div>\r\n    ");
	templateBuilder.Append("    <div class=\"vt_footer\">\r\n        Copyright &copy; 2007-2011 <a href=\"http://www.stacms.com\" title=\"站易内容管理系统\" target=\"_blank\" class=\"cdarkgrey\">STACMS</a> 中视网维 版权所有\r\n    </div>");

	templateBuilder.Append("\r\n</div>\r\n</body>\r\n</html>\r\n");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
