<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="STA.Web.Admin.Plus.ctmvariable.list" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>自定义变量管理</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../../plugin/scripts/datepicker/WdatePicker.js"></script>
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
                    变量搜索</div>
                <div class="con">
                    <table class="top">
                        <tr>
                            <td>
                                页大小：<cc1:TextBox ID="txtPageSize" Text="20" Width="30" runat="server" />&nbsp;&nbsp;
                                标识：<cc1:DropDownList runat="server" ID="ddlLikeids">
                                         <asp:ListItem Text="请选择" Value="" Selected="True"/>
                                      </cc1:DropDownList>&nbsp;&nbsp;
                                键名：<cc1:TextBox ID="txtKey" Width="100" runat="server" />&nbsp;&nbsp;
                                变量名：<cc1:TextBox ID="txtName" Width="100" runat="server" />&nbsp;&nbsp;
                                <cc1:Button ID="btnSearch" runat="server" Text="搜索变量" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;自定义变量管理</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            标识
                                        </th>
                                        <th>
                                           变量名
                                        </th>
                                        <th>
                                           键名
                                        </th>
                                        <th>
                                           键值
                                        </th>
                                        <th>
                                           系统
                                        </th>
                                        <th>
                                           描述
                                        </th>
                                        <th>
                                            操作
                                        </th>
                                        <th width="60">
                                            <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" /> 选择
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                         <%#((DataRowView)Container.DataItem)["likeid"]%>
                                    </td>
                                    <td>
                                          <%#((DataRowView)Container.DataItem)["name"]%>
                                    </td>
                                    <td>
                                          <%#((DataRowView)Container.DataItem)["key"]%>
                                    </td>
                                    <td>
                                          <span class="vtip"><%#Utils.RemoveHtml((((DataRowView)Container.DataItem)["value"]).ToString())%></span>
                                    </td>
                                    <td>
                                          <%#(((DataRowView)Container.DataItem)["system"]).ToString()=="1"?"是":"否"%>
                                    </td>
                                    <td>
                                        <span class="dtip"><%#((DataRowView)Container.DataItem)["desc"]%></span>
                                    </td>
                                    <td>
                                        <a title="编辑" href="add.aspx?action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>">编辑</a>
                                        <a title="删除" system="<%#((DataRowView)Container.DataItem)["system"]%>" href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delvariable', <%#((DataRowView)Container.DataItem)["id"]%>); }, '确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 吗？');">删除</a>
                                    </td>
                                    <td>
                                      <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn', 'EditBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                      <input type="hidden" name="key<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["key"]%>" />
                                      <input type="hidden" name="system<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["system"]%>" />
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
                    <cc1:Button runat="server" Enabled="false" ID="EditBtn" AutoPostBack="false" Text="编辑" />
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除" Enabled="false"/>
                    <cc1:Button ID="Button6"  runat="server" AutoPostBack="false" ButtontypeMode="WithImage" ButtonImgUrl="../../images/icon/add.gif" OnClientClick="location.href='add.aspx'" Text=" 创建变量 " />
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        EditForList("EditBtn", Ele("form1"), "add.aspx?action=edit&id=", true);
        $("a[system='1']").remove();
        RegColumnPostip(".dtip", 52, "..");
        RegColumnPostip(".vtip", 30, "..");
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn', 'EditBtn')
        };
    </script>
</body>
</html>
