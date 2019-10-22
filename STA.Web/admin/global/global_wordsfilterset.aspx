<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_wordsfilterset.aspx.cs" Inherits="STA.Web.Admin.wordsfilterset" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Register Src="../controls/Pager.ascx" TagName="PageGuide" TagPrefix="ucl" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <title>词语过滤</title>
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
            <cc1:PageInfo ID="PageInfo1" runat="server" Text="替换前的内容可以使用限定符 {x} 以限定相邻两字符间可忽略的文字，x 是忽略字符的个数。如 'sh{1}i{2}t '(不含引号) 可以过滤 'shit' 也可过滤 'shxxit' 和 'shoxixt' 等等。查询的内容和替换的内容的最大长度为255个字符。<br/>如需禁止发布包含某个词语的文字，而不是替换过滤，请将其对应的替换内容设置为{BANNED}即可；如需当用户发布包含某个词语的文字时，自动标记为需要人工审核，而不直接显示或替换过滤，请将其对应的替换内容设置为{MOD}即可。设置 '{BANNED}' 或 '{MOD}' 请务必使用大写字母。"/>
            <div class="conb-1">
                <div class="bar">过滤设置</div>
                <div class="con">
                    <table>
	                    <tr><td class="itemtitle2" colspan="2">屏蔽特殊字符</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                   <cc1:TextBox runat="server" ID="txtAntispamreplacemen" Height="80" TextMode="MultiLine"/>
                            </td>
		                    <td class="vtop txt_desc">某些广告发布者会将关键字用各种特殊字符拆开分割，造成过滤程序无法正确识别，<br />
如果将特殊字符填写在下面的文本框当中，则过滤程序会在过滤之前，首先屏蔽文本框内的字符，让不良关键词无所遁形！<br />
填写方法:直接填写即可，字符直接无需任何标识风格。比如“*&^%$#”(不包括双引号)                            </td>
	                    </tr> 
	                    <tr><td class="itemtitle2" colspan="2">批量添加过滤</td></tr>
	                    <tr>
		                    <td class="vtop rowform">
			                   <cc1:TextBox runat="server" ID="txtWords" Height="80" TextMode="MultiLine"/>
                                  <cc1:RadioButtonList id="rblFilter" runat="server"  RepeatColumns="1" RepeatLayout="flow">
	                                <asp:ListItem Value="0">清空当前词表后导入新词语，此操作不可恢复</asp:ListItem>
	                                <asp:ListItem Value="1">使用新的设置覆盖已经存在的词语</asp:ListItem>
	                                <asp:ListItem Value="2" Selected="true">不导入已经存在的词语</asp:ListItem>
                                 </cc1:RadioButtonList> 
                            </td>
		                    <td class="vtop txt_desc">每行一组过滤词语，不良词语和替换词语之间使用“=”进行分割;<br />
如果只是想将某个词语直接替换成 **,则只输入词语即可;                            </td>
	                    </tr> 
                    </table>
                </div>
            </div>
            <div class="navbutton" style="margin-bottom:15px">
                <cc1:Button runat="server" ID="SaveInfo" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/state2.gif" Text=" 提 交 配 置 "/>
            </div>
            <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;过滤词语</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th width="40">
                                            <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" />
                                        </th>
                                        <th>
                                            提交人
                                        </th>
                                        <th>
                                            替换词
                                        </th>
                                        <th>
                                            替换为
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                      <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelBtn')" name="cbid" value="<%#((DataRowView)Container.DataItem)["id"]%>" />
                                    </td>
                                    <td>
                                        <%#((DataRowView)Container.DataItem)["username"]%>
                                    </td>
                                    <td>
                                        <input type="hidden" name="iid" value="<%#((DataRowView)Container.DataItem)["id"]%>"/>
                                         <input type="text" class="txt" onfocus="this.className='txt_focus';" value="<%#((DataRowView)Container.DataItem)["find"]%>"
                                            onblur="this.className='txt';" style="width: 200px;" name="txtFind<%#((DataRowView)Container.DataItem)["id"]%>" />
                                    </td>
                                    <td>
                                        <input type="text" class="txt" onfocus="this.className='txt_focus';" value="<%#((DataRowView)Container.DataItem)["replacement"]%>"
                                            onblur="this.className='txt';" style="width: 200px;" name="txtReplacement<%#((DataRowView)Container.DataItem)["id"]%>" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        <ucl:PageGuide ID="pGuide" runat="server" />
                    </div>
                </div>
            <div class="operate">
                <cc1:Button ID="BtnSaveWords" runat="server" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/state2.gif" Text=" 保存过滤词修改" />
                <cc1:Button runat="server" Enabled="false" ID="DelBtn" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/del.gif"  AutoPostBack="false" OnClientClick="ControlPostBack('DelBtn','确认删除所选吗？');return;" Text=" 删除过滤词" />
                <cc1:Button ID="Button1" AutoPostBack="false" runat="server" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/add.gif" Text=" 导出过滤词" OnClientClick="location.href = '../tools/contdo.aspx?t=downloadword'"/>
                <cc1:Button ID="BtnCreateWord" AutoPostBack="false" runat="server" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/add.gif" Visible="false" Text=" 新建过滤词" />
            </div>
        </div>
        <input type="hidden" id="hidAction" runat="server" value="" />
        <input type="hidden" id="hidValue" runat="server" value="" />
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'DelBtn')
        }
    </script>
</body>
</html>