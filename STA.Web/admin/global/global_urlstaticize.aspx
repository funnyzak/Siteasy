<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_urlstaticize.aspx.cs" Inherits="STA.Web.Admin.urlstaticize" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>URL静态管理</title>
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
                        &nbsp;&nbsp;URL静态列表</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            ID
                                        </th>
                                        <th>
                                            名称
                                        </th>
                                        <th>
                                            编码
                                        </th>
                                        <th>
                                            上次生成时间
                                        </th>
                                        <th>
                                            后缀
                                        </th>
                                        <th width="130">
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
                                        <%#((DataRowView)Container.DataItem)["id"]%>
                                    </td>
                                    <td>
                                        <a href="<%#(((DataRowView)Container.DataItem)["url"]).ToString().Replace("{weburl}",sitepath)%>" target="_blank"><%#((DataRowView)Container.DataItem)["title"]%></a>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["charset"]%>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "maketime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                         <%#((DataRowView)Container.DataItem)["suffix"]%>
                                    </td>
                                    <td>
                                        <a href="global_urlstaticizeadd.aspx?action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>">编辑</a>
                                        <a href="javascript:void(0)" onclick="SubmitForm('make', <%#((DataRowView)Container.DataItem)["id"]%>);">生成</a>
                                        <a href="<%=sitepath%><%#((DataRowView)Container.DataItem)["savepath"]%>/<%#((DataRowView)Container.DataItem)["filename"]%>.<%#((DataRowView)Container.DataItem)["suffix"]%>" target="_blank">查看</a>
                                        <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('del', <%#((DataRowView)Container.DataItem)["id"]%>); }, '删除后不可恢复，确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["title"])%></b> 吗？');">删除</a>
                                    </td>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn','MakeHtmlBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                        <input type="hidden" name="name<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["title"]%>" />
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
                    <cc1:Button ID="MakeHtmlBtn" runat="server" Enabled="false" AutoPostBack="false" Text="生成静态" />
                    <cc1:Button ID="Button6"  runat="server" AutoPostBack="false" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/add.gif" OnClientClick="location.href='global_urlstaticizeadd.aspx'" Text=" URL静态添加" />
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        EditForList("MakeHtmlBtn", Ele("form1"));
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn', 'MakeHtmlBtn')
        }
    </script>
</body>
</html>
