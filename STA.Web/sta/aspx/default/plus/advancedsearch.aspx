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
	int ty__loop__id=0;


	DataTable list;
	DataTable pht;
	DataTable ls;
	DataTable lts;

	templateBuilder.Capacity = 220000;
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>高级搜索 - ");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("</title>\r\n<meta name=\"description\" content=\"高级搜索,");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\"/>\r\n<meta name=\"keywords\" content=\"高级搜索,");
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

	templateBuilder.Append("\r\n    <div class=\"votetit\">\r\n    	自定义高级搜索\r\n    </div>\r\n    <div class=\"votecon\">\r\n    <form method=\"get\" action=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/search.aspx\">\r\n    <div class=\"vform\">\r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">网站频道：</div>\r\n            <div class=\"left radio\">\r\n                ");	 list = SubChlList(0);
	
	templateBuilder.Append("\r\n                <select name=\"chlid\">\r\n                    <option value=\"-1\">默认</option>\r\n                    ");
	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	templateBuilder.Append("\r\n                    <option value=\"" + item["id"].ToString().Trim() + "\">" + item["name"].ToString().Trim() + "</option>\r\n                    ");	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	DataTable sublist = SubChlList(cid);
	

	im__loop__id=0;
	foreach(DataRow im in sublist.Rows)
	{
		im__loop__id++;

	templateBuilder.Append("\r\n                    <option value=\"" + im["id"].ToString().Trim() + "\">&nbsp;&nbsp;&nbsp;&nbsp;" + im["name"].ToString().Trim() + "</option>  \r\n                    ");
	}	//end loop


	}	//end loop

	templateBuilder.Append("\r\n                </select>\r\n            </div>\r\n        </div>\r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">搜索模式：</div>\r\n            <div class=\"input left\">\r\n                <input type=\"radio\" name=\"searchtype\" value=\"1\" id=\"st_1\" checked=\"checked\"/><label for=\"st_1\">标题搜索</label>\r\n                <input type=\"radio\" name=\"searchtype\" value=\"2\" id=\"st_2\"/><label for=\"st_2\">智能搜索</label>\r\n            </div>\r\n        </div>\r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">关键字：</div>\r\n            <div class=\"input left\">\r\n                <input type=\"text\" class=\"txt\" id=\"query\" name=\"query\" value=\"" + STARequest.GetString("query") + "\" style=\"width:230px;\"/>\r\n            </div>\r\n        </div>\r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">内容类型：</div>\r\n            <div class=\"left radio\">\r\n                ");	 list = Caches.GetContypeTable(GeneralConfigs.GetConfig().Cacheinterval * 60);
	
	templateBuilder.Append("\r\n                <select name=\"typeid\">\r\n                    <option value=\"-1\">默认</option>\r\n                    ");
	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;


	if (item["open"].ToString().Trim()=="1")
	{

	templateBuilder.Append("\r\n                    <option value=\"" + item["id"].ToString().Trim() + "\">" + item["name"].ToString().Trim() + "</option>\r\n                    ");
	}	//end if


	}	//end loop

	templateBuilder.Append("\r\n                </select>\r\n            </div>\r\n        </div>\r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">发布时间：</div>\r\n            <div class=\"left radio\">\r\n                <select name=\"durday\">\r\n                    <option value=\"0\">默认</option>\r\n                    <option value=\"7\">一周内</option>\r\n                    <option value=\"30\">一个月内</option>\r\n                    <option value=\"90\">三个月内</option>\r\n                    <option value=\"365\">一年内</option>\r\n                </select>\r\n            </div>\r\n        </div> \r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">排序方式：</div>\r\n            <div class=\"left radio\">\r\n                <select name=\"order\">\r\n                    <option value=\"0\">默认</option>\r\n                    <option value=\"0\">发布时间</option>\r\n                    <option value=\"7\">更新时间</option>\r\n                    <option value=\"3\">浏览量</option>\r\n                    <option value=\"6\">评论量</option>\r\n                </select>\r\n            </div>\r\n        </div> \r\n        <div class=\"row clearfix\">\r\n            <div class=\"field left s13\">每页显示：</div>\r\n            <div class=\"left radio\">\r\n                <select name=\"persize\">\r\n                    <option value=\"15\">默认&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</option>\r\n                    <option value=\"10\">10条</option>\r\n                    <option value=\"15\">15条</option>\r\n                    <option value=\"20\">20条</option>\r\n                    <option value=\"30\">30条</option>\r\n                </select>\r\n            </div>\r\n        </div> \r\n        <div class=\"row2\">\r\n            <input type=\"button\" id=\"btnsub\" name=\"btnsub\" class=\"com\" value=\"搜索\"/>&nbsp;&nbsp;\r\n            <input type=\"button\" id=\"Button1\" class=\"com\" value=\"重置\" onclick=\"$('form').get(0).reset();\"/>\r\n        </div>\r\n    </div>\r\n            </form>\r\n    </div>\r\n    ");
	templateBuilder.Append("    <div class=\"vt_footer\">\r\n        Copyright &copy; 2007-2011 <a href=\"http://www.stacms.com\" title=\"站易内容管理系统\" target=\"_blank\" class=\"cdarkgrey\">STACMS</a> 中视网维 版权所有\r\n    </div>");

	templateBuilder.Append("\r\n</div>\r\n<script type=\"text/javascript\">\r\n    var ay = [\"键入关键字搜索..\"];\r\n    function searchEvent() {\r\n        $( \"#query\").mouseover(function () {\r\n            $(this).val($(this).val() == ay[0] ? \"\" : $(this).val()).focus();\r\n        });\r\n        var gosearch = function () {\r\n            var v = $.trim($( \"#query\").val());\r\n            if (v.length == 0 || v == ay[0]) {\r\n                $(\"#query\").select();\r\n                return false;\r\n            };\r\n            $(\"form\").get(0).submit();\r\n        };\r\n        $(\"#btnsub\").bind(\"click\", gosearch);\r\n        $(document).keypress(function (event) {\r\n            if (event.which == '13') { gosearch(); }\r\n        });\r\n    };\r\n    searchEvent();\r\n</");
	templateBuilder.Append("script>\r\n</body>\r\n</html>\r\n");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
