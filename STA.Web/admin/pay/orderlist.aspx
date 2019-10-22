<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderlist.aspx.cs" Inherits="STA.Web.Admin.Pay.orderlist" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<%@ Import Namespace="STA.Entity" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>订单管理</title>
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
                    订单搜索</div>
                <div class="con">
                    <table class="top">
                        <tr>
                            <td>
                                页大小：<cc1:TextBox ID="txtPageSize" Text="20" Width="30" runat="server" />&nbsp;&nbsp;
                                订单状态：<cc1:DropDownList runat="server" ID="ddlStatus"/>&nbsp;&nbsp;
                                快递方式：<cc1:DropDownList runat="server" ID="ddlDlist"/>&nbsp;&nbsp;
                                生成日期：<cc1:TextBox ID="txtStartDate" RequiredFieldType="日期" Width="80" runat="server" /> - <cc1:TextBox ID="txtEndDate" RequiredFieldType="日期" Width="80" runat="server" />&nbsp;&nbsp;
                                订单金额：<cc1:TextBox ID="txtSprice" Width="50" runat="server" />元 - <cc1:TextBox ID="txtEprice" Width="50" runat="server" />元
                            </td>
                        </tr>
                        <tr>
                            <td>
                                付款方式：<cc1:DropDownList runat="server" ID="ddlPlist" Width="180"/>&nbsp;&nbsp;
                                订单用户：<cc1:TextBox ID="txtUsers" Width="120" HelpText="如果多个用户中间请用空格或半角逗号隔开" runat="server" />&nbsp;&nbsp;
                                订单号：<cc1:TextBox ID="txtOid" Width="260" runat="server" />&nbsp;
                                <cc1:Button ID="btnSearch" runat="server" Text="搜索订单" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;订单管理</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                           <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" /> 订单号
                                        </th>
                                        <th>
                                            订单用户
                                        </th>
                                        <th>
                                           配送方式
                                        </th>
                                        <th>
                                           付款方式
                                        </th>
                                        <th>
                                            总金额
                                        </th>
                                        <th>
                                            需要发票
                                        </th>
                                        <th>
                                            生成日期
                                        </th>
                                        <th>
                                            订单状态
                                        </th>
                                        <th>
                                            订单说明
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                      <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn', 'BtnEdit', 'BtnSPayed', 'BtnSSended', 'BtnSEnd')" name="cbid" value="<%#((DataRowView)Container.DataItem)["oid"]%>" /> <a href="orderset.aspx?oid=<%#((DataRowView)Container.DataItem)["oid"]%>"><%#((DataRowView)Container.DataItem)["oid"]%></a>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["username"]%>
                                    </td>
                                    <td did="<%#((DataRowView)Container.DataItem)["did"]%>">
                                        <%#((DataRowView)Container.DataItem)["did"]%>
                                    </td>
                                    <td pid="<%#((DataRowView)Container.DataItem)["pid"]%>">
                                        <%#((DataRowView)Container.DataItem)["pid"]%>
                                    </td>
                                    <td>
                                        <%#string.Format("{0:C}",(((DataRowView)Container.DataItem)["totalprice"]).ToString())%>元
                                    </td>
                                    <td title="抬头：<%#((DataRowView)Container.DataItem)["invoicehead"]%>">
                                        <%#(((DataRowView)Container.DataItem)["isinvoice"]).ToString()=="1"?"是":"否"%>
                                    </td>
                                    <td>
                                       <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <%#((OrderStatus)TypeParse.StrToInt(((DataRowView)Container.DataItem)["status"])).ToString()%>
                                    </td>
                                    <td>
                                       <span class="ctip"><%#((DataRowView)Container.DataItem)["remark"]%></span>
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
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除订单" Enabled="false"/>
                    <cc1:Button ID="BtnEdit" runat="server" Text="修改订单" Enabled="false" AutoPostBack="false"/>
                    <cc1:Button ID="BtnSPayed" runat="server" OnClientClick="ControlPostBack('BtnSPayed', '确认设置所选订单为已付款状态吗?');return;" Text="设为已付款状态" Enabled="false"/>
                    <cc1:Button ID="BtnSSended" runat="server" OnClientClick="ControlPostBack('BtnSSended', '确认设置所选订单为已发货状态吗?');return;" Text="设为已发货状态" Enabled="false"/>
                    <cc1:Button ID="BtnSEnd" runat="server" OnClientClick="ControlPostBack('BtnSEnd', '确认设置所选订单为已完成状态吗?');return;" Text="设为已完成状态" Enabled="false"/>
                    <cc1:Button ID="BtnExport" runat="server" Text="导出订单" Enabled="false" Visible="false"/>
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        var groups = [], groups2 = [];
        $.each($("#ddlDlist option"), function (i, o) { if (o.value != "0") { groups.push(o); } });
        $.each($("#ddlPlist option"), function (i, o) { if (o.value != "0") { groups2.push(o); } });
        function GetGroupName(gs, gid) {
            if (gid == "0") return "未知";
            for (var i = 0; i < gs.length; i++) {
                if (gs[i].value == gid) return gs[i].text;
            }
            return "未知";
        };
        $("td[did]").each(function () {
            $(this).html(GetGroupName(groups,$(this).attr("did")));
        });
        $("td[pid]").each(function () {
            $(this).html(GetGroupName(groups2, $(this).attr("pid")));
        });
        $("#txtStartDate,#txtEndDate").click(function () { WdatePicker({ isShowWeek: true }) });
        RegColumnPostip(".ctip", 24, "..");
        EditForList("BtnEdit", Ele("form1"), "orderset.aspx?oid=", true, null);
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn', 'BtnEdit', 'BtnSPayed', 'BtnSSended', 'BtnSEnd')
        };
    </script>
</body>
</html>
