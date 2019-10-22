<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_templatelist.aspx.cs" Inherits="STA.Web.Admin.templatelist" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>模版管理</title>
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
                        &nbsp;&nbsp;模版列表</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            模版名
                                        </th>
                                        <th>
                                            路径
                                        </th>
                                        <th>
                                            作者
                                        </th>
                                        <th>
                                            发布时间
                                        </th>
                                        <th>
                                            版权
                                        </th>
                                        <th>
                                            版本
                                        </th>
                                        <th>
                                            状态
                                        </th>
                                        <th>
                                            启用
                                        </th>
                                        <th width="100">
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
                                        <span title="<img src='../../templates/<%#((DataRowView)Container.DataItem)["pathname"]%>/about.png' width='300'/>" class="timg"><%#((DataRowView)Container.DataItem)["name"]%></span>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["pathname"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["author"]%>
                                    </td>
                                    <td>
                                       <%#((DataRowView)Container.DataItem)["createtime"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["copyright"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["version"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["storage"]%>
                                    </td>
                                    <td>
                                        <a href="javascript:;" title="启用此模版" onclick="SubmitForm('usetpl', '<%#((DataRowView)Container.DataItem)["id"]%>');"><%#((DataRowView)Container.DataItem)["use"]%></a>
                                    </td>
                                    <td>
                                        <a href="javascript:;" onclick="SAlert('如果模版文件过多，生成可能需要点时间...', 100); SubmitForm('maketpl', '<%#((DataRowView)Container.DataItem)["id"]%>');">生成</a>
                                        <a href="global_tplfiles.aspx?path=<%#((DataRowView)Container.DataItem)["pathname"]%>">管理</a>
                                        <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('deltpl', '<%#((DataRowView)Container.DataItem)["id"]%>'); }, '删除后不可恢复，确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 模板吗？');">删除</a>
                                    </td>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn','MakeBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                        <input type="hidden" name="name<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["pathname"]%>" />
                                        <input type="hidden" name="tname<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["name"]%>" />
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
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除" Enabled="false"/>
                    <cc1:Button ID="MakeBtn" runat="server" OnClientClick="ControlPostBack('MakeBtn', '如果模版文件过多，生成可能需要点时间，确认继续吗?');return;"  Text="生成" Enabled="false"/>
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        RegPostip($(".timg"));
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn','MakeBtn')
        }
    </script>
</body>
</html>
