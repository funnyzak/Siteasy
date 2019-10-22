<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="votelist.aspx.cs" Inherits="STA.Web.Admin.Plus.votelist" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>简易投票管理</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../../plugin/scripts/datepicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../../js/public.js"></script>
</head>
<body>
    <div id="wrapper">
        <form id="form1" runat="server">
        <div id="mwrapper">
            <div id="main">
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;简易投票管理</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            投票标题
                                        </th>
                                        <th>
                                           开始日期
                                        </th>
                                        <th>
                                            结束日期
                                        </th>
                                        <th>
                                            总票数
                                        </th>
                                        <th>
                                            状态
                                        </th>
                                        <th width="180">
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
                                        <span class="ctip" onclick="window.open('../../../plus/vote.aspx?id=<%#((DataRowView)Container.DataItem)["id"]%>','_blank')"><%#((DataRowView)Container.DataItem)["title"]%></span>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "startdate", "{0:yyyy-MM-dd}")%>
                                    </td>
                                    <td>
                                         <%#DataBinder.Eval(Container.DataItem, "enddate", "{0:yyyy-MM-dd}")%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["totalcount"]%>
                                    </td>
                                    <td>
                                        <%#(((DataRowView)Container.DataItem)["isenable"]).ToString()=="0"?"禁用":"启用"%>
                                    </td>
                                    <td>
                                        <a title="编辑" href="voteadd.aspx?action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>">编辑</a>
                                        <a title="删除" href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delvote', <%#((DataRowView)Container.DataItem)["id"]%>); }, '确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["title"])%></b> 吗？');">删除</a>
                                        <a onclick="ShowCode(<%#((DataRowView)Container.DataItem)["id"]%>);">获取引用文件</a>
                                        <a href="voteview.aspx?id=<%#((DataRowView)Container.DataItem)["id"].ToString()%>" target="_blank">预览</a>
                                    </td>
                                    <td>
                                      <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn', 'EditBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
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
                    <cc1:Button runat="server" Enabled="false" ID="EditBtn" AutoPostBack="false" Text="编辑" />
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除" Enabled="false"/>
                    <cc1:Button ID="Button6"  runat="server" AutoPostBack="false" ButtontypeMode="WithImage" ButtonImgUrl="../../images/icon/add.gif" OnClientClick="location.href='voteadd.aspx'" Text=" 发起投票 " />
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        EditForList("EditBtn", Ele("form1"), "voteadd.aspx?action=edit&id=", true);
        RegColumnPostip(".ctip", 52, "..");
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn', 'EditBtn')
        };
        function ShowCode(code) {
            code = "<div style='padding:0 20px 0 13px;'><textarea name=\"jscode\" class=\"txt\" style=\"overflow:hidden;height:70px;width:100%;\">" + "<script type=\"text/javascript\" src=\"<%=config.Weburl%><%=sitepath%>/sta/plus/vote.aspx?id=" + code + "\"><\/script>" + "</textarea></div>";
            PopWindow("CTRL+C 复制以下投票代码", code, "common", 400, 0);
            $("textarea[name='jscode']").select();
        };
    </script>
</body>
</html>
