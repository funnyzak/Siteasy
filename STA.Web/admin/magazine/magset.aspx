<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="magset.aspx.cs" Inherits="STA.Web.Admin.magazineset" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>杂志内容编辑</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link rel="stylesheet" href="../plugin/scripts/fancybox/fancybox.css" type="text/css" />
    <link rel="stylesheet" href="../plugin/scripts/jqueryui/jquery-ui.css" type="text/css" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../plugin/scripts/fancybox/mousewheel.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script type="text/javascript" src="../plugin/scripts/fancybox/fancybox.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
    <style type="text/css">
    #sortable {list-style-type: none; margin: 0; padding: 0; width: 100%; }
    #sortable li {margin: 3px 6px 3px 0; padding: 1px; float: left; width: 100px; height: 130px; font-size: 4em; text-align: center; position:relative;}
    #sortable li .remove{width:18px;height:18px;position:absolute;right:0;top:0;background:url(../images/icon/remove.png) no-repeat left top; cursor:pointer}
    #sortable li .fpage{width:70px;text-align:right;height:14px;position:absolute;right:3px;bottom:3px;font-size:12px;}
    </style>
    <script type="text/javascript">
        $(function () {
            $("#sortable").sortable({
                cursor: "move",
                opacity: 0.8,
                containment: ".contbox",
                stop: function (event, ui) { FormatPage() }
            });
            $("#sortable").disableSelection();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">杂志内容编辑</div>
                <div class="con">
                    <table>
                        <tr>
	                        <td class="itemtitle"><cc1:Help ID="Help2" runat="server" Text="杂志说明"/>：</td>
                            <td>
                            <div class="filetext" runat="server" id="filetext"/>
                            </td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help ID="Help1" runat="server" Text="内容编排" HelpText="对杂志内容进行排序.操作：1.单击可放大浏览 2.单击拖动可排序"/>：</td>
                            <td class="contbox">
<ul id="sortable">
<%int loop = 0; %>
    <%foreach (DataRow dr in list.Rows)
      {
          loop++;%>
        <li class="ui-state-default"><a href="<%=dr["url"].ToString()%>" rel="fbig" title="第<%=loop %>页"><img src="<%=dr["url"].ToString()%>" attid="<%=dr["attid"].ToString()%>" orderid="<%=dr["orderid"].ToString()%>" name="<%=dr["name"].ToString()%>" width="100" height="130" /></a><div class="remove" title="删除此页"></div><div class="fpage"></div></li>
    <%} %>
</ul>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <textarea runat="server" name="content" id="content" style="display:none;"></textarea>
            <cc1:Button ID="SaveInfo" runat="server" AutoPostBack="false" Text=" 完 成 "/>
            <cc1:Button ID="AddPage" runat="server" AutoPostBack="false" Text=" 添加页 "/>
            <cc1:Button ID="Button1" runat="server" AutoPostBack="false" Text=" 上一步 "/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'maglist.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#Button1").click(function () { location.href = 'magups.aspx?id=<%=STARequest.GetString("id")%>'; });
        RegFancyBox("a[rel=fbig]");
        $("#SaveInfo").click(function () {
            $("#content").val("");
            $("img[attid]").each(function (idx, obj) {
                var item = "<item name=\"" + $(obj).attr("name") + "\" url=\"" + $(obj).attr("src") + "\" orderid=\"" + $(obj).attr("orderid") + "\" attid=\"" + $(obj).attr("attid") + "\"/>";
                $("#content").val($("#content").val() + item);
            });
            $("#form1").submit();
        });

        function FormatPage() {
            $("a[rel=fbig]").each(function (idx2, obj2) {
                $(obj2).attr("title", "第" + (idx2 + 1) + "页");
            });
            $(".fpage").each(function (idx2, obj2) {
                $(obj2).html((idx2 + 1) + "页");
            });
        }

        function RegRemove() {
            $(".remove").each(function (idx, obj) {
                $(obj).click(function () {
                    $(obj).parent().remove();
                    FormatPage();
                });
            });
            ;
        }

        RegSelectFilePopWin("AddPage", "杂志页选择", "root=<%=filesavepath%>&filetype=jpeg,jpg,gif,png,bmp&fullname=1&cltmed=5&cusmethod=SelectMagMethod", "click");

        function SelectMagMethod(val) {
            $("#sortable").append("<li class=\"ui-state-default\"><a href=\"" + val + "\" rel=\"fbig\" title=\"\"><img onerror=\"\" src=\"" + val + "\" attid=\"90\" orderid=\"0\" name=\"杂志内容:<%=info.Name%>\" width=\"100\" height=\"130\" /></a><div class=\"remove\" title=\"删除此页\"></div></li>");
            RegRemove();
        }

        RegRemove();
        FormatPage();
    </script>
</body>
</html>