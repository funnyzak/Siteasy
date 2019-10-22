<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_baseset.aspx.cs" Inherits="STA.Web.Admin.baseset" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <title>基本设置</title>
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
                <div class="bar">基本设置</div>
                <div class="con">
                    <table>
	                    <tr><td class="itemtitle2" colspan="2">网站访问模式</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblWebBrowerModel" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="0" Selected="True">静态</asp:ListItem>
                                    <asp:ListItem Value="1">动态</asp:ListItem>
                                    <asp:ListItem Value="2">重写</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">网站访问量较大推荐使用静态模式，静态模式需要发布静态页面</td>
	                    </tr>
	                    <tr style="display:none;" class="trrws"><td class="itemtitle2" colspan="2" style="padding-top:10px;">重写URL后缀</td></tr>
	                    <tr style="display:none;" class="trrws">
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" ID="txtReWriteSuffix" Width="100" Text=".aspx"/>
                            </td>
		                    <td class="vtop txt_desc">如果网站访问模式为重写,设置重写的URL后缀建议为.aspx,如果使用其他后缀配合urlrewrite.dll使用</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2" style="padding-top:10px;">Cookie域</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="" ID="txtCookiesName" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">跨域共享cookie的域名，用于同一个域名下二级或多级域名的信息共享。</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">模板文件夹</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="tpl" ID="txtTemplatesavedir" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc" title="Templatesavedirname">网站模板文件夹名称，为了防止有人恶意下载网站模板，最好修改默认文件夹，必须与实际文件夹同步</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">信息页每页条数</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="20" ID="txtPageListNum" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">信息列表下每页所显示内容的条数</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">主页链接名</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="首页" ID="txtIndexLinkName" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc" title="IndexLinkName">主页链接名，用在页面导航中, 或其他地方</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">页面导航的间隔符号</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text=" > " ID="txtLocationBlank" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">页面导航之间的分割符号</td>
	                    </tr>

                        <tr style="display:none"><td class="itemtitle2" colspan="2">开启翻译</td></tr>
	                    <tr style="display:none">
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblOpentran" RepeatDirection="Horizontal" RepeatColumns="6">
                                    <asp:ListItem Value="1" Selected="True">开启</asp:ListItem>
                                    <asp:ListItem Value="0">关闭</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">开启网站翻译,如开启网页可借助翻译器切换语言;准确性视使用的翻译器</td>
	                    </tr>
                        <tr style="display:none"><td class="itemtitle2" colspan="2">网站语言</td></tr>
	                    <tr style="display:none">
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblWeblang" RepeatDirection="Horizontal" RepeatColumns="6">
                                    <asp:ListItem Value="zh-CN" Selected="True">中文</asp:ListItem>
                     <%--               <asp:ListItem Value="zh-TW">繁体</asp:ListItem>--%>
                                    <asp:ListItem Value="en">英文</asp:ListItem>
<%--                                    <asp:ListItem Value="ru">俄文</asp:ListItem>
                                    <asp:ListItem Value="ja">日文</asp:ListItem>--%>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">当前网站制作所使用的语言;如开启翻译此项必须设置正确</td>
	                    </tr>

	                    <tr><td class="itemtitle2" colspan="2">开启管理日志</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblAdminlogs" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1" Selected="True">开启</asp:ListItem>
                                    <asp:ListItem Value="0">关闭</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">开启管理日志，管理员在后台的日常操作会记录到数据库，否则不记录</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">静态页面后缀</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:DropDownList runat="server" ID="ddlSuffix">
                                    <asp:ListItem Value=".html">html</asp:ListItem>
                                    <asp:ListItem Value=".htm">htm</asp:ListItem>
                                    <asp:ListItem Value=".shtml">shtml</asp:ListItem>
                                    <asp:ListItem Value=".shtm">shtm</asp:ListItem>
                                </cc1:DropDownList>
                            </td>
		                    <td class="vtop txt_desc">生成的静态文件后缀名</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">静态文件保存路径</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="/html" ID="txtHtmlSavePath" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">网站的生成时，静态文件所保存的网站路径。格式如：/html</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">是否关闭网站</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblClosed" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="0" Selected="True">开放</asp:ListItem>
                                    <asp:ListItem Value="1">关闭</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">如果想禁止用户访问本网站，可选择“关闭”，关闭后后台可以依然正常访问</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">网站关闭原因/描述</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                   <cc1:TextBox runat="server" ID="txtClosereason" Height="70" TextMode="MultiLine"/>
                            </td>
		                    <td class="vtop txt_desc">如果网站关闭后，用户访问前台，所看到的友好信息</td>
	                    </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 配 置 "/>
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("input[name='rblWebBrowerModel']").click(function () {
            $(".trrws").css("display", $(this).val() == "2" ? "" : "none");
        });
        $("input[name='rblWebBrowerModel']:checked").trigger("click")
    </script>
</body>
</html>