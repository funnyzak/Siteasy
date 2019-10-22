<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.PageBase" %>
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
		This page was created by Siteasy CMS Template Engine at 2019/10/22 21:38:15.
		本页面代码由Siteasy CMS模板引擎生成于 2019/10/22 21:38:15. 
	*/

	base.OnInit(e);


	int item__loop__id=0;
	int gp__loop__id=0;
	int im__loop__id=0;
	int i__loop__id=0;
	int citem__loop__id=0;
	int oitem__loop__id=0;


	DataTable list;
	DataTable pht;
	DataTable ls;
	DataTable lts;

	templateBuilder.Capacity = 220000;
	templateBuilder.Append("<!doctype html public \"-//w3c//dtd xhtml 1.0 transitional//en\" \"http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\">\r\n<title>杂志书柜 - ");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("</title>\r\n<link href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/favicon.ico\" type=\"image/x-icon\" rel=\"icon\">\r\n<meta name=\"keywords\" content=\"杂志,在线阅读下载\">\r\n<meta name=\"description\" content=\"在线阅读下载\">\r\n<link href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/css/mag/style.css\" rel=\"stylesheet\" type=\"text/css\">\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/jquery.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/config.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/common.js\"></");
	templateBuilder.Append("script>\r\n</head>\r\n<body>\r\n	<div style=\"position:absolute;top:0;left:0;z-index:0;width:100%;height:100%\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/css/mag/images/01.jpg\" class=\"zbg\" width=\"100%\" height=\"100%\"/></div>\r\n    <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"maglisttitle\" style=\"position:absolute;top:0;left:0;z-index:1\">\r\n        <tbody><tr>\r\n            <td class=\"bookcaseico\" style=\"width: 140px\">\r\n            <a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" class=\"g3booklink\"></a>\r\n            </td>\r\n            <td id=\"magcasename\" class=\"bookcasename\">");
	templateBuilder.Append(weburl.substring(7).ToString().Trim());
	templateBuilder.Append("</td>\r\n\r\n            <td class=\"bookcaseright\">\r\n                <a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" class=\"g3link\" target=\"_blank\">&lt;&lt;返回首页</a>\r\n            </td>\r\n        </tr>\r\n    </tbody></table>\r\n    <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"books\" style=\"position:absolute;top:50px;left:0;z-index:1\">\r\n        <tbody><tr>\r\n            <td class=\"maglist_l\">\r\n            </td>\r\n            <td id=\"magcontent\" class=\"maglist_m\">  \r\n                ");	string likeid = STARequest.GetString("likeid");
	

	if (likeid!="")
	{


	list = GetSqlTable("select id,name,cover from tbprefix_magazines where status = 1 and likeid = 'variable_likeid' order by orderid desc,id desc");


	}
	else
	{


	list = GetSqlTable("select id,name,cover from tbprefix_magazines where status = 1 order by orderid desc,id desc");


	}	//end if


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	templateBuilder.Append("\r\n                <div class=\"magdt\">\r\n                	<div class=\"magname\">\r\n                    	<a target=\"_blank\" href=\"magview.aspx?id=" + item["id"].ToString().Trim() + "\" title=\"" + item["name"].ToString().Trim() + "\">");	templateBuilder.Append(Utils.GetUnicodeSubString(item["name"].ToString().Trim(),14,""));
	templateBuilder.Append("</a></div><div class=\"magimg\"><a target=\"_blank\" href=\"magview.aspx?id=" + item["id"].ToString().Trim() + "\" title=\"" + item["name"].ToString().Trim() + "\"><img alt=\"" + item["name"].ToString().Trim() + "\" src=\"" + item["cover"].ToString().Trim() + "\"></a>\r\n                    </div>\r\n                </div>\r\n                ");
	}	//end loop

	templateBuilder.Append(" \r\n			</td>\r\n            <td class=\"maglist_r\">\r\n            </td>\r\n        </tr>\r\n    </tbody></table>\r\n</body>\r\n</html>");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
