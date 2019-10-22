<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_optimizeset.aspx.cs" Inherits="STA.Web.Admin.optimizeset" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <title>性能优化</title>
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
                <div class="bar">性能优化</div>
                <div class="con">
                    <table>
	                    <tr><td class="itemtitle2" colspan="2">文档分页</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblContentpage" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1" Selected="True">开启</asp:ListItem>
                                    <asp:ListItem Value="0">关闭</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">关闭文档分页后,文档里的分页标签将被忽略,文档不会分页,但会加快HTML生成速度</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">开启搜索</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblOpensearch" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1" Selected="True">开启</asp:ListItem>
                                    <asp:ListItem Value="0">关闭</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">全站的内容搜索功能;如果关闭,站内搜索对一般会员或网友将失效,但管理员可以使用</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">压缩输出</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblHtmlcompress" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1">开启</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True">关闭</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">是否对所有页面大小进行压缩输出,开启此选项请确保页面代码格式正确</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">开启评论</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblOpencomment" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1" Selected="True">开启</asp:ListItem>
                                    <asp:ListItem Value="0">关闭</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">全站的内容评论功能;如果关闭,评论功能对一般会员或网友将失效,但管理员可以使用</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">统计功能</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblUpdateclick" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1" Selected="True">开启</asp:ListItem>
                                    <asp:ListItem Value="0">关闭</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">如果关闭统计功能，将不再更新内容、专题等所有文档类型的访问量</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">缓存时间</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="1440" ID="txtCacheinterval" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">缓存过期时间，也就是数据缓存的有效分钟数，单位：分</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">搜索缓存时间</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="30" ID="txtSearchcachetime" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">搜索功能,搜索文档内容所缓存的时间，最小为10分. 单位：分</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">搜索间隔时间</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="0" ID="txtSearchinterval" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">搜索功能,单次搜索所间隔的时间,可禁止用户恶意频繁搜索，0 为不开启. 单位：秒</td>
	                    </tr>
                        <tr><td class="itemtitle2" colspan="2">防刷新时间</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="0" ID="txtReflushinterval" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">主要防止恶意刷新行为. 如果设置0则关闭此项. 单位：秒</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">禁止搜索关键字</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                   <cc1:TextBox runat="server" ID="txtForbidswords" Height="70" TextMode="MultiLine"/>
                            </td>
		                    <td class="vtop txt_desc">禁止搜索的关键字,主要方式用户恶意搜索，如:的 了 你 等无意义的关键字,多个请用空格隔开</td>
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