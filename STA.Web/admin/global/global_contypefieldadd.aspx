<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_contypefieldadd.aspx.cs" Inherits="STA.Web.Admin.contypefieldadd" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>模型表字段添加</title>
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
                <div class="bar">模型表(<%=STA.Common.STARequest.GetString("name")%>)字段添加</div>
                <div class="con">
                    <table>
                        <tr>
                            <td class="itemtitle">
                               允许为空：
                            </td>
                            <td>
                                <cc1:RadioButtonList RepeatDirection="Horizontal" RepeatColumns="3" Width="300" runat="server"
                                    ID="rblNull">
                                    <asp:ListItem Text="不允许" Value="0" />
                                    <asp:ListItem Text="允许" Selected="True" Value="1" />
                                </cc1:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                显示名称：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" HelpText="字段所表示的名称" ID="txtDesctext" CanBeNull="必填"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                字段名：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" ID="txtFieldName" CanBeNull="必填" HelpText="字段名由英文、数字或下划线组成"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                数据类型：
                            </td>
                            <td>
                                <cc1:DropDownList runat="server" ID="ddlFieldType">
                                    <asp:ListItem Text="文本(char)" Value="char" />
                                    <asp:ListItem Text="文本(nchar)" Value="nchar" />
                                    <asp:ListItem Text="文本(varchar)" Value="varchar" />
                                    <asp:ListItem Text="文本(nvarchar)" Value="nvarchar" />
                                    <asp:ListItem Text="内容(ntext)" Value="ntext" />
                                    <asp:ListItem Text="整数(int)" Value="int" />
                                    <asp:ListItem Text="带小数点(decimal)" Value="decimal(18,2)" />
                                    <asp:ListItem Text="时间日期(datetime)" Value="datetime" />
                                    <asp:ListItem Text="使用option下拉框" Value="select" />
                                    <asp:ListItem Text="使用radio选项卡" Value="radio" />
                                    <asp:ListItem Text="文件选择" Value="selectfile" />
                                    <asp:ListItem Text="联动类型" Value="stepselect" />
                                    <asp:ListItem Text="Checkbox多选框" Value="checkbox" />
                                    <asp:ListItem Text="所见即所得编辑器" Value="editor" />
                                </cc1:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                字段长度：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" Width="100" RequiredFieldType="数据校验" ID="txtLength" CanBeNull="必填" Text="20" HelpText="文本数据必须填写，大于8000(char,nchar)、大于4000(varchar,nvarchar)将转为text类型"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                字段权重：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" Width="100" RequiredFieldType="数据校验" ID="txtOrderId" CanBeNull="必填" Text="0"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                提示内容：
                            </td>
                            <td>
                                <cc1:TextBox runat="server"  Width="400"  ID="txtTipText" HelpText="在输入的数据的时候，提示的内容"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                默认值：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" TextMode="MultiLine" Width="400" Height="50" ID="txtDefValue" HelpText="如果数据类型为select、radio、checkbox时，(用英文“,”分开，如“同事,同学,家人”)，如果为联动选框，这里填写联动选项值。"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="itemtitle">
                                自定义表单：
                            </td>
                            <td>
                                <cc1:TextBox runat="server" TextMode="MultiLine" Width="400" Height="100" ID="txtVinnertext" HelpText="自定义表单HTML:~name~字段显示名称,~field~字段名,~text~值,此三个参数必须在自定义表单定义"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="navbutton">
            <input type="hidden" runat="server" id="hidId" />
            <input type="hidden" runat="server" id="hidAction" />
            <input type="hidden" runat="server" id="hidFieldname" />
            <cc1:Button runat="server" ID="SaveInfo" Text=" 保 存 字 段"/>
            <cc1:Button ID="GoBack" runat="server" AutoPostBack="false" Text=" 返 回 " />
        </div>
        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $("#GoBack").click(function () { location.href = "global_contypefieldedit.aspx?id=<%=STA.Common.STARequest.GetString("cid")%>"; });
    </script>
</body>
</html>