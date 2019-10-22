<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_errorlogs.aspx.cs" Inherits="STA.Web.Admin.errorlogs" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>错误日志</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link rel="stylesheet" href="../plugin/scripts/fancybox/fancybox.css" type="text/css" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
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
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;错误日志</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            日志名
                                        </th>
                                        <th>
                                            日志大小
                                        </th>
                                        <th>
                                            创建时间
                                        </th>
                                        <th>
                                            最近报错时间
                                        </th>
                                        <td>
                                            操作
                                        </td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <a ftype="txtread" class="fancybox.iframe" href="../tools/readfilecontent.aspx?url=../../sta/logs/error/<%# Eval("name")%>"><%# Eval("name")%></a>      
                                    </td>
                                    <td rel="fsize" folder="<%# Eval("isfolder")%>" size="<%# Eval("size")%>"> 
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "CreationDate", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "LastWriteDate", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <a ftype="txtread" class="fancybox.iframe" href="../tools/readfilecontent.aspx?url=../../sta/logs/error/<%# Eval("name")%>">查看</a>
                                        <a href="javascript:" onclick="SConfirm(function () {SubmitForm('dellogs', '<%# Eval("name")%>');}, '确认删除吗？');">删除</a>
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
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        $("td[rel=fsize]").each(function (idx) {
            var size = parseInt($(this).attr("size")), isfolder = $(this).attr("folder");
            $(this).html(isfolder != "True" ? ConvertSize(size) : "");
        });
        $('a[ftype="txtread"]').each(function () { $(this).html($(this).html().split('.')[0]); });
        RegFancyBox('a[ftype="txtread"]');
    </script>
</body>
</html>
