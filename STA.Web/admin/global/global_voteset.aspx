<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_voteset.aspx.cs" Inherits="STA.Web.Admin.voteset" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <title>投票设置</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/datepicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">投票设置</div>
                <div class="con">
                    <table>
	                    <tr><td class="itemtitle2" colspan="2">是否开启算术验证码</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblVcode" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">提交投票时是否开启算术验证码，如果开启可有效防止大部分刷票机器人</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">是否登录才能投票</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblVoteonlymember" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">是否需要注册，就是指是否需要注册成为会员才可以投票，默认为任何人都可以投票，无特殊情况不要选是.</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">开启手机短信验证码</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblVotephoneverify" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">是否开启手机短信验证码，开启手机短信验证码后用户投票必须输入手机短信地址接受验证码，获取验证码方可投票.</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">IP时间间隔</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="1440" ID="txtVotetimeinterval" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">IP时间间隔,就是同一个IP对已主题投票后，多长时间可以再对此主题进行投票. 0则不限制. 单位（分)</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">时间段</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" ID="txtVotestime" Width="50" Text="00:00"/> - <cc1:TextBox runat="server" Text="23:59" ID="txtVoteetime" Width="50"/>
                            </td>
		                    <td class="vtop txt_desc">投票时间段，一天中只有在指定的时间段才可以进行投票,格式如：00:00。</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">开启信息登记</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblVoteinfoinput" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                                    <asp:ListItem Value="0">否</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">是否开启信息登记，开启后用户投票必须输入自己的个人信息方可投票.</td>
	                    </tr>
	                    <tr t='info' style="display:none;"><td class="itemtitle2" colspan="2">登记信息内容</td></tr>
	                    <tr t='info' style="display:none;">
		                    <td class="vtop rowform">
			                   <cc1:TextBox runat="server" ID="txtVoteinfos" Height="70" TextMode="MultiLine"/>
                            </td>
		                    <td class="vtop txt_desc">设置要登记的信息字段，格式如：&lt;item name='realname' desc='真实姓名' isnull='必填'/&gt;</td>
	                    </tr> 
	                    <tr><td class="itemtitle2" colspan="2">禁止IP段</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                   <cc1:TextBox runat="server" ID="txtVoteforbidips" Height="70" TextMode="MultiLine"/>
                            </td>
		                    <td class="vtop txt_desc">设置不能投票的IP段，格式192.168.1.*。如果有多个IP段用|分割。</td>
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
//        $("input[name='rblVoteinfoinput']").click(function () { $("tr[t]").css("display", $(this).val() == "1" ? "" : "none"); });
        //        $("input[name='rblVoteinfoinput']:checked").trigger("click");

        $("#txtVotestime,#txtVoteetime").click(function () { WdatePicker({dateFmt: 'HH:mm' }) });
    </script>
</body>
</html>