<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_channelbatchadd.aspx.cs" Inherits="STA.Web.Admin.channelbatchadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>频道批量添加</title>
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
                <div class="bar">频道批量添加</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">父频道：</td>
                            <td>
                                <cc1:DropDownTreeList runat="server" ID="ddrChannels"/>
                            </td>
                        </tr>
                        <tr>
	                        <td class="itemtitle">内容模型：</td>
	                        <td>
                                <cc1:DropDownList runat="server" ID="ddlConType"/>
                            </td>
                        </tr> 
                        <tr>
	                        <td class="itemtitle">频道填写：</td>
	                        <td>
                                <cc1:TextBox CanBeNull="必填" TextMode="MultiLine" HelpText="每个频道换行分割，下级频道在频道前添加“-”字符，索引可以放到括号中。如：<br/><b>频道(索引)<br/>-下级频道(索引)<br/>--下级频道(索引)</b>" Width="500" AlignX="right" AlignY="center" Height="200" ID="txtChannels" runat="server"/>
                            </td>
                        </tr> 
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 所 有 频 道 "/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'global_channelist.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
</body>
</html>