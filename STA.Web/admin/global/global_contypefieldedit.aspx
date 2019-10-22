<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_contypefieldedit.aspx.cs" Inherits="STA.Web.Admin.contypefieldedit" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>模型表字段管理</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../styles/base.css" type="text/css" rel="stylesheet" />
    <link href="../themes/<%=systyle%>/style.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/jqueryui/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript" src="../plugin/scripts/poshytip/poshytip.js"></script>
    <script language="javascript" type="text/javascript" src="../js/public.js"></script>
</head>
<body>
    <div id="wrapper">
        <form id="form1" runat="server">
        <div id="mwrapper">
            <div id="main">
                <div class="conb-2">
                    <div class="bar">
                        &nbsp;&nbsp;模型表(<span class="red"><%=cinfo.Name%></span>)字段管理</div>
                    <div class="con">
                        <asp:Repeater ID="rptData" runat="server">
                            <HeaderTemplate>
                                <table class="list">
                                    <tr>
                                        <th>
                                            名称
                                        </th>
                                        <th>
                                            字段名
                                        </th>
                                        <th>
                                            数据类型
                                        </th>
                                        <th>
                                            排序
                                        </th>
                                        <th width="100">
                                            操作
                                        </th>
                                        <th width="60">
                                            <input type="checkbox" name="checkall" onclick="CheckAll(this.form,this.checked);" /> 选择
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                      <%#Eval("desctext")%>
                                    </td>
                                    <td>
                                      <%#Eval("fieldname")%>
                                    </td>
                                    <td>
                                      <%#Eval("fieldtype")%>
                                    </td>
                                    <td>
                                        <input type="hidden" name="hidid" value="<%#Eval("id")%>" />
                                        <input type="text" class="txt" onfocus="this.className='txt_focus';" value="<%#Eval("orderid")%>"
                                            onblur="this.className='txt';" style="width: 50px;" name="txtOrderId<%#Eval("id")%>" />
                                    </td>
                                    <td>
                                        <a href="global_contypefieldadd.aspx?action=edit&id=<%#Eval("id")%>&name=<%=cinfo.Name%>&cid=<%=cinfo.Id%>">编辑</a>
                                        <a href="javascript:void(0)" onclick="Del('<%#Eval("id")%>')">删除</a>
                                    </td>
                                    <td>
                                        <input type="checkbox" onclick="CheckedEnabledButton(this.form,'cbid','DelFieldBtn','BulidBtn')" name="cbid" value="<%#Eval("id")%>" />
                                        <input type="hidden" name="cfieldname<%#Eval("id")%>" value="<%#Eval("fieldname")%>" />
                                        <input type="hidden" name="cfieldtype<%#Eval("id")%>" value="<%#Eval("fieldtype")%>" />
                                        <input type="hidden" name="csize<%#Eval("id")%>" value="<%#Eval("length")%>" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <input type="hidden" id="hidAction" runat="server" value="" />
                <input type="hidden" id="hidcname" runat="server" value="<%=info.Name%>" />
                <input type="hidden" id="hidValue" runat="server" value="" />
                <div class="operate">
                    <cc1:Button ID="Button6" runat="server" AutoPostBack="false" ButtontypeMode="WithImage" ButtonImgUrl="../images/icon/add.gif" Text=" 新增字段" />
                    <cc1:Button ID="DelFieldBtn" runat="server" OnClientClick="ControlPostBack('DelFieldBtn', '确认删除吗?');return;" Text="删除字段" Enabled="false"/>
                    <cc1:Button ID="BulidBtn" runat="server" Text="重建字段" Enabled="false"/>
                    <cc1:Button ID="RebulidFieldBtn" runat="server" Text="重建所有字段" /> 
                    <cc1:Button ID="SubmitEditBtn" runat="server" Text="保存" />
                    <cc1:Button ID="Button1" runat="server" AutoPostBack="false" OnClientClick="location.href='global_contypes.aspx'" Text="返回" />
                </div>
            </div>
            <div id="footer">
                <%=footer %>
            </div>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        $("#Button6").click(function () { location.href = "global_contypefieldadd.aspx?name=<%=cinfo.Name%>&cid=<%=cinfo.Id%>"; });
        function CheckAll(form, checked) {
            CheckByName(form, 'cbid', checked);
            CheckedEnabledButton(form, 'cbid', 'BulidBtn', 'DelFieldBtn')
        }
        function Del(id) {
            SConfirm(function () {
                SubmitForm("delfield", id);
            }, "确认删除吗，删除后与此关联的频道下的内容相应的字段也将删除？");
        }
    </script>
</body>
</html>
