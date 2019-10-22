<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_otherset.aspx.cs" Inherits="STA.Web.Admin.otherset" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <title>其他选项</title>
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
                <div class="bar">其他选项</div>
                <div class="con">
                    <table>
	                    <tr><td class="itemtitle2" colspan="2" style="padding-top:10px;">附加网址</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblwithWeburl" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1" Selected="True">加上</asp:ListItem>
                                    <asp:ListItem Value="0">不加</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">网站所有内容生成的超链接是否加网站地址前缀</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">翻译器</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                   <cc1:DropDownList runat="server" ID="ddlTranslator" />
                            </td>
		                    <td class="vtop  txt_desc">所有用到语言翻译的模块所使用的翻译器</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">SiteMap数量</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" ID="txtSitemapconcount" Width="100" Text="100"/>
                            </td>
		                    <td class="vtop txt_desc">系统<a href="../tools/sitemapmake.aspx">定时任务</a>默认生成Sitemap地图所包含的内容数量,建议1000以内。</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">RSS数量</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" ID="txtRssconcount" Width="100" Text="100"/>
                            </td>
		                    <td class="vtop txt_desc">单个频道发布RSS订阅文件生成的最大的内容条数,建议100以内。</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">商品发票税点</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" ID="txtInvoicerate" Width="100" Text="0"/>
                            </td>
		                    <td class="vtop txt_desc">商品发票的税点,填写1到100的数字;如果填0表示发票不另外收费</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">订单过期日期</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" ID="txtOrderbackday" Width="100" Text="3"/>
                            </td>
		                    <td class="vtop txt_desc">未付款的订单多少天过期(过期的订单系统将自动取消),请填写1到100的整数</td>
	                    </tr>
                        <tr style="display:none;"><td class="itemtitle2" colspan="2">订单号规则</td></tr>
	                    <tr style="display:none;">
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" ID="txtOrdernorule" Text="{@ram9}" HelpText="{@year02}、{@year04}、{@month}、{@day} 年月日<br/>{@hour}、{@minute}、{@second} 时分秒<br/>{@ram9} 生成随机数,可设置1到9"/>
                            </td>
		                    <td class="vtop txt_desc">商品订单号生成规则;订单号作为商品的唯一标识请务必设置随机规则</td>
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
</body>
</html>