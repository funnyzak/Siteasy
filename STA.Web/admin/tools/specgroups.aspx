<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="specgroups.aspx.cs" Inherits="STA.Web.Admin.Tools.specgroups" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>编辑专题内容组</title>
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
    <form id="form1" runat="server">
    <div id="mwrapper" style="padding:0 7px 0 12px;overflow:hidden;">
        <div id="main" style="margin:0;overflow:hidden;">
            <div class="conb-1">
                <div class="bar">
                    组创建/修改</div>
                <div class="con">
                    <table>
                        <tr>
                            <td style="width:100px;">
                                &nbsp;&nbsp;&nbsp;权重：
                            </td>
                            <td >
                                <cc1:TextBox ID="txtOrderid" Width="120" runat="server" />&nbsp;&nbsp;&nbsp;
                                <cc1:Button ID="AddBtn" runat="server" ButtontypeMode="WithImage" ButtonImgUrl="../images/submit.gif" Text=" 创建组 " />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp;组名称：
                            </td>
                            <td >
                                <cc1:TextBox ID="txtGroupname" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="conb-2">
                <div class="bar">
                    &nbsp;&nbsp;专题内容组</div>
                <div class="con" style="width:97%;padding:10px 1% 10px 2%;margin:0 1% 0 0;">
                    <asp:Repeater ID="rptData" runat="server">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th width="30">
                                        ID
                                    </th>
                                    <th>
                                        名称
                                    </th>
                                    <th>
                                        权重
                                    </th>
                                    <th>
                                        操作
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["id"]%>
                                </td>
                                <td>
                                    <a href="view.aspx?id=<%#((DataRowView)Container.DataItem)["specid"]%>&group=<%#((DataRowView)Container.DataItem)["id"]%>&name=specgroup" target="_blank"><%#((DataRowView)Container.DataItem)["name"]%></a>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["orderid"]%>
                                </td>
                                <td>
                                    <img alt="编辑" title="编辑" src="../images/conedit.png"  onclick="SubmitForm('gedit',<%#((DataRowView)Container.DataItem)["id"]%>);"/>
                                    <img alt="删除" title="删除" src="../images/icon/del.gif" onclick="SConfirm(function () { SubmitForm('del', <%#((DataRowView)Container.DataItem)["id"]%>); }, '确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 吗？');"/>
                                    <input type="hidden" value="<%#((DataRowView)Container.DataItem)["orderid"]%>" name="order<%#((DataRowView)Container.DataItem)["id"]%>" />
                                    <input type="hidden" value="<%#((DataRowView)Container.DataItem)["name"]%>" name="name<%#((DataRowView)Container.DataItem)["id"]%>" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hidAction" runat="server" value="" />
    <input type="hidden" id="hidValue" runat="server" value="" />
    </form>
</body>
</html>
