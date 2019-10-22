<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sendpmtogroup.aspx.cs" Inherits="STA.Web.Admin.sendpmtogroup" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>群发短消息</title>
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
        function SendPM_Call() {
            Loading("开始发送消息,请稍等..");
            SendPM(0);
        };
        function SendPM(startuid) {
            Ajax("sendpmtogroup&topnumber=100&groupidlist=<%=groupidlist%>&msgfrom=<%=Utils.UrlEncode(username)%>&msguid=<%=userid%>&folder=0&start_uid=" + startuid + "&subject=" + escape($("#subject").val()) + "&message=" + escape($("#message").val()), function (resp) {
                resp = ToJson(resp);
                if (resp.count != "0") {
                    count += parseInt(resp.count);
                    Loading("已发送了" + count + "条消息");
                    SendPM(resp.startuid);
                } else {
                    $.unblockUI();
                    $("#subject,#message").val("");
                    SAlert("全部发送完毕,共发送了 " + count + "条消息！");
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
                <div class="bar">群发短消息</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                <cc1:Help ID="Help1" runat="server" Text="接收用户组"/>:
                            </td>
                            <td class="ugroup">
                                <cc1:CheckBoxList runat="server" ID="cblGlist" RepeatDirection="Horizontal" RepeatColumns="10"></cc1:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                            <cc1:Help ID="Help2" runat="server" Text="消息标题"/>：
                                
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="subject" Text=""  Width="420"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                
                                <cc1:Help ID="Help3" runat="server" Text="消息内容" HelpText=""/>:
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="message" TextMode="MultiLine" Height="200" Width="600" Text=""/>
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