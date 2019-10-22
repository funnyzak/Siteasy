<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_filesexplore.aspx.cs" Inherits="STA.Web.Admin.filesexplore" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>文件浏览器</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="../plugin/scripts/fancybox/fancybox.css" type="text/css" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../plugin/scripts/fancybox/mousewheel.js"></script>
    <script type="text/javascript" src="../plugin/scripts/fancybox/fancybox.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
    <style type="text/css">
        .conb-1 .con{padding-top: 15px;padding-bottom: 5px;}
    </style>
</head>
<body>
    <div id="wrapper">
        <form id="form1" runat="server">
        <div id="mwrapper">
            <div id="main">
                <div class="conb-1">
                    <div class="bar">
                        快捷操作</div>
                    <div class="con">
                        <table class="top">
                            <tr>
                                <td>
                                    <ul class="ftools">
                                        <li title="返回系统根目录" onclick="SubmitForm('openfolder','^rootpath^');">[根目录]</li>
                                        <li title="刷新当前目录" onclick="SubmitForm();">[刷新]</li>
                                        <li title="如果剪贴板有文件或目录即可粘贴" onclick="SubmitForm('pastefile');">[粘贴]</li>
                                        <li title="压缩当前目录" onclick="SAlert('压缩可能需要一点时间，请稍等...',100);SubmitForm('zipfolder');">
                                            [压缩]</li>
                                        <li title="新建文件" onclick="PopWindow('新建文件', '../tools/createnewfile.aspx?path=<%=currentvirtualpath%>', '', 605,295);">
                                            [新建文件]</li>
                                        <li title="在当前目录上传文件" onclick="PopWindow('上传文件', '../tools/uploadfiles.aspx?path=<%=currentvirtualpath%>', '', 320, 200);">
                                            [上传文件]</li>
                                        <li title="在当前路径新建文件夹" onclick="PopWindow('创建文件夹', '../tools/createnewfolder.aspx?path=<%=currentvirtualpath%>', '', 420, 70);">
                                            [新建文件夹]</li>
                                        <li title="检查当前目录占用空间大小" onclick="SubmitForm('spacesize');">[空间检查]</li>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;文件浏览器</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            文件名
                                        </th>
                                        <th>
                                            文件大小
                                        </th>
                                        <th>
                                            最后修改时间
                                        </th>
                                        <th width="120">
                                            操作
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Eval("other")%>
                                    </td>
                                    <td rel="fsize" folder="<%# Eval("isfolder")%>" size="<%# Eval("size")%>">
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "LastWriteDate", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td class="foperate" folder="<%# Eval("isfolder")%>" name="<%# Eval("name")%>">
                                        <span onclick="EditFileName('<%# Eval("isfolder")%>','<%# ReplaceJsComma(Eval("name"))%>')">编辑</span>
                                        <a href="javascript:;" onclick="DelFile('<%# Eval("isfolder")%>','<%# ReplaceJsComma(Eval("name"))%>');">删除</a> 
                                        <a href="javascript:;" onclick="SubmitForm('cutfile','<%# ReplaceJsComma(Eval("name"))%>');">剪切</a> 
                                        <a href="javascript:;" onclick="SubmitForm('copyfile','<%# ReplaceJsComma(Eval("name"))%>');">复制</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <input type="hidden" id="hidAction" runat="server" value="" />
                <input type="hidden" id="hidValue" runat="server" value="" />
                <div class="operate">
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        $(function () {
            if ($("span.topdir").length > 0) {
                $(".list tr:eq(1) td:gt(0)").remove(); ;
                $(".list tr:eq(1) td").attr("colspan", "4");
            }
            $("td.foperate").each(function () {
                if ($(this).attr("folder") == "True") {
                    $(this).find("span:eq(0)").html("改名");
                } else {
                    if (!IsFileReadType($(this).attr("name"))) {
                        $(this).find("span:eq(0)").remove();
                    }
                }

                var temp = $(this).attr("name").toLowerCase().split(".");
                if ($.inArray(temp[temp.length - 1], ("zip").split(",")) >= 0) {
                    $(this).prepend("<span onclick=\"SConfirm(function () { SubmitForm('unzipfile', '" + $(this).attr("name") + "') }, '确定解压 <b>  " + $(this).attr("name") + " </b> 到当前目录吗？');\">解压</span>");
                }
            });
        });
        RegFancyBox('a[rel="img"]');
        RegFancyBox('a[ftype="swf"]');
        RegFancyBox('a[ftype="txtread"]');
        RegPostip('span[for="tip"]');
        $("td[rel=fsize]").each(function (idx) {
            var size = parseInt($(this).attr("size")), isfolder = $(this).attr("folder");
            $(this).html(isfolder != "True" ? ConvertSize(size) : "");
        });
        function DelFile(folder, path) {
            if (folder == "True") {
                SConfirm(function () { SAlert("如果文件夹文件过多，删除可能需要几分钟时间，请稍等片刻...", 100); SubmitForm('delfile', path) }, '删除后不可恢复，确认删除文件夹 <b>' + path + '</b> 吗？');
            } else {
                SConfirm(function () { SubmitForm('delfile', path) }, '删除后不可恢复，确认删除文件 <b>' + path + '</b> 吗？');
            }
        }
        function EditFileName(folder, path) {
            if (folder == "True") {
                PopWindow('修改文件夹 ' + path, '../tools/createnewfolder.aspx?path=<%=currentvirtualpath%>' + path, '', 420, 70);
            } else {
                if (IsFileReadType(path)) {
                    PopWindow('修改文件 ' + path, '../tools/createnewfile.aspx?path=<%=currentvirtualpath%>' + path, '', 605, 300);
                } else {
                    SAlert(path+" 不是可在线编辑的文件类型！");
                }
            }
        }
    </script>
</body>
</html>
