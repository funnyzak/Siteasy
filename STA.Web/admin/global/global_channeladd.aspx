<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_channeladd.aspx.cs"Inherits="STA.Web.Admin.channeladd"%>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>频道添加/编辑</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="../plugin/scripts/fancybox/fancybox.css" type="text/css" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script type="text/javascript" src="../plugin/scripts/fancybox/fancybox.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mwrapper">
        <div id="main">
            <div class="contab clearfix">
                <ul>
                    <li>基本信息</li>
                    <li>高级设置</li>
                    <li>频道内容</li>
                    <li>权限设置</li>
                </ul>
            </div>
            <div class="cont-3">
                <div class="box">
                    <table>
                        <tr>
	                        <td class="itemtitle"><cc1:Help runat="server" Text="属性"/>：</td>
	                        <td>
                                <cc1:CheckBox runat="server" ID="cbIsPost" Text="支持投稿" Checked="true"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <cc1:CheckBox runat="server" ID="cbIsHidden" Text="隐藏频道" Checked="false"/>
                            </td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help runat="server" Text="内容模型"/>：</td>
	                        <td>
                                <cc1:DropDownList runat="server" ID="ddlConType"/>
                            </td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help runat="server" Text="频道权重" HelpText="只能添数字,默认数字越大频道越靠前"/>：</td>
	                        <td><cc1:TextBox runat="server" ID="txtOrderId" CanBeNull="必填" Text="0" RequiredFieldType="数据校验" Width="100"/></td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help ID="Help2" runat="server" Text="列表显示条数" HelpText="列表页信息显示条数,只对最终列表频道有效;如果为小于等于0,按默认显示条数"/>：</td>
	                        <td><cc1:TextBox runat="server" ID="txtListcount" CanBeNull="必填" Text="0" RequiredFieldType="数据校验" Width="100"/></td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help runat="server" Text="频道名称"/>：</td>
	                        <td><cc1:TextBox runat="server" ID="txtName" CanBeNull="必填"/></td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help runat="server" Text="频道目录" HelpText="频道生成静态所保存的目录"/>：</td>
	                        <td><cc1:TextBox runat="server" ID="txtSavePath" HelpText="格式如：/channel/stacms"/></td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help runat="server" Text="频道索引" HelpText="一般用来做频道静态文件的名称、不包括汉字和特殊字符"/>：</td>
	                        <td><cc1:TextBox runat="server" ID="txtFileName"/></td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help ID="Help1" runat="server" Text="频道图片"/>：</td>
	                        <td><cc1:TextBox runat="server" ID="txtImg"/>
                            <span id="selectimgfile" class="selectbtn">选择</span> <a href="javascript:;" id="previewImg">预览</a></td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help runat="server" Text="频道展示类型"/>：</td>
	                        <td>
                                <cc1:RadioButtonList RepeatDirection="Vertical" runat="server"  ID="rblCtype">
                                    <asp:ListItem Text="最终列表频道（允许在本频道发布内容，并生成内容列表）" Value="1" Selected="True"/>
                                    <asp:ListItem Text="频道封面（频道本身不允许发布内容）" Value="2"/>
                                    <asp:ListItem Text="外部连接（在频道目录处填写网址）" Value="3"/>
                                </cc1:RadioButtonList>
                            </td>
                        </tr>  
                        <tr>
	                        <td class="itemtitle"><cc1:Help ID="Help7" runat="server" Text="继承选项"/>：</td>
	                        <td><cc1:CheckBox runat="server" ID="cbInherit" Text="同时更改下级频道的浏览权限、内容类型、模板风格、命名规则等通用属性" Checked="false"/></td>
                        </tr>
		            </table>
                 </div>
            </div>
            <div class="cont-3" style="display:none;">
                <div class="box">
                    <table>
