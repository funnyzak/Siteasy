<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_contypes.aspx.cs" Inherits="STA.Web.Admin.contypes" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>内容模型管理</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
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
                        &nbsp;&nbsp;内容模型列表</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                           <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" /> 模型名称
                                        </th>
                                        <th>
                                            标识
                                        </th>
                                        <th>
                                            附加表
                                        </th>
                                        <th>
                                            权重
                                        </th>
                                        <th>
                                            状态
                                        </th>
                                        <th>
                                            类型
                                        </th>
                                        <th width="150">
                                            操作
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid', 'BtnOpen', 'BtnClose')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                        <input type="hidden" name="name<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["name"]%>" />
                                        <%#((DataRowView)Container.DataItem)["name"]%>[ID:<%#((DataRowView)Container.DataItem)["id"]%>]
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["ename"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["extable"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["orderid"]%>
                                    </td>
                                    <td>
                                        <%#(((DataRowView)Container.DataItem)["open"]).ToString()=="1"?"开启":"关闭"%>
                                    </td>
                                    <td>
                                        <%#(((DataRowView)Container.DataItem)["system"]).ToString()=="1"?"系统":"自动"%>
                                    </td>
                                    <td tid="<%#((DataRowView)Container.DataItem)["id"]%>">
                                        <a href="global_contypeadd.aspx?action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>">编辑</a>
                                        <a href="global_contypefieldedit.aspx?action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>">字段维护</a>
                                        <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delcontype', <%#((DataRowView)Container.DataItem)["id"]%>); }, '确认删除吗，删除后与此关联的频道将失效，确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 吗？');">删除</a>
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
                    <cc1:Button runat="server" ID="BtnOpen" Enabled="false" Text="开启模型" />&nbsp;
                    <cc1:Button runat="server" ID="BtnClose" Enabled="false" Text="关闭模型" />&nbsp;
                    <cc1:Button ID="Button6" ButtonImgUrl="../images/icon/add.gif" ButtontypeMode="WithImage"
                        runat="server" AutoPostBack="false" OnClientClick="location.href='global_contypeadd.aspx'"
                        Text=" 内容模型添加" />
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        $("td[tid]").each(function () { if (parseInt($(this).attr("tid")) <= 6) { $(this).find("a:last").html("系统").attr("onclick", "return false;"); }; });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'BtnOpen', 'BtnClose')
        };
    </script>
</body>
</html>
