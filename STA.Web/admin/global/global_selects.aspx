<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_selects.aspx.cs" Inherits="STA.Web.Admin.selects" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>联动类型管理</title>
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
                        &nbsp;&nbsp;联动类型管理</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            联动名称
                                        </th>
                                        <th>
                                            系统
                                        </th>
                                        <th>
                                            联动标识
                                        </th>
                                        <th width="150">
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
                                        <a href="global_selectlist.aspx?ename=<%#((DataRowView)Container.DataItem)["ename"]%>"><%#((DataRowView)Container.DataItem)["name"]%></a>
                                    </td>
                                    <td>
                                        <%#(((DataRowView)Container.DataItem)["system"]).ToString()=="1"?"是":"否"%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["ename"]%>
                                    </td>
                                    <td>
                                        <a href="javascript:void(0)" onclick="SubmitForm('makefile', <%#((DataRowView)Container.DataItem)["id"]%>);">更新缓存</a>
                                        <a href="global_selectlist.aspx?ename=<%#(((DataRowView)Container.DataItem)["ename"]).ToString().Trim()%>&name=<%#((DataRowView)Container.DataItem)["name"]%>">查看联动项</a>
                                        <span onclick="PopWindow('修改联动类型 ','../tools/createselect.aspx?action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>', '', 320, 100);">编辑</span>
<%--                                        <a system="<%#((DataRowView)Container.DataItem)["system"]%>" title="删除" href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delselect', <%#((DataRowView)Container.DataItem)["id"]%>); }, '删除后不可恢复，确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 吗？');">删除</a>--%>
                                    </td>
                                    <td>
                                        <input type="checkbox" system="<%#((DataRowView)Container.DataItem)["system"]%>" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn','MakeFileBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                        <input type="hidden" name="name<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["name"]%>" />
                                        <input type="hidden" name="ename<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["ename"]%>" />
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
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除" Enabled="false"/>
                    <cc1:Button ID="MakeFileBtn" runat="server" Enabled="false" AutoPostBack="false" Text="更新缓存" />
                    <cc1:Button ID="Button6"  runat="server" AutoPostBack="false" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/add.gif" OnClientClick="PopWindow('联动类型添加', '../tools/createselect.aspx', '', 320, 100);" Text=" 联动类型添加" />
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        $("input[system]").each(function () {
            if ($(this).attr("system") == "1") {
                $(this).attr("disabled","true"); ;
            }
        });
        EditForList("MakeFileBtn", Ele("form1"));
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn', 'MakeFileBtn')
        }
    </script>
</body>
</html>
