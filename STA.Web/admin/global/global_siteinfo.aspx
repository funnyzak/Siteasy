<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_siteinfo.aspx.cs" Inherits="STA.Web.Admin.siteinfo" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <title>站点信息</title>
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
                <div class="bar">站点信息</div>
                <div class="con">
                    <table>
	                    <tr><td class="itemtitle2" colspan="2">网站名称</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" ID="txtWebName"/>
                            </td>
		                    <td class="vtop txt_desc" title="WebName">几个字概括网站的主要内容，可在网站任何地方公共调用全局参数</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">网站标题</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" ID="txtWebTitle"/>
                            </td>
		                    <td class="vtop txt_desc" title="WebTitle">可概括网站内容相关的网站标题，此标题主要用于搜索引擎优化。</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">网站URL地址</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" ID="txtWebUrl"/>
                            </td>
		                    <td class="vtop txt_desc" title="WebUrl">网站 URL，如果网站为静态模式必须填写URL地址, 格式如http://www.stacms.com</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">网站关键字</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                   <cc1:TextBox runat="server" ID="txtKeywords" Height="70" TextMode="MultiLine"/>
                            </td>
		                    <td class="vtop txt_desc" title="Keywords">Keywords 用于记录网站的关键字, 多个关键字间请用半角逗号 "," 隔开, 用于搜索引擎优化</td>
	                    </tr> 
	                    <tr><td class="itemtitle2" colspan="2">网站描述</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                   <cc1:TextBox runat="server" ID="txtDescription" Height="70" TextMode="MultiLine"/>
                            </td>
		                    <td class="vtop txt_desc" title="Description">Description 用于记录网站的描述和概要, 用于搜索引擎优化</td>
	                    </tr> 
	                    <tr><td class="itemtitle2" colspan="2">外部服务代码</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                   <cc1:TextBox runat="server" ID="txtExtCode" Height="70" TextMode="MultiLine"/>
                            </td>
		                    <td class="vtop txt_desc" title="ExtCode">可以添加第三方提供的接口代码，如流量统计服务代码，在线客户代码等</td>
	                    </tr> 
	                    <tr><td class="itemtitle2" colspan="2">管理员邮箱</td></tr>
                        <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" ID="txtAdminmail"/>
                            </td>
		                    <td class="vtop txt_desc">管理员邮箱，在网站底部显示或其他位置公共调用</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">网站备案号</td></tr>
                        <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" ID="txtIcp"/>
                            </td>
		                    <td class="vtop txt_desc" title="Icp">页面底部可以显示 ICP 备案信息,如果网站已备案,在此输入您的备案号,它将显示在页面底部,如果没有请留空</td>
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