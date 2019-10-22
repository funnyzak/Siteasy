<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_scheduleadd.aspx.cs" Inherits="STA.Web.Admin.scheduleadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>定时任务设置</title>
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
        <cc1:PageInfo id="PageInfo1" runat="server" Text="请勿随意添加计划任务,此功能仅适用于开发人员。"/>
            <div class="conb-1">
                <div class="bar">定时任务设置</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                启用：
                            </td>
                            <td>
			                    <cc1:RadioButtonList ID="rblEnable" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
				                    <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
				                    <asp:ListItem Value="0">否</asp:ListItem>
			                    </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                任务标识：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtName" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                任务类型：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtType" Width="500" CanBeNull="必填" HelpText="例如:STA.Event.DemoEvent, STA.Event"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                执行方式：
                            </td>
                            <td>
			                    <asp:RadioButton ID="type1" GroupName="type" runat="server" Checked="true" />&nbsp;定时执行&nbsp;&nbsp;
			                    <cc1:DropDownList ID="hour" runat="server"/>&nbsp;时&nbsp;<cc1:DropDownList ID="minute" runat="server" />&nbsp;分&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			                    <asp:RadioButton ID="type2" GroupName="type" runat="server" />&nbsp;周期执行&nbsp;&nbsp;
			                    <cc1:TextBox ID="timeserval" runat="server" Width="40" ></cc1:TextBox>&nbsp;分钟
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidAction" />
            <input type="hidden" runat="server" id="oldkey" />
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 配 置 "/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'global_schedulemanage.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
</body>
</html>