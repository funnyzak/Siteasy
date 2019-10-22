<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_useredit.aspx.cs" Inherits="STA.Web.Admin.useredit" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="STA.Core" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>用户编辑</title>
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
                <div class="bar">用户编辑</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                               头像：
                            </td>
                            <td>
                                <img src="<%=STA.Core.Avatars.GetAvatarUrl(STA.Common.STARequest.GetInt("id",0),STA.Core.AvatarSize.Small)%>" width="70" onerror="this.src='../../sta/pics/avator/noavatar_medium.gif'"/> <cc1:CheckBox runat="server" ID="cbDelavatar" Text="删除此头像" Checked="false"/>
                            </td>
                        </tr> 
                        <tr>
                            <td class="itemtitle">
                               性别：
                            </td>
                            <td>
                                <cc1:RadioButtonList runat="server" ID="rblGender" RepeatColumns="3" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">男</asp:ListItem>
                                    <asp:ListItem Value="0">女</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>  
                        <tr>
                            <td class="itemtitle">
                               用户名：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtUserName" HelpText="只可使用字母、数字，长度为3-20个字符。" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                               用户昵称：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" HelpText="昵称长度为20个字节，汉字最大为10个;字母、数字，字符最大为20个。" ID="txtNickName"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                               用户密码：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtPwd" Text="000000" HelpText="提示：密码默认为“000000”" TextMode="Password" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                               请再次输入密码：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtRePwd" Text="000000" TextMode="Password" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
	                        <td class="itemtitle">所属用户组：</td>
	                        <td>
                                <cc1:DropDownList runat="server" ID="ddlUserGroup"/>
                            </td>
                        </tr>
                        <tr>
	                        <td class="itemtitle">系统用户：</td>
	                        <td>
                                <cc1:RadioButtonList runat="server" ID="rblSysuser" AutoPostBack="true" RepeatColumns="3" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr runat="server" id="trSysGroup" visible="false">
	                        <td class="itemtitle">所属系统组：</td>
	                        <td>
                                <cc1:DropDownList runat="server" ID="ddlSysGroup"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                               金币：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtCredits1" RequiredFieldType="数据校验" CanBeNull="必填" Text="0"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                               积分：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtCredits" RequiredFieldType="数据校验" CanBeNull="必填" Text="100"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                               电子邮箱：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtEmail" RequiredFieldType="电子邮箱" CanBeNull="必填"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidId" />
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 用 户 " ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/state2.gif"/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href='global_userlist.aspx'"/>
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
</body>
</html>