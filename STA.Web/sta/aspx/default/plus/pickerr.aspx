<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.Plus.Pickerr" %>
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
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>内容挑错 - ");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("</title>\r\n<meta name=\"description\" content=\"内容挑错,");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\"/>\r\n<meta name=\"keywords\" content=\"内容挑错,");
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

	templateBuilder.Append("\r\n    <div class=\"votetit\">\r\n    	感谢为我们的内容挑错！\r\n    </div>\r\n    <div class=\"votecon\">\r\n    <form action=\"\" method=\"post\">\r\n    <div class=\"vform\">\r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">错误类型：</div>\r\n            <div class=\"left radio\">\r\n                    ");
	                    string[] mtypes = Variable("pickerr_type").Split(',');
	                    
	templateBuilder.Append("\r\n                <select name=\"type\">\r\n                    ");
	ty__loop__id=0;
	foreach(string ty in mtypes)
	{
		ty__loop__id++;

	templateBuilder.Append("\r\n                        <option value=\"");
	templateBuilder.Append(ty.ToString());
	templateBuilder.Append("\">");
	templateBuilder.Append(ty.ToString());
	templateBuilder.Append("</option>\r\n                    ");
	}	//end loop

	templateBuilder.Append("\r\n                </select>\r\n            </div>\r\n        </div>\r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">错误标题：</div>\r\n            <div class=\"input left\">\r\n                <input type=\"text\" class=\"txt\" id=\"title\" name=\"title\" value=\"" + STARequest.GetString("title") + "\"/>\r\n                <input type=\"hidden\" name=\"cid\" value=\"" + STARequest.GetString("cid") + "\"/>\r\n            </div>\r\n        </div>\r\n        <div class=\"row clearfix\" style=\"height:auto;\">\r\n            <div class=\"field left s13\">错误内容：</div>\r\n            <div class=\"input left\" style=\"width:750px;\">\r\n                <textarea class=\"msg\" name=\"errortxt\" id=\"errortxt\">" + STARequest.GetString("errortxt") + "</textarea>\r\n            </div>\r\n        </div> \r\n        <div class=\"row clearfix\" style=\"height:auto;padding-bottom:0;\">\r\n            <div class=\"field left s13\">正确内容：</div>\r\n            <div class=\"input left\" style=\"width:750px;\">\r\n                <textarea class=\"msg\" name=\"righttxt\" id=\"righttxt\" onclick=\"if($(this).val()=='可以把修改建议写到这里...'){ $(this).empty(); }\">可以把修改建议写到这里...</textarea>\r\n            </div>\r\n        </div> \r\n        <div class=\"row2\">\r\n            <input type=\"button\" id=\"btnsub\" name=\"btnsub\" class=\"com\" value=\"提交错误给我们\" onclick=\"$('form').get(0).submit();\"/>\r\n        </div>\r\n    </div>\r\n            </form>\r\n    </div>\r\n    ");
	templateBuilder.Append("    <div class=\"vt_footer\">\r\n        Copyright &copy; 2007-2011 <a href=\"http://www.stacms.com\" title=\"站易内容管理系统\" target=\"_blank\" class=\"cdarkgrey\">STACMS</a> 中视网维 版权所有\r\n    </div>");

	templateBuilder.Append("\r\n</div>\r\n</body>\r\n</html>\r\n");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
