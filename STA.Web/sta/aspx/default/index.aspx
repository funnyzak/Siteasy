<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.Index" %>
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


	DataTable list;
	DataTable pht;
	DataTable ls;
	DataTable lts;

	templateBuilder.Capacity = 220000;

	AddLinkRss(siteurl + "/sta/data/rss/0.xml",webname+"RSS订阅");
	AddLinkScript(siteurl + "/sta/js/myfocus/myfocus.js");
	AddMetaInfo("","","<base target='_blank' />");
	

	string defcolor = "666";
	
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>");
	templateBuilder.Append(seotitle.ToString());
	templateBuilder.Append("</title>");
	templateBuilder.Append(meta.ToString());
	templateBuilder.Append("\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(seodescription.ToString());
	templateBuilder.Append("\"/>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(seokeywords.ToString());
	templateBuilder.Append("\"/>\r\n<meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\" />\r\n<link rel=\"shortcut icon\" href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/favicon.ico\" type=\"image/x-icon\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/css/base.css\" type=\"text/css\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/css/default.css\" type=\"text/css\" />");
	templateBuilder.Append(link.ToString());
	templateBuilder.Append("\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/jquery.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/config.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/common.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/jquery.tools.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/js/global.js\"></");
	templateBuilder.Append("script>");
	templateBuilder.Append(script.ToString());
	templateBuilder.Append("\r\n</head>\r\n<body>\r\n<div class=\"wrapper\">\r\n    <div class=\"topheaderwrap\">\r\n	    <div class=\"topheader\">\r\n    	    <div class=\"left cgrey2\">站易CMS - 让每个人都可以轻松建站！</div>\r\n            <div class=\"right cblue\">\r\n        	    <span class=\"topuserinfo\" style=\"display:none;padding-right:10px;\">您好,<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/\" class=\"cred\"><script type=\"text/javascript\">document.writeln(stauser.nickname);</");
	templateBuilder.Append("script></a>&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/useraction.aspx?action=loginout\" class=\"cgrey\">[退出]</a></span><span class=\"toplogin\" style=\"display:none;padding-right:10px;\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/register.aspx\" class=\"cblue\" target=\"_self\">注册</a> | <a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/login.aspx\" class=\"cblue\" target=\"_self\">登录</a></span> <a href=\"");
	templateBuilder.Append(Urls.Sitemap().ToString().Trim());
	templateBuilder.Append("\" class=\"cblue\" target=\"_blank\">网站地图</a> | <a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/taglist.html\" class=\"cblue\" target=\"_blank\">TAG标签</a>&nbsp;&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/rss.html\" class=\"cblue\" target=\"_blank\">RSS订阅</a>&nbsp;&nbsp;[<a href=\"javascript:setHomePage('");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("');\" class=\"cblue\" target=\"_self\">设为首页</a>] [<a href=\"javascript:addFavourite('");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("', '");
	templateBuilder.Append(weburl.ToString());
	templateBuilder.Append("');\" class=\"cblue\" target=\"_self\">加入收藏</a>] \r\n            </div>\r\n            <script type=\"text/javascript\">\r\n                $(\".topuserinfo\").css(\"display\", $.inArray(stauser.userid, [\"\", \"0\", \"-1\"]) < 0 ? \"\" : \"none\");\r\n                $(\".toplogin\").css(\"display\", $.inArray(stauser.userid, [\"\", \"0\", \"-1\"]) < 0 ? \"none\" : \"\");\r\n            </");
	templateBuilder.Append("script>\r\n        </div>\r\n    </div>\r\n    <div class=\"header\">\r\n    	<div class=\"logo\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" title=\"");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/logo.png\" /></a></div>\r\n        <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/files/ad/201212061740514746.js?1\"></");
	templateBuilder.Append("script>\r\n    </div>\r\n    <div class=\"menu\">\r\n    	<ul class=\"m\">     \r\n        	<li style=\"background:none;\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" title=\"");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\" class=\"cwhite fyahei s14\">首页</a></li>\r\n            ");
	list = GetTable("channel", "num=" +  "9" + " fields=id,name order=" +  "2" + "");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	templateBuilder.Append("\r\n            <li><a href=\"");
	templateBuilder.Append(Urls.Channel(cid).ToString().Trim());
	templateBuilder.Append("\" class=\"cwhite fyahei s14\">" + item["name"].ToString().Trim() + "</a></li>\r\n            ");
	}	//end loop

	templateBuilder.Append("\r\n            <li><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/magazine.aspx\" class=\"cwhite fyahei s14\">在线杂志</a></li>\r\n        </ul>\r\n        <div class=\"search\">\r\n        	<div class=\"s_txt cblue s13\">全站搜索：</div>\r\n            <div class=\"s_input\"><input class=\"query\" type=\"text\" placeholder=\"键入关键字搜索..\" name=\"query\" autocomplete=\"off\" id=\"query\"/></div>\r\n            <div class=\"s_btn gosearch\">&nbsp;</div>\r\n            <script type=\"text/javascript\">searchEvent();</");
	templateBuilder.Append("script>\r\n            <div class=\"s_hotwords\">\r\n            	<span class=\"s13 cdarkgrey\">热门标签:</span>\r\n                ");
	list = GetTable("tag", "num=" +  "10" + " fields=id,name order=" +  "2" + "");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	templateBuilder.Append("\r\n                    <a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/tags.aspx?id=" + item["id"].ToString().Trim() + "&name=" + item["name"].ToString().Trim() + "\" title=\"" + item["name"].ToString().Trim() + "\" class=\"cblue\" target=\"_blank\">" + item["name"].ToString().Trim() + "</a>&nbsp;\r\n                ");
	}	//end loop

	templateBuilder.Append("\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/files/ad/201212061815017226.js?3\"></");
	templateBuilder.Append("script>");

	templateBuilder.Append("\r\n    <div class=\"icontent\">\r\n    	<div class=\"itop\">\r\n            <div style=\"width:282px;height:358px;overflow:hidden;float:left;\">\r\n\r\n        	<div class=\"slide\">\r\n               <div id=\"topslide\" style=\"visibility:hidden\">\r\n                  <div class=\"loading\"><span>请稍候...</span></div>\r\n                  <ul class=\"pic\">\r\n                    ");
	list = GetTable("content", "num=" +  "7" + " fields=id,typeid,savepath,filename,subtitle,img order=" +  "2" + " property=f");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	templateBuilder.Append("\r\n                    <li><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" target=\"_blank\"><img alt=\"" + item["subtitle"].ToString().Trim() + "\" text=\"\" width=\"273\" height=\"188\" src=\"" + item["img"].ToString().Trim() + "\"/></a></li> \r\n                    ");
	}	//end loop

	templateBuilder.Append("       \r\n                  </ul>\r\n                </div>\r\n            </div>\r\n            <div class=\"bbar w_275\" style=\"margin:6px 0 0 0;\">\r\n                <div class=\"tit\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" class=\"cblue bold\">编辑特荐内容</a></div>\r\n            </div>\r\n            <div class=\"bcont w_275\" style=\"height:135px;\">\r\n                <ul class=\"l275\" style=\"padding-top:4px\">\r\n                    ");
	list = GetTable("content", "num=" +  "5" + " fields=id,typeid,savepath,filename,color,property,title order=" +  "2" + " property=a");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	string title = TitleFormat(item["title"].ToString().Trim(),item["property"].ToString().Trim(),(item["color"].ToString().Trim() == "000000"? defcolor: item["color"].ToString().Trim()),36,"..");
	
	templateBuilder.Append("	\r\n                    <li><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" target=\"_blank\" title=\"" + item["title"].ToString().Trim() + "\">");
	templateBuilder.Append(title.ToString());
	templateBuilder.Append("</a></li>\r\n                    ");
	}	//end loop

	templateBuilder.Append("  \r\n                </ul>\r\n            </div>\r\n\r\n            </div>\r\n\r\n            <script type=\"text/javascript\">\r\n                $('#topslide').myFocus({ pattern: 'mF_classicHB', time: 5, width: 273, auto: true, height: 188, delay: 0, trigger: \"mouseover\" });\r\n            </");
	templateBuilder.Append("script>\r\n            <div class=\"cntbox\">\r\n                <div class=\"topcons\">\r\n                    <div class=\"onecon\">   \r\n                    ");
	list = GetTable("content", "type=channel num=" +  "1" + " fields=id,typeid,savepath,filename,subtitle,title,seodescription order=" +  "2" + " property=h");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	templateBuilder.Append("\r\n                        <h2><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" class=\"s18 bold cdarkgrey\">" + item["subtitle"].ToString().Trim() + "</a></h2>\r\n                        <p class=\"cgrey\">");	templateBuilder.Append(Utils.GetUnicodeSubString(item["seodescription"].ToString().Trim(),110,".."));
	templateBuilder.Append("<a class=\"cgrey\" href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" target=\"_blank\">[查看全文]</a></p>\r\n                    ");
	}	//end loop

	templateBuilder.Append("    \r\n                        <ul class=\"ulast clearfix\">\r\n                    ");
	list = GetTable("content", "type=congroup id=" +  "1" + " num=" +  "4" + " fields=id,typeid,savepath,filename,color,property,subtitle,title order=" +  "2" + "");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	string title = TitleFormat(item["title"].ToString().Trim(),item["property"].ToString().Trim(),(item["color"].ToString().Trim() == "000000"? defcolor: item["color"].ToString().Trim()),28,"..");
	
	templateBuilder.Append("	\r\n                            <li><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" title=\"" + item["title"].ToString().Trim() + "\">");
	templateBuilder.Append(title.ToString());
	templateBuilder.Append("</a></li>\r\n                    ");
	}	//end loop

	templateBuilder.Append(" \r\n                        </ul>\r\n                     </div>\r\n                </div>\r\n                <div class=\"lastest\">&nbsp;</div>\r\n                <ul class=\"ulast\">\r\n                    ");
	list = GetTable("content", "num=" +  "14" + " fields=id,typeid,savepath,filename,color,property,title order=" +  "0" + "");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	string title = TitleFormat(item["title"].ToString().Trim(),item["property"].ToString().Trim(),(item["color"].ToString().Trim() == "000000"? defcolor: item["color"].ToString().Trim()),24,"..");
	
	templateBuilder.Append("	\r\n                    <li class=\"middot\"><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" tilte=\"" + item["title"].ToString().Trim() + "\">");
	templateBuilder.Append(title.ToString());
	templateBuilder.Append("</a></li>\r\n                    ");
	}	//end loop

	templateBuilder.Append("  \r\n               </ul>\r\n            </div>\r\n            <div class=\"sright\">\r\n            	<div class=\"bbar w_235\">\r\n                	<div class=\"tit\"><a href=\"#\" class=\"cblue bold\">资讯点击排行</a></div>\r\n                    <div class=\"more\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" class=\"s12 clightgrey\">更多>></a></div>\r\n                </div>\r\n                <div class=\"bcont w_235\" style=\"height:332px;\">\r\n                	<ul class=\"l235\">\r\n                ");
	list = GetTable("content", "type=channel id=" +  "1" + " num=" +  "12" + " fields=id,typeid,savepath,filename,color,property,title,click order=" +  "3" + "");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	string title = TitleFormat(item["title"].ToString().Trim(),item["property"].ToString().Trim(),(item["color"].ToString().Trim() == "000000"? defcolor: item["color"].ToString().Trim()),30,"..");
	
	templateBuilder.Append("	\r\n                        <li><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" title=\"" + item["title"].ToString().Trim() + "(点击:" + item["click"].ToString().Trim() + ")\">");
	templateBuilder.Append(title.ToString());
	templateBuilder.Append("</a></li>\r\n                ");
	}	//end loop

	templateBuilder.Append(" \r\n                    </ul>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"icontent\">\r\n        <div class=\"ibleft clearfix\">\r\n            <div class=\"iclist-718\">\r\n            	<div class=\"bbar w_718\">\r\n                	<div class=\"tit\"><a href=\"");
	templateBuilder.Append(Urls.Channel(2).ToString().Trim());
	templateBuilder.Append("\" class=\"cblue bold\">图集推荐</a></div>\r\n                    <div class=\"more\"><a href=\"");
	templateBuilder.Append(Urls.Channel(2).ToString().Trim());
	templateBuilder.Append("\" target=\"_blank\" class=\"s12 clightgrey\">更多>></a></div>\r\n                </div>\r\n                <div class=\"bcont w_718\" style=\"height:auto;\">\r\n                	\r\n                 	<div class=\"pslide\">\r\n                    	<div class=\"scrollable\">\r\n                        <div class=\"items\">\r\n                        <div class=\"pslide-con\">\r\n                        ");
	list = GetTable("content", "ctype=2 num=" +  "28" + " fields=id,typeid,savepath,filename,color,property,title,img order=" +  "0" + " property=r");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	string title = TitleFormat(item["title"].ToString().Trim(),item["property"].ToString().Trim(),(item["color"].ToString().Trim() == "000000"? defcolor: item["color"].ToString().Trim()),20,"..");
	
	templateBuilder.Append("	\r\n                        	<ul>\r\n                            	<li class=\"m\"><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" title=\"" + item["title"].ToString().Trim() + "\" target=\"_blank\"><img src=\"" + item["img"].ToString().Trim() + "\" alt=\"" + item["title"].ToString().Trim() + "\"/></a></li>\r\n                                <li class=\"t\"><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" title=\"" + item["title"].ToString().Trim() + "\" target=\"_blank\">");
	templateBuilder.Append(title.ToString());
	templateBuilder.Append("</a></li>\r\n                            </ul>\r\n                        ");
	if (item__loop__id%4==0&&item__loop__id!=list.Rows.Count)
	{

	templateBuilder.Append("\r\n                        </div><div class=\"pslide-con\">\r\n                        ");
	}	//end if


	}	//end loop

	templateBuilder.Append("\r\n                        </div>\r\n                     	</div>\r\n                        </div>\r\n                        <div class=\"lbtn\">&nbsp;</div>\r\n                        <div class=\"rbtn\">&nbsp;</div>\r\n						<script type=\"text/javascript\">\r\n						     $(\".scrollable\").scrollable({ next: \".lbtn\", prev: \".rbtn\", circular: true, speed: 700 });\r\n                        </");
	templateBuilder.Append("script>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n            ");
	list = GetTable("channel", "type=chlgroup id=" +  "2" + " num=" +  "30" + " fields=id,name order=" +  "2" + "");


	citem__loop__id=0;
	foreach(DataRow citem in list.Rows)
	{
		citem__loop__id++;

	int chid = TypeParse.StrToInt(citem["id"].ToString().Trim());
	

	if (citem__loop__id%2==1)
	{

	templateBuilder.Append("\r\n        	<div class=\"iclist\" style=\"margin-right:6px;\">\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n            <div class=\"iclist\">\r\n            ");
	}	//end if

	templateBuilder.Append("\r\n            	<div class=\"bbar w_356\">\r\n                	<div class=\"tit\"><a href=\"");
	templateBuilder.Append(Urls.Channel(chid).ToString().Trim());
	templateBuilder.Append("\" class=\"cblue bold\">" + citem["name"].ToString().Trim() + "</a></div>\r\n                    <div class=\"more\"><a href=\"");
	templateBuilder.Append(Urls.Channel(chid).ToString().Trim());
	templateBuilder.Append("\" target=\"_blank\" class=\"s12 clightgrey\">更多>></a></div>\r\n                </div>\r\n                <div class=\"bcont w_356\" style=\"padding-bottom:5px\">\r\n                	<ul class=\"l356\">\r\n                        ");
	list = GetTable("content", "type=channel id=" + chid.ToString() + " num=" +  "8" + " fields=id,typeid,savepath,filename,color,property,title,addtime order=" +  "0" + "");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	string title = TitleFormat(item["title"].ToString().Trim(),item["property"].ToString().Trim(),(item["color"].ToString().Trim() == "000000"? defcolor: item["color"].ToString().Trim()),34,"..");
	
	templateBuilder.Append("	\r\n                        <li class=\"middot\"><span class=\"left\"><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" target=\"_blank\" title=\"" + item["title"].ToString().Trim() + "\">");
	templateBuilder.Append(title.ToString());
	templateBuilder.Append("</a></span><span class=\"right cgrey\">");	templateBuilder.Append(Convert.ToDateTime(Convert.ToDateTime(item["addtime"].ToString().Trim())).ToString(" MM-dd"));
	templateBuilder.Append("</span></li>\r\n                        ");
	}	//end loop

	templateBuilder.Append("  \r\n                    </ul>\r\n                </div>\r\n            </div>\r\n            ");
	}	//end loop

	templateBuilder.Append("\r\n        </div>\r\n        <div class=\"sright\">\r\n            <div class=\"bbar w_235\">\r\n                <div class=\"tit\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" class=\"cblue bold\">编辑推荐内容</a></div>\r\n            </div>\r\n            <div class=\"bcont w_235\" style=\"height:299px;margin:0 0 6px 0;\">\r\n                <ul class=\"l235\">\r\n                    ");
	list = GetTable("content", "num=" +  "11" + " fields=id,typeid,savepath,filename,color,property,title order=" +  "2" + " property=r");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	string title = TitleFormat(item["title"].ToString().Trim(),item["property"].ToString().Trim(),(item["color"].ToString().Trim() == "000000"? defcolor: item["color"].ToString().Trim()),30,"..");
	
	templateBuilder.Append("	\r\n                    <li><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" target=\"_blank\" title=\"" + item["title"].ToString().Trim() + "\">");
	templateBuilder.Append(title.ToString());
	templateBuilder.Append("</a></li>\r\n                    ");
	}	//end loop

	templateBuilder.Append("  \r\n                </ul>\r\n            </div>\r\n            <div class=\"bbar w_235\">\r\n                <div class=\"tit\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" class=\"cblue bold\">做个小调查</a></div>\r\n            </div>\r\n            <div class=\"bcont w_235\" style=\"height:auto;margin:0 0 6px 0;\">\r\n                <div style=\"padding:0 0 10px 8px;\">\r\n                    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/plus/vote.aspx?id=1\"></");
	templateBuilder.Append("script>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"friend\">\r\n        <div class=\"bbar w_960\">\r\n            <div class=\"tit\"><a href=\"");
	templateBuilder.Append(weburl.ToString());
	templateBuilder.Append("/flink.aspx\" class=\"cblue bold\">友情链接</a></div>\r\n            <div class=\"fnav\">\r\n                <ul class=\"fnav-ul\">\r\n            ");
	lts = GetDbTable("sta_linktypes","num=" +  "100" + " fields=id,name order=orderid_desc cache=" +  "0" + "");


	item__loop__id=0;
	foreach(DataRow item in lts.Rows)
	{
		item__loop__id++;

	templateBuilder.Append("\r\n                 <li>" + item["name"].ToString().Trim() + "</li>\r\n            ");
	}	//end loop

	templateBuilder.Append("\r\n                </ul>\r\n            </div>\r\n            <div class=\"more\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/plus/flinkreg.aspx\" class=\"s12 clightgrey\">申请加入</a>&nbsp;&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/flink.aspx\" class=\"s12 clightgrey\">所有链接</a></div>\r\n        </div>\r\n        <div class=\"bcont w_960\" style=\"min-height:30px;padding:7px 0 15px;\">\r\n            <div class=\"flink\">\r\n            ");
	list = GetTable("linktype", "num=" +  "8" + " fields=id,name order=" +  "2" + "");


	im__loop__id=0;
	foreach(DataRow im in list.Rows)
	{
		im__loop__id++;

	templateBuilder.Append("\r\n                <div class=\"flink-con\">\r\n                ");	int cid = TypeParse.StrToInt(im["id"].ToString().Trim(),0);
	

	list = GetTable("link", "id=" + cid.ToString() + " num=" +  "500" + " fields=id,name,url order=" +  "2" + "");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	templateBuilder.Append("\r\n                    <a href=\"" + item["url"].ToString().Trim() + "\" title=\"" + item["name"].ToString().Trim() + "\" target=\"_blank\" class=\"s12 cgrey\">" + item["name"].ToString().Trim() + "</a>&nbsp;&nbsp;\r\n                ");
	}	//end loop

	templateBuilder.Append("\r\n                </div>\r\n            ");
	}	//end loop

	templateBuilder.Append("\r\n            </div>\r\n        </div>\r\n        <script type=\"text/javascript\">\r\n            $(\"ul.fnav-ul\").tabs(\".flink-con\", { event: 'mouseover', current: 'on' });\r\n        </");
	templateBuilder.Append("script>\r\n    </div>\r\n    ");
	templateBuilder.Append("    <div class=\"footer\">\r\n    	 Copyright &copy; 2007-2011 <a href=\"http://www.stacms.com\" title=\"站易内容管理系统\" target=\"_blank\" class=\"cdarkgrey\">STACMS</a> 中视网维 版权所有");
	templateBuilder.Append(config.Extcode.ToString().Trim());
	templateBuilder.Append("\r\n    </div>\r\n</div>\r\n</body>\r\n</html>\r\n");

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
