<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_waterset.aspx.cs" Inherits="STA.Web.Admin.waterset" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <title>图片水印设置</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="../plugin/scripts/fancybox/fancybox.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script type="text/javascript" src="../plugin/scripts/fancybox/fancybox.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="conb-1">
                <div class="bar">图片水印设置</div>
                <div class="con">
                    <table>
<%--	                    <tr><td class="itemtitle2" colspan="2" style="padding-bottom:0;">开启水印功能</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblOpenwatermark" RepeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="1" Selected="True">开启</asp:ListItem>
                                    <asp:ListItem Value="0">关闭</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">开启水印功能后，所有网站用户新上传的图片文件，将自动添加水印</td>
	                    </tr>--%>
	                    <tr><td class="itemtitle2" colspan="2" style="padding:5px 0 0 7px;">水印方式</td></tr>
	                    <tr>
		                    <td class="vtop rowform" style="padding-top:0;">
			                    <cc1:RadioButtonList runat="server" ID="rblWatertype" epeatDirection="Horizontal" RepeatColumns="3">
                                    <asp:ListItem Value="0">文字水印</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">图片水印</asp:ListItem>
                                </cc1:RadioButtonList>
                            </td>
		                    <td class="vtop txt_desc">文字水印将在图片上只加文字，图片水印将在图片上添加图片</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2" style="padding:5px 0 0 7px;">图片水印位置</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <div class="water_position">
                                    <div>
                                        <span><input type="radio" value="1" name="position"/>左上</span>
                                        <span><input type="radio" value="2" name="position"/>中上</span>
                                        <span><input type="radio" value="3" name="position"/>右上</span>
                                    </div>
                                    <div>
                                        <span><input type="radio" value="4" name="position"/>左中</span>
                                        <span><input type="radio" value="5" name="position"/>中部</span>
                                        <span><input type="radio" value="6" name="position"/>右中</span>
                                    </div>
                                    <div>
                                        <span><input type="radio" value="7" name="position"/>左下</span>
                                        <span><input type="radio" value="8" name="position" checked="checked"/>中下</span>
                                        <span><input type="radio" value="9" name="position"/>右下</span>
                                    </div>
                                </div>
                                <input type="radio" value="0" name="position"/>关闭水印功能
                                <input type="hidden" value="9" runat="server" id="hidPosition" />
                            </td>
		                    <td class="vtop txt_desc">选择图片水印所加上传图片的位置，共9个位置可选</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2" style="padding:5px 0 0 7px;">水印透明度</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="60" ID="txtWateropacity" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">设置水印的透明度，数值从0到100</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">水印图片尺寸</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="200*200" ID="txtWaterlimitsize" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">要添加水印图片的最小宽度和高度,如果为0，则不限制,如0*300则不限制宽，高最小为300；0为都不限制</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">水印图片质量</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="80" ID="txtWaterquality" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">图片在加水印时的质量参数，范围为 0～100 的整数，数值越大结果图片效果越好，但尺寸也越大,建议80。</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">水印图片</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" ID="txtWaterimg" Width="160"/>
                                <span id="selectimg" class="selectbtn">选择</span>
                                <a href="javascript:;" id="previewImg">预览</a>
                            </td>
		                    <td class="vtop txt_desc">如图片水印必须选择一张水印图片,如留空则水印方式变为文字水印</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">水印字体</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:DropDownList runat="server" ID="ddlWaterfontname" />
                            </td>
		                    <td class="vtop txt_desc">文字水印所使用的字体</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">字体大小(单位：像素)</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="13" ID="txtWaterfontsize" Width="100"/>
                            </td>
		                    <td class="vtop txt_desc">文字水印字体大小</td>
	                    </tr>
	                    <tr><td class="itemtitle2" colspan="2">文字水印内容</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                    <cc1:TextBox runat="server" Text="上传于{1}{2}" ID="txtWatertext" Width="200"/>
                            </td>
		                    <td class="vtop txt_desc">可以使用的变量: {1}表示当前日期 {2}表示当前时间 {3}网站名称 {4}网站地址</td>
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
        RegPreviewImg("#txtWaterimg", "#previewImg");
        RegSelectFilePopWin("selectImg", "txtWaterimg", "click");
        SetRadioValue("position", $("#hidPosition").val());
        RegSelectFilePopWin("selectimg", "水印图片选择", "root=<%=filesavepath%>&path=<%=filesavepath%>,watermark" +
        "&filetype=jpeg,jpg,gif,png,bmp&fullname=1&cltmed=1&fele=txtWaterimg&watermark=no&rename=0", "click", { of: $("#txtWaterimg"), my: 'left center', at: "right bottom", offset: '70px 20px', collision: "fit none" });
    </script>
</body>
</html>