<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_adminlogs.aspx.cs" Inherits="STA.Web.Admin.adminlogs" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>系统日志</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/datepicker/WdatePicker.js"></script>
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
                        系统日志搜索</div>
                    <div class="con">
                        <table class="top">
                            <tr>
                                <td>
                                    时间：<cc1:TextBox ID="txtStartDate" Width="70" runat="server" />
                                    -
                                    <cc1:TextBox ID="txtEndDate" Width="70" runat="server" />
                                    &nbsp; 管理员：<cc1:TextBox ID="txtUsers" Width="120" HelpText="如果多个管理员中间请用空格或半角逗号隔开"
                                        runat="server" />&nbsp;&nbsp; 关键字：<cc1:TextBox ID="txtKeywords" HelpText="可以是动作名或动作描述"
                                            Width="220" runat="server" />&nbsp;
                                    <cc1:Button ID="btnSearch" runat="server" Text="搜索日志" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;系统日志</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            管理员
                                        </th>
                                        <th>
                                            管理组
                                        </th>
                                        <th>
                                            IP地址
                                        </th>
                                        <th>
                                            操作日期
                                        </th>
                                        <th>
                                            动作
                                        </th>
                                        <th>
                                            描述
                                        </th>
                                        <th width="40">
                                            操作
                                        </th>
                                        <th width="60">
                                            <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" />
                                            选择
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["username"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["groupname"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["ip"]%>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["action"]%>
                                    </td>
                                    <td>
                                        <span class="ptip"><%#((DataRowView)Container.DataItem)["remark"]%></span>
                                    </td>
                                    <td>
                                        <a href="javascript:void(0)" onclick="Del('<%#((DataRowView)Container.DataItem)["id"]%>')">
                                            删除</a>
                                    </td>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn')"
                                            name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
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
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;"
                        Text="删除所选" Enabled="false" />&nbsp;
                    <cc1:Button ID="EmptyBtn" runat="server" OnClientClick="ControlPostBack('EmptyBtn', '确认清空符合当前条件的记录吗?');return;"
                        Text="清空当前" />&nbsp;
                    <cc1:Button ID="EmptyAllBtn" runat="server" OnClientClick="ControlPostBack('EmptyAllBtn', '确认所有记录吗?');return;"
                        Text="全部清空" />&nbsp;
                    <cc1:Button ID="StopBtn" runat="server" OnClientClick="ControlPostBack('StopBtn', '确认停止日志吗?');return;"
                        Text="停止日志功能" />&nbsp;
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        function Del(id) {
            SConfirm(function () {
                SubmitForm("dellogs", id);
            }, "删除后不可恢复，确认删除吗？");
        }
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn')
        }
        $("#txtStartDate,#txtEndDate").click(function () { WdatePicker({ isShowWeek: true }) });
        RegColumnPostip(".ptip", 36, "..");
    </script>
</body>
</html>
