<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paylist.aspx.cs" Inherits="STA.Web.Admin.Pay.paylogs" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<%@ Import Namespace="STA.Entity" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>支付记录管理</title>
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
                    支付记录搜索</div>
                <div class="con">
                    <table class="top">
                        <tr>
                            <td>
                                页大小：<cc1:TextBox ID="txtPageSize" Text="20" Width="30" runat="server" />&nbsp;&nbsp;
                                付款方式：<cc1:DropDownList runat="server" ID="ddlPlist" Width="150"/>&nbsp;&nbsp;
                                商品类型：<cc1:DropDownList runat="server" ID="ddlGtype"/>&nbsp;&nbsp;
                                支付日期：<cc1:TextBox ID="txtStartDate" RequiredFieldType="日期" Width="70" runat="server" /> - <cc1:TextBox ID="txtEndDate" RequiredFieldType="日期" Width="70" runat="server" />&nbsp;&nbsp;
                                支付金额：<cc1:TextBox ID="txtSprice" Width="50" runat="server" />元 - <cc1:TextBox ID="txtEprice" Width="50" runat="server" />元
                            </td>
                        </tr>
                        <tr>
                            <td>
                                相关用户：<cc1:TextBox ID="txtUsers" Width="120" HelpText="如果多个用户中间请用空格或半角逗号隔开" runat="server" />&nbsp;&nbsp;
                                订单号：<cc1:TextBox ID="txtOid" Width="260" runat="server" />&nbsp;
                                <cc1:Button ID="btnSearch" runat="server" Text="搜索记录" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;支付记录管理</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th >
                                           <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" /> 订单号
                                        </th>
                                        <th>
                                            相关用户
                                        </th>
                                        <th>
                                           商品类型
                                        </th>
                                        <th>
                                           支付金额
                                        </th>
                                        <th>
                                           支付方式
                                        </th>
                                        <th>
                                           支付日期
                                        </th>
                                        <th>
                                           相关描述
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid', 'DelBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                      <%#((DataRowView)Container.DataItem)["oid"]%>
                                        <input type="hidden" value="<%#((DataRowView)Container.DataItem)["oid"]%>" id="oid_<%#((DataRowView)Container.DataItem)["id"]%>" />
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["username"]%>
                                    </td>
                                    <td>
                                      <%#((GoodType)TypeParse.StrToInt(((DataRowView)Container.DataItem)["gtype"])).ToString()%>
                                    </td>
                                    <td>
                                      <%#string.Format("{0:C}",(((DataRowView)Container.DataItem)["amount"]).ToString())%>元
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["payname"]%>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <span class="ctip"><%#((DataRowView)Container.DataItem)["title"]%></span>
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
                     <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除记录" Enabled="false"/>
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        RegColumnPostip(".ctip", 30, "..");
        $("#txtStartDate,#txtEndDate").click(function () { WdatePicker({ isShowWeek: true }) });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn');
        };
    </script>
</body>
</html>
