<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_pluginzip.aspx.cs" Inherits="STA.Web.Admin.pluginzip" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>扩展包安装</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">扩展包安装</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                选择扩展包(zip)：
                            </td>
                            <td>
                                <asp:FileUpload ID="zipfile" runat="server" CssClass="fileup"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">覆盖重名文件：</td>
                            <td>
			                    <cc1:RadioButtonList runat="server" ID="rblOver" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="0" Selected="True">不覆盖</asp:ListItem>
                                    <asp:ListItem Value="1">覆盖</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <cc1:Button ID="SaveInfo" runat="server" Text="上传安装"/>
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#SaveInfo").bind("click", function () { Loading("这可能需要一点时间...");})
    </script>
</body>
</html>