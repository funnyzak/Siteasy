<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="STA.Web.Admin.login" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
<title>管理员登陆 - Siteasy CMS 内容管理系统</title>
<meta http-equiv="x-ua-compatible" content="ie=7" />
<meta name="keywords" content="Siteasy 内容管理系统" />
<meta name="description" content="Siteasy,CMS,asp.net" />
<link href="styles/login.css" type="text/css" rel="stylesheet" />
<script language ="javascript" type ="text/javascript" src ="js/jquery.js"></script>
<script language ="javascript" type ="text/javascript" src ="js/zlib.js"></script>
<script type="text/javascript">if (top.location != self.location) {top.location.href = "login.aspx";};</script>
</head>
<body style="background:#f8f8f6;text-align:center;">
<form method="post" id="form" runat="server">
	<div id="loginpanel">
    	<div id="inputpanel">
        	<div class="row">
            	<label for="username">用&nbsp;户:</label>&nbsp;<cc1:TextBox runat="server" ID="username" TabIndex="1" Width="200"/>
            </div>
        	<div class="row">
            	<label for="password">密&nbsp;码:</label>&nbsp;<cc1:TextBox runat="server" ID="password" TextMode="Password" TabIndex="2" Width="200"/>
            </div>
        	<div class="row" style="padding-top:0;">
            	<label for="vcode">验&nbsp;证:</label>&nbsp;<cc1:TextBox runat="server" ID="vcode" TabIndex="3" Width="70"/><img src="../sta/vcode/vcode.aspx?cookiename=syslogin&num=5&live=0" id="vimg" align="absMiddle" style="cursor:pointer;" alt="验证码" title="看不清，换一张"/>
            </div>
            <div class="row" style="text-align:left;padding:5px 0 0 90px;">
            	<input class="loginbtn" type="button" onclick="SubLogin();" tabindex="4" title="试试Enter键"/>&nbsp;<span style="color:#ff0000;" id="errortips"></span>
            </div>
        </div>
        <div id="copyright">Copyright © 2009-2011 By <a href="http://www.stacms.com/" style="color:#333;" target="_blank">Siteasy</a> All Rights Reserved</div>
    </div>
</form>
<script type="text/javascript">
    function SubLogin() {
        var username = $("#username").val(), password = $("#password").val(), vcode = $("#vcode").val();
        if (username == "" || password == "" || vcode == "") {
            ErrorTips("登录信息填写不完整！");
            return false;
        }
        $("form").submit();
    }
    function ErrorTips(tips) {
        $("#errortips").show().html(tips);
        setTimeout(function () { $("#errortips").fadeOut("slow"); }, 2000);
    }
    $(function () {
        if ($B.ie6) {
            ErrorTips("为了更好的操作体验,请升级浏览器！");
        }
        $("#vimg").css("display", "").click(function () { $(this).attr("src", "../sta/vcode/vcode.aspx?cookiename=syslogin&num=5&live=0&date=" + new Date()); });
        $($("#username").attr("readonly") == "readonly" ? "#password" : "#username").focus();
        $(document).keydown(function (event) {
            if (event.keyCode == "13") {
                SubLogin();
                return false;
            }
        });
    });
</script>
<%=script%>
</body>
</html>