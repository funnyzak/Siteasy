<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="readfilecontent.aspx.cs" Inherits="STA.Web.Admin.Tools.readfilecontent" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>查看文件</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
    <style type="text/css">
     form,body,textarea{height:100%;width:100%;}
     body{overflow:hidden;}
    </style>
</head>
<body style="overflow:hidden;">
    <form id="form1" runat="server">
        <textarea runat="server" id="txtContent" style="height:100%;width:100%;border:0;"></textarea>
<%--        <div style="padding:8px 0 0 0;text-align:center;width:100%; ">
        <cc1:Button runat="server" ID="SaveInfo" Text="修 改 文 件" ButtonImgUrl="../images/submit.gif"/>&nbsp;&nbsp;
        </div>--%>
    </form>
    <script type="text/javascript">
       // $("#txtContent").bind("keydown", function () { return false; });
        $(window).resize(function () {
            $("#txtContent").height($(window).height());
        }).trigger("resize");
    </script>
</body>
</html>