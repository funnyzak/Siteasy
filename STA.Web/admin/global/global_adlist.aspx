<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_adlist.aspx.cs" Inherits="STA.Web.Admin.adlist" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<%@ Import Namespace="STA.Entity" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>广告管理</title>
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
                        广告搜索</div>
                    <div class="con">
                        <table class="top">
                            <tr>
                                <td>
                                    添加时间：<cc1:TextBox ID="txtStartDate" Width="70" runat="server" /> - <cc1:TextBox ID="txtEndDate" Width="70" runat="server" /> &nbsp; 
                                    类型：<cc1:DropDownList runat="server" ID="ddlAdtype" />&nbsp;&nbsp; 
                                    关键字：<cc1:TextBox ID="txtKeywords" Width="220" runat="server" />&nbsp;
                                    <cc1:Button ID="btnSearch" runat="server" Text="搜索广告" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;广告列表</div>
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
                                            状态
                                        </th>
                                        <th>
                                            类型
                                        </th>
                                        <th>
                                            开始时间
                                        </th>
                                        <th>
                                            结束时间
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
                                        <%#((DataRowView)Container.DataItem)["id"]%>
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["name"]%>
                                    </td>
                                    <td>
                                        <%#((AdStatus)TypeParse.StrToInt(((DataRowView)Container.DataItem)["status"])).ToString()%>
                                    </td>
                                    <td>
                                        <%#((AdType)TypeParse.StrToInt(((DataRowView)Container.DataItem)["adtype"])).ToString()%>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "startdate", "{0:yyyy-MM-dd}")%>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "enddate", "{0:yyyy-MM-dd}")%>
                                    </td>
                                    <td>
                                        <a href="global_adadd.aspx?action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>">编辑</a>
                                        <a href="javascript:void(0);" onclick="ShowCode('<%=sitepath%><%#(((DataRowView)Container.DataItem)["filename"]).ToString().Replace("/","\\/")%>');" name="">调用</a>
                                        <a href="javascript:void(0);" onclick="RunCode('<%#(((DataRowView)Container.DataItem)["filename"]).ToString().Replace("/","\\/")%>');" name="">预览</a>
                                        <a href="javascript:;" onclick="SubmitForm('copyad', <%#((DataRowView)Container.DataItem)["id"]%>);">复制</a>
                                        <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delad', <%#((DataRowView)Container.DataItem)["id"]%>); }, '删除后不可恢复，确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 吗？');">删除</a>
                                    </td>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn','CopyBtn','WriteBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                        <input type="hidden" name="name<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["name"]%>" />
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
                    <cc1:Button ID="CopyBtn" runat="server" Text="复制" Enabled="false"/>
                    <cc1:Button ID="WriteBtn" runat="server" Text="生成文件" Enabled="false"/>
                    <cc1:Button ID="Button6"  runat="server" AutoPostBack="false" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/add.gif" OnClientClick="location.href='global_adadd.aspx'" Text=" 广告添加" />
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        EditForList("CopyBtn", Ele("form1"));
        EditForList("WriteBtn", Ele("form1"));
        $("#txtStartDate,#txtEndDate").click(function () { WdatePicker({ isShowWeek: true }) });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn','CopyBtn','WriteBtn')
        }
        function ShowCode(code) {
            code = "<div style='padding:0 20px 0 13px;'><textarea name=\"jscode\" class=\"txt\" style=\"overflow:hidden;height:70px;width:100%;\">" + "<script type=\"text/javascript\" src=\"" + code + "\"><\/script>" + "</textarea></div>";
            PopWindow("CTRL+C 复制以下广告代码", code, "common", 400, 0);
            $("textarea[name='jscode']").select();
        };

        function RunCode(code) {
            window.open("../tools/runad.aspx?file="+ encodeURI(code), "_blank");
        }
    </script>
</body>
</html>
