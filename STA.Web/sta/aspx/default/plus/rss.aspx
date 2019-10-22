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


	DataTable list;
	DataTable pht;
	DataTable ls;
	DataTable lts;

	templateBuilder.Capacity = 220000;
	 iscompress = false;
	
	templateBuilder.Append("\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>RSS订阅中心 - ");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("</title>\r\n<meta name=\"description\" content=\"RSS订阅,");
	templateBuilder.Append(seodescription.ToString());
	templateBuilder.Append("\"/>\r\n<meta name=\"keywords\" content=\"RSS订阅,");
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
	templateBuilder.Append("\" class=\"cblue s14\">返回首页</a>&nbsp;&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" class=\"cblue s14\">帮助</a></div>\r\n    </div>\r\n	<div class=\"icont\">\r\n    	<div class=\"con_bar_1\">\r\n        	<div class=\"tit left s14\">从RSS订阅中心订阅您感兴趣内容！</div>\r\n        </div>\r\n        <div class=\"con_body\">\r\n        \r\n            <div style=\"width:870px;margin:0 auto;text-align:left;line-height:30px;padding:10px 0;\" class=\"cdarkgrey c14\">\r\n            <b class=\"s14\">什么是RSS</b><br />\r\nRSS是站点用来和其他站点之间共享内容的一种简易方式（也叫聚合内容），网络用户可以在客户端借助于支持RSS的新闻聚合工具软件，在不打开网站内容页面的情况下阅读支持RSS输出的网站内容。网站提供RSS输出，有利于让用户发现网站内容的更新。通过订阅此“真正简单的联合发布系统”（RSS），您可以在任何 RSS 阅读器或聚合器预览对此空间所作的更新。RSS 允许您从几处来源的订阅项目，并自动将信息组合到一个列表中。 您可以快速浏览列表，而不用访问每个网站，就可以搜索您感兴趣的最新信息。<br />\r\n <b class=\"s14\">如何使用RSS</b><br />\r\n您一般需要下载和安装一个RSS新闻阅读器，然后从网站提供的聚合新闻目录列表中订阅您感兴趣的新闻栏目的内容。订阅后，您将会及时获得所订阅新闻频道的最新内容。\r\n\r\n            </div>\r\n            ");	DataTable chls = SubChlList(0);
	

	item__loop__id=0;
	foreach(DataRow item in chls.Rows)
	{
		item__loop__id++;


	if (item["ishidden"].ToString()!="1")
	{

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim(),0);
	
	string rurl = siteurl + "/sta/data/rss/" + item["id"].ToString().Trim() + ".xml";
	
	templateBuilder.Append("\r\n        	<div class=\"forsm\">\r\n            	<div class=\"tit clearfix\">\r\n                    <div class=\"left\" style=\"width:169px;\">\r\n                            <a href=\"");
	templateBuilder.Append(Urls.Channel(cid).ToString().Trim());
	templateBuilder.Append("\" title=\"" + item["name"].ToString().Trim() + "\" class=\"s14 bold cblue2\">");	templateBuilder.Append(Utils.GetUnicodeSubString(item["name"].ToString().Trim(),18,".."));
	templateBuilder.Append("</a>\r\n                    </div>\r\n                    <div class=\"left\" style=\"width:310px\">\r\n                            <a href=\"");
	templateBuilder.Append(rurl.ToString());
	templateBuilder.Append("\" title=\"" + item["name"].ToString().Trim() + "订阅\" class=\"cblue\" target=\"_blank\">");
	templateBuilder.Append(rurl.ToString());
	templateBuilder.Append("</a>\r\n                    </div>\r\n                    <div class=\"left\" style=\"width:370px;\">\r\n                        <a href=\"");
	templateBuilder.Append(rurl.ToString());
	templateBuilder.Append("\" title=\"" + item["name"].ToString().Trim() + "订阅\" target=\"_blank\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/rss/rsspic2.gif\" alt=\"" + item["name"].ToString().Trim() + "订阅\"/></a>&nbsp;&nbsp;&nbsp;<a href=\"http://www.google.com/ig/add?feedurl=");
	templateBuilder.Append(rurl.ToString());
	templateBuilder.Append("\" title=\"" + item["name"].ToString().Trim() + "订阅\" target=\"_blank\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/rss/21.gif\" alt=\"" + item["name"].ToString().Trim() + "订阅\"/></a><a href=\"http://add.my.yahoo.com/rss?url=");
	templateBuilder.Append(rurl.ToString());
	templateBuilder.Append("\" title=\"" + item["name"].ToString().Trim() + "订阅\" target=\"_blank\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/rss/22.gif\" alt=\"" + item["name"].ToString().Trim() + "订阅\"/></a><a href=\"http://my.msn.com/addtomymsn.armx?id=rss&ut=");
	templateBuilder.Append(rurl.ToString());
	templateBuilder.Append("\" title=\"" + item["name"].ToString().Trim() + "订阅\" target=\"_blank\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/rss/26.gif\" alt=\"" + item["name"].ToString().Trim() + "订阅\"/></a><a href=\"http://www.live.com/?add=");
	templateBuilder.Append(rurl.ToString());
	templateBuilder.Append("\" title=\"" + item["name"].ToString().Trim() + "订阅\" target=\"_blank\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/rss/29.gif\" alt=\"" + item["name"].ToString().Trim() + "订阅\"/></a>\r\n                    </div>\r\n                </div>\r\n                ");	DataTable lls = SubChlList(cid);
	

	if (lls.Rows.Count>0)
	{

	templateBuilder.Append("\r\n                <div class=\"sub\">\r\n                	<ul class=\"clearfix\">\r\n                        ");
	i__loop__id=0;
	foreach(DataRow i in lls.Rows)
	{
		i__loop__id++;

	int iid = TypeParse.StrToInt(i["id"].ToString().Trim(),0);
	

	if (i["ishidden"].ToString()!="1")
	{

	 rurl = siteurl + "/sta/data/rss/" + i["id"].ToString().Trim() + ".xml";
	
	templateBuilder.Append("\r\n                        <li class=\"line\">\r\n                            <div class=\"left\" style=\"width:160px;\">\r\n                                 <a href=\"");
	templateBuilder.Append(Urls.Channel(iid).ToString().Trim());
	templateBuilder.Append("\" title=\"" + i["name"].ToString().Trim() + "\" class=\"cblue\">");	templateBuilder.Append(Utils.GetUnicodeSubString(i["name"].ToString().Trim(),18,".."));
	templateBuilder.Append("</a>\r\n                            </div>\r\n                            <div class=\"left\" style=\"width:310px\">\r\n                                 <a href=\"");
	templateBuilder.Append(rurl.ToString());
	templateBuilder.Append("\" title=\"" + i["name"].ToString().Trim() + "订阅\" class=\"cblue\" target=\"_blank\">");
	templateBuilder.Append(rurl.ToString());
	templateBuilder.Append("</a>\r\n                            </div>\r\n                            <div class=\"left\" style=\"width:370px;\">\r\n                                <a href=\"");
	templateBuilder.Append(rurl.ToString());
	templateBuilder.Append("\" title=\"" + i["name"].ToString().Trim() + "订阅\" target=\"_blank\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/rss/rsspic2.gif\" alt=\"" + i["name"].ToString().Trim() + "订阅\"/></a>&nbsp;&nbsp;&nbsp;<a href=\"http://www.google.com/ig/add?feedurl=");
	templateBuilder.Append(rurl.ToString());
	templateBuilder.Append("\" title=\"" + i["name"].ToString().Trim() + "订阅\" target=\"_blank\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/rss/21.gif\" alt=\"" + i["name"].ToString().Trim() + "订阅\"/></a><a href=\"http://add.my.yahoo.com/rss?url=");
	templateBuilder.Append(rurl.ToString());
	templateBuilder.Append("\" title=\"" + i["name"].ToString().Trim() + "订阅\" target=\"_blank\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/rss/22.gif\" alt=\"" + i["name"].ToString().Trim() + "订阅\"/></a><a href=\"http://my.msn.com/addtomymsn.armx?id=rss&ut=");
	templateBuilder.Append(rurl.ToString());
	templateBuilder.Append("\" title=\"" + i["name"].ToString().Trim() + "订阅\" target=\"_blank\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/rss/26.gif\" alt=\"" + i["name"].ToString().Trim() + "订阅\"/></a><a href=\"http://www.live.com/?add=");
	templateBuilder.Append(rurl.ToString());
	templateBuilder.Append("\" title=\"" + i["name"].ToString().Trim() + "订阅\" target=\"_blank\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/rss/29.gif\" alt=\"" + i["name"].ToString().Trim() + "订阅\"/></a>\r\n                            </div>\r\n                        </li>\r\n                        ");
	}	//end if


	}	//end loop

	templateBuilder.Append("\r\n                    </ul>\r\n                </div>\r\n                ");
	}	//end if

	templateBuilder.Append("\r\n            </div>\r\n            ");
	}	//end if


	}	//end loop

	templateBuilder.Append("\r\n        </div>\r\n    </div>\r\n   ");
	templateBuilder.Append("    <div class=\"vt_footer\">\r\n        Copyright &copy; 2007-2011 <a href=\"http://www.stacms.com\" title=\"站易内容管理系统\" target=\"_blank\" class=\"cdarkgrey\">STACMS</a> 中视网维 版权所有\r\n    </div>");

	templateBuilder.Append("\r\n</body>\r\n</html>\r\n");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
