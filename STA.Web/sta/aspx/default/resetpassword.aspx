<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.ResetPassword" %>
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
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>重置密码 - ");
	templateBuilder.Append(webname.ToString());
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
	templateBuilder.Append("script>\r\n</head>\r\n<body class=\"bg_grey\">\r\n<div class=\"wrapper\">\r\n    <div class=\"topheader_wrap\">\r\n        <div class=\"topheader\">\r\n            <div class=\"left\">\r\n                <div class=\"fav\"><a href=\"javascript:;\" onclick=\"AddFavourite('");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("','");
	templateBuilder.Append(weburl.ToString());
	templateBuilder.Append("/')\" class=\"cgrey\">收藏");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("</a></div>\r\n            </div>\r\n            <div class=\"right\">\r\n                <a href=\"#\" class=\"cgrey\">帮助</a> <a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/login.aspx\" class=\"cgrey\">登录</a> <a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/register.aspx\" class=\"cgrey\">注册</a> <a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" class=\"cgrey\">网站首页</a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"header\">\r\n        <div class=\"logo\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/logo.png\" /></a></div>\r\n    </div>\r\n	<div class=\"icont\">\r\n    	<div class=\"con_bar_1 con_bar_2\">\r\n        	<div class=\"tit left s14\">建议输入数字、密码、字符组合的密码,并请牢记您的密码！</div>\r\n        </div>\r\n        <div class=\"con_body\">\r\n        	<div class=\"lgn_box clearfix\">\r\n                <div class=\"lgn_left\">\r\n                    <div class=\"fwrongtip2 cred\" style=\"display:none;\">\r\n                    </div>\r\n                    <form action=\"\" method=\"post\">\r\n                    <div class=\"row clearfix\">\r\n                        <div class=\"field left s14\">新&nbsp密&nbsp码：</div>\r\n                        <div class=\"input left\">\r\n                            <input type=\"password\" class=\"greyipt\" id=\"password\" name=\"password\" value=\"" + STARequest.GetString("password") + "\"/>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"row clearfix\" style=\"padding-bottom:0;\">\r\n                        <div class=\"field left s14\">确认密码：</div>\r\n                        <div class=\"input left\">\r\n                            <input type=\"password\" class=\"greyipt\" id=\"repassword\" name=\"repassword\" value=\"" + STARequest.GetString("repassword") + "\"/>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"row clearfix\" style=\"padding-bottom:10px;padding-top:27px;\">\r\n                        <div class=\"field left s14\">验&nbsp证&nbsp码：</div>\r\n                        <div class=\"input left\" style=\"width:90px;\">\r\n                            <input type=\"text\" class=\"greyipt\" style=\"width:70px;\" id=\"vcode\" name=\"vcode\"/>\r\n                        </div>\r\n                        <div class=\"vcode left\">\r\n                            <img src=\"sta/vcode/vcode.aspx?cookiename=resetpassword&num=5&live=0\" id=\"vimg\" height=\"26\" style=\"cursor:pointer;\" />\r\n                        </div>\r\n                        <div class=\"changevcode left\">\r\n                        看不清,<a href=\"javascript:;\" id=\"chgcode\" class=\"cblue2 underline\">换一张</a>\r\n                        </div>\r\n                    </div>\r\n                    <script type=\"text/javascript\">\r\n                        $(\"#vimg,#chgcode\").click(function () { $(\"#vimg\").attr(\"src\", \"../sta/vcode/vcode.aspx?cookiename=resetpassword&num=5&live=0&date=\" + new Date()); });\r\n                    </");
	templateBuilder.Append("script>\r\n                    <div class=\"row2\">\r\n						<input type=\"button\" id=\"btnlogin\" name=\"btnlogin\" class=\"btnlogin fogpwd\" value=\"\" onclick=\"SubForm()\"/>\r\n                    </div>\r\n                    </form>\r\n                </div>\r\n                <div class=\"lgn_right\">\r\n                	<div><strong>还不本站用户？</strong><br />\r\n                    本站的账号都没有？你也太落伍了赶紧去注册一个吧。</div>\r\n                    <div class=\"reg\">\r\n                    	<input type=\"button\" value=\"\" class=\"reg\" onclick=\"location.href='register.aspx';\" />\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"footer\">\r\n    	<div class=\"left\">Copyright © 2009-2012 STAECMS. 中视网维 版权所有</div>\r\n        <div class=\"right\" id=\"time\"></div>\r\n    </div>\r\n</div>\r\n<script language=\"javascript\" type=\"text/javascript\">\r\n    window.onload = function () {\r\n        setInterval(\"document.getElementById('time').innerHTML=new Date().toLocaleString()+' 星期'+'日一二三四五六'.charAt(new Date().getDay());\", 1000);\r\n    };\r\n    function Errtip(msg){\r\n        $(\".fwrongtip2\").html(msg).css(\"display\",\"block\");\r\n        setTimeout(\"$('.fwrongtip2').css('display','none')\",3000);\r\n    };\r\n    function SubForm() { \r\n        var pwd = $(\"#password\").val(), pwd2 = $(\"#repassword\").val();\r\n        if(pwd == '' || pwd2== ''){\r\n            Errtip(\"请输入新密码！\");\r\n        }else if(pwd != pwd2){\r\n            Errtip(\"两次输入的密码不一致！\");\r\n        }else if($(\"#vcode\").val() == ''){\r\n            Errtip(\"请填写验证码！\");\r\n        }else{\r\n            $(\"form\").get(0).submit();\r\n        }\r\n    };\r\n    ");
	if (errors>0)
	{

	templateBuilder.Append("\r\n    Errtip(\"");
	templateBuilder.Append(msgtext.ToString());
	templateBuilder.Append("\");\r\n    ");
	}	//end if

	templateBuilder.Append("\r\n</");
	templateBuilder.Append("script>\r\n</body>\r\n</html>");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
