<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_contypeadd.aspx.cs" Inherits="STA.Web.Admin.contypeadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>内容模型添加</title>
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
                <div class="bar">内容模型添加</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                                是否开启：
                            </td>
                            <td>
                                <cc1:RadioButtonList RepeatDirection="Horizontal" RepeatColumns="3" Width="300" runat="server"
                                    ID="rblOpen">
                                    <asp:ListItem Text="不开启" Value="0" />
                                    <asp:ListItem Text="开启" Selected="True" Value="1" />
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                权重：
                            </td>
                            <td>
                                 <cc1:TextBox runat="server" Width="100" ID="txtOrderid" Text="0" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                模型类型：
                            </td>
                            <td>
                                <cc1:RadioButtonList RepeatDirection="Horizontal" RepeatColumns="3" Width="300" runat="server"
                                    ID="rblSystem">
                                    <asp:ListItem Text="自动模型" Selected="True" Value="0" />
                                    <asp:ListItem Text="系统模型" Value="1" />
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                模型名称：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" HelpText="模型的名称,可任意填写" ID="txtName" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                模型标识：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtEname" AlignX="right" AlignY="center" CanBeNull="必填" HelpText="由英文、数字或下划线 7到20个字符以内组成，标识用于模版里数据调用扩展参数(ext). 还用于文档动态链接的前缀，如标识为：stacms,那么内容模型为stacms的链接为：stacms.aspx?id=ID号(不可使用page,channel,search,comment,vote等系统关键字,以免造成冲突). <span class='red'>建议由英文和数字组成.</span>"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                后台添加程序：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtBgaddmod" Text="global_contentadd.aspx"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                后台编辑程序：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtBgeditmod" Text="global_contentadd.aspx"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                后台管理程序：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtBglistmod" Text="global_contentlist.aspx"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                会员添加程序：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtAddmod"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                会员编辑程序：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtEditmod"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                会员管理程序：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtListmod"/>
                            </td>
                        </tr>
<%--                        <tr>
                            <td class="itemtitle">
                                附加表：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtExtable" CanBeNull="必填" HelpText="除主表以外其它自定义类型数据存放数据的表，必须由英文、数字、下划线组成。"/>
                            </td>
                        </tr>--%>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 模 型"/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" OnClientClick="location.href='global_contypes.aspx'" Text=" 返 回 " />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
</body>
</html>
