<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_votelist.aspx.cs" Inherits="STA.Web.Admin.votelist" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>投票管理</title>
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
    <div id="wrapper">
        <form id="form1" runat="server">
        <div id="mwrapper">
            <div id="main">
            <div class="conb-1">
                <div class="bar">
                    投票搜索</div>
                <div class="con">
                    <table class="top">
                        <tr>
                            <td>
                                页大小：<cc1:TextBox ID="txtPageSize" Text="20" Width="30" runat="server" />&nbsp;&nbsp;
                                投票分类：<cc1:DropDownList runat="server" ID="ddlVotecates"/>&nbsp;&nbsp;
                                 发布时间：<cc1:TextBox ID="txtStartDate" RequiredFieldType="日期" Width="70" runat="server" /> - <cc1:TextBox ID="txtEndDate" RequiredFieldType="日期" Width="70" runat="server" />&nbsp;&nbsp;
                                 投票类型：<cc1:DropDownList runat="server" ID="ddlType">
                                    <asp:ListItem Selected="True">全部</asp:ListItem>
                                    <asp:ListItem Value="1">普通</asp:ListItem>
                                    <asp:ListItem Value="2">图片</asp:ListItem>
                                 </cc1:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                关键字：<cc1:TextBox ID="txtKeywords" Width="250" runat="server" />&nbsp;
                                <cc1:Button ID="btnSearch" runat="server" Text="搜索投票" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;投票管理</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            主题
                                        </th>
                                        <th>
                                            分类
                                        </th>
                                        <th>
                                            截至时间
                                        </th>
                                        <th>
                                            类型
                                        </th>
                                        <th>
                                            总票数
                                        </th>
                                        <th>
                                            标识
                                        </th>
                                        <th>
                                            权重
                                        </th>
                                        <th width="230">
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
                                        <a class="ctip" href='../../voteshow.aspx?vtype=ids&relval=<%#((DataRowView)Container.DataItem)["id"]%>' target="_blank"><%#((DataRowView)Container.DataItem)["name"]%></a><br />
                                        发布时间：<%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <span t='cname' cid="<%#((DataRowView)Container.DataItem)["cateid"]%>"><%#((DataRowView)Container.DataItem)["catename"]%></span><br />
                                        ID:<%#((DataRowView)Container.DataItem)["id"]%>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "endtime", "{0:yyyy-MM-dd HH:mm:ss}")%><br />
                                        可选：<%#((DataRowView)Container.DataItem)["maxvote"]%> 项
                                    </td>
                                    <td>
                                         <span t='type' tid="<%#((DataRowView)Container.DataItem)["type"]%>"></span>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["votecount"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["likeid"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["orderid"]%>
                                    </td>
                                    <td>
                                        <a title="编辑" href="global_voteadd.aspx?action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>">编辑</a>
                                        <a href="global_voteitems.aspx?id=<%#((DataRowView)Container.DataItem)["id"]%>" title="编辑投票选项">编辑选项</a>
                                        <a title="删除" href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delvote', <%#((DataRowView)Container.DataItem)["id"]%>); }, '确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 吗？');">删除</a>
                                        <a onclick="ShowCode(<%#((DataRowView)Container.DataItem)["id"]%>);">获取引用文件</a>
                                        <a title="预览" href="../../sta/vote.aspx?vtype=ids&relval=<%#((DataRowView)Container.DataItem)["id"]%>&display=html" target="_blank">预览</a>
                                        <%--<a href=""><img src="../images/icon/preview.gif" /></a>--%>
                                    </td>
                                    <td>
                                      <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn', 'EditBtn','EditOption','VoteRecord')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
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
                    <cc1:Button runat="server" Enabled="false" ID="EditOption" AutoPostBack="false" Text="编辑选项" />
                    <cc1:Button runat="server" Enabled="false" ID="VoteRecord" AutoPostBack="false" Text="投票记录" />
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除" Enabled="false"/>
                    <cc1:Button ID="Button6"  runat="server" AutoPostBack="false" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/add.gif" OnClientClick="location.href='global_voteadd.aspx'" Text=" 发起投票 " />
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
        EditForList("EditBtn", Ele("form1"), "global_voteadd.aspx?action=edit&id=", true);
        EditForList("EditOption", Ele("form1"), "global_voteitems.aspx?id=", true);
        EditForList("VoteRecord", Ele("form1"), "global_voterecords.aspx?tid=", true);
        $("span[t='cname']").each(function () { var cid = $(this).attr("cid"); if (cid == "0") { $(this).html("未分类"); } });
        $("span[t='type']").each(function () { var tid = $(this).attr("tid"); if (tid == "1") { $(this).html("普通"); } else { $(this).html("图片"); } });
        RegColumnPostip(".ctip", 36, "..");
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn', 'EditBtn', 'EditOption', 'VoteRecord')
        };
        function ShowCode(code) {
            code = "<div style='padding:0 20px 0 13px;'><textarea name=\"jscode\" class=\"txt\" style=\"overflow:hidden;height:70px;width:100%;\">" + "<script type=\"text/javascript\" src=\"<%=config.Weburl%><%=sitepath%>/sta/vote.aspx?vtype=ids&relval=" + code + "\"><\/script>" + "</textarea></div>";
            PopWindow("CTRL+C 复制以下投票代码", code, "common", 400, 0);
            $("textarea[name='jscode']").select();
        };
    </script>
</body>
</html>
