<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pickerrlist.aspx.cs" Inherits="STA.Plus.Admin.pickerrlist" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>内容挑错管理</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/public.js"></script>
</head>
<body>
    <div id="wrapper">
        <form id="form1" runat="server">
        <div id="mwrapper">
            <div id="main">
                <div class="conb-1">
                    <div class="bar">
                        内容挑错搜索</div>
                    <div class="con">
                        <table class="top">
                            <tr>
                                <td>
                                    来源IP：<cc1:TextBox ID="txtIp" Width="100" runat="server" />&nbsp;&nbsp;
                                    错误类型：<cc1:DropDownList runat="server" ID="ddlType"></cc1:DropDownList>&nbsp;&nbsp;
                                    标题：<cc1:TextBox ID="txtKeywords" Width="220" runat="server" />&nbsp;
                                    <cc1:Button ID="btnSearch" runat="server" Text="搜索" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;内容挑错列表</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                           <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" /> 标题
                                        </th>
                                        <th>
                                           错误类型
                                        </th>
                                        <th>
                                           提交时间
                                        </th>
                                        <th>
                                            来源IP
                                        </th>
                                        <th>
                                            出错说明
                                        </th>
                                        <th>
                                            指出错误
                                        </th>
                                        <th width="100">
                                            操作
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                        <a class="ptip" href="<%#GetConAddPage(TypeParse.StrToInt(((DataRowView)Container.DataItem)["typeid"],1))%>&action=edit&id=<%#((DataRowView)Container.DataItem)["cid"]%>&url=<%=Utils.UrlEncode(currentpage)%>"><%#((DataRowView)Container.DataItem)["title"]%></a>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["type"]%>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "subtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["ip"]%>
                                    </td>
                                    <td>
                                       <span class="ptip2"><%#((DataRowView)Container.DataItem)["errortxt"]%></span>
                                    </td>
                                    <td>
                                        <span class="ptip3"><%#((DataRowView)Container.DataItem)["righttxt"]%></span>
                                    </td>
                                    <td>
                                        <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delpickerr', <%#((DataRowView)Container.DataItem)["id"]%>); }, '删除后不可恢复，确认删除吗？');">删除</a>
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
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除" Enabled="false"/>
                    <cc1:Button ID="Button1" runat="server" OnClientClick="window.open('/plus/pickerr.aspx')" Text="打开前台" AutoPostBack="false"/>
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        RegColumnPostip(".ptip", 30, "..");
        RegColumnPostip(".ptip2", 30, "..");
        RegColumnPostip(".ptip3", 30, "..");
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn')
        }
    </script>
</body>
</html>
