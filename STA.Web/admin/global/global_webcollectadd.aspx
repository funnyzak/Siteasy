<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_webcollectadd.aspx.cs" Inherits="STA.Web.Admin.webcollectadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>采集规则添加</title>
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
                <div class="bar">站点采集规则添加</div>
                <div class="con">
                    <table>
                         <tr>
                            <td class="itemtitle">
                                入库频道：
                            </td>
                            <td>
                                <cc1:DropDownTreeList runat="server" ID="ddlConType" Width="140"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                规则名称：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtName" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                默认状态：
                            </td>
                            <td>
                               <cc1:RadioButtonList RepeatDirection="Horizontal" RepeatColumns="5" Width="500" runat="server" ID="rblStatus"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                主机地址：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtHosturl" HelpText="如果目标网站的链接格式为绝对路径(非全路径)则必须填写主机地址. 填写格式如:http://www.stacms.com,尾部不加/"/>
                                <a href="javascript:void(0);" onclick="OpenInputLink('#txtHosturl')">打开</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                网页编码：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtEncode" CanBeNull="必填" Text="UTF-8" HelpText="采集页面所使用的页面编码" Width="100"/>
                                <a href="javascript:;" class="setvalue">UTF-8</a>&nbsp;
                                <a href="javascript:;" class="setvalue">GB2312</a>&nbsp;
                                <a href="javascript:;" class="setvalue">GBK</a>&nbsp;
                                <a href="javascript:;" class="setvalue">BIG5</a>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                采集方式：
                            </td>
                            <td>
                               <cc1:RadioButtonList RepeatDirection="Horizontal" RepeatColumns="5" Width="500" runat="server" ID="rblCletype"/>
                            </td>
                        </tr>
                        <tr cletype='3'>
                            <td class="itemtitle">
                                单页面地址：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtCurl" Width="400"/>
                                <a href="javascript:void(0);" onclick="OpenInputLink('#txtCurl')">打开</a>
                            </td>
                        </tr>
                        <tr cletype='2'>
                            <td class="itemtitle">
                                网址集合：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtCurls" Width="400" TextMode="MultiLine" Height="100" HelpText="网址列表集合，每个网站请用换行(回车)隔开"/>
                            </td>
                        </tr>
                        <tr cletype='1'>
                            <td class="itemtitle">
                                索引分页设置：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtClisturl" HelpText="如：http://www.stacms.com/channel5_@page.html,其中@page表示页码"/>&nbsp;&nbsp;
                                从第 <cc1:TextBox runat="server" ID="txtPagestart" Width="30" Text="1"/> 页, 到
                                第 <cc1:TextBox runat="server" ID="txtPageend" Width="30" Text="2"/> 页
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                采集选项：
                            </td>
                            <td>
                               <cc1:CheckBox runat="server" ID="cbDownImg" Checked="true" Text="下载图片" HelpText="是否下载内容里的图片到本地服务器"/>&nbsp;
                               <cc1:CheckBox runat="server" ID="cbFilterRepeat" Checked="true" Text="过滤重复" HelpText="当采集内容标题与站内文档标题重复,是否过滤"/>&nbsp;
                               <cc1:CheckBox runat="server" ID="cbFilterempty" Checked="true" Text="过滤空内容" HelpText="当采集页的内容为空,是否过滤"/>&nbsp;
                               <cc1:CheckBox runat="server" ID="cbCSort" Checked="true" Text="倒序采集" HelpText="采用倒序采集可以保持和被采集的文档列表顺序一致"/>&nbsp;
                               <cc1:CheckBox runat="server" ID="cbFilterpage" Checked="false" Text="忽略分页" HelpText="是否采集带文档分页的页面. 开启此选项必须设置分页匹配规则,否则此选项失效"/>&nbsp;
                               <cc1:CheckBox runat="server" ID="cbFirstimg" Text="首图作为文档图片" HelpText="如果内容中存在图片,即将内容中的第一张图片作为文档图片"/>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                链接列表匹配规则：
                            </td>
                            <td>
                                <div class="description">说明：以下参数匹配设置,参数请用“[匹配内容]”代替,可变内容用“[变量]”代替.</div>
                                <cc1:TextBox runat="server" ID="txtSTurllist" CanBeNull="必填" Width="500" AlignY="bottom" HelpText="即为匹配的内容链接列表" TextMode="MultiLine" Height="70"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                单条链接匹配规则：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" CanBeNull="必填" ID="txtSTurl" Width="500" AlignY="bottom" HelpText="从内容链接列表匹配内容链接" TextMode="MultiLine" Height="70"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                标题匹配规则：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtSTtitle" Width="500" TextMode="MultiLine" Height="70" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                来源匹配规则：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtSTsource" Width="500" TextMode="MultiLine" Height="70"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                作者匹配规则：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtSTauthor" Width="500" TextMode="MultiLine" Height="70"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                添加时间匹配规则：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtSTaddtime" Width="500" TextMode="MultiLine" Height="70"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                内容匹配规则：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtSTcontent" Width="500" TextMode="MultiLine" Height="70" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                内容过滤选项：
                            </td>
                            <td>
                               <cc1:CheckBoxList runat="server" Width="530" ID="chbFilter" RepeatColumns="7" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="script">脚本script</asp:ListItem>
                                    <asp:ListItem Value="object">对象object</asp:ListItem>
                                    <asp:ListItem Value="iframe">框架iframe</asp:ListItem>
                                    <asp:ListItem Value="a">链接a</asp:ListItem>
                                    <asp:ListItem Value="br">换行br</asp:ListItem>
                                    <asp:ListItem Value="table">表格table</asp:ListItem>
                                    <asp:ListItem Value="tbody">表格体tbody</asp:ListItem>
                                    <asp:ListItem Value="tr">表格行tr</asp:ListItem>
                                    <asp:ListItem Value="td">单元td</asp:ListItem>
                                    <asp:ListItem Value="font">字体font</asp:ListItem>
                                    <asp:ListItem Value="div">层div</asp:ListItem>
                                    <asp:ListItem Value="span">行span</asp:ListItem>
                                    <asp:ListItem Value="img">图象img</asp:ListItem>
                                   <%-- <asp:ListItem Value="space">空格</asp:ListItem>--%>
                               </cc1:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                内容其他过滤规则：
                            </td>
                            <td>
                                  <cc1:TextBox runat="server" ID="txtConfilter" Width="500" TextMode="MultiLine" Height="70"/>
                                  <br />格式如：&lt;item match="[a-z]*" replace=""/&gt;可设置多个.
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                分页匹配规则：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" HelpText="分页采集只支持全部列出的分页列表" AlignY="bottom" ID="txtSTconpage" Width="500" TextMode="MultiLine" Height="70"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                分页链接匹配规则：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtSTconpageurl" Width="500" TextMode="MultiLine" Height="70"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <textarea id="likeidlist" runat="server" style="display:none;"/>
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 规 则 "/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " OnClientClick="location.href = 'global_webcollect.aspx'" />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("input[name='rblCletype']").click(function () {
            $("tr[cletype]").css("display", "none");
            $("tr[cletype='" + $(this).val() + "']").css("display", "");
        });
        $("input[name='rblCletype']:checked").trigger("click");
        RegSetTargetValue($(".setvalue"), $("#txtEncode"));
    </script>
</body>
</html>