<%--                        <tr>
	                        <td class="itemtitle"><cc1:Help ID="Help8" runat="server" Text="生成HTML"/>：</td>
	                        <td>
                                <cc1:CheckBox runat="server" ID="CheckBox1" Text="频道生成HTML" Checked="true"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <cc1:CheckBox runat="server" ID="CheckBox2" Text="内容生成HTML" Checked="false"/>
                            </td>
                        </tr>--%>
                        <tr>
	                        <td class="itemtitle"><cc1:Help runat="server" Text="多站点支持" HelpText="多站点支持只在站点为静态模式是有效，建议设置频道目录为唯一目录，避免和其他频道冲突"/>：</td>
	                        <td><cc1:CheckBox runat="server" ID="cbMoreSite" Text="开启" Checked="false"/></td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help runat="server" Text="绑定域名" HelpText="如果开启多站点支持需填写绑定域名,并需手动把域名指向该频道目录：格式如：http://abc.stacms.com 注意该频道模版里所有链接、文件必须使用带网址的完全路径"/>：</td>
	                        <td><cc1:TextBox runat="server" ID="txtSiteUrl"/>&nbsp;</td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help runat="server" Text="封面模板"/>：</td>
	                        <td><cc1:TextBox runat="server" ID="txtCoverTem" Text="channelindex_default" />&nbsp;<span id="selectcovertem" class="selectbtn">选择</span></td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help runat="server" Text="列表模板"/>：</td>
	                        <td><cc1:TextBox runat="server" ID="txtListTem" Text="channellist_default" />&nbsp;<span id="selectlisttem" class="selectbtn">选择</span></td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help runat="server" Text="内容模板"/>：</td>
	                        <td><cc1:TextBox runat="server" ID="txtConTem"/>&nbsp;<span id="selectcontem" class="selectbtn">选择</span></td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help ID="Help5" runat="server" Text="列表命名规则" HelpText="此频道下内容分页文件命名规则"/>：</td>
	                        <td><cc1:TextBox runat="server" ID="txtListRule" CanBeNull="必填" Text="{@channelpath}/list_{@channelid}_{@page}" Width="400" HelpText="支持变量：{@page}、{@channelpath}、{@channelid} 页码、频道目录、频道ID"/></td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help ID="Help6" runat="server" Text="内容命名规则" HelpText="此频道下属内容静态文件的路径+文件名命名规则"/>：</td>
	                        <td><cc1:TextBox runat="server" ID="txtConRule" CanBeNull="必填" Text="{@channelpath}/{@year02}/{@month}/{@day}/{@hour}/{@contentid}" Width="400" HelpText="支持变量：<br/>{@year02}、{@year04}、{@month}、{@day} 年月日<br/>{@hour}、{@minute}、{@second} 时分秒<br/>{@channelid}、{@contentid}频道ID、内容ID<br/>{@channelpath} 频道目录<br/>{@ram9} 生成随机数,可设置1到9"/></td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help ID="Help3" runat="server" HelpText="只有当用户处于本列表中的 IP 地址时才可以访问本频道及频道下内容, 列表以外的地址访问将视为 IP 被禁止. 每个 IP 一行, 例如 '192.168.*.*'(不含引号) 可匹配 192.168.0.0~192.168.255.255 范围内的所有地址, 留空为所有 IP 除明确禁止的以外均可访问" Text="IP访问列表"/>：</td>
	                        <td><cc1:TextBox runat="server" ID="txtIpaccess" TextMode="MultiLine" Height="50" Width="400"/></td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help ID="Help4" runat="server" Text="IP禁止访问列表" HelpText="当用户处于本列表中的 IP 地址时将禁止访问本频道及频道下内容. 每个 IP 一行, 例如 '192.168.*.*'(不含引号) 可匹配 192.168.0.0~192.168.255.255 范围内的所有地址"/>：</td>
	                        <td><cc1:TextBox runat="server" ID="txtIpdenyaccess" TextMode="MultiLine" Height="50" Width="400"/></td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help runat="server" Text="SEO标题" HelpText="针对搜索引擎设置页面标题"/>：</td>
	                        <td><cc1:TextBox runat="server" ID="txtPageTitle" Width="500"/></td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help runat="server" Text="页面关键字" HelpText="针对搜索引擎设置页面关键字"/>：</td>
	                        <td><cc1:TextBox runat="server" ID="txtKeywords" TextMode="MultiLine" Width="500" Height="70"/></td>
                        </tr>
                        <tr>
	                        <td class="itemtitle"><cc1:Help runat="server" Text="页面描述" HelpText="针对搜索引擎设置页面描述"/>：</td>
	                        <td><cc1:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Height="70" Width="500"/></td>
                        </tr>
		            </table>
                 </div>
            </div>
            <div class="cont-3" style="display:none;">
                <div class="box" style="padding:5px;">
                <div class="inner-pad-1"><cc1:CheckBox runat="server" ID="IsFiterDangerousCode" Checked="false" HelpText="如果内容是从其他网站复制的，过滤隐藏脚本" Text="过滤不安全代码" />
                <cc1:CheckBox Checked="false" ID="cbIsRemote" runat="server" HelpText="把内容里的资源下载到本地服务器,建议勿下载有大件的资源否则保存会比较慢" Text="下载远程资源"/> 
                <span style="display:none;" id="spanRFT">类型：<cc1:TextBox ID="txtResourceFType" Text="gif,jpg,png,bmp,jpeg" runat="server" Width="120px"/></span>
                 <cc1:CheckBox Checked="false" ID="cbFiltertags" runat="server" HelpText="过滤HTML标签,每个标签请用半角逗号隔开" Text="过滤标签" />：<cc1:TextBox ID="txtFiltertags" runat="server" Width="170" Text="iframe,object,font"/>&nbsp;</div>
                    <script type="text/javascript" src="../../sta/editor/ckeditor/<%=browser.IndexOf("ie")>=0?"3.6.3":"4.1.1"%>/ckeditor.js"></script> 
                    <cc1:TextBox runat="server" ID="txtContent" TextMode="MultiLine"/>
                    <script type="text/javascript">
                        CKEDITOR.replace('txtContent', config.editorset );
                    </script> 
                </div>
            </div>
            <div class="cont-3" style="display:none;">
                <table style="margin:10px 0 0 0;">
                    <tr>
			            <td width="1%">&nbsp;</td>
                        <td width="1%">&nbsp;</td>
			            <td width="15%">&nbsp;</td>
                        <td><input type="checkbox" id="c1" onclick="selByCheckName('viewchl',this.checked)"/><label for="c1">浏览频道</label></td>
                        <td><input type="checkbox" id="c2" onclick="selByCheckName('viewchlcons',this.checked)"/><label for="c2">浏览频道内容</label></td>
                    </tr>
