<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_pluginlist.aspx.cs" Inherits="STA.Web.Admin.pluginlist" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>扩展管理</title>
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
                        &nbsp;&nbsp;扩展列表</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th width="30%">
                                            <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" /> 扩展名
                                        </th>
                                        <th width="15%">
                                            发布时间
                                        </th>
                                        <th width="15%">
                                            作者
                                        </th>
                                        <th width="15%">
                                            状态
                                        </th>
                                        <th width="25%">
                                            操作
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                        <input type="hidden" name="name<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["name"]%>" />
                                        <%#((DataRowView)Container.DataItem)["name"]%>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "pubtime", "{0:yyyy-MM-dd}")%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["author"]%>
                                    </td>
                                    <td>
                                        <%#(((DataRowView)Container.DataItem)["setup"]).ToString()=="1"?"已安装":"未安装"%>
                                    </td>
                                    <td>
                                        <a href="global_pluginadd.aspx?action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>">编辑</a>
                                        <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('setup', <%#((DataRowView)Container.DataItem)["id"]%>); }, '确认安装 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 吗？');">安装</a>
                                        <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('unsetup', <%#((DataRowView)Container.DataItem)["id"]%>); }, '确认卸载 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 吗？');">卸载</a>
                                        <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delplugin', <%#((DataRowView)Container.DataItem)["id"]%>); }, '<b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 的对应文件及对应数据库将全部删除, 且不可恢复, 确认删除吗？');">删除</a>
                                        <a href="../tools/plugindesc.aspx?id=<%#((DataRowView)Container.DataItem)["id"]%>" target="_blank">使用说明</a>
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
                    <cc1:Button ID="Setup" runat="server"  AutoPostBack="false" Text=" 上传扩展包 " Visible="false"/>
                    <cc1:Button ID="Button6"  runat="server" AutoPostBack="false" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/add.gif" OnClientClick="location.href='global_pluginadd.aspx'" Text=" 扩展创建向导" />
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn')
        }
    </script>
</body>
</html>
