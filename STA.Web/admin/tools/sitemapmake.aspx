<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sitemapmake.aspx.cs" Inherits="STA.Web.Admin.Tools.sitemapmake" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>Sitemap生成</title>
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">Sitemap生成</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">页面更新频率：</td>
                            <td>
                                <cc1:DropDownList runat="server" ID="ddlFrequency">
                                    <asp:ListItem Value="always">每10分钟更新一次</asp:ListItem>
                                    <asp:ListItem Value="hourly">每小时更新一次</asp:ListItem>
                                    <asp:ListItem Value="daily" Selected="True">每天更新一次</asp:ListItem>
                                    <asp:ListItem Value="weekly">每周更新一次</asp:ListItem>
                                    <asp:ListItem Value="monthly">每月更新一次</asp:ListItem>
                                    <asp:ListItem Value="yearly">每年更新一次</asp:ListItem>
                               </cc1:DropDownList>
                            </td>
                        </tr>
                        <tr>
	                        <td class="itemtitle">页面重要性：</td>
	                        <td>
                                <cc1:DropDownList runat="server" ID="ddlPriority">
                                    <asp:ListItem Value="0.1">0.1&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="0.2">0.2</asp:ListItem>
                                    <asp:ListItem Value="0.3">0.3</asp:ListItem>
                                    <asp:ListItem Value="0.4">0.4</asp:ListItem>
                                    <asp:ListItem Value="0.5">0.5</asp:ListItem>
                                    <asp:ListItem Value="0.6">0.6</asp:ListItem>
                                    <asp:ListItem Value="0.7">0.7</asp:ListItem>
                                    <asp:ListItem Value="0.8" Selected="True">0.8</asp:ListItem>
                                    <asp:ListItem Value="0.9">0.9</asp:ListItem>
                                    <asp:ListItem Value="1.0">1.0</asp:ListItem>
                                </cc1:DropDownList>
                            </td>
                        </tr> 
                        <tr>
	                        <td class="itemtitle">生成最新条数：</td>
	                        <td>
                                <cc1:TextBox runat="server" ID="txtNewcount" Text="1000" Width="50" RequiredFieldType="数据校验" CanBeNull="必填"></cc1:TextBox>
                            </td>
                        </tr> 
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <cc1:Button runat="server" AutoPostBack="false" ID="SaveInfo" Text=" 提交生成Sitemap "  ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/state2.gif"/>
        </div>
            <cc1:PageInfo ID="PageInfo1" runat="server" Text="Sitemaps 服务旨在使用 Feed 文件 sitemap.xml 通知 Google、Yahoo! 以及 Microsoft 等 Crawler(爬虫)网站上哪些文件需要索引、这些文件的最后修订时间、更改频度、文件位置、相对优先索引权，这些信息将帮助他们建立索引范围和索引的行为习惯。详细信息请查看 <a href='http://www.sitemaps.org' target='_blank' class='red'>sitemaps.org</a> 网站上的说明。"/>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#SaveInfo").click(function () {
            Loading("正在生成Siteamp,请稍等..")
            Ajax("sitemapmake&count=" + $("#txtNewcount").val()
                 + "&priority=" + $("#ddlPriority").val() + "&frequency="
                 + $("#ddlFrequency").val(),
                 function (ret) {
                     $.unblockUI({
                         onUnblock: function () { SAlert("Sitemap文件已成功生成！<a href='<%=sitepath%>/sitemap.xml' target='_blank' class='red'>查看地图</a> ", 100); }
                 }); 
           });
        });
    </script>
</body>
</html>