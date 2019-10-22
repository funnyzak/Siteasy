<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_attachments.aspx.cs" Inherits="STA.Web.Admin.attachments" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>附件管理</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link rel="stylesheet" href="../plugin/scripts/fancybox/fancybox.css" type="text/css" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../plugin/scripts/fancybox/mousewheel.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/datepicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../plugin/scripts/fancybox/fancybox.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <div id="wrapper">
        <form id="form1" runat="server">
        <div id="mwrapper">
            <div id="main">
                <div class="conb-1">
                    <div class="bar">
                        附件搜索</div>
                    <div class="con">
                        <table class="top">
                            <tr>
                                <td>
                                     页大小：<cc1:TextBox ID="txtPageSize" Text="20" Width="30" runat="server" />&nbsp;&nbsp;
                                    上传时间：<cc1:TextBox ID="txtStartDate" Width="80" runat="server" /> - <cc1:TextBox  ID="txtEndDate" Width="80" runat="server" />&nbsp;&nbsp;
                                    相关用户：<cc1:TextBox ID="txtUsers" Width="125" HelpText="多个用户中间请用空格或半角逗号隔开" runat="server" />&nbsp;&nbsp;
                                    附件类型：<cc1:TextBox ID="txtFiletype" Width="125" HelpText="多个请用空格或半角逗号隔开,如gif,png" runat="server" />&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    文件大小(Kb)：<cc1:TextBox ID="txtMinsize" Text="0" Width="50" runat="server" /> - <cc1:TextBox ID="txtMaxsize" Width="50" runat="server" />&nbsp;&nbsp;      
                                    关键字：<cc1:TextBox ID="txtKeywords" Width="120" runat="server" />&nbsp;&nbsp;
                                    <cc1:Button ID="btnSearch" runat="server" Text="搜索附件" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;附件列表</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            附件名
                                        </th>
                                        <th>
                                            附件大小
                                        </th>
                                        <th>
                                            上传/编辑用户
                                        </th>
                                        <th>
                                            上传时间
                                        </th>
                                        <th>
                                            附件类型
                                        </th>
                                        <th>
                                            下载量
                                        </th>
                                        <th width="100">
                                            操作
                                        </th>
                                        <th width="60">
                                            <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" /> 选择
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td tit="<%#((DataRowView)Container.DataItem)["attachment"]%>" tid="t<%#((DataRowView)Container.DataItem)["id"]%>"
                                        path="<%=sitepath%><%#(((DataRowView)Container.DataItem)["filename"]).ToString().Trim()%>"
                                        ftype="<%#(((DataRowView)Container.DataItem)["fileext"]).ToString().Trim()%>">
                                        <%#Utils.GetUnicodeSubString((((DataRowView)Container.DataItem)["attachment"]).ToString().Trim(),40,"..")%>
                                    </td>
                                    <td rel="fsize">
                                        <%#((DataRowView)Container.DataItem)["filesize"]%>
                                    </td>
                                    <td>
                                        <%#(((DataRowView)Container.DataItem)["username"]).ToString().Trim()%>/<%#(((DataRowView)Container.DataItem)["lasteditusername"]).ToString().Trim()%>
                                        <span aid="<%#((DataRowView)Container.DataItem)["id"]%>" style="display: none;" class="titext">
                                            <b>名称:</b><%#((DataRowView)Container.DataItem)["attachment"]%><br />
                                            <b>描述:</b><%#((DataRowView)Container.DataItem)["description"]%>
                                        </span>
                                    </td>
                                    <td title="编辑时间：<%#DataBinder.Eval(Container.DataItem, "lastedittime", "{0:yyyy-MM-dd HH:mm:ss}")%>">
                                        <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%> 
                                    </td>
                                    <td>
                                        <span class="filetype" id="fy<%#((DataRowView)Container.DataItem)["id"]%>" ftype="<%#(((DataRowView)Container.DataItem)["fileext"]).ToString().Trim()%>">
                                        </span>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["downloads"]%>次
                                    </td>
                                    <td>
                                        <a target="_blank" href="../../attachment.aspx?attid=<%#((DataRowView)Container.DataItem)["id"]%>">下载</a>
                                        <span onclick="PopWindow('附件修改', 'global_attachmentedit.aspx?action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>','', 380, 197);">编辑</span>
                                        <a href="javascript:void(0)" onclick=" SConfirm(function () {SubmitForm('delattachment', <%#((DataRowView)Container.DataItem)["id"]%>);}, '删除后不可恢复，确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["attachment"])%></b> 吗？');">删除</a>
                                    </td>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn', 'BtnCopyCode', 'BtnCopyLink')"
                                            name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                        <input type="hidden" name="path<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#(((DataRowView)Container.DataItem)["filename"]).ToString().Trim()%>" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        <ucl:PageGuide ID="pGuide" runat="server" />
                    </div>
                </div>
                <input type="hidden" id="hidAction" runat="server" value="" />
                <input type="hidden" id="hidValue" runat="server" value="" />
                <div class="operate">
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '删除后不可恢复，确认删除吗?');return;"
                        Text="删除所选" Enabled="false" />
                    <cc1:Button ID="EmptyBtn" runat="server" OnClientClick="ControlPostBack('EmptyBtn', '删除后不可恢复，确认清空符合当前条件的所有附件吗?');return;"
                        Text="清空当前" />
                    <cc1:Button ID="BtnCopyLink" runat="server" Text="复制附件链接" AutoPostBack="false"/>
                     <cc1:Button ID="BtnCopyCode" runat="server" Text="复制图片代码" AutoPostBack="false"/>
                    <cc1:Button ID="Button6" runat="server" AutoPostBack="false" ButtontypeMode="WithImage"
                        ButtonImgUrl="../images/icon/add.gif" OnClientClick="PopWindow('附件添加','global_attachmentedit.aspx', '', 380, 197);"
                        Text=" 上传附件" />
                    <cc1:Button ID="Button1" runat="server" AutoPostBack="false" ButtontypeMode="WithImage"
                        ButtonImgUrl="../images/icon/add.gif" OnClientClick="location.href='global_attachmentuploadpre.aspx';"
                        Text=" 批量上传附件" />
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <%--<script language="javascript" type="text/javascript" src="../js/ZeroClipboard.js"></script>--%>
    <script type="text/javascript">
        $(".filetype").each(function () { $(this).html(GetFileExtStr($(this).attr("ftype")) + " [" + $(this).attr("ftype") + "]"); });
        $("td[path]").each(function (idx, obj) {
            var ftype = $(this).attr("ftype").toLowerCase(), path = $(this).attr("path");
            var isimg = $.inArray(ftype, ("jpg,jpeg,gif,bmp,png").split(",")) >= 0, id = $(this).attr("tid").replace("t",""), temp = "";
            var istxtread = IsFileReadType(ftype);
            if (ftype == "flv" || ftype == "mp4")
                path = "../tools/playvideo.aspx?id=" + id;
            if (istxtread)
                path = "../tools/readfilecontent.aspx?url=" + encodeURIComponent(path);
            temp = "<a href='" + path + "' title='" + $(this).attr("tit") + "' ";
            if (isimg)
                temp += " rel='imglist' ";
            else if (ftype == "swf")
                temp += " ftype='swf' ";
            else if (istxtread || ftype == "flv" || ftype == "mp4")
                temp += " ftype='txtread' class='fancybox.iframe' ";
            else
                temp += " target='_blank' ";
            temp += " id='preview" + idx + "'>";
            $(this).html(temp + "<img title=\"查看\" onerror=\"this.src='../images/file/other.gif';\" src='../images/file/" + ftype + ".gif'/> " + $(this).html() + "</a>");
        });
        RegFancyBox("a[rel=imglist]");
        RegFancyBox("a[ftype='swf']");
        RegFancyBoxUrl("a[ftype='txtread']");
        $("td[rel=fsize]").each(function (idx) { $(this).html(ConvertSize(parseInt($(this).html()))); });
        $(".titext").each(function () {
            var title = $(this).html(), id = $(this).attr("aid");
            var path = $("td[tid='t" + id + "']").attr("path");
            if (IsImgFilename(path.toLowerCase())) {
                title += "<br/><img style='padding:5px 0 0;' width='150' src='" + path + "'/>"
            }
            title = "<div style='width:150px;word-wrap:break-word;'>" + title + "</div>";
            $("td[tid='t" + id + "']").find("a").wrap("<span/>");
            RegPostip($("td[tid='t" + id + "']").find("span").attr("title", title));
        });
        $("#txtStartDate,#txtEndDate").click(function () { WdatePicker({ isShowWeek: true }) });
        EditForList("BtnCopyLink", Ele("form1"), "", false, function (val) {
            var ret = "";
            $.each(val.split(","), function (idx, obj) {
                ret += config.weburl + $("td[tid='t" + obj + "']").attr("path") + "\n";
            });
            code = "<div style='padding:0 20px 0 13px;'><textarea name=\"attlinks\" class=\"txt\" style=\"overflow-x:hidden;height:90px;width:100%;\">" + ret + "</textarea></div>";
            PopWindow("CTRL+C 复制以下链接", code, "common", 450, 100);
            $("textarea[name='attlinks']").select();
        });
        EditForList("BtnCopyCode", Ele("form1"), "", false, function (val) {
            var ret = "";
            $.each(val.split(","), function (idx, obj) {
                var path = $("td[tid='t" + obj + "']").attr("path");
                if (IsImgFilename(path)) {
                    ret += "<img src=\"" + config.weburl + path + "\" /><br />";
                }
            });
            if (ret == "") {
                SAlert("请选择要复制代码的图片附件！");
                return;
            }
            code = "<div style='padding:0 20px 0 13px;'><textarea name=\"attcode\" class=\"txt\" style=\"overflow-x:hidden;height:90px;width:100%;\">" + ret.substring(0, ret.length - 6) + "</textarea></div>";
            PopWindow("CTRL+C 复制以下图片代码", code, "common", 450, 100);
            $("textarea[name='attcode']").select();
        });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn', 'BtnCopyCode', 'BtnCopyLink')
        }
    </script>
</body>
</html>
