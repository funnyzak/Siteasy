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
		This page was created by Siteasy CMS Template Engine at 2019/10/22 21:38:14.
		本页面代码由Siteasy CMS模板引擎生成于 2019/10/22 21:38:14. 
	*/

	base.OnInit(e);


	int item__loop__id=0;
	int gp__loop__id=0;
	int im__loop__id=0;
	int i__loop__id=0;


	DataTable list;
	DataTable pht;
	DataTable ls;

	templateBuilder.Capacity = 220000;
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>友情链接 - ");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("</title>\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(seodescription.ToString());
	templateBuilder.Append("\"/>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(seokeywords.ToString());
	templateBuilder.Append("\"/>\r\n<meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\" />\r\n<link rel=\"shortcut icon\" href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/favicon.ico\" type=\"image/x-icon\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/css/stacms.css\" type=\"text/css\" />\r\n</head>\r\n<body class=\"sitemapwp\">\r\n    <div class=\"sm_header\">\r\n        <div class=\"left header_logo\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" title=\"");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\" target=\"_self\"><img title=\"");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\" src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/logo.png\" /></a></div>\r\n        <div class=\"right header_summary\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" title=\"");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\" class=\"cblue s14\">返回首页</a>&nbsp;&nbsp;<a href=\"plus/flinkreg.aspx\" class=\"cblue s14\">申请友链</a></div>\r\n    </div>\r\n	<div class=\"icont\">\r\n    	<div class=\"con_bar_1\">\r\n        	<div class=\"tit left s14\">欢迎您和我们交换友情链接！</div>\r\n        </div>\r\n        <div class=\"con_body\">\r\n            ");
	list = GetTable("linktype", "num=" +  "1000" + " fields=id,name order=" +  "2" + "");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim(),0);
	
	templateBuilder.Append("	\r\n        	<div class=\"forsm\">\r\n            	<div class=\"tit\"><a href=\"javascript:;\" title=\"" + item["name"].ToString().Trim() + "\" class=\"s14 bold cblue2\">" + item["name"].ToString().Trim() + "</a></div>\r\n                ");
	ls = GetTable("link", "num=" +  "1000" + " id=" + cid.ToString() + " fields=name,url order=" +  "2" + "");


	if (ls.Rows.Count>0)
	{

	templateBuilder.Append("\r\n                <div class=\"sub\">\r\n                	<ul class=\"clearfix\">\r\n                        ");
	i__loop__id=0;
	foreach(DataRow i in ls.Rows)
	{
		i__loop__id++;

	templateBuilder.Append("\r\n                        <li><a href=\"" + i["url"].ToString().Trim() + "\" title=\"" + i["name"].ToString().Trim() + "\" target=\"_blank\">" + i["name"].ToString().Trim() + "</a></li>\r\n                        ");
	}	//end loop

	templateBuilder.Append("\r\n                    </ul>\r\n                </div>\r\n                ");
	}	//end if

	templateBuilder.Append("\r\n            </div>\r\n            ");
	}	//end loop

	templateBuilder.Append("\r\n        </div>\r\n    </div>\r\n   ");
	templateBuilder.Append("    <div class=\"vt_footer\">\r\n        Copyright &copy; 2007-2011 <a href=\"http://www.stacms.com\" title=\"站易内容管理系统\" target=\"_blank\" class=\"cdarkgrey\">STACMS</a> 中视网维 版权所有");
	templateBuilder.Append(config.Extcode.ToString().Trim());
	templateBuilder.Append("\r\n    </div>");

	templateBuilder.Append("\r\n</body>\r\n</html>\r\n");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
