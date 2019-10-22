<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_contentadd.aspx.cs" Inherits="STA.Web.Admin.contentadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>视频添加</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link rel="stylesheet" href="../plugin/scripts/colorpicker/css/colorpicker.css" type="text/css" />
    <link rel="stylesheet" href="../plugin/scripts/fancybox/fancybox.css" type="text/css" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../../sta/js/select.js"></script>
    <script type="text/javascript" src="../plugin/scripts/fancybox/fancybox.js"></script>
    <script type="text/javascript" src="../plugin/scripts/colorpicker/js/colorpicker.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <div id="wrapper">
        <form id="form1" runat="server">
        <div id="mwrapper">
            <div id="main">
                <div class="contab clearfix">
                    <ul>
                        <li>基本信息</li>
                        <li>高级设置</li>
                    </ul>
                </div>
                <div class="cont-3">
                    <div class="box">
                        <table>
                            <tr>
                                <td class="itemtitle">
                                    视频名称：
                                </td>
                                <td>
                                    <cc1:TextBox runat="server" ID="txtTitle" Width="320" />
                                    <input type="hidden" value="000000" runat="server" id="hidTitleColor" />
                                    <img src="../images/Rect.gif" alt="设置标题颜色" id="picTitleColor" width="20" height="19"
                                        style='cursor: pointer; background-color: #<%= hidTitleColor.Value%>' title="设置标题颜色" />&nbsp;&nbsp;&nbsp;
                                    视频简称：<cc1:TextBox runat="server" ID="txtSubTitle" Width="200" />
                                </td>
                            </tr>
                            <tr>
                                <td class="itemtitle">
                                    自定义属性：
                                </td>
                                <td>
                                    <cc1:CheckBox runat="server" ID="cbP_h" Text="头条[h]" Checked="false" />
                                    <cc1:CheckBox runat="server" ID="cbP_r" Text="推荐[r]" Checked="false" />
                                    <cc1:CheckBox runat="server" ID="cbP_f" Text="幻灯[f]" Checked="false" />
                                    <cc1:CheckBox runat="server" ID="cbP_a" Text="特荐[a]" Checked="false" />
                                    <cc1:CheckBox runat="server" ID="cbP_s" Text="滚动[s]" Checked="false" />
                                    <cc1:CheckBox runat="server" ID="cbP_b" Text="加粗[b]" Checked="false" />
                                    <cc1:CheckBox runat="server" ID="cbP_i" Text="斜体[i]" Checked="false" />
                                    <cc1:CheckBox runat="server" ID="cbP_p" Text="图片[p]" Checked="false" />
                                    <cc1:CheckBox runat="server" ID="cbP_j" Text="跳转[j]" Checked="false" />
                                </td>
                            </tr>
                            <tr runat="server" id="trImg">
                                <td class="itemtitle">
                                    缩略图：
                                </td>
                                <td>
                                    <cc1:TextBox runat="server" ID="txtImg" Width="300" />
                                    <span id="selectimgfile" class="selectbtn">选择</span>
                                    <span class="previewimgbox" style="margin-left:50px;"></span>
                                </td>
                            </tr>
                            <tr style="display: none;" id="trUrl" runat="server">
                                <td class="itemtitle">
                                    跳转地址：
                                </td>
                                <td>
                                    <cc1:TextBox runat="server" HelpText="如果属性包括跳转,需设置跳转网站" ID="txtUrl" Width="350" />
                                    <a href="javascript:void(0);" onclick="if($.trim($('#txtUrl').val())==''){return}window.open($('#txtUrl').val(),'_blank')">打开</a>
                                </td>
                            </tr>
                            <tr>
                                <td class="itemtitle">
                                    视频标签：
                                </td>
                                <td>
                                    <cc1:TextBox runat="server" ID="txtTags" Width="300" /> <a href="javascript:;" id="atags">热门标签</a>
                                    (每个标签用','号分开)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 权重：<cc1:TextBox runat="server" ID="txtOrderId"
                                        CanBeNull="必填" Text="0" HelpText="只能添数字,默认数字越大频道越靠前" RequiredFieldType="数据校验"
                                        Width="50" />
                                </td>
                            </tr>
                            <tr>
                                <td class="itemtitle">
                                    视频来源：
                                </td>
                                <td>
                                    <cc1:TextBox runat="server" ID="txtSource" Width="200" />
                                    <a href="javascript:;" id="asource">选择</a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 视频作者：<cc1:TextBox
                                        runat="server" ID="txtAuthor" Width="200" />
                                    <a href="javascript:;" id="aauthor">选择</a>
                                </td>
                            </tr>
                            <tr>
                                <td class="itemtitle">
                                    所属频道：
                                </td>
                                <td>
                                    <cc1:DropDownTreeList runat="server" ID="ddlConType" Width="140"/>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 副频道：<cc1:TextBox runat="server" ID="txtExtChannels" Text=","/>
                                    <a href="javascript:;" onclick="SelectExtChannels($('#txtExtChannels'));">选择</a>
                                </td>
                            </tr>
                            <tr>
                                <td class="itemtitle">
                                    内容:
                                </td>
                                <td>
                                    <div class="inner-pad-2">
                                        <cc1:CheckBox runat="server" ID="cbIsFiterDangerousCode" Checked="false" HelpText="如果视频是从其他网站复制的，过滤隐藏脚本"
                                            Text="过滤不安全代码" />
                                        <cc1:CheckBox Checked="false" ID="cbPickImg" runat="server" HelpText="提取内容里的图片作为缩略图"
                                            Text="提取内容图片" />
                                        <span style="display: none;" id="spanPickImg">提取第<cc1:TextBox ID="txtPicImgLocal"
                                            Text="1" runat="server" Width="30" />张</span>
                                        <cc1:CheckBox Checked="false" ID="cbIsRemote" runat="server" HelpText="把视频里的资源下载到本地服务器,建议勿下载有大件的资源否则保存会比较慢"
                                            Text="下载远程资源" />
                                        <span style="display: none;" id="spanRFT">类型：<cc1:TextBox ID="txtResourceFType" Text="gif,jpg,png,bmp,jpeg"
                                            runat="server" Width="120px" /></span>
                                         <cc1:CheckBox Checked="false" ID="cbFiltertags" runat="server" HelpText="过滤HTML标签,每个标签请用半角逗号隔开" Text="过滤标签" />：<cc1:TextBox ID="txtFiltertags" runat="server" Width="170" Text="iframe,object,font"/>&nbsp;</div>
                                    <script type="text/javascript" src="../../sta/editor/ckeditor/<%=browser.IndexOf("ie")>=0?"3.6.3":"4.1.1"%>/ckeditor.js"></script>
                                    <cc1:TextBox runat="server" ID="txtContent" TextMode="MultiLine" />
                                    <script type="text/javascript">
                                        CKEDITOR.replace('txtContent', config.editorset );
                                    </script>
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td class="itemtitle">
                                    视频状态：
                                </td>
                                <td>
                                    <cc1:RadioButtonList RepeatDirection="Horizontal" RepeatColumns="5" Width="500" runat="server" ID="rblStatus"/>
                                </td>
                            </tr>
                            <asp:Literal runat="server" ID="extHtml"></asp:Literal>
                        </table>
                    </div>
                </div>
                <div class="cont-3">
                    <div class="box">
                        <table>
                            <tr>
                                <td class="itemtitle">
                                    评论选项：
                                </td>
                                <td>
                                    <cc1:RadioButtonList RepeatDirection="Horizontal" RepeatColumns="2" Width="500" runat="server"
                                        ID="rblIsComment">
                                        <asp:ListItem Text="允许评论" Value="1" Selected="True" />
                                        <asp:ListItem Text="关闭评论" Value="0" />
                                    </cc1:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="itemtitle">
                                    浏览量：
                                </td>
                                <td>
                                    <cc1:TextBox runat="server" ID="txtClick" Text="1" RequiredFieldType="数据校验" Width="100" />
                                </td>
                            </tr>
                            <tr>
                                <td class="itemtitle">
                                    查看消耗金币：
                                </td>
                                <td>
                                    <cc1:TextBox runat="server" ID="txtCredits" Text="0" RequiredFieldType="数据校验" Width="100" />
                                </td>
                            </tr>
                            <tr>
                                <td class="itemtitle">
                                    顶踩数：
                                </td>
                                <td>
                                    <cc1:TextBox runat="server" ID="txtDiggCount" Text="0" RequiredFieldType="数据校验" Width="50" />&nbsp;次顶&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <cc1:TextBox runat="server" ID="txtStampCount" Text="0" RequiredFieldType="数据校验"
                                        Width="50" />&nbsp;次踩
                                </td>
                            </tr>
                            <tr>
                                <td class="itemtitle">
                                    发布时间：
                                </td>
                                <td>
                                    <cc1:TextBox runat="server" ID="txtAddtime" Width="200"/>
                                </td>
                            </tr>
                            <tr>
                                <td class="itemtitle">
                                    视频模板：
                                </td>
                                <td>
                                    <cc1:TextBox runat="server" ID="txtTemplate" />
                                    <span id="selecttemplate" class="selectbtn">选择</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="itemtitle">
                                    静态保存目录：
                                </td>
                                <td>
                                    <cc1:TextBox runat="server" ID="txtSavePath" HelpText="格式如：/channel/stacms"/>
                                </td>
                            </tr>
                            <tr>
                                <td class="itemtitle">
                                    静态保存文件名：
                                </td>
                                <td>
                                    <cc1:TextBox runat="server" ID="txtFileName" HelpText="不包括文件后缀名,填写如：stacms"/>
                                </td>
                            </tr>
                            <tr>
                                <td class="itemtitle">
                                    SEO页标题：
                                </td>
                                <td>
                                    <cc1:TextBox runat="server" ID="txtPageTitle" Width="500" />
                                </td>
                            </tr>
                            <tr>
                                <td class="itemtitle">
                                    SEO页关键字：
                                </td>
                                <td>
                                    <cc1:TextBox runat="server" ID="txtKeywords" Width="500" TextMode="MultiLine" Height="70" />
                                </td>
                            </tr>
                            <tr>
                                <td class="itemtitle">
                                    SEO页描述：
                                </td>
                                <td>
                                    <cc1:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Height="70"
                                        Width="500" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="navbutton">
                <input type="hidden" runat="server" id="hidId" />
                <input type="hidden" runat="server" id="hidAction" />
                <textarea id="tagslist" runat="server" style="display:none;"/>
                <cc1:Button runat="server" ID="SaveInfo" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/state2.gif"
                    Text=" 保 存 视 频 " />
                <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " />
            </div>
            <div id="footer">
                <%=footer%>
            </div>
        </div>
        </form>
    </div>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/datepicker/WdatePicker.js"></script>
    <script type="text/javascript">
        var channels = [];
        $("#txtAddtime").click(function () { WdatePicker({ isShowWeek: true  ,dateFmt:'yyyy-MM-dd HH:mm:ss'}) });
        RegPreviewImg("#txtImg", null, ".previewimgbox");
        $("#cbP_j").change(function () { $("#trUrl").css("display", $(this).attr("checked") ? "" : "none"); }).trigger("change");
        RegColorPicer("#picTitleColor", "#hidTitleColor", function (col) { $('#picTitleColor').css("background-color", col); });
        $.each($("#ddlConType option"), function (i, o) { if (o.value != "0") { channels.push(o); } });
        $("#cbIsRemote").change(function () { $("#spanRFT").css("display", $(this).attr("checked") ? "" : "none"); }).trigger("change");
        $("#cbPickImg").change(function () { $("#spanPickImg").css("display", $(this).attr("checked") ? "" : "none"); }).trigger("change");
        $("#asource").click(function () { Ajax("getfield&node=ContentSource", function (data) { RegisterPopInsertText(data, "txtSource", "ContentSource"); }); });
        $("#aauthor").click(function () { Ajax("getfield&node=ContentAuthor", function (data) { RegisterPopInsertText(data, "txtAuthor", "ContentAuthor"); }); });
        $("#GoBack").click(function () { location.href = "global_contentlist.aspx?type=<%=STARequest.GetString("type")%>"; });
        RegSelectFilePopWin("selectimgfile", "视频图片选择", "root=<%=filesavepath%>&filetype=jpeg,jpg,gif,png,bmp&fullname=1&cltmed=1&fele=txtImg", "click",$("#txtImg"));
        RegSelectFilePopWin("selecttemplate", "视频模版选择", "root=templates,<%=templatename%>,<%=config.Templatesavedirname%>&filetype=htm&fullname=0&cltmed=1&retsux=0&fele=txtTemplate&rename=0&query=content_", "click",$("#txtTemplate"));
        RegSelectFilePopWin("selectvideo", "视频文件选择", "root=<%=filesavepath%>&path=<%=filesavepath%>&filetype=mp4,flv&fullname=1&cltmed=1&fele=ext_vfile", "click");
        RegPreviewFlv("#ext_vfile", "#previewvideo");
        RegChannelSelect("ddlConType");
        $("#txtExtChannels").bind("keydown", function () { return false; });
        $("#atags").click(function () { RegisterPopInsertText($("#tagslist").val(),"txtTags","",true);});
    </script>
</body>
</html>
