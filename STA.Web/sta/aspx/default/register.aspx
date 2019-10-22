<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.Register" %>
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
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>用户注册 - ");
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
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/register.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\">\r\n    var config = {\r\n        regforbidwords: '");
	templateBuilder.Append(config.Forbiduserwords.ToString().Trim());
	templateBuilder.Append("',\r\n        emailmult: ");
	templateBuilder.Append(config.Emailmultuser.ToString().Trim());
	templateBuilder.Append("\r\n    }\r\n    webconfig = $.extend(webconfig, config);\r\n</");
	templateBuilder.Append("script>\r\n</head>\r\n<body class=\"bg_grey\">\r\n<div class=\"wrapper\">\r\n    <div class=\"topheader_wrap\">\r\n        <div class=\"topheader\">\r\n            <div class=\"left\">\r\n                <div class=\"fav\"><a href=\"javascript:;\" onclick=\"addFavourite('");
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
	templateBuilder.Append("/images/logo.png\" /></a></div>\r\n    </div>\r\n	<div class=\"icont\">\r\n    	<div class=\"con_bar_1\">\r\n        	<div class=\"tit left s14\">欢迎注册");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("网站会员，注册后，可享受更多功能！</div>\r\n        </div>\r\n        <div class=\"con_body\">\r\n        	<div class=\"regtip s14 cgrey\">\r\n            	提示：如果已有\"");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\"帐号，您可以<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/login.aspx\" class=\"cblue2 s14 underline\">直接登录</a>\r\n            </div>\r\n            ");
	if (IsErr()&&ispost)
	{

	templateBuilder.Append("\r\n            <div class=\"fwrongtip cred\">\r\n            ");
	templateBuilder.Append(msgtext.ToString());
	templateBuilder.Append("\r\n            </div>\r\n            <script type=\"text/javascript\">\r\n                setTimeout(function () { $(\".fwrongtip\").fadeOut(\"slow\"); }, 3000);\r\n            </");
	templateBuilder.Append("script>\r\n            ");
	}	//end if

	templateBuilder.Append("\r\n            <form action=\"\" method=\"post\">\r\n                <div class=\"regform\">\r\n                    <div class=\"row clearfix\">\r\n                        <div class=\"field left s13\">用户名：</div>\r\n                        <div class=\"input left\">\r\n                            <input type=\"text\" class=\"txt\" id=\"username\" name=\"username\" onfocus=\"TxtFocus('username')\" onblur=\"CheckTxt('username')\" value=\"");
	templateBuilder.Append(info.Username.ToString().Trim());
	templateBuilder.Append("\"/>\r\n                        </div>\r\n                        <div class=\"tip left s13 tipusername\">6-20位小写字母、数字组成。</div>\r\n                    </div>\r\n                    <div class=\"row clearfix\">\r\n                        <div class=\"field left s13\">昵称：</div>\r\n                        <div class=\"input left\">\r\n                            <input type=\"text\" class=\"txt\" id=\"nickname\" name=\"nickname\" onfocus=\"TxtFocus('nickname')\" onblur=\"CheckTxt('nickname')\" value=\"");
	templateBuilder.Append(info.Nickname.ToString().Trim());
	templateBuilder.Append("\"/>\r\n                        </div>\r\n                        <div class=\"tip left s13 tipnickname\"></div>\r\n                    </div> \r\n                    <div class=\"row clearfix\">\r\n                        <div class=\"field left s13\">密码：</div>\r\n                        <div class=\"input left\">\r\n                            <input type=\"password\" class=\"txt\"  id=\"password\" name=\"password\" onfocus=\"TxtFocus('password')\" onblur=\"CheckTxt('password')\" value=\"" + STARequest.GetString("password") + "\"/>\r\n                        </div>\r\n                        <div class=\"tip left s13 tippassword\">长度必须大于6位，数字、字母混合。</div>\r\n                    </div>  \r\n                    <div class=\"row clearfix\">\r\n                        <div class=\"field left s13\">确认密码：</div>\r\n                        <div class=\"input left\">\r\n                            <input type=\"password\" class=\"txt\"  id=\"repassword\" name=\"repassword\" onfocus=\"TxtFocus('repassword')\" onblur=\"CheckTxt('repassword')\" value=\"" + STARequest.GetString("password") + "\"/>\r\n                        </div>\r\n                        <div class=\"tip left s13 tiprepassword\"></div>\r\n                    </div> \r\n                    <div class=\"row clearfix\">\r\n                        <div class=\"field left s13\">常用邮箱：</div>\r\n                        <div class=\"input left\">\r\n                            <input type=\"text\" class=\"txt\" name=\"email\" id=\"email\" onfocus=\"TxtFocus('email')\" onblur=\"CheckTxt('email')\" value=\"");
	templateBuilder.Append(info.Email.ToString().Trim());
	templateBuilder.Append("\"/>\r\n                        </div>\r\n                        <div class=\"tip left s13 tipemail\">输入常用邮箱，以便于账号验证或密码找回</div>\r\n                    </div> \r\n                    ");
	if (config.Vcodemods.IndexOf("1")>=0)
	{

	templateBuilder.Append("\r\n                    <div class=\"row clearfix\">\r\n                        <div class=\"field left s13\">验证码：</div>\r\n                        <div class=\"input left\" style=\"width:110px;\">\r\n                            <input type=\"text\" class=\"txt\" style=\"width:90px;\" id=\"vcode\" name=\"vcode\" onfocus=\"TxtFocus('vcode')\" onblur=\"CheckTxt('vcode')\"/>\r\n                        </div>\r\n                        <div class=\"vcode left\">\r\n                            <img src=\"sta/vcode/vcode.aspx?cookiename=register&num=5&live=0\" id=\"vimg\" height=\"26\" style=\"cursor:pointer;\" />\r\n                        </div>\r\n                        <div class=\"changevcode left\">\r\n                        看不清,<a href=\"javascript:;\" id=\"chgcode\" class=\"cblue2 underline\">换一张</a>\r\n                        </div>\r\n                        <div class=\"tip left s13 tipvcode\" style=\"width:200px;\"></div>\r\n                    </div> \r\n                    <script type=\"text/javascript\">\r\n                        $(\"#vimg,#chgcode\").click(function () { $(\"#vimg\").attr(\"src\", \"../sta/vcode/vcode.aspx?cookiename=register&num=5&live=0&date=\" + new Date()); });\r\n                    </");
	templateBuilder.Append("script>\r\n                    ");
	}	//end if

	templateBuilder.Append("\r\n                    <div class=\"row clearfix\" style=\"height:auto;padding-bottom:0;\">\r\n                        <div class=\"field left s13\">会员注册协议：</div>\r\n                        <div class=\"input left\" style=\"width:750px;\">\r\n                            <div id=\"protocol-con\">\r\n                                 <p><strong>继续注册前请先阅读以下协议</strong></p>\r\n                                    <p>欢迎您加入本站点参加交流和讨论，为维护网上公共秩序和社会稳定，请您自觉遵守以下条款：</p>\r\n                                    <p>一、 不得利用本站危害国家安全、泄露国家秘密，不得侵犯国家社会集体的和公民的合法权益，不得利用本站制作、复制和传播下列信息：</p>\r\n                                    <p>（一）煽动抗拒、破坏宪法和法律、行政法规实施的；</p>\r\n                                    <p>（二）煽动颠覆国家政权，推翻社会主义制度的；</p>\r\n                                    <p>（三）煽动分裂国家、破坏国家统一的；</p>\r\n                                    <p>（四）煽动民族仇恨、民族歧视，破坏民族团结的；</p>\r\n                                    <p>（五）捏造或者歪曲事实，散布谣言，扰乱社会秩序的；</p>\r\n                                    <p>（六）宣扬封建迷信、淫秽、色情、赌博、暴力、凶杀、恐怖、教唆犯罪的；</p>\r\n                                    <p>（七）公然侮辱他人或者捏造事实诽谤他人的，或者进行其他恶意攻击的；</p>\r\n                                    <p>（八）损害国家机关信誉的；</p>\r\n                                    <p>（九）其他违反宪法和法律行政法规的；</p>\r\n                                    <p>（十）进行商业广告行为的。</p>\r\n                                    <p>二、互相尊重，对自己的言论和行为负责。</p>\r\n                                    <p>三、您必需同意不发表带有辱骂，淫秽，粗俗，诽谤，带有仇恨性，恐吓的，不健康的或是任何违反法律的内容。 如果您这样做将导致您的账户将立即和永久性的被封锁。（您的网络服务提供商也会被通知）。 在这个情况下，这个IP地址的所有会员都将被记录。您必须同意系统管理成员们有在任何时间删除，修改，移动或关闭任何内容的权力。 作为一个使用者， 您必须同意您所提供的任何资料都将被存入数据库中，这些资料除非有您的同意，系统管理员们绝不会对第三方公开，然而我们不能保证任何可能导致资料泄露的骇客入侵行为。 本系统使用cookie来储存您的个人信息（在您使用的本地计算机）， 这些cookie不包含任何您曾经输入过的信息，它们只为了方便您能更方便的浏览。 电子邮件地址只用来确认您的注册和发送密码使用。（如果您忘记了密码，将会发送新密码的地址） 点击下面的按钮代表您同意受到这些服务条款的约束。</p>\r\n                            </div>\r\n                        </div>\r\n                    </div> \r\n                    <div class=\"row2\"><input type=\"checkbox\" checked=\"checked\" name=\"aggree\" id=\"aggree\" style=\"padding:0;margin:0;\"/> 已阅读并完全接受服务协议</div>\r\n                    <div class=\"row2\">\r\n                        <input type=\"button\" id=\"btnsub\" name=\"btnsub\" class=\"submit\" value=\"\" onclick=\"SubRegForm()\"/>\r\n                    </div>\r\n                </div>\r\n            </form>\r\n        </div>\r\n    </div>\r\n    <div class=\"footer\">\r\n    	<div class=\"left\"> Copyright &copy; 2007-2011 <a href=\"http://www.stacms.com\" title=\"站易内容管理系统\" target=\"_blank\" class=\"cdarkgrey\">STACMS</a> 中视网维 版权所有</div>\r\n        <div class=\"right\" id=\"time\"></div>\r\n    </div>\r\n</div>\r\n<script language=\"javascript\" type=\"text/javascript\">\r\n    window.onload = function () {\r\n        setInterval(\"document.getElementById('time').innerHTML=new Date().toLocaleString()+' 星期'+'日一二三四五六'.charAt(new Date().getDay());\", 1000);\r\n    };\r\n</");
	templateBuilder.Append("script>\r\n</body>\r\n</html>\r\n");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
