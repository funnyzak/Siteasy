<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qrcode.aspx.cs" Inherits="STA.Web.Admin.Tools.qrcode" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>条码工具</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="../plugin/scripts/fancybox/fancybox.css" type="text/css" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script type="text/javascript" src="../plugin/scripts/fancybox/fancybox.js"></script>
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
                    条码扫描</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle" style="padding:15px 0 15px 15px;">
                               扫描类型：
                            </td>
                            <td>
                                <cc1:RadioButtonList runat="server" ID="rblScantype" RepeatColumns="5" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">二维码地址</asp:ListItem>
                                    <asp:ListItem Value="2">二维码上传</asp:ListItem>
                                    <asp:ListItem Value="3">线性码地址</asp:ListItem>
                                    <asp:ListItem Value="4">线性码上传</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr style="display:none;" scan="1">
                            <td class="itemtitle">
                               二维码地址：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtQrcode" Width="500" Text="http://" HelpText="设置一个有效的二维码图片地址,如:http://www.example.com/qrcode.png"/>
                            </td>
                        </tr>
                        <tr style="display:none;" scan="2">
                            <td class="itemtitle">
                               上传二维码：
                            </td>
                            <td>
                                <asp:FileUpload ID="fileQrcode" runat="server" CssClass="fileup" Width="505"/>
                            </td>
                        </tr>
                        <tr style="display:none;" scan="3">
                            <td class="itemtitle">
                               条形码地址：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtBarcode" Width="500" Text="http://" HelpText="设置一个有效的线性码图片地址,如:http://www.example.com/barcode.png"/>
                            </td>
                        </tr>
                        <tr style="display:none;" scan="4">
                            <td class="itemtitle">
                               上传条形码：
                            </td>
                            <td>
                                <asp:FileUpload ID="fileBarcode" runat="server" CssClass="fileup" Width="505"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle" style="padding:20px 0 0 0;">
                               &nbsp;
                            </td>
                            <td>
                                <cc1:Button runat="server" ID="BtnDecode" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/state2.gif" Text=" 扫描二维码/线性码 " onclick="BtnDecode_Click"/>&nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="trDecoderlt" visible="false">
                            <td class="itemtitle">
                               扫描结果：
                            </td>
                            <td>
                                <cc1:TextBox TextMode="MultiLine" Width="500" Height="100" ID="txtDecoderlt" runat="server" Enabled="false"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="conb-1">
                <div class="bar">
                    条码生成</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle" style="padding:15px 0 15px 15px;">
                               条码类型：
                            </td>
                            <td>
                                <cc1:DropDownList ID="ddlMake" runat="server">
                                </cc1:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                               条码比例：
                            </td>
                            <td>
                               宽：<cc1:TextBox runat="server" ID="txtWidth" Width="70" Text="200"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                               高：<cc1:TextBox runat="server" ID="txtHeight" Width="70" Text="200"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                               条码内容：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtCodecontent" Width="500" Text=""/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle" style="padding:20px 0 0 0;">
                               &nbsp;
                            </td>
                            <td>
                                <cc1:Button runat="server" ID="BtnMake" ButtontypeMode="WithImage" 
                                    ButtonImgUrl="../images/icon/state2.gif" Text=" 生成二维码/线性码 " 
                                    onclick="BtnMake_Click"/>&nbsp;
                            </td>
                        </tr>
                        <tr runat="server" id="trMake" visible="false">
                            <td class="itemtitle">
                               条码图片：
                            </td>
                            <td>
                                <asp:Image ID="imgMake" runat="server" />  
                                &nbsp;&nbsp;<asp:HyperLink ID="hlDown" runat="server">下载条码</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                               <cc1:PageInfo ID="PageInfo1" runat="server" Text="QR_CODE是二维条码的一种,QR码比较普通条码可以存储更多数据;ITF为常用的条形码格式。<br/>EAN码是国际物品编码协会制定的一种商品用条码，通用于全世界。EAN码符号有标准版（EAN-13）和缩短版（EAN-8）两种。<br/>CODABAR(库德巴码)也可表示数字和字母信息，主要用于医疗卫生、图书情报、物资等领域的自动识别<br/>UPC码是美国统一代码委员会制定的一种商品用条码，主要用于美国和加拿大地区，我们在美国进口的商品上可以看到。<br/>39码是一种可表示数字、字母等信息的条码，主要用于工业、图书及票证的自动化管理，目前使用极为广泛。 了解更多请到：<a href='http://zh.wikipedia.org/wiki/%E6%9D%A1%E5%BD%A2%E7%A0%81' target='_blank' class='cred'>维基百科</a>"/>
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
        $("input[name='rblScantype']").click(function () { $("tr[scan]").hide();$("tr[scan='" + $(this).val() + "']").show()});
        $("input[name='rblScantype']:checked").trigger("click");
//        $("#ddlMake").change(function () {
//            var txt = "", width = 200, height = 200;
//            switch ($(this).val()) {
//                case "CODE_39": 
//                    txt = "ABC-1234"; 
//                    height = 90; 
//                    break;
//                default: 
//                    txt = ""; 
//                    break;
//            }
//            $("#txtCodecontent").val(txt);
//            $("#txtWidth").val(width);
//            $("#txtHeight").val(height);
//        });
    </script>
</body>
</html>