<asp:Literal ID="powerset" runat="server"></asp:Literal>
                </table>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <input type="hidden" runat="server" id="hidParentId" />
            <input type="hidden" runat="server" id="hidViewchl" />
            <input type="hidden" runat="server" id="hidViewchlcons" />
            <cc1:Button runat="server" ID="SaveInfo" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/state2.gif" Text=" 保 存 频 道"/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" OnClientClick="location.href='global_channelist.aspx'" Text=" 返 回 " />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
<script type="text/javascript">
    $("#cbIsRemote").change(function () { $("#spanRFT").css("display", $(this).attr("checked") ? "" : "none"); }).trigger("change"); ;
    RegPreviewImg("#txtImg", "#previewImg");
    RegSelectFilePopWin("selectimgfile", "频道图片选择", "root=<%=filesavepath%>&filetype=jpeg,jpg,gif,png,bmp&fullname=1&cltmed=1&fele=txtImg", "click",$("#txtImg"));
    RegSelectFilePopWin("selectcovertem", "封面模版选择", "root=templates,<%=templatename%>,<%=config.Templatesavedirname%>&filetype=htm&fullname=0&cltmed=1&retsux=0&fele=txtCoverTem&rename=0&query=channelindex_", "click", $("#txtCoverTem"));
    RegSelectFilePopWin("selectlisttem", "列表模版选择", "root=templates,<%=templatename%>,<%=config.Templatesavedirname%>&filetype=htm&fullname=0&cltmed=1&retsux=0&fele=txtListTem&rename=0&query=channellist_", "click", $("#txtListTem"));
    RegSelectFilePopWin("selectcontem", "内容模版选择", "root=templates,<%=templatename%>,<%=config.Templatesavedirname%>&filetype=htm&fullname=0&cltmed=1&retsux=0&fele=txtConTem&rename=0&query=content_", "click", $("#txtConTem"));
    $(function () {
        if ($("#hidAction").val() == "edit") {
            var viewchl = $("#hidViewchl").val(), viewchlcons = $("#hidViewchlcons").val();
            setCheckboxChecked("viewchl", viewchl);
            setCheckboxChecked("viewchlcons", viewchlcons);
        }
    });
    function setCheckboxChecked(name, values) {
        $.each(values.split(","), function (idx, obj) {
            if (obj.trim() == "") return;
            $("input[name='" + name + "'][value='" + obj + "']").attr("checked", true);
        });
    }
</script>
</body>
</html>
