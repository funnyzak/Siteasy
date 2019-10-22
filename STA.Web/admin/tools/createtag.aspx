<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="createtag.aspx.cs" Inherits="STA.Web.Admin.Tools.createtag" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>新建/修改标签</title>
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
            <table>
                <tr>
                    <td style="width:80px;">
                        标签名称：
                    </td>
                    <td>
                        <cc1:TextBox runat="server" ID="txtName" Width="200"/>
                    </td>
                </tr>
            </table>
        <div class="navbutton" style="text-align:center;">
             <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保存标签 "/>
        </div>
    </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#txtName").focus();
    </script>
</body>
</html>