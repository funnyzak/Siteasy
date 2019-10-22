<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.Login" %>
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
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>用户登录 - ");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("</title>\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(seodescription.ToString());
	templateBuilder.Append("\"/>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(seokeywords.ToString());
	templateBuilder.Append("\"/>\r\n<meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\" />\r\n<link rel=\"shortcut icon\" href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/favicon.ico\" type=\"image/x-icon\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/css/stacms.css\" type=\"text/css\" />");
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
	templateBuilder.Append("script>");
	templateBuilder.Append(script.ToString());
	templateBuilder.Append("\r\n</head>\r\n<body class=\"bg_grey\">\r\n<div class=\"wrapper\">\r\n    <div class=\"topheader_wrap\">\r\n        <div class=\"topheader\">\r\n            <div class=\"left\">\r\n                <div class=\"fav\"><a href=\"javascript:;\" onclick=\"addFavourite('");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("','");
	templateBuilder.Append(weburl.ToString());
	templateBuilder.Append("/')\" class=\"cgrey\">收藏");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("</a></div>\r\n            </div>\r\n            <div class=\"right\">\r\n                <a href=\"#\" class=\"cgrey\">帮助</a>&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/login.aspx\" class=\"cgrey\">登录</a>&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/register.aspx\" class=\"cgrey\">注册</a>&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" class=\"cgrey\">网站首页</a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"header\">\r\n        <div class=\"logo\"><a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/logo.png\" /></a></div>\r\n    </div>\r\n	<div class=\"icont\">\r\n    	<div class=\"con_bar_1 con_bar_2\">\r\n        	<div class=\"tit left s14\">欢迎登录");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("网站！</div>\r\n        </div>\r\n        <div class=\"con_body\">\r\n        	<div class=\"lgn_box clearfix\">\r\n                <div class=\"lgn_left\">\r\n                    <div class=\"fwrongtip2 cred\" style=\"display:none;\">\r\n                    </div>\r\n                    <form action=\"\" method=\"post\">\r\n                    <div class=\"row clearfix\">\r\n                        <div class=\"field left s14\">用户名：</div>\r\n                        <div class=\"input left\">\r\n                            <input type=\"text\" class=\"greyipt\" id=\"username\" name=\"username\" value=\"" + STARequest.GetString("username") + "\"/>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"row clearfix\" style=\"padding-bottom:0;\">\r\n                        <div class=\"field left s14\">密&nbsp;&nbsp;码：</div>\r\n                        <div class=\"input left\">\r\n                            <input type=\"password\" class=\"greyipt\" id=\"password\" name=\"password\" value=\"" + STARequest.GetString("password") + "\"/>\r\n                        </div>\r\n                        <div class=\"left\" style=\"padding:4px 0 0 15px;\">\r\n                        	<a href=\"forgetpassword.aspx\" class=\"s13 cblue2\">忘记密码啦?</a>\r\n                        </div>\r\n                    </div>\r\n                    ");
	if (config.Vcodemods.IndexOf("2")>=0)
	{

	templateBuilder.Append("\r\n                    <div class=\"row clearfix\" style=\"padding-bottom:10px;padding-top:27px;\">\r\n                        <div class=\"field left s14\">验证码：</div>\r\n                        <div class=\"input left\" style=\"width:90px;\">\r\n                            <input type=\"text\" class=\"greyipt\" style=\"width:70px;\" id=\"vcode\" name=\"vcode\"/>\r\n                        </div>\r\n                        <div class=\"vcode left\">\r\n                            <img src=\"sta/vcode/vcode.aspx?cookiename=userlogin&num=5&live=0\" id=\"vimg\" height=\"26\" style=\"cursor:pointer;\" />\r\n                        </div>\r\n                        <div class=\"changevcode left\">\r\n                        看不清,<a href=\"javascript:;\" id=\"chgcode\" class=\"cblue2 underline\">换一张</a>\r\n                        </div>\r\n                    </div>\r\n                    <script type=\"text/javascript\">\r\n                        $(\"#vimg,#chgcode\").click(function () { $(\"#vimg\").attr(\"src\", \"../sta/vcode/vcode.aspx?cookiename=userlogin&num=5&live=0&date=\" + new Date()); });\r\n                    </");
	templateBuilder.Append("script>\r\n                    ");
	}	//end if

	templateBuilder.Append("\r\n                    <div class=\"row2\">\r\n                        <input type=\"checkbox\" checked=\"checked\" name=\"expires\" id=\"expires\" value=\"999999\"/> <label for=\"expires\" title=\"选择后用户将自动登录\">记住我</label>\r\n                    </div>\r\n                    <div class=\"row2\">\r\n						<input type=\"button\" id=\"btnlogin\" name=\"btnlogin\" class=\"btnlogin\" value=\"\" onclick=\"SubLoginForm()\"/>\r\n                    </div>\r\n                    </form>\r\n                </div>\r\n                <div class=\"lgn_right\">\r\n                	<div><strong>还不本站用户？</strong><br />\r\n                    本站的账号都没有？你也太落伍了赶紧去注册一个吧。</div>\r\n                    <div class=\"reg\">\r\n                    	<input type=\"button\" value=\"\" class=\"reg\" onclick=\"location.href='register.aspx';\" />\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"footer\">\r\n    	<div class=\"left\"> Copyright &copy; 2007-2011 <a href=\"http://www.stacms.com\" title=\"站易内容管理系统\" target=\"_blank\" class=\"cdarkgrey\">STACMS</a> 中视网维 版权所有</div>\r\n        <div class=\"right\" id=\"time\"></div>\r\n    </div>\r\n</div>\r\n<script language=\"javascript\" type=\"text/javascript\">\r\n    window.onload = function () {\r\n        setInterval(\"document.getElementById('time').innerHTML=new Date().toLocaleString()+' 星期'+'日一二三四五六'.charAt(new Date().getDay());\", 1000);\r\n    };\r\n\r\n    function Errtip(msg){\r\n        $(\".fwrongtip2\").html(msg).css(\"display\",\"block\");\r\n        setTimeout(\"$('.fwrongtip2').css('display','none')\",3000);\r\n    };\r\n\r\n    function SubLoginForm() { \r\n        var username = $(\"#username\").val(), pwd = $(\"#password\").val();\r\n        if(username == '' || pwd== ''){\r\n            Errtip(\"用户名或密码不能为空！\");\r\n        }else if($(\"#vcode\") && $(\"#vcode\").val() == ''){\r\n            Errtip(\"请填写验证码！\");\r\n        }else{\r\n            $(\"form\").get(0).submit();\r\n        }\r\n    };\r\n\r\n	$(document).keypress(function (event) {\r\n        if (event.which == '13') { SubLoginForm(); }\r\n    });\r\n\r\n    ");
	if (IsErr()&&ispost)
	{

	templateBuilder.Append("\r\n    Errtip(\"");
	templateBuilder.Append(msgtext.ToString());
	templateBuilder.Append("\");\r\n    ");
	}	//end if

	templateBuilder.Append("\r\n</");
	templateBuilder.Append("script>\r\n");
	if (ispost&&!IsErr())
	{


	if (config.Thirducenter==1)
	{

	string ucenterlogin = STA.Core.Api.UCenter.UserSynlogin(STA.Core.Api.UCenter.UserLogin(username, password));
	
	templateBuilder.Append("\r\n    ");
	templateBuilder.Append(ucenterlogin.ToString());
	templateBuilder.Append("\r\n    ");
	}	//end if


	}	//end if


	if (userid>0||(ispost&&!IsErr()))
	{

	templateBuilder.Append("\r\n<script type=\"text/javascript\">\r\n    ");	 returnurl = returnurl!=""?returnurl :"user/";
	
	templateBuilder.Append("\r\n    setTimeout(function () { location.href = \"");
	templateBuilder.Append(returnurl.ToString());
	templateBuilder.Append("\"; }, 200);\r\n</");
	templateBuilder.Append("script>\r\n");
	}	//end if

	templateBuilder.Append("\r\n</body>\r\n</html>");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
