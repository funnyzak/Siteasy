<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_attachmentset.aspx.cs" Inherits="STA.Web.Admin.attachmentset" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <title>附件设置</title>
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
                <div class="bar">附件设置</div>
                <div class="con">
                    <table>
	                    <tr><td class="itemtitle2" colspan="2" style="padding-bottom:0;">文件保存方式</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblAttachsaveway" RepeatDirection="Vertical">
                                    <asp:ListItem Value="0">按年存入不同目录(不推荐)</asp:ListItem>
                                    <asp:ListItem Value="1">按年/月存入不同目录</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">按年/月/日存入不同目录</asp:ListItem>
                                    <asp:ListItem Value="3">按年/月/日/时存入不同目录</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">本设置只对新上传的文件, 设置更改之前的文件仍然保存在原来位置</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2" style="padding:5px 0 0 7px;">文件命名方式</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblAttachnameway" RepeatDirection="Vertical">
                                    <asp:ListItem Value="0">保持原文件名不变</asp:ListItem>
                                    <asp:ListItem Value="1">自动随机字符串</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">原文件名和随机字符串组合</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">本设置只影响新上传的文件, 设置更改之前的文件名仍保持不变</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2" style="padding:5px 0 0 7px;">图片/上传文件保存路径</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="/files" ID="txtAttachsavepath" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">用户编辑内容上传图片/文件所保存的网站路径。格式如：/files</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2" style="padding:5px 0 0 7px;">大文件保存目录</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="/z" ID="txtAttachbigfilepath" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">当文件很大可以使用大文件上传功能,大文件默认保存的目录。格式如：/bigfile</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">用户允许上传的图片类型</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="gif,jpg,jpeg,bmp,png,swf" ID="txtAttachimgtype" TextMode="MultiLine" Height="50"/>
                            </td>
		                    <td class="vtop txt_desc">配置系统可以上传的所有图片的类型，建议只配置常用类型</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">用户允许上传的图片大小(单位：MB)</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="2" ID="txtAttachimgmaxsize" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">配置系统可以上传的所有图片的类型的文件大小</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">用户允许上传的媒体类型</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="rm,rmvb,mp3,flv,wav,mid,midi,ra,avi,mpg,mpeg,asf,asx,wma,mov" ID="txtAttachmediatype" TextMode="MultiLine" Height="50"/>
                            </td>
		                    <td class="vtop txt_desc">配置系统可以上传的所有媒体的类型，建议只配置常用类型</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">用户允许上传的媒体大小(单位：MB)</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="10" ID="txtAttachmediamaxsize" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">配置系统可以上传的所有媒体的类型的文件大小</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">用户允许上传的软件/附件类型</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="zip,gz,rar,iso,doc,xsl,ppt,wps" ID="txtAttachsofttype" TextMode="MultiLine" Height="50"/>
                            </td>
		                    <td class="vtop txt_desc">配置系统可以上传的所有软件/附件的类型，建议只配置常用类型</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">用户允许上传的软件大小(单位：MB)</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="10" ID="txtAttachsoftmaxsize" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">配置系统可以上传的所有软件的类型的文件大小</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2" style="padding:5px 0 0 7px;">缩略图尺寸</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="0" ID="txtThumbsize" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">上传的图片生成缩略图的默认尺寸,如：120,90，则表示宽为120px，高90px</td>
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