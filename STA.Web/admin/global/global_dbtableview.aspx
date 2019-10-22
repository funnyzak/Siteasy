<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_dbtableview.aspx.cs" Inherits="STA.Web.Admin.dbtableview" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<%@ Import Namespace="System.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>表结构查看</title>
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">
                    表结构查看</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                数据库表：
                            </td>
                            <td>
                                <cc1:ListBox runat="server" ID="ltbTables" AutoPostBack="true" Height="150" Width="150"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="conb-2" runat="server" id="tablestruct">
                <div class="bar">
                    &nbsp;&nbsp;<span runat="server" id="spantablename"/>表结构</div>
                <div class="con">
                    <asp:Repeater ID="rptData" runat="server">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>				
                                    <th>
                                        字段名
                                    </th>
                                    <th>
                                        数据类型
                                    </th>
                                    <th>
                                        数据长度
                                    </th>
                                    <th>
                                        数据精度
                                    </th>
                                    <th>
                                        小数位数
                                    </th>
                                    <th>
                                       属性
                                    </th>
                                    <th>
                                       默认值
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["name"]%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["type"]%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["length"]%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["precision"]%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["scale"]%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["property"]%>
                                </td>
                                <td class="defval">
                                    <%#((DataRowView)Container.DataItem)["definition"]%>
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
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $(".defval").each(function () { $(this).html($.trim($(this).html())==""?"null":$(this).html()); });
    </script>
</body>
</html>
