<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_translate.aspx.cs" Inherits="STA.Web.Admin.translate" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>语言翻译</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../plugin/scripts/selectbox/css/jquery.selectbox.css" type="text/css" rel="stylesheet" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/selectbox/js/jquery.selectbox.js"></script>
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
                    站易语言翻译</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="pad-1" style="padding-bottom:0;">
                                请把要翻译的文本粘贴或写到这里：
                            </td>
                        </tr>
                        <tr>
                            <td class="pad-2">
                                <cc1:TextBox TextMode="MultiLine" Width="100%" Height="150" ID="txtText" Text="" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="pad-1">
                                <cc1:DropDownList ID="ddlLang" runat="server">
                                    <asp:ListItem Value="auto">自动检测语言</asp:ListItem>
                                    <asp:ListItem Value="zh-CN,en">中 → 英</asp:ListItem>
                                    <asp:ListItem Value="en,zh-CN">英 → 中</asp:ListItem>
                                    <asp:ListItem Value="zh-CN,th">中 → 泰</asp:ListItem>
                                    <asp:ListItem Value="zh-CN,ko">中 → 韩</asp:ListItem>
                                    <asp:ListItem Value="zh-CN,ja">中 → 日</asp:ListItem>
                                    <asp:ListItem Value="zh-CN,fr">中 → 法</asp:ListItem>
                                    <asp:ListItem Value="zh-CN,ru">中 → 俄</asp:ListItem>
<%--                                    <asp:ListItem Value="ja,zh-CH">日 → 中</asp:ListItem>
                                    <asp:ListItem Value="ru,zh-CN">俄 → 中</asp:ListItem>
                                    <asp:ListItem Value="fr,zh-CN">法 → 中</asp:ListItem>
                                    <asp:ListItem Value="ko,zh-CN">韩 → 中</asp:ListItem>
                                    <asp:ListItem Value="en,ja">英 → 日</asp:ListItem>
                                    <asp:ListItem Value="ja,en">日 → 英</asp:ListItem>--%>
                                </cc1:DropDownList>
                                <cc1:Button runat="server" ID="Translate" ButtontypeMode="WithImage" AutoPostBack="false" ButtonImgUrl="../images/icon/state2.gif" Text=" 开始翻译 "/>
                            </td>
                        </tr>
                        <tr class="trrlt" style="display:none;">
                            <td class="pad-1" style="padding-top:30px;">
                                <div id="copytxt" style="" class="cred s13">复制翻译结果：</div>
                                <div class="txtrlt" style="width:100%;font-size:13px;">
                                </div>
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
    <script language="javascript" type="text/javascript" src="../js/ZeroClipboard.js"></script>
    <script type="text/javascript">
        $("#Translate").click(function () {
            $("tr.trrlt").css("display", "none");
            var text = $("#txtText").val(), sl = "auto", tl = "en";
            if ($("#ddlLang").val() != "auto") {
                sl = $("#ddlLang").val().split(",")[0];
                tl = $("#ddlLang").val().split(",")[1];
            }
            if ($.trim(text) == "") {
                SAlert("请把要翻译的文本粘贴或写到文本框里！");
                return;
            }
            Loading("正在努力为您翻译..")
            Ajax("translate&def=0&text=" + encodeURI(text) + "&sl=" + sl + "&tl=" + tl, function (txt) {
                $.unblockUI();
                if (txt != "" && txt != "0") {
                    $("tr.trrlt").slideDown("slow");
                    ClipReg(txt, "copytxt", "翻译结果已复制到剪贴板")
                    $(".txtrlt").html(HtmlEncode(txt));
                } else {
                    SAlert("抱歉，暂时无法翻译！");
                }
            });
        });
    </script>
</body>
</html>
