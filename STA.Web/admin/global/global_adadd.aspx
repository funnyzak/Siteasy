<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_adadd.aspx.cs" Inherits="STA.Web.Admin.adadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>广告添加</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link rel="stylesheet" href="../plugin/scripts/colorpicker/css/colorpicker.css" type="text/css" />
    <link rel="stylesheet" href="../plugin/scripts/fancybox/fancybox.css" type="text/css" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../../sta/editor/xheditor/xheditor.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/datepicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script type="text/javascript" src="../plugin/scripts/fancybox/fancybox.js"></script>
    <script type="text/javascript" src="../plugin/scripts/colorpicker/js/colorpicker.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">广告添加</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                               广告名称：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" AlignX="right" AlignY="center" HelpText="广告名称由字母或数字或下划线或汉字组成. 模版调用广告标签格式：{ad 广告名称} 建议名称唯一,如名称重复,则调用最新一条. " ID="txtName" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                是否生效：
                            </td>
                            <td>
                                <cc1:RadioButtonList runat="server" ID="rblStatus" RepeatColumns="5" RepeatDirection="Horizontal"/>
                            </td>
                        </tr>
                        <tr style="display:none;" tpe="date">
                            <td class="itemtitle">
                                开始日期：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtStartdate"  Width="100"/>
                            </td>
                        </tr>
                        <tr style="display:none;" tpe="date">
                            <td class="itemtitle">
                                结束日期：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtEnddate" Width="100"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                广告类型：
                            </td>
                            <td>
                                <cc1:DropDownList runat="server" ID="ddlAdtype" />
                            </td>
                        </tr>
                        <tr fill="f" ape="1">
                            <td class="itemtitle">
                                文字内容：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtTextcontent"/>
                            </td>
                        </tr>
                        <tr fill="f" ape="1">
                            <td class="itemtitle">
                                文字链接：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtTextlink"/>
                                <a href="javascript:void(0);" onclick="if($.trim($('#txtTextlink').val())==''){return}window.open($('#txtTextlink').val(),'_blank')">打开</a>
                            </td>
                        </tr>
                        <tr fill="f" ape="1">
                            <td class="itemtitle">
                                文字大小：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtTextsize" HelpText="可使用pt、px、em为单位" Text="12px"  Width="100"/>
                            </td>
                        </tr>
                        <tr fill="f" ape="1">
                            <td class="itemtitle">
                                文字颜色：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtTextcolor" Width="100"/>
                            </td>
                        </tr>
                        <tr fill="f" ape="2">
                            <td class="itemtitle">
                                图片地址：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtImgurl"/>
                                <span id="selectimgfile" class="selectbtn">选择</span>
                                <a href="javascript:;" id="previewImg">预览</a>
                            </td>
                        </tr>
                        <tr fill="f" ape="2">
                            <td class="itemtitle">
                                图片链接：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtImglink"/>
                                <a href="javascript:void(0);" onclick="if($.trim($('#txtImglink').val())==''){return}window.open($('#txtImglink').val(),'_blank')">打开</a>
                            </td>
                        </tr>
                        <tr fill="f" ape="2">
                            <td class="itemtitle">
                                图片描述：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtImgalt"/>
                            </td>
                        </tr>
                        <tr fill="f" ape="2">
                            <td class="itemtitle">
                                图片宽度：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtImgwidth" Width="100"/>
                            </td>
                        </tr>
                        <tr fill="f" ape="2">
                            <td class="itemtitle">
                                图片高度：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtImgheight" Width="100"/>
                            </td>
                        </tr>

                        <tr fill="f" ape="3">
                            <td class="itemtitle">
                                Flash地址：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtFlashurl"/>
                                <span id="selectflashfile" class="selectbtn">选择</span>
                                <a href="javascript:;" id="previewFlash">预览</a>
                            </td>
                        </tr>
                        <tr fill="f" ape="3">
                            <td class="itemtitle">
                                Flash宽度：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtFlashwidth" Width="100"/>
                            </td>
                        </tr>
                        <tr fill="f" ape="3">
                            <td class="itemtitle">
                                Flash高度：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtFlashheight" Width="100"/>
                            </td>
                        </tr>
                        <tr fill="f" ape="4">
                            <td class="itemtitle">
                              <cc1:Help Text="代码：" runat="server" HelpText="双击可以选择追加文件;代码支持HTML代码、JS代码; 带有JS代码建议切换源代码编辑,且需要使用script标签" />
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtCode" TextMode="MultiLine" Height="170" Width="570"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                               失效显示内容：
                            </td>
                            <td>
                                <cc1:TextBox HelpText="如果广告过期，或不生效显示的内容，支持HTML代码" Text="广告已经过期" runat="server" ID="txtOutdate" TextMode="MultiLine" Height="70" Width="565"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <asp:Button ID="SaveInfo" runat="server" Text="保 存 广 告" CssClass="mbutton"/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'global_adlist.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#txtStartdate,#txtEnddate").click(function () { WdatePicker({ isShowWeek: true }) });
        $("input[name='rblStatus']").click(function () { $("tr[tpe]").css("display", $(this).val() == "1" ? "" : "none"); });
        $("input[name='rblStatus']:checked").trigger("click");
        RegColorPicer("#txtTextcolor");
        $("#ddlAdtype").change(function () { $("tr[fill]").css("display", "none"); $("tr[ape='" + $(this).val() + "']").css("display", ""); }).trigger("change");
        RegSelectFilePopWin("selectimgfile", "广告图片选择", "root=<%=filesavepath%>&filetype=jpeg,jpg,gif,png,bmp&fullname=1&cltmed=1&fele=txtImgurl", "click", $("#txtImgurl"));
        RegSelectFilePopWin("selectflashfile", "Flash选择", "root=<%=filesavepath%>&filetype=swf&fullname=1&cltmed=1&fele=txtFlashurl", "click", $("#txtFlashurl"));
        //RegSelectFilePopWin("txtCode", "选择文件", "root=<%=filesavepath%>&fullname=1&cltmed=2&fele=txtCode", "dblclick");
        RegPreviewImg("#txtImgurl", "#previewImg");
        RegPreviewFlash("#txtFlashurl", "#previewFlash");
        $("#txtCode").xheditor($.extend(xhconfig, { upImgUrl: "!../tools/selectfile.aspx?root=<%=filesavepath%>&filetype=jpeg,jpg,gif,png,bmp&cltmed=3", upFlashUrl: "!../tools/selectfile.aspx?root=<%=filesavepath%>&filetype=swf&cltmed=3" }));
    </script>
</body>
</html>