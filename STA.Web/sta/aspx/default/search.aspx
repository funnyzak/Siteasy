<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.Search" %>
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


	DataTable list;

	templateBuilder.Capacity = 220000;
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>");
	templateBuilder.Append(seotitle.ToString());
	templateBuilder.Append("</title>\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(seodescription.ToString());
	templateBuilder.Append("\"/>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(seokeywords.ToString());
	templateBuilder.Append("\"/>\r\n<meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\" />\r\n<link rel=\"shortcut icon\" href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/favicon.ico\" type=\"image/x-icon\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/css/stacms.css\" type=\"text/css\" />\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/jquery.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/config.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/common.js\"></");
	templateBuilder.Append("script>\r\n</head>\r\n<body>\r\n<div class=\"topheader_wrap\">\r\n    <div class=\"topheader2\">\r\n        <div class=\"left cgrey2\">\r\n          &nbsp;站易CMS - 让每个人都可以轻松建站！\r\n        </div>\r\n        <div class=\"right cblue\">\r\n            <span class=\"topuserinfo\" style=\"display:none;padding-right:10px;\">您好,<script type=\"text/javascript\">document.writeln(stauser.nickname);</");
	templateBuilder.Append("script>&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/useraction.aspx?action=loginout\" class=\"cgrey\">[退出]</a></span><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/plus/advancedsearch.aspx\" class=\"cblue\" target=\"_blank\">高级搜索</a>&nbsp;|&nbsp;<a href=\"");
	templateBuilder.Append(Urls.Sitemap().ToString().Trim());
	templateBuilder.Append("\" class=\"cblue\" target=\"_blank\">网站地图</a>&nbsp;|&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/taglist.html\" class=\"cblue\" target=\"_blank\">TAG标签</a>&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/rss.html\" class=\"cblue\" target=\"_blank\">RSS订阅</a>&nbsp;&nbsp;[<a href=\"javascript:setHomePage('");
	templateBuilder.Append(weburl.ToString());
	templateBuilder.Append("');\" class=\"cblue\" target=\"_self\">设为首页</a>][<a href=\"javascript:addFavourite('");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("', '");
	templateBuilder.Append(weburl.ToString());
	templateBuilder.Append("');\" class=\"cblue\" target=\"_self\">加入收藏</a>]&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" class=\"cblue\">返回首页</a> &nbsp;\r\n        </div>\r\n        <script type=\"text/javascript\">$(\".topuserinfo\").css(\"display\", $.inArray(stauser.userid, [\"\", \"0\", \"-1\"]) < 0 ? \"\" : \"none\");</");
	templateBuilder.Append("script>\r\n    </div>\r\n</div>\r\n<div class=\"header2 clearfix\">\r\n    <div class=\"logo left\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/logo.png\" /></a></div>\r\n    <div class=\"searchbox cgrey s13 left\">\r\n        <div class=\"stype\">\r\n            <input type=\"radio\" name=\"searchtype\" id=\"searchtype-1\" value=\"1\" /> <label for=\"searchtype-1\">标题检索</label>&nbsp;\r\n            <input type=\"radio\" name=\"searchtype\" id=\"searchtype-2\" value=\"2\" /> <label for=\"searchtype-2\">智能检索</label>\r\n        </div>\r\n        <div class=\"sparms clearfix\">\r\n            <div class=\"left iptquery\"><input type=\"text\" class=\"query\" id=\"query\" name=\"query\" value=\"");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\" autocomplete=\"off\"/></div>\r\n            <div class=\"left iptsch\"><input type=\"button\" class=\"gosearch\" id=\"gosearch\" name=\"gosearch\"/></div>\r\n            <div class=\"left txtbtn\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/plus/advancedsearch.aspx\" class=\"s12 cblue\">高级搜索</a></div>\r\n        </div>\r\n        <div class=\"ad right\">\r\n        \r\n        </div>\r\n    </div>\r\n</div>\r\n");	DataTable datas = null;
	
	int pagecount = 0;
	
	string orderdesc = "时间";
	
	int recordcount = 0;
	
	string pageguide = Paging("id,savepath,filename,title,addtime,content,click,channelid,channelname",20,out datas,out pagecount,out recordcount);
	

	switch(order)
	{
	    case 0: orderdesc = "发布时间"; break;
	    case 1: orderdesc = "ID"; break;
	    case 2: orderdesc = "权重"; break;
	    case 3: orderdesc = "点击量"; break;
	    case 4: orderdesc = "顶数"; break;
	    case 5: orderdesc = "踩数"; break;
	    case 6: orderdesc = "评论数"; break;
	    case 7: orderdesc = "更新时间"; break;
	    default: orderdesc = "时间"; break;
	}
	
	templateBuilder.Append("\r\n<div class=\"searchbar clearfix\">\r\n    <div class=\"left cgrey s13\">&nbsp;搜索“<strong class=\"cred\">");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("</strong>”的网页 根据");
	templateBuilder.Append(orderdesc.ToString());
	templateBuilder.Append("排序</div>\r\n    <div class=\"right clightgrey s13\">共有 <span class=\"cgrey\">");
	templateBuilder.Append(recordcount.ToString());
	templateBuilder.Append("</span> 条结果，<span class=\"cgrey\">");
	templateBuilder.Append(pagecount.ToString());
	templateBuilder.Append("</span> 页&nbsp;</div>\r\n</div>\r\n<div class=\"searchcon clearfix\">\r\n	<div class=\"searchleft left\">\r\n\r\n        ");
	if (datas.Rows.Count>0)
	{

	templateBuilder.Append("\r\n        <div class=\"searchlist\">\r\n            ");
	item__loop__id=0;
	foreach(DataRow item in datas.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	int channelid = TypeParse.StrToInt(item["channelid"].ToString().Trim());
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = config.Withweburl==1? Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim()):(weburl+Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim()));
	
	string hname = Utils.HightLightStr(Utils.GetUnicodeSubString(item["title"].ToString(),150,".."), query, "f00", false, "14");
	
	string html = Utils.RemoveLine(Utils.RemoveHtml(item["content"].ToString())).Replace(" ","");
	
	 html = Utils.GetInputAroundString(html, query.Split(' '), 100,"...");
	
	 html = Utils.HightLightStr(html, query, "f00", false, "14");
	
	templateBuilder.Append("\r\n        	<div class=\"searchitem\">\r\n            	<div class=\"tit\"><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" target=\"_blank\" title=\"" + item["title"].ToString().Trim() + "\" class=\"s14 bold cblue2\">");
	templateBuilder.Append(hname.ToString());
	templateBuilder.Append("</a></div>\r\n                <div class=\"desc sdarkgrey s13\">");
	templateBuilder.Append(html.ToString());
	templateBuilder.Append("</div>\r\n                <div class=\"summay cgrey\"><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" target=\"_blank\" title=\"" + item["title"].ToString().Trim() + "\" class=\"cblue3 s13\">");	templateBuilder.Append(Utils.GetUnicodeSubString(curl,46,".."));
	templateBuilder.Append("</a>&nbsp;&nbsp;频道:<a href=\"");
	if (config.Withweburl!=1)
	{
	templateBuilder.Append(weburl.ToString());
	}	//end if
	templateBuilder.Append(Urls.Channel(channelid).ToString().Trim());
	templateBuilder.Append("\" target=\"_blank\" title=\"" + item["channelname"].ToString().Trim() + "\" class=\"cblue3\">" + item["channelname"].ToString().Trim() + "</a> 浏览量:" + item["click"].ToString().Trim() + "&nbsp;&nbsp;发布时间:");	templateBuilder.Append(Convert.ToDateTime(Convert.ToDateTime(item["addtime"].ToString().Trim())).ToString(" yyyy-MM-dd"));
	templateBuilder.Append("</div>\r\n            </div>\r\n            ");
	}	//end loop

	templateBuilder.Append("\r\n        </div>\r\n\r\n        ");
	}
	else
	{

	templateBuilder.Append("\r\n    	<div class=\"cdarkgrey s14\" style=\"padding:10px 0;\">\r\n			没有找到关于“<strong class=\"cred\">");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("</strong>”的网页！可以试试用 <a href=\"http://www.baidu.com/baidu?wd=");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\" target=\"_blank\" class=\"cblue underline\" title=\"百度搜索\">Baidu</a> 或  <a href=\"https://www.google.com.hk/#hl=zh-CN&newwindow=1&safe=strict&site=&source=hp&q=");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\" target=\"_blank\" title=\"谷歌搜索\" class=\"cblue underline\">谷歌</a> 找一下\r\n        </div>\r\n        ");
	}	//end if


	if (pageguide!="")
	{

	templateBuilder.Append("\r\n        <div class=\"pguide\">");
	templateBuilder.Append(pageguide.ToString());
	templateBuilder.Append("</div>\r\n        ");
	}	//end if

	templateBuilder.Append("\r\n    </div>\r\n    <div class=\"searchright right s13 cdarkgrey\">\r\n    	用 <a href=\"http://www.baidu.com/baidu?wd=");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\" target=\"_blank\" class=\"cblue s13 underline\">百度</a> 搜索一下<br/>\r\n        	<b>按时间筛选</b><br/>\r\n        	        <a class=\"cblue underline dd0\" href=\"search.aspx?searchtype=");
	templateBuilder.Append(searchtype.ToString());
	templateBuilder.Append("&order=");
	templateBuilder.Append(order.ToString());
	templateBuilder.Append("&ordertype=");
	templateBuilder.Append(ordertype.ToString());
	templateBuilder.Append("&persize=");
	templateBuilder.Append(persize.ToString());
	templateBuilder.Append("&durdate=0&typeid=");
	templateBuilder.Append(typeid.ToString());
	templateBuilder.Append("&chlid=");
	templateBuilder.Append(chlid.ToString());
	templateBuilder.Append("&query=");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\">全部</a>&nbsp;&nbsp;&nbsp;\r\n                    <a class=\"cblue underline dd7\" href=\"search.aspx?searchtype=");
	templateBuilder.Append(searchtype.ToString());
	templateBuilder.Append("&order=");
	templateBuilder.Append(order.ToString());
	templateBuilder.Append("&ordertype=");
	templateBuilder.Append(ordertype.ToString());
	templateBuilder.Append("&persize=");
	templateBuilder.Append(persize.ToString());
	templateBuilder.Append("&durdate=7&typeid=");
	templateBuilder.Append(typeid.ToString());
	templateBuilder.Append("&chlid=");
	templateBuilder.Append(chlid.ToString());
	templateBuilder.Append("&query=");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\">一周内</a>&nbsp;&nbsp;&nbsp;\r\n                    <a class=\"cblue underline dd30\" href=\"search.aspx?searchtype=");
	templateBuilder.Append(searchtype.ToString());
	templateBuilder.Append("&order=");
	templateBuilder.Append(order.ToString());
	templateBuilder.Append("&ordertype=");
	templateBuilder.Append(ordertype.ToString());
	templateBuilder.Append("&persize=");
	templateBuilder.Append(persize.ToString());
	templateBuilder.Append("&durdate=30&typeid=");
	templateBuilder.Append(typeid.ToString());
	templateBuilder.Append("&chlid=");
	templateBuilder.Append(chlid.ToString());
	templateBuilder.Append("&query=");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\">一月内</a>&nbsp;&nbsp;&nbsp;\r\n                    <a class=\"cblue underline dd90\" href=\"search.aspx?searchtype=");
	templateBuilder.Append(searchtype.ToString());
	templateBuilder.Append("&order=");
	templateBuilder.Append(order.ToString());
	templateBuilder.Append("&ordertype=");
	templateBuilder.Append(ordertype.ToString());
	templateBuilder.Append("&persize=");
	templateBuilder.Append(persize.ToString());
	templateBuilder.Append("&durdate=90&typeid=");
	templateBuilder.Append(typeid.ToString());
	templateBuilder.Append("&chlid=");
	templateBuilder.Append(chlid.ToString());
	templateBuilder.Append("&query=");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\">三月内</a>&nbsp;&nbsp;&nbsp;\r\n                    <a class=\"cblue underline dd365\" href=\"search.aspx?searchtype=");
	templateBuilder.Append(searchtype.ToString());
	templateBuilder.Append("&order=");
	templateBuilder.Append(order.ToString());
	templateBuilder.Append("&ordertype=");
	templateBuilder.Append(ordertype.ToString());
	templateBuilder.Append("&persize=");
	templateBuilder.Append(persize.ToString());
	templateBuilder.Append("&durdate=365&typeid=");
	templateBuilder.Append(typeid.ToString());
	templateBuilder.Append("&chlid=");
	templateBuilder.Append(chlid.ToString());
	templateBuilder.Append("&query=");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\">一年内</a>\r\n    </div>\r\n</div>\r\n<div class=\"search-footer cgrey\">\r\n	 Copyright &copy; 2007-2011 <a href=\"http://www.stacms.com\" title=\"站易内容管理系统\" target=\"_blank\" class=\"cdarkgrey\">STACMS</a> 中视网维 版权所有\r\n</div>\r\n<script language=\"javascript\" type=\"text/javascript\">\r\n    var ay = [\"键入关键字搜索..\"];\r\n    function searchEvent() {\r\n        $(\"#query\").mouseover(function () {\r\n            $(this).val($(this).val() == ay[0] ? \"\" : $(this).val()).focus();\r\n        });\r\n        var gosearch = function () {\r\n            var v = $.trim($(\"#query\").val());\r\n            if (v.length == 0 || v == ay[0]) {\r\n                $(\"#query\").select();\r\n                return false;\r\n            };\r\n            location.href = \"search.aspx?searchtype=\" +$(\"input[name='searchtype']:checked\").val()+ \"&persize=15&durdate=0&query=\" + escape(v);\r\n        };\r\n        $(\"#gosearch\").bind(\"click\", gosearch);\r\n        $(document).keypress(function (event) {\r\n            if (event.which == '13') { gosearch(); }\r\n        });\r\n    };\r\n    searchEvent();\r\n    $(\"input[name='searchtype'][value='");
	templateBuilder.Append(searchtype.ToString());
	templateBuilder.Append("']\").attr(\"checked\", true);\r\n    $(\".dd");
	templateBuilder.Append(durday.ToString());
	templateBuilder.Append("\").addClass(\"bold\");\r\n</");
	templateBuilder.Append("script>\r\n</body>\r\n</html>\r\n");

	 if(config.Opentran==1 && curlang!=config.Weblang){
	    string tranrlt = Translate(templateBuilder.ToString(), config.Weblang, curlang);
	    if(tranrlt != "")
	    {
	        Response.Write(iscompress ? Utils.CompressHtml(tranrlt) : tranrlt);
	        return;
	    }
	 }
	 



	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
