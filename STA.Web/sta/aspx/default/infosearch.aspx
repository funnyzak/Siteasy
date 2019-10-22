<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.Plus.InfoSearch" %>
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

	AddLinkScript(siteurl + "/sta/data/select/infotype.js");
	AddLinkScript(siteurl + "/sta/data/select/nativeplace.js");
	AddLinkScript(siteurl + "/sta/js/select.js");
	
	 seotitle = "分类信息检索 - " + webname;
	
	 location = location + " "+ config.Locationseparator +" 分类信息检索";
	

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

	templateBuilder.Append("    \r\n    <div class=\"icontent\" style=\"padding:3px 0 0 0;\">\r\n        <div class=\"ibleft clearfix\">\r\n            <div class=\"bbar w_718\">\r\n                <div class=\"tit cblue location\">您现在的位置：");
	templateBuilder.Append(location.ToString());
	templateBuilder.Append("</div>\r\n            </div>\r\n");	DataTable datas = null;
	
	string pageguide = Paging("id,savepath,filename,title,addtime,channelid,channelname,property,color,ext_nativeplace,ext_infotype","",30,10,false,out datas);
	
	templateBuilder.Append("\r\n          <div class=\"bcont w_718\" style=\"min-height:700px;padding:0 0 15px 0;\">\r\n				<div class=\"parminfos\">\r\n                   <dl class=\"choose\">\r\n                   <dt>发布区域:</dt>\r\n                      <dd>\r\n                         ");	SelectInfo nativeparent = SelectParentNode("nativeplace",native);
	
	SelectInfo nativecur = SelectByVal("nativeplace",native);
	
	 list = SelectSubList("nativeplace",native);
	

	if (list.Rows.Count<=0)
	{

	 list = SelectSubList("nativeplace",nativeparent.Value);
	

	}	//end if

	templateBuilder.Append("\r\n                         <a href=\"infosearch.aspx?native=0&infotype=");
	templateBuilder.Append(infotype.ToString());
	templateBuilder.Append("&chnid=");
	templateBuilder.Append(chnid.ToString());
	templateBuilder.Append("&query=");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\">不限</a>\r\n                         ");
	if (nativeparent.Id<=0&&nativecur.Name!="")
	{

	templateBuilder.Append("\r\n                         <a href=\"infosearch.aspx?native=0&infotype=");
	templateBuilder.Append(infotype.ToString());
	templateBuilder.Append("&chnid=");
	templateBuilder.Append(chnid.ToString());
	templateBuilder.Append("&query=");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\" class=\"bold\">");
	templateBuilder.Append(nativecur.Name.ToString().Trim());
	templateBuilder.Append(">></a>\r\n                        ");
	}
	else if (nativeparent.Id>0)
	{

	templateBuilder.Append("\r\n                        <a href=\"infosearch.aspx?native=");
	templateBuilder.Append(nativeparent.Value.ToString().Trim());
	templateBuilder.Append("&infotype=");
	templateBuilder.Append(infotype.ToString());
	templateBuilder.Append("&chnid=");
	templateBuilder.Append(chnid.ToString());
	templateBuilder.Append("&query=");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\" class=\"bold\">");
	templateBuilder.Append(nativecur.Name.ToString().Trim());
	templateBuilder.Append(">></a>\r\n                        ");
	}	//end if


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;


	if (item["value"].ToString().Trim()==native)
	{

	templateBuilder.Append("\r\n                        <a href=\"infosearch.aspx?native=" + item["value"].ToString().Trim() + "&infotype=");
	templateBuilder.Append(infotype.ToString());
	templateBuilder.Append("&chnid=");
	templateBuilder.Append(chnid.ToString());
	templateBuilder.Append("&query=");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\" class=\"on\">" + item["name"].ToString().Trim() + "</a>\r\n                        ");
	}
	else
	{

	templateBuilder.Append("\r\n                        <a href=\"infosearch.aspx?native=" + item["value"].ToString().Trim() + "&infotype=");
	templateBuilder.Append(infotype.ToString());
	templateBuilder.Append("&chnid=");
	templateBuilder.Append(chnid.ToString());
	templateBuilder.Append("&query=");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\">" + item["name"].ToString().Trim() + "</a>\r\n                        ");
	}	//end if


	}	//end loop

	templateBuilder.Append(" \r\n                      </dd>\r\n                   <dt>信息类型:</dt>\r\n                      <dd>\r\n                         ");	SelectInfo infotypeparent = SelectParentNode("infotype",infotype);
	
	SelectInfo infotypecur = SelectByVal("infotype",infotype);
	
	 list = SelectSubList("infotype",infotype);
	

	if (list.Rows.Count<=0)
	{

	 list = SelectSubList("infotype",infotypeparent.Value);
	

	}	//end if

	templateBuilder.Append("\r\n                          <a href=\"infosearch.aspx?native=");
	templateBuilder.Append(native.ToString());
	templateBuilder.Append("&infotype=0&chnid=");
	templateBuilder.Append(chnid.ToString());
	templateBuilder.Append("&query=");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\">不限</a>\r\n                         ");
	if (infotypeparent.Id<=0&&infotypecur.Name!="")
	{

	templateBuilder.Append("\r\n                         <a href=\"infosearch.aspx?native=");
	templateBuilder.Append(native.ToString());
	templateBuilder.Append("&infotype=0&chnid=");
	templateBuilder.Append(chnid.ToString());
	templateBuilder.Append("&query=");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\" class=\"bold\">");
	templateBuilder.Append(infotypecur.Name.ToString().Trim());
	templateBuilder.Append(">></a>\r\n                        ");
	}
	else if (infotypeparent.Id>0)
	{

	templateBuilder.Append("\r\n                        <a href=\"infosearch.aspx?native=");
	templateBuilder.Append(native.ToString());
	templateBuilder.Append("&infotype=");
	templateBuilder.Append(infotypeparent.Value.ToString().Trim());
	templateBuilder.Append("&chnid=");
	templateBuilder.Append(chnid.ToString());
	templateBuilder.Append("&query=");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\" class=\"bold\">");
	templateBuilder.Append(infotypecur.Name.ToString().Trim());
	templateBuilder.Append(">></a>\r\n                        ");
	}	//end if


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;


	if (item["value"].ToString().Trim()==infotype)
	{

	templateBuilder.Append("\r\n                        <a href=\"infosearch.aspx?native=");
	templateBuilder.Append(native.ToString());
	templateBuilder.Append("&infotype=" + item["value"].ToString().Trim() + "&chnid=");
	templateBuilder.Append(chnid.ToString());
	templateBuilder.Append("&query=");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\" class=\"on\">" + item["name"].ToString().Trim() + "</a>\r\n                        ");
	}
	else
	{

	templateBuilder.Append("\r\n                        <a href=\"infosearch.aspx?native=");
	templateBuilder.Append(native.ToString());
	templateBuilder.Append("&infotype=" + item["value"].ToString().Trim() + "&chnid=");
	templateBuilder.Append(chnid.ToString());
	templateBuilder.Append("&query=");
	templateBuilder.Append(query.ToString());
	templateBuilder.Append("\">" + item["name"].ToString().Trim() + "</a>\r\n                        ");
	}	//end if


	}	//end loop

	templateBuilder.Append(" \r\n                      </dd>\r\n                  </dl>\r\n                </div>\r\n                ");
	if (datas.Rows.Count>0)
	{

	templateBuilder.Append("\r\n                <ul class=\"l718\">\r\n                    ");
	item__loop__id=0;
	foreach(DataRow item in datas.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	string title = TitleFormat(item["title"].ToString().Trim(),item["property"].ToString().Trim(),(item["color"].ToString().Trim() == "000000"? defcolor: item["color"].ToString().Trim()),82,"..");
	
	templateBuilder.Append("	\r\n                    <li class=\"middot\"><span class=\"cblue s14\" val=\"" + item["ext_infotype"].ToString().Trim() + "\"></span><a class=\"cgrey2\" title=\"" + item["title"].ToString().Trim() + "\" target=\"_self\" href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\">");
	templateBuilder.Append(title.ToString());
	templateBuilder.Append("</a>&nbsp;<span class=\"cgrey\">");	templateBuilder.Append(Convert.ToDateTime(Convert.ToDateTime(item["addtime"].ToString().Trim())).ToString(" yyyy-MM-dd"));
	templateBuilder.Append("</span></li>\r\n                    ");
	}	//end loop

	templateBuilder.Append("\r\n                </ul>\r\n                ");
	}
	else
	{

	templateBuilder.Append("\r\n                    <div class=\"searchdesc\" style=\"border:none;padding:20px 10px;\">抱歉，没有找到相关信息，看看最新发布的信息吧. <a href=\"infosearch.aspx\" class=\"s14 cblue underline\">点击这里</a></div>\r\n                ");
	}	//end if


	if (pageguide!="")
	{

	templateBuilder.Append("\r\n                <div class=\"pages\">");
	templateBuilder.Append(pageguide.ToString());
	templateBuilder.Append("</div>\r\n                ");
	}	//end if

	templateBuilder.Append("\r\n            </div>\r\n        </div>\r\n        <div class=\"sright\">\r\n        ");
	templateBuilder.Append("            <div class=\"bbar w_235\">\r\n                <div class=\"tit\"><a href=\"#\" class=\"cblue bold\">热门信息</a></div>\r\n            </div>\r\n            <div class=\"bcont w_235\" style=\"margin:0 0 6px 0;padding-bottom:6px;\">\r\n                <ul class=\"l235\">\r\n                    ");
	list = GetTable("content", "type=channel ctype=5 ext=info num=" +  "10" + " fields=id,savepath,filename,color,property,title,ext_infotype order=" +  "3" + "");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	string title = TitleFormat(item["title"].ToString().Trim(),item["property"].ToString().Trim(),(item["color"].ToString().Trim() == "000000"? defcolor: item["color"].ToString().Trim()),24,"..");
	
	templateBuilder.Append("	\r\n                    <li><span class=\"cblue s13\" val=\"" + item["ext_infotype"].ToString().Trim() + "\"></span><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" tilte=\"" + item["title"].ToString().Trim() + "\">");
	templateBuilder.Append(title.ToString());
	templateBuilder.Append("</a></li>\r\n                    ");
	}	//end loop

	templateBuilder.Append("  \r\n                </ul>\r\n            </div>\r\n            <div class=\"bbar w_235\">\r\n                <div class=\"tit\"><a href=\"#\" class=\"cblue bold\">推荐信息</a></div>\r\n            </div>\r\n            <div class=\"bcont w_235\" style=\"margin:0 0 6px 0;padding-bottom:6px;\">\r\n                <ul class=\"l235\">\r\n                    ");
	list = GetTable("content", "type=channel ctype=5 ext=info num=" +  "9" + " fields=id,savepath,filename,color,property,title,ext_infotype order=" +  "2" + " property=r");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	string title = TitleFormat(item["title"].ToString().Trim(),item["property"].ToString().Trim(),(item["color"].ToString().Trim() == "000000"? defcolor: item["color"].ToString().Trim()),24,"..");
	
	templateBuilder.Append("	\r\n                    <li><span class=\"cblue s13\" val=\"" + item["ext_infotype"].ToString().Trim() + "\"></span><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" tilte=\"" + item["title"].ToString().Trim() + "\">");
	templateBuilder.Append(title.ToString());
	templateBuilder.Append("</a></li>\r\n                    ");
	}	//end loop

	templateBuilder.Append("         \r\n                </ul>\r\n            </div>");

	templateBuilder.Append("   \r\n        </div>\r\n    </div>\r\n    <script type=\"text/javascript\">\r\n        $(\"span[val]\").each(function () { $(this).html(\"[\" + getSelectTextFromData(\"infotype\", $(this).attr(\"val\")) + \"]\"); });\r\n    </");
	templateBuilder.Append("script>\r\n	");
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
