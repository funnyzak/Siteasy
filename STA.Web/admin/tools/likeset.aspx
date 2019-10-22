<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="likeset.aspx.cs" Inherits="STA.Web.Admin.Tools.likeset" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <title>个人偏好设置</title>
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
                <div class="bar">个人偏好设置</div>
                <div class="con">
                    <table>
	                    <tr><td class="itemtitle2" colspan="2">界面主题</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:DropDownList runat="server" ID="ddlTheme"/>
                            </td>
		                    <td class="vtop">后台管理界面的主题风格，改变后页面会自动刷新以更换风格</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">界面预览</td></tr>
                        <tr>
                            <td colspan="2">
                                <img src="<%=sitepath%><%=adminpath%>/themes/<%=systyle%>/about.png" alt="界面预览" id="previewtheme" width="300" height="180"/>
                            </td>
                        </tr>
	                    <tr><td class="itemtitle2" colspan="2">操作提示</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:RadioButtonList runat="server" ID="rblTipMsg" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1" Selected="True">开启</asp:ListItem>
                                    <asp:ListItem Value="0">关闭</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop">在执行任何管理操作，在执行完毕后是否给予友好提示</td>
	                    </tr>
<%--	                    <tr><td class="itemtitle2" colspan="2">操作提示背景色</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:RadioButtonList runat="server" ID="rblOverclass" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="white" Selected="true">白色</asp:ListItem>
                                    <asp:ListItem Value="black">黑色</asp:ListItem>
                                    <asp:ListItem Value="blue">蓝色</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop">操作弹出提示的背景颜色</td>
	                    </tr>--%>
	                    <tr><td class="itemtitle2" colspan="2">操作提示透明度</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="60" ID="txtOraylay" Width="100"/>
                            </td>
		                    <td class="vtop">操作提示的背景透明度，范围为0到100，设置0则关闭透明背景，100则为不透明</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">管理页条数</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="20" ID="txtManageCount" Width="100"/>
                            </td>
		                    <td class="vtop">内容管理页面，每页记录默认所显示的条数</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">常用功能个数</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="20" ID="txtFastmenucount" Width="100"/>
                            </td>
		                    <td class="vtop">顶部常用功能菜单显示个数，最大为15</td>
	                    </tr>
                    </table>
                </div>
            </div>
            <div class="navbutton">
                <input type="hidden" runat="server" id="hidtheme" />
                <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 配 置 "/>
            </div>
            <div class="margin-top20">
                <div class="conb-1">
                    <div class="bar">修改登录密码</div>
                    <div class="con">
                        <table>
	                        <tr><td class="itemtitle2" colspan="2">原密码</td></tr>
	                        <tr>
		                        <td class="vtop rowform">
			                        <cc1:TextBox runat="server" ID="txtPwd" TextMode="Password"/>
                                </td>
		                        <td class="vtop">为了保证信息安全,请确认原始密码</td>
	                        </tr>
	                        <tr><td class="itemtitle2" colspan="2">新密码</td></tr>
	                        <tr>
		                        <td class="vtop rowform">
			                        <cc1:TextBox runat="server" ID="txtNpwd" TextMode="Password"/>
                                </td>
		                        <td class="vtop">建议6~16个字符，区分大小写</td>
	                        </tr>
	                        <tr><td class="itemtitle2" colspan="2">确认新密码</td></tr>
	                        <tr>
		                        <td class="vtop rowform">
			                        <cc1:TextBox runat="server" ID="txtNpwd2" TextMode="Password"/>
                                </td>
		                        <td class="vtop">再次确认新密码</td>
	                        </tr>
                        </table>
                    </div>
                </div>
                <div class="navbutton">
                    <cc1:Button runat="server" ID="UpdatePwd" Text=" 修 改 密 码 " AutoPostBack="false"/>
                    &nbsp;&nbsp;<span style="color:#ff0000;" id="tips"></span>
                </div>
            </div>
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#ddlTheme").change(function (){
            $("#previewtheme").attr("src","<%Response.Write(sitepath+adminpath);%>/themes/"+$(this).val()+"/about.png");
        });

        function Tips(tips) {
            $("#tips").show().html(tips).fadeOut(1000);
        };

        $("#UpdatePwd").click(function () {
            var pwd = $.trim($("#txtPwd").val()), npwd = $.trim($("#txtNpwd").val()), npwd2 = $.trim($("#txtNpwd2").val());
            if (pwd == "" || npwd == "" || npwd2 == "") {
                Tips("请填写密码！");
            }
            else if (npwd != npwd2) {
                Tips("请确保两次输入的新密码相同！");
            } else {
                __doPostBack("UpdatePwd", "");
            }
        });
    </script>
</body>
</html>