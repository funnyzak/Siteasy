<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mailsend.aspx.cs" Inherits="STA.Web.Admin.mailsend" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>订阅邮件发送</title>
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
                <div class="bar">订阅邮件发送</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                <cc1:Help ID="Help1" runat="server" Text="接收组"/>:
                            </td>
                            <td>
                                <cc1:CheckBoxList runat="server" ID="cblGlist" RepeatDirection="Horizontal" RepeatColumns="10"></cc1:CheckBoxList>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td class="itemtitle">
                                <cc1:Help runat="server" Text="收件人" HelpText="在这里可自由填写其他收件人邮箱,每个邮箱用半角(英文)逗号隔开;已存在的邮件地址,系统会自动过滤" />:
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtUsers" TextMode="MultiLine" Height="30"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                            <cc1:Help ID="Help2" runat="server" Text="邮件主题"/>：
                                
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtTitle" Text="{receiver},你好！"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                
                                <cc1:Help ID="Help3" runat="server" Text="邮件内容" HelpText="{unsubscribeurl}:退订地址、{receiver}:接收者"/>:
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtCont" TextMode="MultiLine" Height="350" Text="<a href='{unsubscribeurl}' target='_blank'>退订点这里</a>"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <asp:Button runat="server" ID="Send" Text=" 发送订阅邮件 " CssClass="mbutton"/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回  " OnClientClick="location.href = 'mailrecords.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#txtCont").css("width", "100%").xheditor($.extend(xhconfig, { upImgUrl: "!../tools/selectfile.aspx?root=<%=filesavepath%>&filetype=jpeg,jpg,gif,png,bmp&cltmed=3&weburl=1", upFlashUrl: "!../tools/selectfile.aspx?root=<%=filesavepath%>&filetype=swf&cltmed=3&weburl=1", tools: ',Source,|,Blocktag,List,|,Fontface,Bold,Italic,Underline,|,FontColor,BackColor,Removeformat,|,Link,Unlink,Img,|,Table,Fullscreen,|,Preview,' }));
        $(window).resize(function () {
            $("#txtTitle,#txtUsers").width("100%").width($("#txtTitle").width() - 6);
        }).trigger("resize");
        $("#Send").click(function () { Loading("发送中,请等待.."); });
    </script>
</body>
</html>