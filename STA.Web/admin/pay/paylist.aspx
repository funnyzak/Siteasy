<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paylist.aspx.cs" Inherits="STA.Web.Admin.Pay.paylist" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<%@ Import Namespace="STA.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>支付方式管理</title>
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
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;支付方式管理</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th style="width:20%;">
                                           <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" /> 支付名称
                                        </th>
                                        <th style="width:52%;">
                                            支付方式说明
                                        </th>
                                        <th style="width:7%;">
                                           是否安装
                                        </th>
                                        <th style="width:7%;">
                                           是否启用
                                        </th>
                                        <th style="width:7%;">
                                            作者
                                        </th>
                                        <th style="width:7%;">
                                            版本
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','BtnEdit', 'BtnSetup', 'BtnUnstall')" name="cbid" value="<%#((DataRowView)Container.DataItem)["dll"]%>" />
                                        <input type="hidden" value="<%#((DataRowView)Container.DataItem)["setup"]%>" id="setup_<%#((DataRowView)Container.DataItem)["dll"]%>" />
                                        <input type="hidden" value="<%#((DataRowView)Container.DataItem)["name"]%>" id="name_<%#((DataRowView)Container.DataItem)["dll"]%>" />
                                        <a href="<%#((DataRowView)Container.DataItem)["officeurl"]%>" target="_blank"><%#((DataRowView)Container.DataItem)["name"]%></a>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["defaultdescription"]%>
                                    </td>
                                    <td>
                                      &nbsp;<%#(((DataRowView)Container.DataItem)["setup"]).ToString() == "1" ? "<span class='cblue'>已安装<span>" : "<span class='cred'>未安装<span>"%>
                                    </td>
                                    <td>
                                      &nbsp;<%#(((DataRowView)Container.DataItem)["isvalid"]).ToString() == "1" ? "已启用" : "未启用"%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["author"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["version"]%>
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
                    <cc1:Button ID="BtnSetup" runat="server" Text="安 装" Enabled="false"/>
                    <cc1:Button ID="BtnUnstall" runat="server" Text="卸 载" OnClientClick="ControlPostBack('BtnUnstall', '确认卸载所选支付方式吗?');return;" Enabled="false"/>
                    <cc1:Button ID="BtnEdit"  runat="server" AutoPostBack="false" Text=" 配置支付方式 " Enabled="false"/>
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        EditForList("BtnEdit", Ele("form1"), "", true, function (id) {
            if ($("#setup_" + id).val() == "0") {
                SAlert("<b>" + $("#name_" + id).val() + "</b>未安装,不可配置！");
            } else {
                location.href = "payset.aspx?dll=" + id;
            }
        });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'BtnEdit', 'BtnSetup', 'BtnUnstall');
        };
    </script>
</body>
</html>
