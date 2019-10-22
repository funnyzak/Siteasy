<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="runad.aspx.cs" Inherits="STA.Web.Admin.Tools.runad" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>运行广告代码</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
</head>
<body>
    <script type="text/javascript" src="<%=Utils.UrlDecode(STARequest.GetString("file"))%>"></script>
</body>
</html>
