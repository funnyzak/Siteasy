<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_collectset.aspx.cs" Inherits="STA.Web.Admin.collectset" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <title>采集选项</title>
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
                <div class="bar">采集选项</div>
                <div class="con">
                    <table>
	                    <tr><td class="itemtitle2" colspan="2">自动内链</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblAutolink" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">在内容随机插入关键词链接,推荐开启，对搜索引擎权重很有好处</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">同义词替换</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblTitrplopen" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">是否开启标题同义词替换,SEO优化作用</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">文件入库</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblColfilestorage" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">远程采集的附件如图片，flash等是否在数据库里进行记录(如记录会对性能有影响)</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">标题关键词</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblTititpos" RepeatDirection="Horizontal" RepeatColumns="4">
                                    <asp:ListItem Value="1">前</asp:ListItem>
                                    <asp:ListItem Value="2">后</asp:ListItem>
                                    <asp:ListItem Value="3">随机</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True">关闭</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">在标题插入随机关键词的位置</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">默认浏览数</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" ID="txtClickrange" Width="100" Text="1"/>
                            </td>
		                    <td class="vtop txt_desc">采集内容的默认浏览次数,可设置一个具体的数值,或者设置数值范围如：1,100。</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">SEO频率</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" ID="txtSeorate" Width="100" Text="0"/>
                            </td>
		                    <td class="vtop txt_desc">seo关键词随机随机插入内容的数量。</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">标题关键词</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                   <cc1:TextBox runat="server" ID="txtTitkeywords" Height="70" TextMode="MultiLine"/>
                            </td>
		                    <td class="vtop txt_desc">标题关键词列表,添加格式如:百度|站易;
                            </td>
	                    </tr> 
	                    <tr><td class="itemtitle2" colspan="2">标题同义词</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                   <cc1:TextBox runat="server" ID="txtTitreplace" Height="70" TextMode="MultiLine"/>
                            </td>
		                    <td class="vtop txt_desc">标题同义词替换列表;多个请用换行隔开;格式如:高温|好热
                            </td>
	                    </tr> 
	                    <tr><td class="itemtitle2" colspan="2">SEO词语或链接</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                   <cc1:TextBox runat="server" ID="txtSeocontent" Height="70" TextMode="MultiLine"/>
                            </td>
		                    <td class="vtop txt_desc">采集的内容插入的关键词列表,支持html，多个换行符分开;
        如：
		&#60;a&nbsp;href="http://www.stacms.com"&#62;站易&#60;/a&#62;</td>
	                    </tr> 
	                    <tr><td class="itemtitle2" colspan="2">关键词内链</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                   <cc1:TextBox runat="server" ID="txtSeolinks" Height="70" TextMode="MultiLine"/>
                            </td>
		                    <td class="vtop txt_desc">内容内如包含下列关键词将添加链接,多个请用换行符隔开;
                            如：百度|http://www.baidu.com
                            </td>
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