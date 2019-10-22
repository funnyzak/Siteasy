<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sendpmsingle.aspx.cs" Inherits="STA.Web.Admin.sendpmsingle" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>单发短消息</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../../sta/editor/xheditor/xheditor.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">单发短消息</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                <cc1:Help ID="Help1" runat="server" Text="接收人" HelpText="请填写接收人用户名,多个请用半角逗号隔开"/>:
                            </td>
                            <td>
                               <cc1:TextBox runat="server" ID="msgto" HelpText="" Width="300" CanBeNull="必填"></cc1:TextBox> 
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                            <cc1:Help ID="Help2" runat="server" Text="消息标题"/>：
                                
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="subject" Text="" Width="420" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                
                                <cc1:Help ID="Help3" runat="server" Text="消息内容" HelpText=""/>:
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="message" TextMode="MultiLine" Height="200" Width="600" Text="" CanBeNull="必填"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <cc1:Button runat="server" ID="Send" Text=" 发 送 消 息 " ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/state2.gif"/>
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
</body>
</html>