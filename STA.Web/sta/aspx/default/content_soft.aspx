<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.Content" %>
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


	if(info==null){
	    Server.Transfer("nofound.aspx");
	    return;
	}
	



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

	templateBuilder.Append("    \r\n    <div class=\"icontent\" style=\"padding:3px 0 0 0;\">\r\n        <div class=\"ibleft clearfix\">\r\n            <div class=\"bbar w_718\">\r\n            	");	 location = location + "&nbsp;" + config.Locationseparator + "&nbsp;"+info.Title;
	
	templateBuilder.Append("\r\n                <div class=\"tit cblue location\">您现在的位置：");
	templateBuilder.Append(location.ToString());
	templateBuilder.Append("</div>\r\n            </div>\r\n            <div class=\"bcont w_718\">\r\n				<div class=\"contit\">\r\n                	<h1 style=\"text-align:center\">");
	templateBuilder.Append(info.Title.ToString().Trim());
	templateBuilder.Append("</h1>\r\n                </div>\r\n                <div class=\"softinfo clearfix\">\r\n                    <div class=\"softsummary\">\r\n                         ");	int softlevel = TypeParse.StrToInt(info.Ext["ext_softlevel"]);
	
	templateBuilder.Append("\r\n                        <label>软件类型：</label><span>" + info.Ext["ext_softtype"].ToString().Trim() + "</span>\r\n                        <label>授权方式：</label><span>" + info.Ext["ext_license"].ToString().Trim() + "</span>\r\n                        <label>界面语言：</label><span>" + info.Ext["ext_language"].ToString().Trim() + "</span>\r\n                        <label>软件大小：</label><span>" + info.Ext["ext_filesize"].ToString().Trim() + "</span>\r\n                        <label>运行环境：</label><span title=\"" + info.Ext["ext_environment"].ToString().Trim() + "\">" + info.Ext["ext_environment"].ToString().Trim() + "</span>\r\n                        <label>软件等级：</label><span>");	for (int i = 0; i < softlevel; i++)
	{
		templateBuilder.Append("★");
	}

	for (int i = 0; i < 5-softlevel; i++)
	{
		templateBuilder.Append("☆");
	}

	templateBuilder.Append("</span>\r\n                        <label>发布时间：</label><span>");	templateBuilder.Append(Convert.ToDateTime(Convert.ToDateTime(info.Addtime)).ToString(" yyyy-MM-dd"));
	templateBuilder.Append("</span>\r\n                        <label>更新时间：</label><span>");	templateBuilder.Append(Convert.ToDateTime(Convert.ToDateTime(info.Updatetime)).ToString(" yyyy-MM-dd"));
	templateBuilder.Append("</span>\r\n                        <label>官方网址：</label><span><a href=\"" + info.Ext["ext_officesite"].ToString().Trim() + "\" title=\"" + info.Ext["ext_officesite"].ToString().Trim() + "\" target=\"_blank\">" + info.Ext["ext_officesite"].ToString().Trim() + "</a></span>\r\n                        <label>演示网址：</label><span><a href=\"" + info.Ext["ext_demourl"].ToString().Trim() + "\" title=\"" + info.Ext["ext_demourl"].ToString().Trim() + "\" target=\"_blank\">" + info.Ext["ext_demourl"].ToString().Trim() + "</a></span>\r\n                        <label>浏览次数：</label><span class=\"conclick\">");
	templateBuilder.Append(info.Click.ToString().Trim());
	templateBuilder.Append("</span>\r\n                        <label>下载次数：</label><span class=\"downcount\">" + info.Ext["ext_downcount"].ToString().Trim() + "</span>\r\n                    </div>\r\n                    <script type=\"text/javascript\">\r\n                        getDowncount(");
	templateBuilder.Append(id.ToString());
	templateBuilder.Append(",function(ret){ $(\".downcount\").html(ret); });\r\n                    </");
	templateBuilder.Append("script>\r\n                    <div class=\"softsummarypic\">\r\n                         ");	string softimg = info.Img==""?(tempurl+ "/images/pub/nopic.png"):info.Img;
	
	templateBuilder.Append("\r\n                        <a href=\"");
	templateBuilder.Append(softimg.ToString());
	templateBuilder.Append("\" target=\"_blank\"><img width=\"320\" src='");
	templateBuilder.Append(softimg.ToString());
	templateBuilder.Append("' onerror=\"this.src='");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/nopic.png'\"/></a>\r\n                    </div>\r\n				</div>\r\n				<div class=\"info-intro fyahei s16 bold\">软件介绍</div>\r\n                <div class=\"pcontent\" style=\"width:670px;padding-left:10px;\">\r\n                     ");
	templateBuilder.Append(info.Content.ToString().Trim());
	templateBuilder.Append("\r\n                </div>\r\n                <div class=\"info-intro fyahei s16 bold\">下载地址</div>\r\n				<div class=\"pcontent\" style=\"width:670px;padding-left:10px;\">\r\n                ");	 list = SoftList(info.Ext["ext_downlinks"].ToString().Trim());
	

	if (list.Rows.Count>0)
	{

	templateBuilder.Append("\r\n                    <ul class=\"softdownlist\">\r\n                        ");
	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	string durl = GetDownloadUrl(item["url"].ToString().Trim(),info.Id);
	
	templateBuilder.Append("\r\n                        <li><a href=\"");
	templateBuilder.Append(durl.ToString());
	templateBuilder.Append("\"  target=\"_blank\">" + item["name"].ToString().Trim() + "</a></li>\r\n                        ");
	}	//end loop

	templateBuilder.Append("\r\n                    </ul>\r\n                    ");
	}	//end if

	templateBuilder.Append("\r\n                </div>\r\n                <div class=\"info-intro fyahei s16 bold\">下载说明</div>\r\n				<div class=\"pcontent\" style=\"width:670px;padding-left:10px;\">\r\n?推荐使用第三方专业下载工具下载本站软件，使用 WinRAR v3.10 以上版本解压本站软件。<br />	\r\n?如果这个软件总是不能下载的请点击报告错误,谢谢合作!!<br />	\r\n?下载本站资源，如果服务器暂不能下载请过一段时间重试！<br />	\r\n?如果遇到什么问题，请到本站论坛去咨寻，我们将在那里提供更多 、更好的资源！<br />	\r\n?本站提供的一些商业软件是供学习研究之用，如用于商业用途，请购买正版。\r\n                </div>\r\n                ");
	templateBuilder.Append("                <div class=\"conup clearfix\">\r\n                    <div class=\"conupbox left condigg\">\r\n                    	<div class=\"left corange fyahei bold\" style=\"width:25px;font-size:24px;line-height:26px;\">赞</div>\r\n                        <div class=\"right tright cgrey s11\" style=\"width:100px;margin-right:10px;padding-top:11px;\"><span class=\"cdarkgrey diggcount s12\">");
	templateBuilder.Append(info.Diggcount.ToString().Trim());
	templateBuilder.Append("</span>人觉得很赞</div>\r\n                    </div>\r\n                    <div class=\"conupbox left constamp\" style=\"margin-left:47px;\">\r\n                    	<div class=\"left cblue fyahei bold\" style=\"width:25px;font-size:24px;line-height:26px;\">踩</div>\r\n                        <div class=\"right tright cgrey s11\" style=\"width:100px;margin-right:10px;padding-top:11px;\"><span class=\"cdarkgrey stampcount s12\">");
	templateBuilder.Append(info.Stampcount.ToString().Trim());
	templateBuilder.Append("</span>人觉得很烂</div>\r\n                    </div>\r\n                </div>\r\n                ");	 list = Contents.GetTagsByCid(id);
	

	if (list.Rows.Count>0)
	{

	templateBuilder.Append("\r\n                <div class=\"articletags\">\r\n                标签:\r\n                ");
	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	templateBuilder.Append("\r\n                    <a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/tags.aspx?id=" + item["id"].ToString().Trim() + "&name=" + item["name"].ToString().Trim() + "\" target=\"_blank\" title=\"" + item["name"].ToString().Trim() + "\">" + item["name"].ToString().Trim() + "</a>\r\n                ");
	}	//end loop

	templateBuilder.Append("\r\n                </div>\r\n                ");
	}	//end if

	templateBuilder.Append("\r\n				<div class=\"articlesum\">\r\n                	<div class=\"left cgrey\" style=\"width:400px;padding-left:10px;\">\r\n                        ");	ContentInfo pinfo = PrevCon(true, "prev");
	

	if (pinfo!=null)
	{

	string url = Urls.Content(pinfo.Id,pinfo.Typeid,pinfo.Savepath,pinfo.Filename);
	
	templateBuilder.Append("\r\n                            上一篇：<a href=\"");
	templateBuilder.Append(url.ToString());
	templateBuilder.Append("\" title=\"");
	templateBuilder.Append(pinfo.Title.ToString().Trim());
	templateBuilder.Append("\" class=\"cblue\">");	templateBuilder.Append(Utils.GetUnicodeSubString(pinfo.Title,46,".."));
	templateBuilder.Append("</a><br/>\r\n                        ");
	}
	else
	{

	templateBuilder.Append("\r\n                            上一篇：<a href=\"javascript:;\">没有了</a><br/>\r\n                        ");
	}	//end if

	 pinfo = PrevCon(true, "");
	

	if (pinfo!=null)
	{

	string url = Urls.Content(pinfo.Id,pinfo.Typeid,pinfo.Savepath,pinfo.Filename);
	
	templateBuilder.Append("\r\n                            下一篇：<a href=\"");
	templateBuilder.Append(url.ToString());
	templateBuilder.Append("\" title=\"");
	templateBuilder.Append(pinfo.Title.ToString().Trim());
	templateBuilder.Append("\" class=\"cblue\">");	templateBuilder.Append(Utils.GetUnicodeSubString(pinfo.Title,46,".."));
	templateBuilder.Append("</a>\r\n                        ");
	}
	else
	{

	templateBuilder.Append("\r\n                            下一篇：<a href=\"javascript:;\">没有了</a>\r\n                        ");
	}	//end if

	templateBuilder.Append("\r\n                    </div>\r\n                    <div class=\"right tright\" style=\"width:260px;padding:10px 10px 0 0;\">\r\n                        <div class=\"actbox\">\r\n                            <ul>\r\n                                <li id=\"act-fav\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/useraction.aspx?action=addfavorite&cid=");
	templateBuilder.Append(info.Id.ToString().Trim());
	templateBuilder.Append("\">收藏</a></li>\r\n                                <li id=\"act-err\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/plus/pickerr.aspx?cid=");
	templateBuilder.Append(id.ToString());
	templateBuilder.Append("&title=");templateBuilder.Append(Utils.UrlEncode(info.Title));
	templateBuilder.Append("\" target=\"_blank\">挑错</a></li>\r\n                                <li id=\"act-pus\"><a href=\"javascript:;\">分享</a></li>\r\n                                <li id=\"act-pnt\"><a href=\"javascript:;\" onclick=\"window.print();\">打印</a></li>\r\n                            </ul>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n                <!-- JiaThis Button BEGIN -->\r\n                <div class=\"jiathis_style\" style=\"display:none;\">\r\n	                <span class=\"jiathis_txt\">分享到</span>\r\n	                <a href=\"http://www.jiathis.com/share?uid=1665239\" class=\"jiathis jiathis_txt jiathis_separator jtico jtico_jiathis\" target=\"_blank\">更多</a>\r\n	                <a class=\"jiathis_counter_style\"></a>\r\n                </div>\r\n                <script type=\"text/javascript\">\r\n                    var jiathis_config = { data_track_clickback: 'true' };\r\n                </");
	templateBuilder.Append("script>\r\n                <script type=\"text/javascript\" src=\"http://v3.jiathis.com/code/jia.js?uid=1344725024936987\" charset=\"utf-8\"></");
	templateBuilder.Append("script>\r\n                <!-- JiaThis Button END -->\r\n\r\n                <script type=\"text/javascript\">\r\n                    getConDiggcount(");
	templateBuilder.Append(info.Id.ToString().Trim());
	templateBuilder.Append(", function(ret){ $(\".diggcount\").html(ret.split(\",\")[0]); $(\".stampcount\").html(ret.split(\",\")[1]); });\r\n                    $(\".condigg\").click(function(){ setConDiggcount(");
	templateBuilder.Append(info.Id.ToString().Trim());
	templateBuilder.Append(", \"digg\"); });\r\n                    $(\".constamp\").click(function(){ setConDiggcount(");
	templateBuilder.Append(info.Id.ToString().Trim());
	templateBuilder.Append(", \"stamp\"); });\r\n                    $(\"#act-pus\").click(function(){ $(\".jiathis\").trigger(\"click\"); });\r\n                </");
	templateBuilder.Append("script>");

	templateBuilder.Append(" \r\n            </div>\r\n             ");

	if (config.Opencomment==1&&info.Iscomment==1)
	{

	templateBuilder.Append("\r\n            <link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/artdialog/artdialog.css\" type=\"text/css\" />\r\n            <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/pagination.js\"></");
	templateBuilder.Append("script>\r\n            <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/blockUI.js\"></");
	templateBuilder.Append("script>\r\n            <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/artdialog/jquery.artDialog.min.js?skin=opera\"></");
	templateBuilder.Append("script>\r\n            <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/artdialog/artDialog.plugins.js\"></");
	templateBuilder.Append("script>\r\n            <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/comment.js\"></");
	templateBuilder.Append("script>\r\n            <div class=\"replaytpl\" style=\"display:none;\">\r\n                <div class=\"replay-#id\" style=\"padding-top:10px;\">\r\n                    <textarea class=\"comment msg#id\" style=\"width:620px;\"></textarea>\r\n                    <div class=\"comment-sub clearfix\">\r\n                            <input type=\"hidden\" value=\"#replayid\" class=\"replay#id\"/>\r\n                            <div class=\"left cmtcuruser\" style=\"padding:3px 15px 0 0;display:none;\"></div>\r\n                        <div class=\"left vcodebox\" style=\"padding-top:3px;\">验证码：</div>\r\n                        <div class=\"left vcodebox\"><input type=\"text\" class=\"ipt-comment-code vcode#id\" name=\"vcode\">&nbsp;#vcode</div>\r\n                        <div class=\"left\" style=\"margin-left:15px;\"><input type=\"button\" class=\"btn-comment btn-comment\" name=\"btnsub\" onclick=\"commentSubmit('#id');\" value=\"提交评论\"/></div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n            <div class=\"comtpl\" style=\"display:none;\">\r\n        	    <div class=\"comment comt-#id\">\r\n            	    <div class=\"clearfix\">\r\n                        <div class=\"comment-face left\">\r\n                            <a href=\"#uurl\">#face</a>\r\n                        </div>\r\n                        <div class=\"comment-cont left com-#tempid\">\r\n                    	    <div class=\"tit clearfix\">\r\n                        	    <span class=\"left cgrey\"><a href=\"#uurl\" class=\"cblue s13\">#username</a> [");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("#city网友]：</span>\r\n                                <span class=\"right cgrey tright\">#time 发表</span>\r\n                            </div>\r\n                            #quote\r\n                            <div class=\"comt\">#msg</div>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"comment-summary\">\r\n                	    <a href=\"javascript:;\" onclick=\"cmtreplay(cmtid,'#tempid')\" class=\"cblue\">回复</a><a href=\"javascript:;\" onclick=\"cmtdigg(this, cmtid, 'digg')\" class=\"cblue\">顶[#digg]</a><a href=\"javascript:;\" onclick=\"cmtdigg(this, cmtid, 'stamp')\" class=\"cblue\">踩[#stamp]</a>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n\r\n            <div class=\"bbar w_718 cmtlistbox\" style=\"margin-top:8px;\">\r\n                <div class=\"tit cblue\">最新评论</div>\r\n                <div class=\"more\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/comment.aspx?id=");
	templateBuilder.Append(id.ToString());
	templateBuilder.Append("\" target=\"_blank\" class=\"s12 cgrey\">目前有<span class=\"cmtcount cred\"></span>人发表了评论</a></div>\r\n            </div>\r\n            <div class=\"bcont w_718 cmtlistbox\">\r\n                <div class=\"comcon\">\r\n                    <div class=\"commentload cmtload\">正在加载评论,请稍等...</div>\r\n                    <div class=\"commentload cmtsofa\" style=\"background:0;display:none;\">目前还没有人发表评论,还不快抢沙发...</div>\r\n                    <div class=\"cmtlist\"></div>\r\n                    <div class=\"pagination cmtpage\"></div>\r\n                </div>\r\n            </div>\r\n\r\n            <div class=\"bbar w_718 cmtpublish\" style=\"margin-top:8px;\">\r\n                <div class=\"tit cblue\">发表评论</div>\r\n                <div class=\"more\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/comment.aspx?id=");
	templateBuilder.Append(id.ToString());
	templateBuilder.Append("\" target=\"_blank\" class=\"s12 cgrey\">目前有<span class=\"cmtcount cred\"></span>人发表了评论</a></div>\r\n            </div>\r\n            <div class=\"bcont w_718 cmtpublish\" style=\"padding:15px 0;\">\r\n                <textarea class=\"comment msg\" style=\"width:650px;\"></textarea>\r\n                <div class=\"comment-sub clearfix\" style=\"padding-left:33px;\">\r\n                <input type=\"hidden\" value=\"0\" class=\"replay\"/>\r\n               <div class=\"left cmtcuruser\" style=\"padding:3px 15px 0 0;display:none;\"></div>\r\n                <div class=\"left  vcodebox\" style=\"padding-top:3px;\">验证码：</div>\r\n                <div class=\"left  vcodebox\"><input type=\"text\" class=\"ipt-comment-code vcode\" name=\"vcode\" id=\"vcode\">&nbsp;<img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/vcode/validatecode.aspx?cookiename=stausercommentsubmit&live=0\" onclick=\"this.src='");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/vcode/validatecode.aspx?cookiename=stausercommentsubmit&live=0&date='+ new Date();\" id=\"vimg\" height=\"23\" width=\"70\" style=\"cursor:pointer;\" class=\"img-vcode\" title=\"刷新验证码\"/></div>\r\n                <div class=\"left\" style=\"margin-left:15px;\"><input type=\"button\" class=\"btn-comment btn-comment\" name=\"btnsub\" id=\"btn-sub\" onclick=\"commentSubmit('');\" value=\"提交评论\"/></div>\r\n                 </div>\r\n            </div>\r\n            <script type=\"text/javascript\">\r\n            function commentSubmit(id){ \r\n                subcomment(id, ");
	templateBuilder.Append(id.ToString());
	templateBuilder.Append(", \"");
	templateBuilder.Append(info.Title.ToString().Trim());
	templateBuilder.Append("\", \"");
	templateBuilder.Append(info.Title.ToString().Trim());
	templateBuilder.Append("\", function(){ $(\".cmtcount\").html(cmtrcount); });\r\n            };\r\n            $(function(){\r\n                $(\".cmtpublish\").click(cmtmustlogin);\r\n                cmtperpage = 3, cmtopen = ");
	templateBuilder.Append(info.Iscomment.ToString().Trim());
	templateBuilder.Append(";\r\n                if(webconfig.opencomment == 0 || cmtopen == 0){\r\n                    $(\".cmtpublish,.cmtlistbox,.commentcount\").remove();\r\n                    return;\r\n                }\r\n                if(webconfig.vcodemods.indexOf(\"3\") < 0) $(\".vcodebox\").remove();\r\n                if($.inArray(stauser.userid,[\"\",\"0\",\"-1\"]) < 0) $(\".cmtcuruser\").css(\"display\",\"\").html(\"你好,\" + stauser.nickname);\r\n                cmtdataload(");
	templateBuilder.Append(id.ToString());
	templateBuilder.Append(", 1, function(){ $(\".cmtcount\").html(cmtrcount); });\r\n            });\r\n            </");
	templateBuilder.Append("script>\r\n");
	}	//end if



	templateBuilder.Append(" \r\n			<script type=\"text/javascript\">\r\n            setContentClick(");
	templateBuilder.Append(info.Id.ToString().Trim());
	templateBuilder.Append(",\"yes\",function(c){ $(\".conclick\").html(c); });\r\n            </");
	templateBuilder.Append("script>\r\n        </div>\r\n        <div class=\"sright\">\r\n        ");	int chnid = info.Channelid;
	
	string chnname = info.Channelname;
	

	templateBuilder.Append("            <div class=\"bbar w_235\">\r\n                <div class=\"tit\"><a href=\"");
	templateBuilder.Append(Urls.Channel(chnid).ToString().Trim());
	templateBuilder.Append("\" class=\"cblue bold\">热门软件</a></div>\r\n            </div>\r\n            <div class=\"bcont w_235\" style=\"margin:0 0 6px 0;padding-bottom:6px;\">\r\n                 <div class=\"phtlist2\">\r\n                    ");
	list = GetTable("content", "type=channel id=" + chnid.ToString() + " ctype=3 ext=soft num=" +  "7" + " fields=id,savepath,filename,color,property,title,img,ext_downcount,click order=" +  "3" + "");


	item__loop__id=0;
	foreach(DataRow item in list.Rows)
	{
		item__loop__id++;

	int cid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	int tid = TypeParse.StrToInt(item["typeid"].ToString().Trim());
	
	string curl = Urls.Content(cid,tid,item["savepath"].ToString().Trim(),item["filename"].ToString().Trim());
	
	string img = item["img"].ToString().Trim()==""?(tempurl+ "/images/random/tb" + (new Random(Guid.NewGuid().GetHashCode())).Next(1,20)  + ".jpg"):item["img"].ToString().Trim();
	
	string title = TitleFormat(item["title"].ToString().Trim(),item["property"].ToString().Trim(),(item["color"].ToString().Trim() == "000000"? defcolor: item["color"].ToString().Trim()),30,"..");
	
	templateBuilder.Append("	\r\n                    <div class=\"phtitem2 clearfix\">\r\n                        <div class=\"left\"><a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" title=\"" + item["title"].ToString().Trim() + "\"><img alt=\"" + item["title"].ToString().Trim() + "\" src=\"");
	templateBuilder.Append(img.ToString());
	templateBuilder.Append("\" onerror=\"this.src='");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/pub/nopic.png'\" width=\"58\" height=\"50\"/></a></div>\r\n                        <div class=\"right\">\r\n                            <a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" title=\"" + item["title"].ToString().Trim() + "\" class=\"cblue s13\">");	templateBuilder.Append(Utils.GetUnicodeSubString(item["title"].ToString().Trim(),20,".."));
	templateBuilder.Append("</a><br />\r\n                            <span class=\"cgrey\">下载:<label class=\"cdarkgrey\">" + item["ext_downcount"].ToString().Trim() + "</label></span>&nbsp;<span class=\"cgrey\">浏览:<label class=\"cdarkgrey\">" + item["Click"].ToString().Trim() + "</label></span>\r\n                        </div>\r\n                    </div>\r\n                    ");
	}	//end loop

	templateBuilder.Append("  \r\n                  </div>\r\n            </div>\r\n            <div class=\"bbar w_235\">\r\n                <div class=\"tit\"><a href=\"");
	templateBuilder.Append(Urls.Channel(chnid).ToString().Trim());
	templateBuilder.Append("\" class=\"cblue bold\">软件推荐</a></div>\r\n            </div>\r\n            <div class=\"bcont w_235\" style=\"margin:0 0 6px 0;padding-bottom:6px;\">\r\n                <ul class=\"l235\">\r\n                    ");
	list = GetTable("content", "type=channel id=" + chnid.ToString() + " num=" +  "9" + " fields=id,typeid,savepath,filename,color,property,title order=" +  "2" + " property=r");


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
	templateBuilder.Append("\" tilte=\"" + item["title"].ToString().Trim() + "\">");
	templateBuilder.Append(title.ToString());
	templateBuilder.Append("</a></li>\r\n                    ");
	}	//end loop

	templateBuilder.Append("         \r\n                </ul>\r\n            </div>");

	templateBuilder.Append("    \r\n        </div>\r\n    </div>\r\n	");
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
