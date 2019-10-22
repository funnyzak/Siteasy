<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_contentmine.aspx.cs"Inherits="STA.Web.Admin.contentmine" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>我发布的文档</title>
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
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">
                    文档搜索</div>
                <div class="con">
                    <table class="top">
                        <tr>
                            <td>
                                页大小：<cc1:TextBox ID="txtPageSize" Text="20" Width="30" runat="server" />&nbsp;&nbsp;
                                频道：<cc1:DropDownTreeList runat="server" ID="ddlConType" Width="140"/>&nbsp;&nbsp; 状态：<cc1:DropDownList runat="server" ID="ddlStatus">
                                     <asp:ListItem Text="全部" Value="-1" Selected="True" />
                                     <asp:ListItem Text="草稿" Value="0" />
                                     <asp:ListItem Text="待审核" Value="1" />
                                     <asp:ListItem Text="通过" Value="2"/>
                                </cc1:DropDownList>&nbsp;&nbsp;
                                属性：<cc1:DropDownList runat="server" ID="ddlProperty">
                                     <asp:ListItem Text="全部" Value="" Selected="True"/>
                                     <asp:ListItem Text="头条" Value="h"/>
                                     <asp:ListItem Text="推荐" Value="r"/>
                                     <asp:ListItem Text="幻灯" Value="f" />
                                     <asp:ListItem Text="特荐" Value="a" />
                                     <asp:ListItem Text="滚动" Value="s" />
                                     <asp:ListItem Text="加粗" Value="b" />
                                     <asp:ListItem Text="斜体" Value="i" />
                                     <asp:ListItem Text="图片" Value="p" />
                                     <asp:ListItem Text="跳转" Value="j" />
                                </cc1:DropDownList>&nbsp;&nbsp;
                                 发布时间：<cc1:TextBox ID="txtStartDate" RequiredFieldType="日期" Width="70" runat="server" /> - <cc1:TextBox ID="txtEndDate" RequiredFieldType="日期" Width="70" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                关键字：<cc1:TextBox ID="txtKeywords" Width="300" runat="server" />&nbsp;
                                <cc1:Button ID="btnSearch" runat="server" Text="搜索内容" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="conb-2">
                <div class="bar">
                    &nbsp;&nbsp;我发布的文档</div>
                <div class="con">
                    <asp:Repeater ID="rptData" runat="server">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th>
                                        类型
                                    </th>
                                    <th>
                                        标题
                                    </th>
                                    <th>
                                        所属频道
                                    </th>
                                    <th>
                                        发布时间
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
                            <tr tid="<%#((DataRowView)Container.DataItem)["id"]%>">
                                <td>
                                    <%#((DataRowView)Container.DataItem)["typename"]%>
                                </td>
                                <td title="<%#((DataRowView)Container.DataItem)["title"]%>" class="ptip" onclick="window.open('../tools/view.aspx?id=<%#((DataRowView)Container.DataItem)["id"]%>&tid=<%#((DataRowView)Container.DataItem)["typeid"]%>&name=content','_blank')">
                                    <%#((DataRowView)Container.DataItem)["title"]%>
                                </td>
                                <td class="chlname" cid="<%#((DataRowView)Container.DataItem)["channelid"]%>">
                                    <%#((DataRowView)Container.DataItem)["channelname"]%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                </td>
                                <td>
                                    <a href="<%#GetConAddPage(TypeParse.StrToInt(((DataRowView)Container.DataItem)["typeid"],1))%>&action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>&url=<%=Utils.UrlEncode(currentpage)%>">编辑</a>
                                    <span id="fe<%#((DataRowView)Container.DataItem)["id"]%>" onclick="FastEditTr('../tools/fastcontentedit.aspx?type=<%#((DataRowView)Container.DataItem)["typeid"]%>&id=', <%#((DataRowView)Container.DataItem)["id"]%>, 6, '100%', '170');">快速编辑</span>
                                    <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delcon', <%#((DataRowView)Container.DataItem)["id"]%>); }, '确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["title"])%></b> 吗？');">删除</a>
                                </td>
                                <td>
                                    <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn','MakeHtmlBtn')" name="cbid" value="<%#Eval("id")%>" />
                                    <input type="hidden" name="name<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["title"]%>" />
                                    <input type="hidden" name="type<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["typeid"]%>" />
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
                <cc1:Button ID="MakeHtmlBtn" runat="server" Enabled="false" Text="生成静态" />
                <cc1:Button runat="server" Enabled="false" ID="DelBtn" AutoPostBack="false" OnClientClick="ControlPostBack('DelBtn','确认删除吗？');return;" Text="删除" />
                <cc1:Button ID="AddContentBtn" AutoPostBack="false" runat="server" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/add.gif" Text=" 内容添加" />
            </div>
        </div>
        <div id="footer">
            <%=footer %>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#AddContentBtn").click(function () { location.href = "global_contentadd.aspx?type=<%=STARequest.GetString("type")%>&url=<%=Utils.UrlEncode(currentpage)%>"; });
        $("#txtStartDate,#txtEndDate").click(function () { WdatePicker({ isShowWeek: true }) });
        EditForList("MakeHtmlBtn", Ele("form1"));
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn', 'MakeHtmlBtn')
        }
        RegColumnPostip(".ptip", 36, "..");
        $(".chlname").each(function(){ var cid = $(this).attr("cid");if(cid=="0"){$(this).html("未分类")} });
    </script>
</body>
</html>
