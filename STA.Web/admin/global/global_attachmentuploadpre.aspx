<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_attachmentuploadpre.aspx.cs" Inherits="STA.Web.Admin.attachmentuploadpre" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>文件批量上传</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
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
                <div class="bar">文件批量上传</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle"><cc1:Help runat="server" Text="相同文件覆盖" HelpText="如果在上传过程中文件保存存在相同文件名,是否进行覆盖处理"/>：</td>
                            <td>
			                    <cc1:RadioButtonList runat="server" ID="rblOver" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="o1">覆盖</asp:ListItem>
                                    <asp:ListItem Value="o0" Selected="True">忽略</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle"><cc1:Help runat="server" Text="图片添加水印" HelpText="自定义文件中的图片类型是否添加水印"/>：</td>
                            <td>
			                    <cc1:RadioButtonList runat="server" ID="rblWater" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="w0">不添加</asp:ListItem>
                                    <asp:ListItem Value="w1" Selected="True">按系统默认</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle"><cc1:Help ID="Help1" runat="server" Text="文件命名" HelpText="自定义命名方式"/>：</td>
                            <td>
			                    <cc1:RadioButtonList runat="server" ID="rblOrfilename" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="name0">按原文件名</asp:ListItem>
                                    <asp:ListItem Value="name1" Selected="True">按系统默认</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle"><cc1:Help ID="Help2" runat="server" Text="文件名称" HelpText="自定义文件名称,如不填则按原文件名保存"/>：</td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtName"></cc1:TextBox>
                            </td>
                        </tr>
                        <tr>
                             <td class="itemtitle"><cc1:Help ID="Help4" runat="server" Text="文件保存路径" HelpText="自定义文件保存路径,只能填写字母或数字或下划线或汉字,格式如：/files/，如文件夹不存在，系统将自动创建。如不填则按照系统默认配置路径保存"/>：</td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtSavepath"></cc1:TextBox> <span id="selectpath" class="selectbtn">选择</span>
                            </td>
                        </tr>
                        <tr>
	                         <td class="itemtitle"><cc1:Help ID="Help3" runat="server" Text="文件描述" HelpText="自定义文件描述,可不填"/>：</td>
	                        <td>
                                <cc1:TextBox runat="server" ID="txtDesc" TextMode="MultiLine" Height="70"></cc1:TextBox>
                            </td>
                        </tr>  
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <cc1:Button runat="server" ID="SaveInfo" Text=" 下 一 步 "/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'global_attachments.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        RegSelectPathPopWin("selectpath", "路径选择", "path=<%=filesavepath%>&fele=txtSavepath", "click", $("#txtSavepath"));
    </script>
</body>
</html>