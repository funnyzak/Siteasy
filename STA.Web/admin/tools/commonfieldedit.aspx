<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="commonfieldedit.aspx.cs" Inherits="STA.Web.Admin.Tools.commonfieldedit" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title><%=fieldname%>设置</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body style="overflow:hidden;">
    <form id="form1" runat="server">
    <div id="mwrapper" style="padding:0 7px 0 12px;overflow:hidden;">
        <div id="main" style="margin:0;overflow:hidden;">
            <table style="width:100%;">
                <tr>
                    <td style="width:100%;">
                       <%=fieldname%>：
                    </td>
                </tr>
                <tr>
                    <td style="width:100%;">
                        <cc1:TextBox runat="server" ID="txtFieldText" Width="310" TextMode="MultiLine" Height="100"/>
                    </td>
                </tr>
            </table>
        <div class="navbutton" style="text-align:right;padding-right:20px;">
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 参 数 "/>
        </div>
    </div>
    </form>
</body>
</html>