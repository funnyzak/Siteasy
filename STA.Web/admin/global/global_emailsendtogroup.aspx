<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_emailsendtogroup.aspx.cs" Inherits="STA.Web.Admin.emailsendtogroup" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>邮件群发</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../../sta/editor/xheditor/xheditor.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
    <script type="text/javascript">
        var count = 0;
        function SendEmail_Call() {
            Loading("开始发送邮件,请稍等..");
            SendEmail(0);
        };
        function SendEmail(startuid) {
            Ajax("usergroupsendemail&topnumber=100&groupidlist=<%=groupidlist%>&start_uid=" + startuid + "&subject=" + escape($("#subject").val()) + "&body=" + escape($("#message").val()), function (resp) {
                console.log(resp);
                resp = ToJson(resp);
                if (resp.count != "0") {
                    count += parseInt(resp.count);
                    Loading("已发送了" + count + "封邮件");
                    SendEmail(resp.startuid);
                } else {
                    $.unblockUI();
                    $("#subject,#message").val("");
                    SAlert("全部发送完毕,共发送了 " + count + "封邮件！");
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">邮件群发</div>
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
                        <tr>
                            <td class="itemtitle">
                                <cc1:Help ID="Help3" runat="server" Text="邮件主题"/>:
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="subject"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                <cc1:Help ID="Help4" runat="server" Text="邮件内容"/>:
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="message" TextMode="MultiLine" Height="200"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <asp:Button runat="server" ID="Send" Text=" 发送邮件 "  CssClass="mbutton"/>
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#message").css("width", "100%").xheditor($.extend(xhconfig, { upImgUrl: "!../tools/selectfile.aspx?root=<%=filesavepath%>&filetype=jpeg,jpg,gif,png,bmp&cltmed=3&weburl=1", upFlashUrl: "!../tools/selectfile.aspx?root=<%=filesavepath%>&filetype=swf&cltmed=3&weburl=1", tools: ',Source,|,Blocktag,List,|,Fontface,Bold,Italic,Underline,|,FontColor,BackColor,Removeformat,|,Link,Unlink,Img,|,Table,Fullscreen,|,Preview,' }));
        $(window).resize(function () {
            $("#subject,#message").width("100%").width($("#txtTitle").width() - 6);
        }).trigger("resize");
    </script>
</body>
</html>