<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_emailsend.aspx.cs" Inherits="STA.Web.Admin.emailsend" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>邮件单发</title>
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
                <div class="bar">邮件单发</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                <cc1:Help ID="Help2" runat="server" Text="接收人"/>:
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtUsers" HelpText="<b>双击即可以选择用户</b><br/>在这里填写收件人邮箱,每个邮箱用半角(英文)逗号隔开" TextMode="MultiLine" Height="30"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                <cc1:Help ID="Help3" runat="server" Text="邮件主题"/>:
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtTitle"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                <cc1:Help ID="Help4" runat="server" Text="邮件内容"/>:
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtContent" TextMode="MultiLine" Height="200"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <asp:Button runat="server" ID="Send" Text=" 发送邮件 " CssClass="mbutton"/>
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#txtContent").css("width", "100%").xheditor($.extend(xhconfig, { upImgUrl: "!../tools/selectfile.aspx?root=<%=filesavepath%>&filetype=jpeg,jpg,gif,png,bmp&cltmed=3&weburl=1", upFlashUrl: "!../tools/selectfile.aspx?root=<%=filesavepath%>&filetype=swf&cltmed=3&weburl=1", tools: ',Source,|,Blocktag,List,|,Fontface,Bold,Italic,Underline,|,FontColor,BackColor,Removeformat,|,Link,Unlink,Img,|,Table,Fullscreen,|,Preview,' }));
        $(window).resize(function () {
            $("#txtTitle,#txtUsers").width("100%").width($("#txtTitle").width() - 6);
        }).trigger("resize");
        $("#txtUsers").dblclick(function () {
            PopWindow("用户邮件选择", "../tools/usermailselect.aspx","", 620, 385,$("#txtUsers"));
        });
        function AddMail(mail) {
            $("#txtUsers").append(mail + ",");
        }
        $("#Send").click(function () { Loading("发送中,请等待.."); });
    </script>
</body>
</html>