<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Loading.aspx.cs" Inherits="STA.Web.Admin.Frame.Loading" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
<title>Siteasy 内容管理系统</title>
<meta http-equiv="x-ua-compatible" content="ie=7" />
<meta name="keywords" content="Siteasy 内容管理系统" />
<meta name="description" content="Siteasy,CMS,asp.net" />
<link href="../styles/base.css" type="text/css" rel="stylesheet" />
<script language ="javascript" type ="text/javascript" src ="../js/jquery.js"></script>
<script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
	<%
	string msg = "操作执行中，请稍侯... ",ico="2";
	if (STARequest.GetQueryString("type") == "loading")
		msg = "载入中，请稍候... ";
        ico = STARequest.GetQueryInt("icon", 2).ToString();
	%>
    <div class="loading<%=ico%>"><%=msg%></div>
    <script type="text/javascript">
        setTimeout(function () { location.href = "<%=Utils.UrlDecode(STARequest.GetQueryString("url"))%>"; }, 10);
	</script>
</body>
</html>