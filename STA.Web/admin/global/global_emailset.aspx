<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_emailset.aspx.cs" Inherits="STA.Web.Admin.emailset" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <title>邮箱设置</title>
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
                <div class="bar">邮箱设置</div>
                <div class="con">
                    <table>
	                    <tr><td class="itemtitle2" colspan="2">发送方式</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                   <cc1:DropDownList runat="server" ID="ddlSendWay" />
                            </td>
		                    <td class="vtop">发送邮件所使用的组件</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">STMP服务器</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Width="200" Text="smtp.163.com" ID="txtSmtpServer"/>
                            </td>
		                    <td class="vtop">发送邮件的SMTP服务器</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">SMTP端口</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Width="200" Text="25" ID="txtSmtpPort"/>
                            </td>
		                    <td class="vtop">发送邮件的SMTP端口</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">发件人昵称</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Width="200" ID="txtNickname" Text=""/>
                            </td>
		                    <td class="vtop">您发出的所有邮件，发件人将显示您的邮箱昵称，您还可以给每个帐户单独设置昵称</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">显示发件人</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Width="200" ID="txtSysemail" Text="silenceace@163.com"/>
                            </td>
		                    <td class="vtop">发送邮件中显示的发件人地址</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">邮箱账户</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Width="200" ID="txtUserName" Text="silenceace"/>
                            </td>
		                    <td class="vtop">发送邮件的账户</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">邮箱密码</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Width="200" ID="txtPassword" TextMode="Password"/>
                            </td>
		                    <td class="vtop">系统邮箱的密码</td>
	                    </tr>
                    </table>
                </div>
            </div>

            <div class="conb-1">
                <div class="bar">订阅设置</div>
                <div class="con">
                    <table>
	                    <tr><td class="itemtitle2" colspan="2">邮件标题</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="尊敬的{receiver}，{webname}邮件订阅成功" ID="txtSubtitle"/>
                            </td>
		                    <td class="vtop">系统发送的邮件标题，不支持 HTML。</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">邮件正文</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="" ID="txtSubcont" TextMode="MultiLine" Height="100"/>
                            </td>
		                    <td class="vtop">系统发送的邮件正文。标题内容均支持变量替换，可以使用如下变量:<BR>
          {receiver} : 邮件接收者<BR>
          {time} :   订阅时间<BR>
          {webname} : 站点名称<br>
          {weburl} : 站点地址<br>
          {unsubscribeurl} : 退订地址</td>
	                    </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 配 置 "/>
        </div>
        <div class="margin-top20">
           <div class="conb-1">
            <div class="bar">测试邮箱</div>
            <div class="con">
                <table>
	                <tr>
		                <td class="vtop rowform">
			                <cc1:TextBox runat="server" Text="" ID="txtTestmail"/>
                        </td>
		                <td class="vtop">填写接收邮件邮箱地址</td>
	                </tr>
                </table>
            </div>
        </div>
        </div>
        <div class="navbutton">
            <cc1:Button runat="server" ID="btnTestmail" Text=" 发送测试邮件 "/>
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
</body>
</html>