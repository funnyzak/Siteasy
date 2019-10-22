<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_jsmin.aspx.cs" Inherits="STA.Web.Admin.jsmin" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>JS脚本压缩</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/jsmin.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">
                    JS脚本压缩</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="pad-1">
                                压缩级别：
                            </td>
                        </tr>
                        <tr>
                            <td class="pad-2" style="padding-top:0;">
                                <cc1:RadioButtonList runat="server" ID="rblLevel" RepeatColumns="3" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">最小</asp:ListItem>
                                    <asp:ListItem Value="2">普通</asp:ListItem>
                                    <asp:ListItem Value="3" Selected="True">超级</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="pad-1" style="padding-bottom:0;">
                                代码信息：
                            </td>
                        </tr>
                        <tr>
                            <td class="pad-2">
                                <cc1:TextBox TextMode="MultiLine" Width="100%" Height="50" ID="txtComment" Text="//" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="pad-1" style="padding-bottom:0;">
                                要压缩的代码，拷贝到下面的框中：
                            </td>
                        </tr>
                        <tr>
                            <td class="pad-2">
                                <cc1:TextBox TextMode="MultiLine" Width="100%" Height="150" ID="txtSource" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="pad-1">
                                <cc1:Button runat="server" ID="MinGo" ButtontypeMode="WithImage" AutoPostBack="false" ButtonImgUrl="../images/icon/state2.gif" Text=" 开始压缩 "/>
                            </td>
                        </tr>
                        <tr class="mined" style="display:none;">
                            <td class="pad-1 mdesc" style="padding-bottom:0;">
                                
                            </td>
                        </tr>
                        <tr class="mined" style="display:none;">
                            <td class="pad-2">
                                <cc1:TextBox TextMode="MultiLine" Width="100%" Height="150" ID="txtMincode" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#MinGo").click(function () {
            var source = $("#txtSource").val(),comment = $("#txtComment").val(),level = GetRadioValue("rblLevel");
            if (source != "") {
                var mined = jsmin(comment, source, level);
                $("#txtMincode").val(mined);
                $(".mdesc").html("原来的大小：<b title=\"共" + jsmin.oldSize + "字节\">" + ConvertSize(jsmin.oldSize) + "</b>, 压缩后的大小：<b title=\"共" + jsmin.newSize + "字节\">"
                                + ConvertSize(jsmin.newSize) + "</b>, 压缩率：<b>" + (Math.round(jsmin.newSize / jsmin.oldSize * 10000) / 100) + '%</b>' + " 代码如下： ");
                $(".mined").show();
                return;
            }
            SAlert("请先粘贴JS代码到代码框再进行压缩！");
            $(".mined").hide();
        });
    </script>
</body>
</html>
