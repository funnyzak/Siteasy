<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.Comment" %>
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
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>网友评论：");
	templateBuilder.Append(cinfo.Title.ToString().Trim());
	templateBuilder.Append("</title>\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(cinfo.Title.ToString().Trim());
	templateBuilder.Append("网友评论\"/>\r\n<meta name=\"keywords\" content=\"网友评论,");
	templateBuilder.Append(seokeywords.ToString());
	templateBuilder.Append("\"/>\r\n<meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\" />\r\n<link rel=\"shortcut icon\" href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/favicon.ico\" type=\"image/x-icon\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/artdialog/artdialog.css\" type=\"text/css\" />\r\n<link rel=\"stylesheet\" href=\"");
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
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/pagination.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/blockUI.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/artdialog/jquery.artDialog.min.js?skin=opera\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/artdialog/artDialog.plugins.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/comment.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/js/global.js\"></");
	templateBuilder.Append("script>\r\n</head>\r\n<body>          \r\n<div class=\"vt_wrapper\">\r\n    ");
	templateBuilder.Append("    <div class=\"header\">\r\n    	<div class=\"left\">\r\n            <a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\"  title=\"");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\" target=\"_self\"><img title=\"");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\" src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/logo.png\" /></a>\r\n        </div>\r\n        <div class=\"right cgrey\">\r\n            ");
	if (userid>0)
	{

	templateBuilder.Append("\r\n                您好,<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/\">");
	templateBuilder.Append(oluser.Nickname.ToString().Trim());
	templateBuilder.Append("</a>&nbsp;&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/\">个人中心</a>&nbsp;|&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/profile.aspx\">我的资料</a>&nbsp;|&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/useraction.aspx?action=loginout\">退出登录</a>\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n                欢迎来到");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append(", 您可以&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/login.aspx?returnurl=");templateBuilder.Append(Utils.UrlEncode(cururl));
	templateBuilder.Append("\">登录</a>&nbsp;或&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/register.aspx\">注册</a>\r\n            ");
	}	//end if

	templateBuilder.Append("\r\n        </div>\r\n    </div>");

	templateBuilder.Append("\r\n    <div class=\"votetit\">\r\n        ");	string curl = Urls.Content(cinfo.Id,cinfo.Typeid,cinfo.Savepath,cinfo.Filename);
	
	templateBuilder.Append("\r\n    	网友评论:<a href=\"");
	templateBuilder.Append(curl.ToString());
	templateBuilder.Append("\" target=\"_blank\" class=\"s14 cblue\">");
	templateBuilder.Append(cinfo.Title.ToString().Trim());
	templateBuilder.Append("</a>\r\n    </div>\r\n    <div class=\"comconbox\">\r\n		<div class=\"combar\">\r\n        	<span class=\"s13 cgrey bold\">评论列表（网友评论仅供网友表达个人看法，并不表明本站同意其观点或证实其描述）</span>\r\n            <div class=\"combartab\">\r\n            	<ul class=\"displyodr\">\r\n                	<li class=\"on\" odr=\"id\">全部评论</li>\r\n                    <li odr=\"diggcount\">顶的最多</li>\r\n                    <li odr=\"stampcount\">踩的最多</li>\r\n                </ul>\r\n            </div>\r\n        </div>\r\n        <div class=\"comcon\">\r\n            <div class=\"commentload cmtload\">正在加载评论,请稍等...</div>\r\n            <div class=\"commentload cmtsofa\" style=\"background:0;display:none;\">目前还没有人发表评论,还不快抢沙发...</div>\r\n            <div class=\"cmtlist\"></div>\r\n            <div class=\"pagination cmtpage\"></div>\r\n        </div>\r\n        <div class=\"replaytpl\" style=\"display:none;\">\r\n            <div class=\"replay-#id\" style=\"padding-top:10px;\">\r\n        	    <textarea class=\"comment msg#id\" style=\"width:850px;\"></textarea>\r\n                <div class=\"comment-sub clearfix\">\r\n                        <input type=\"hidden\" value=\"#replayid\" class=\"replay#id\"/>\r\n                    ");
	if (userid>0)
	{

	templateBuilder.Append("\r\n                    <div class=\"left\" style=\"padding:3px 15px 0 0;\">你好,");
	templateBuilder.Append(oluser.Nickname.ToString().Trim());
	templateBuilder.Append("</div>\r\n                    ");
	}	//end if


	if (config.Vcodemods.IndexOf("3")>=0)
	{

	templateBuilder.Append("\r\n                    <div class=\"left\" style=\"padding-top:3px;\">验证码：</div>\r\n                    <div class=\"left\"><input type=\"text\" class=\"ipt-comment-code vcode#id\" name=\"vcode\">&nbsp;#vcode</div>\r\n                    ");
	}	//end if

	templateBuilder.Append("\r\n                    <div class=\"left\" style=\"margin-left:15px;\"><input type=\"button\" class=\"btn-comment btn-comment\" name=\"btnsub\" onclick=\"commentSubmit('#id');\" value=\"提交评论\"/></div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div class=\"comtpl\" style=\"display:none;\">\r\n        	<div class=\"comment comt-#id\">\r\n            	<div class=\"clearfix\">\r\n                    <div class=\"comment-face left\">\r\n                        <a href=\"#uurl\">#face</a>\r\n                    </div>\r\n                    <div class=\"comment-cont left com-#tempid\">\r\n                    	<div class=\"tit clearfix\">\r\n                        	<span class=\"left cgrey\"><a href=\"#uurl\" class=\"cblue s13\">#username</a> [");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("#city网友]：</span>\r\n                            <span class=\"right cgrey tright\">#time 发表</span>\r\n                        </div>\r\n                        #quote\r\n                        <div class=\"comt\">#msg</div>\r\n                    </div>\r\n                </div>\r\n                <div class=\"comment-summary\">\r\n                	<a href=\"javascript:;\" onclick=\"cmtreplay(cmtid,'#tempid')\" class=\"cblue\">回复</a><a href=\"javascript:;\" onclick=\"cmtdigg(this, cmtid, 'digg')\" class=\"cblue\">顶[#digg]</a><a href=\"javascript:;\" onclick=\"cmtdigg(this, cmtid, 'stamp')\" class=\"cblue\">踩[#stamp]</a>\r\n                </div>\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n    <div class=\"comconbox\" style=\"margin:10px auto 30px;\">\r\n		<div class=\"combar\">\r\n        	<span class=\"s13 cgrey bold\">发表评论(请自觉遵守互联网相关的政策法规，严禁发布色情、暴力、反动的言论)</span>\r\n        </div>\r\n        <div class=\"comcon cmtpublish\">\r\n        	<textarea class=\"comment msg\">" + STARequest.GetString("msg") + "</textarea>\r\n            <div class=\"comment-sub clearfix\">\r\n                    <input type=\"hidden\" value=\"0\" class=\"replay\"/>\r\n                ");
	if (userid>0)
	{

	templateBuilder.Append("\r\n                    <div class=\"left\" style=\"padding:3px 15px 0 0;\">你好,");
	templateBuilder.Append(oluser.Nickname.ToString().Trim());
	templateBuilder.Append("</div>\r\n                ");
	}	//end if


	if (config.Vcodemods.IndexOf("3")>=0)
	{

	templateBuilder.Append("\r\n                <div class=\"left\" style=\"padding-top:3px;\">验证码：</div>\r\n                <div class=\"left\"><input type=\"text\" class=\"ipt-comment-code vcode\" name=\"vcode\" id=\"vcode\">&nbsp;<img src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/vcode/validatecode.aspx?cookiename=stausercommentsubmit&live=0\" onclick=\"this.src='");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/vcode/validatecode.aspx?cookiename=stausercommentsubmit&live=0&date='+ new Date();\" id=\"vimg\" height=\"23\" width=\"70\" style=\"cursor:pointer;\" class=\"img-vcode\" title=\"刷新验证码\"/></div>\r\n                ");
	}	//end if

	templateBuilder.Append("\r\n                <div class=\"left\" style=\"margin-left:15px;\"><input type=\"button\" class=\"btn-comment btn-comment\" name=\"btnsub\" id=\"btn-sub\" onclick=\"commentSubmit('');\" value=\"提交评论\"/></div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    ");
	templateBuilder.Append("    <div class=\"vt_footer\">\r\n        Copyright &copy; 2007-2011 <a href=\"http://www.stacms.com\" title=\"站易内容管理系统\" target=\"_blank\" class=\"cdarkgrey\">STACMS</a> 中视网维 版权所有");
	templateBuilder.Append(config.Extcode.ToString().Trim());
	templateBuilder.Append("\r\n    </div>");

	templateBuilder.Append("\r\n</div>\r\n<script type=\"text/javascript\">\r\n    $(\"ul.displyodr li\").each(function (idx, obj) {\r\n        $(obj).click(function () {\r\n            if (!$(this).hasClass(\"on\")) {\r\n                $(\"ul.displyodr li\").removeClass(\"on\");\r\n                $(this).addClass(\"on\");\r\n                cmtorder = $(this).attr(\"odr\");\r\n                cmtdataload(");
	templateBuilder.Append(id.ToString());
	templateBuilder.Append(", 1);\r\n            }\r\n        });\r\n    });\r\n    function commentSubmit(id){ \r\n        subcomment(id, ");
	templateBuilder.Append(id.ToString());
	templateBuilder.Append(", \"");
	templateBuilder.Append(cinfo.title.ToString().Trim());
	templateBuilder.Append("\", \"");
	templateBuilder.Append(cinfo.title.ToString().Trim());
	templateBuilder.Append("\");\r\n    };\r\n    $(\".cmtpublish\").click(cmtmustlogin);\r\n    cmtdataload(");
	templateBuilder.Append(id.ToString());
	templateBuilder.Append(", parseInt(hashString(\"page\",\"1\")));\r\n</");
	templateBuilder.Append("script>\r\n</body>\r\n</html>\r\n");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
