<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_comments.aspx.cs" Inherits="STA.Web.Admin.comments" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<%@ Import Namespace="STA.Entity" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>评论管理</title>
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
                    评论搜索</div>
                <div class="con">
                    <table class="top">
                        <tr>
                            <td>
                                页大小：<cc1:TextBox ID="txtPageSize" Text="20" Width="30" runat="server" />&nbsp;&nbsp;
                                状态：<cc1:DropDownList runat="server" ID="ddlStatus">
                                     <asp:ListItem Text="全部" Value="-1" Selected="True" />
                                     <asp:ListItem Text="待审" Value="0" />
                                     <asp:ListItem Text="通过" Value="1" />
                                     <asp:ListItem Text="屏蔽" Value="2"/>
                                </cc1:DropDownList>&nbsp;&nbsp;
                                 评论时间：<cc1:TextBox ID="txtStartDate" RequiredFieldType="日期" Width="70" runat="server" /> - <cc1:TextBox ID="txtEndDate" RequiredFieldType="日期" Width="70" runat="server" />&nbsp;&nbsp;
                                文档：<cc1:TextBox ID="txtContitle" HelpText="查看单个文档所有评论,请输入文档的全名" Width="110" runat="server" />&nbsp;&nbsp
                                 用户名：<cc1:TextBox ID="txtUsername" Width="100" runat="server" HelpText="如果多个用户中间请用半角逗号隔开"/>&nbsp;&nbsp
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <!--类型：<cc1:DropDownList runat="server" ID="ddlCtype">
                                            <asp:ListItem Value="0" Selected="True">全部</asp:ListItem>
                                            <asp:ListItem Value="1">文档</asp:ListItem>
                                        </cc1:DropDownList>-->
                                评论IP：<cc1:TextBox ID="txtIp" Width="120" runat="server" />&nbsp;&nbsp
                                关键字：<cc1:TextBox ID="txtKeywords" Width="250" runat="server" />&nbsp;
                                <cc1:Button ID="btnSearch" runat="server" Text="搜索评论" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;评论管理</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" />
                                        </th>
                                        <th>
                                            评论标题
                                        </th>
                                        <th>
                                            详细评论
                                        </th>
                                        <th>
                                            评论时间
                                        </th>
                                        <th>
                                            评论文档
                                        </th>
                                        <th>
                                            用户名
                                        </th>
                                        <th>
                                            顶/踩
                                        </th>
                                        <th>
                                            星级
                                        </th>
                                        <th>
                                            状态
                                        </th>
                                        <th>
                                            来自
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid', 'DelBtn', 'VerifyOk','VerifyNo','ViewInfo')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                    </td>
                                    <td>
                                        <span class="ctip" onclick="ShowInfo(<%#((DataRowView)Container.DataItem)["id"]%>)"><%#((DataRowView)Container.DataItem)["title"]%></span>
                                    </td>
                                    <td>
                                        <span class="cmsg" onclick="ShowInfo(<%#((DataRowView)Container.DataItem)["id"]%>)"><%#Utils.RemoveHtml((((DataRowView)Container.DataItem)["msg"]).ToString())%></span>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm}")%>
                                    </td>
                                    <td>
                                          <a class="ptip" href="<%#(((DataRowView)Container.DataItem)["ctype"]).ToString()=="1"?("../../comment.aspx?id="+(((DataRowView)Container.DataItem)["cid"]).ToString()):config.Weburl%>" target="_blank"><%#((DataRowView)Container.DataItem)["contitle"]%></a>
                                    </td>
                                    <td>
                                        <span class="uname" title="<%#((DataRowView)Container.DataItem)["username"]%>" uid="<%#((DataRowView)Container.DataItem)["uid"]%>"><%#((DataRowView)Container.DataItem)["username"]%></span>
                                    </td>
                                    <td>
                                         <%#((DataRowView)Container.DataItem)["diggcount"]%>/<%#((DataRowView)Container.DataItem)["stampcount"]%>
                                    </td>
                                    <td>
                                         <%#((DataRowView)Container.DataItem)["star"]%>星
                                    </td>
                                    <td>
                                         <span class="cstatus"><%#((CommentStatus)TypeParse.StrToInt(((DataRowView)Container.DataItem)["status"])).ToString()%></span>
                                    </td>
                                    <td>
                                        <input type="hidden" id="ctype<%#((DataRowView)Container.DataItem)["id"]%>" name="ctype<%#((DataRowView)Container.DataItem)["id"]%>" value="<%#((DataRowView)Container.DataItem)["ctype"]%>" />
                                         <span title="<%#(((DataRowView)Container.DataItem)["userip"]).ToString().Trim()%> > <%#((DataRowView)Container.DataItem)["useragent"]%>"><%#((DataRowView)Container.DataItem)["city"]%></span>
                                         <div id="cinfo<%#((DataRowView)Container.DataItem)["id"]%>" style="display:none;">
	                                        <table style="width:480px;overflow:hidden;">
		                                        <tr>
			                                        <td class="itemtitle5">评论标题:</td>
			                                        <td>
				                                        <%#((DataRowView)Container.DataItem)["title"]%>
			                                        </td>
		                                        </tr>
		                                        <tr>
			                                        <td class="itemtitle5">评论时间:</td>
			                                        <td>
				                                        <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
			                                        </td>
		                                        </tr>
		                                        <tr>
			                                        <td class="itemtitle5">评论文档:</td>
			                                        <td>
				                                        <%#((DataRowView)Container.DataItem)["contitle"]%>
			                                        </td>
		                                        </tr>
		                                        <tr>
			                                        <td class="itemtitle5">评论用户:</td>
			                                        <td>
				                                        <span class="uname" title="<%#((DataRowView)Container.DataItem)["username"]%>" uid="<%#((DataRowView)Container.DataItem)["uid"]%>"><%#((DataRowView)Container.DataItem)["username"]%></span>
			                                        </td>
		                                        </tr>
		                                        <tr>
			                                        <td class="itemtitle5">他/她来自:</td>
			                                        <td>
				                                         <%#((DataRowView)Container.DataItem)["city"]%>(<%#((DataRowView)Container.DataItem)["userip"]%>)
			                                        </td>
		                                        </tr>
		                                        <tr>
			                                        <td class="itemtitle5">审核状态:</td>
			                                        <td>
				                                         <span class="cstatus"><%#((CommentStatus)TypeParse.StrToInt(((DataRowView)Container.DataItem)["status"])).ToString()%></span>
			                                        </td>
		                                        </tr>
		                                        <tr>
			                                        <td class="itemtitle5">顶/踩:</td>
			                                        <td>
				                                       <%#((DataRowView)Container.DataItem)["diggcount"]%>/<%#((DataRowView)Container.DataItem)["stampcount"]%>
			                                        </td>
		                                        </tr>
		                                        <tr>
			                                        <td class="itemtitle5">星级:</td>
			                                        <td>
				                                        <%#((DataRowView)Container.DataItem)["star"]%>星
			                                        </td>
		                                        </tr>
		                                        <tr>
			                                        <td class="itemtitle5">评论内容:</td>
			                                        <td>
				                                        <%#((DataRowView)Container.DataItem)["msg"]%>
			                                        </td>
		                                        </tr>
		                                        <tr>
			                                        <td class="itemtitle5">Useragent:</td>
			                                        <td>
				                                        <%#((DataRowView)Container.DataItem)["useragent"]%>
			                                        </td>
		                                        </tr>
                                            </table>
                                         </div>
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
                    <cc1:Button ID="VerifyOk" runat="server" Text="审核通过" Enabled="false"/>
                    <cc1:Button ID="VerifyNo" runat="server" Text="屏蔽" Enabled="false"/>
                    <cc1:Button ID="ViewInfo" runat="server" Text="查看详细" Enabled="false" AutoPostBack="false"/>
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '确认删除吗?');return;" Text="删除" Enabled="false"/>
                </div>
            </div>

            <div class="cominfo jqmWindow" style="display:none;width:500px;margin:auto auto auto -250px;height:220px;overflow-y:auto;border-width:5px;padding:10px 10px 0 15px;"> </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        $("#txtStartDate,#txtEndDate").click(function () { WdatePicker({ isShowWeek: true }) });
        $(".cominfo").jqm({ overlay: config.overlay <= 0 ? 1 : config.overlay, modal: false, overlayClass: config.overlayClass });

        EditForList("VerifyOk", Ele("form1"));
        EditForList("VerifyNo", Ele("form1"));
        EditForList("ViewInfo", Ele("form1"), "", true, function (id) { ShowInfo(id); });

        function ShowInfo(id) {
            $(".cominfo").html($("#cinfo" + id).html());
            $(".cominfo").jqmShow();
        };

        $(".uname").each(function () {
            var uid = $.trim($(this).attr("uid")), name = $(this).html();
            if (parseInt(uid) > 0) {
                $(this).html("<a href='../../userinfo.aspx?id=" + uid + "' target='_blank'>" + name + "</a>");
            } else if(name == ""){
                $(this).html("佚名"); 
            }
        });

        RegColumnPostip(".ptip", 24, "..");
        RegColumnPostip(".ctip", 24, "..");
        RegColumnPostip(".cmsg", 24, "..");

        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn', 'VerifyOk', 'VerifyNo', 'ViewInfo')
        };
    </script>
</body>
</html>
