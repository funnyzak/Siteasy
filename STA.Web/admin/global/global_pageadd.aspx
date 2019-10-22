<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_pageadd.aspx.cs" Inherits="STA.Web.Admin.pageadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>单页模型编辑</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">单页模型编辑</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                页名称：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtName" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                单页标识：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtAlikeid" Width="100" HelpText="可以用标识调用单页集合,或其他作用"/> <a href="javascript:;" id="likeids">已存在标识</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                单页模板：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" Text="page_default" ID="txtTemplate"/> <span id="selecttem" class="selectbtn">选择</span>
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
                                权重：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtOrderid" Width="100" Text="0" RequiredFieldType="数据校验" CanBeNull="必填"/>
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
                                <cc1:TextBox runat="server" ID="txtKeywords" TextMode="MultiLine" Width="500" Height="70"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                SEO页描述：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Height="70" Width="500" />
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                内容:
                            </td>
                            <td>
                                <div class="inner-pad-2">
                                    <cc1:CheckBox runat="server" ID="cbIsFiterDangerousCode" Checked="false" HelpText="如果内容是从其他网站复制的，过滤隐藏脚本"
                                        Text="过滤不安全代码" />
                                    <cc1:CheckBox Checked="false" ID="cbIsRemote" runat="server" HelpText="把内容里的资源下载到本地服务器,建议勿下载有大件的资源否则保存会比较慢"
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
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <input type="hidden" runat="server" id="hidFieldname" />
            <textarea id="likeidlist" runat="server" style="display:none;"/>
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 单 页 "/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'global_pagelist.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        RegSelectFilePopWin("selecttem", "模版选择", "root=templates,<%=templatename%>,<%=config.Templatesavedirname%>&filetype=htm&fullname=0&cltmed=1&retsux=0&fele=txtTemplate&rename=0&query=page_", "click", $("#txtTemplate"));
        $("#cbIsRemote").change(function () { $("#spanRFT").css("display", $(this).attr("checked") ? "" : "none");}).trigger("change");
        $("#likeids").click(function () { RegisterPopInsertText($("#likeidlist").val(),"txtAlikeid");});
    </script>
</body>
</html>