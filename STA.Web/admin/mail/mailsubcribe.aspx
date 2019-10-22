<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mailsubcribe.aspx.cs" Inherits="STA.Web.Admin.mailsubcribe" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<%@ Import Namespace="STA.Entity" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>邮件订阅用户</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/datepicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
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
                        邮件订阅搜索</div>
                    <div class="con">
                        <table class="top">
                            <tr>
                                <td>
                                    订阅状态：
                                    <cc1:DropDownList runat="server" ID="ddlStatus" Width="70">
                                        <asp:ListItem Value="-1">全部</asp:ListItem>
                                        <asp:ListItem Value="1">正常</asp:ListItem>
                                        <asp:ListItem Value="0">取消</asp:ListItem>
                                    </cc1:DropDownList>
                                    &nbsp;&nbsp; 
                                    订阅时间：<cc1:TextBox ID="txtStartDate" Width="70" runat="server" /> - <cc1:TextBox ID="txtEndDate" Width="70" runat="server" /> &nbsp; 
                                    订阅分组：<cc1:DropDownList runat="server" ID="ddlForgroup" Width="150"/>&nbsp;&nbsp;
                                     邮件地址：<cc1:TextBox ID="txtMail" Width="200" runat="server" />&nbsp;&nbsp; 
                                </td>
                            </tr>
                            <tr>
                                <td> 
                                    来路IP：<cc1:TextBox ID="txtIp" Width="120" runat="server" />&nbsp;&nbsp; 
                                    订阅人：<cc1:TextBox ID="txtName" Width="120" runat="server" />&nbsp;
                                    <cc1:Button ID="btnSearch" runat="server" Text="搜索订阅" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;订阅列表</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                           联系人
                                        </th>
                                        <th>
                                            订阅时间
                                        </th>
                                        <th>
                                            邮件地址
                                        </th>
                                        <th>
                                            来路IP
                                        </th>
                                        <th>
                                            状态
                                        </th>
                                        <th>
                                            分组名
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
                                        <%#((DataRowView)Container.DataItem)["name"]%>
                                    </td>
                                    <td>
                                         <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["mail"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["ip"]%>
                                    </td>
                                    <td>
                                        <%#(((DataRowView)Container.DataItem)["status"]).ToString()=="1"?"正常":"取消"%>
                                    </td>
                                    <td>
                                      <%#(((DataRowView)Container.DataItem)["forgroup"]).ToString().Trim() == "" ? "无" : (((DataRowView)Container.DataItem)["forgroup"]).ToString()%>
                                    </td>
                                    <td>
                                        <a href="mailsubadd.aspx?action=edit&mail=<%#((DataRowView)Container.DataItem)["mail"]%>">编辑</a>
                                        <a href="../../unsubscribe.aspx?m=<%#Utils.UrlEncode((((DataRowView)Container.DataItem)["mail"]).ToString())%>&s=<%#Utils.UrlEncode((((DataRowView)Container.DataItem)["safecode"]).ToString())%>" target="_blank">取消订阅</a>
                                        <a href="../global/global_emailsend.aspx?email=<%#((DataRowView)Container.DataItem)["mail"]%>" title="单独发送邮件">发邮件</a>
                                        <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delsubmail', <%#((DataRowView)Container.DataItem)["id"]%>); }, '删除后不可恢复，确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 吗？');">删除</a>
                                    </td>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                        <input type="hidden" name="name<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["name"]%>" />
                                        <input type="hidden" name="mail<%# ((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["mail"]%>" />
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
                    <cc1:Button ID="OutBtn" runat="server" Text="导出订阅地址"/>
                    <cc1:Button ID="Button6"  runat="server" AutoPostBack="false" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/add.gif" OnClientClick="location.href='mailsubadd.aspx'" Text=" 订阅添加 " />
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        $("#txtStartDate,#txtEndDate").click(function () { WdatePicker({ isShowWeek: true }) });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn')
        }
       
    </script>
</body>
</html>
