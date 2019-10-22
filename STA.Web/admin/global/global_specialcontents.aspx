<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_specialcontents.aspx.cs"Inherits="STA.Web.Admin.specialcontents" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>专题内容编辑</title>
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
            <div class="conb-2">
                <div class="bar">
                    &nbsp;&nbsp;专题：<span class="red"><%=cinfo.Title%></span></div>
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
                                        内容组
                                    </th>
                                    <th>
                                        所属频道
                                    </th>
                                    <th>
                                        发布时间
                                    </th>
                                    <th>
                                        发布用户
                                    </th>
                                    <th width="60">
                                        <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" /> 选择
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["typename"]%>
                                </td>
                                <td title="<%#((DataRowView)Container.DataItem)["title"]%>" class="ptip" onclick="location.href='<%#GetConAddPage(TypeParse.StrToInt(((DataRowView)Container.DataItem)["typeid"],1))%>&action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>&url=<%=Utils.UrlEncode(currentpage)%>'">
                                    <%#((DataRowView)Container.DataItem)["title"]%>
                                </td>
                                <td gid="<%#((DataRowView)Container.DataItem)["groupid"]%>"></td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["channelname"]%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                </td>
                                <td>
                                    <%#((DataRowView)Container.DataItem)["addusername"]%>
                                </td>
                                <td>
                                    <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn','MoveBtn')" name="cbid" value="<%#Eval("id")%>" />
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
                <cc1:DropDownList runat="server" ID="ddlGroups"></cc1:DropDownList>
                <cc1:Button ID="MoveBtn" runat="server" Text=" 移动到该组 " AutoPostBack="false" Enabled="false"/>
                <cc1:Button runat="server" Enabled="false" ID="DelBtn" AutoPostBack="false" OnClientClick="ControlPostBack('DelBtn','确认删除吗？');return;" Text="删除" />
                <cc1:Button ID="Edit" AutoPostBack="false" runat="server" Text=" 编辑专题 " />
                <cc1:Button ID="Button2" AutoPostBack="false" runat="server" OnClientClick="location.href='global_speciallist.aspx'" Text=" 返回专题管理 " />
                <cc1:Button ID="AddBtn" AutoPostBack="false" runat="server" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/add.gif" Text=" 添加专题内容 " />
            </div>
        </div>
        <div id="footer">
            <%=footer %>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        var groups = [];
        $.each($("#ddlGroups option"), function (i, o) { if (o.value != "0") { groups.push(o); } });
        function GetGroupName(gid){
            if(gid == "0") return "未分组";
            for(var i=0;i<groups.length;i++){
                if(groups[i].value == gid) return groups[i].text+"(ID:" + gid + ")";
            }
            return "未分组";
        };
        $("td[gid]").each(function(){
            $(this).html(GetGroupName($(this).attr("gid")));
        });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn', 'MoveBtn')
        }
        EditForList("MoveBtn", Ele("form1"));
        $("#AddBtn").click(function () { PopWindow("专题内容添加", "../tools/congroupconselect.aspx?type=spec&sid="+<%=STARequest.GetString("id")%>, "",620, 390,null,function(){location.href=location.href;})});
        RegColumnPostip(".ptip", 36, "..");
        $("#Edit").click(function () {location.href="global_specialadd.aspx?type=0&action=edit&id=<%=STARequest.GetString("id")%>";});
    </script>
</body>
</html>
