<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_congroupchannels.aspx.cs" Inherits="STA.Web.Admin.congroupchannels" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>频道组管理</title>
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
                        &nbsp;&nbsp;频道组列表</div>
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
                                            添加时间
                                        </th>
                                        <th>
                                            描述
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
                                    <td id="name<%#((DataRowView)Container.DataItem)["id"]%>">
                                        <%#((DataRowView)Container.DataItem)["name"]%>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                <td title="<%#((DataRowView)Container.DataItem)["desctext"]%>">
                                    <%#Utils.GetUnicodeSubString((((DataRowView)Container.DataItem)["desctext"]).ToString(),40,"..")%>
                                </td>
                                    <td>
                                        <span onclick="PopWindow('频道组修改:<%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%>', 'global_congroupadd.aspx?type=1&action=edit&id=<%#((DataRowView)Container.DataItem)["id"]%>', '', 380, 160);">编辑</span>
                                        <a href="javascript:void(0);" onclick="ConEdit(<%#((DataRowView)Container.DataItem)["id"]%>,'<%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%>');">组维护</a>
                                        <a href="javascript:void(0)" onclick="SConfirm(function () { SubmitForm('delcgroup', <%#((DataRowView)Container.DataItem)["id"]%>); }, '删除后不可恢复，确认删除 <b><%#ReplaceJsComma(((DataRowView)Container.DataItem)["name"])%></b> 吗？');">删除</a>
                                    </td>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
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
                    <cc1:Button ID="DelBtn" runat="server" OnClientClick="ControlPostBack('DelBtn', '删除后不可恢复，确认删除吗?');return;" Text="删除" Enabled="false"/>
                    <cc1:Button ID="AddBtn" OnClientClick="PopWindow('频道组添加','global_congroupadd.aspx?type=1', '', 380, 160);" runat="server" AutoPostBack="false" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/add.gif" Text=" 组添加" />
                </div>
            </div>
            <div id="footer">
                <%=footer %> 
                <cc1:DropDownTreeList runat="server" ID="ddlConType" CssClass="hide"/>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        var channels = []
        $.each($("#ddlConType option"), function (i, o) { if (o.value != "0") { channels.push(o); } });
        $("#ddlConType").css("display","none");
        function ConEdit(id,name) {
            Ajax("chnelgroupids&gid=" + id, function (data) {
                var list = ToJson(data), selected = ",";
                $.each(list, function (i, o) { selected += o.cid + ","; });
                var html = "<div class='inner-pad-3' id=\"clists\" style=\"height:300px\;overflow-y:auto;\">";
                for (var i = 0; i < channels.length; i++) {
                    var checked = "", icheck = "", text = "";
                    if (selected != "" && selected.indexOf(","+channels[i].value+",") >= 0)
                        checked = "checked='checked'";
                    text = channels[i].text;
                    icheck = "<input type='checkbox' " + checked + " value='" + channels[i].value + "'/>"
                    html += "<span title='" + channels[i].text + "' style='margin:0 5px 0 0;'>" + icheck + text + "</span><br/>";
                }
                html += "</div>";
                PopWindow("频道组维护:"+GetCutString(name,30,".."), html, "common", 400, 0);
                $("#clists").find(":checkbox").each(function () {
                    $(this).click(function () {
                        if ($(this).attr("checked"))
                            Ajax("addcongroupcon&cid=" + $(this).val() + "&gid=" + id);
                        else
                            Ajax("delcongroupcon&cid=" + $(this).val()+"&gid="+id);
                    });
                });
            });
        }
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn')
        }
    </script>
</body>
</html>
