<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderset.aspx.cs" Inherits="STA.Web.Admin.Pay.orderset" %>
<%@ Register TagPrefix="cc1" Namespace="STA.Control" Assembly="STA.Control" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="STA.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <title>修改订单</title>
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
  
            <div class="conb-2">
            <div class="bar">
                &nbsp;&nbsp;订单修改</div>
            <div class="con">
                <table>
                    <tr>
                        <td colspan="2">
                            <table>
					            <tr>
						            <td class="itemtitle6">订单状态:</td>
						            <td>
                                        <cc1:RadioButtonList runat="server" RepeatDirection="Horizontal" RepeatColumns="10" ID="rblStatus"></cc1:RadioButtonList>
                                    </td>
					            </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%">
                            <table>
                                <tr>
						            <td class="itemtitle6">订单号码:</td>
						            <td><%=orderinfo.Oid%></td>
                                </tr>
                                <tr>
						            <td class="itemtitle6">付款方式:</td>
						            <td>
                                        <cc1:DropDownList runat="server" ID="ddlPids" Width="200"></cc1:DropDownList>
                                    </td>
                                </tr>
                                <tr>
						            <td class="itemtitle6">订单用户:</td>
                                     <td><%=orderinfo.Username%>元</td>
                                </tr>
                                <tr>
						            <td class="itemtitle6">购买总数:</td>
						            <td><%=orderinfo.Cartcount%>元</td>
                                </tr>
                                <tr>
						            <td class="itemtitle6">开具发票:</td>
						            <td><%=orderinfo.Isinvoice == 1? ("抬头:"+ orderinfo.Invoicehead):"不要发票"%> (费用:<%=orderinfo.Invoicecost%>元)</td>
                                </tr>
                                <tr>
						            <td class="itemtitle6">商品价格:</td>
						            <td><%=orderinfo.Price%>元</td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%">
                            <table>
					            <tr>
						            <td class="itemtitle6">下单日期:</td>
						            <td><%=orderinfo.Addtime%></td>
					            </tr>
                                <tr>
						            <td class="itemtitle6">配送方式:</td>
						            <td><%=deliveryinfo.Name%></td>
                                </tr>
                                <tr>
						            <td class="itemtitle6">快递单号:</td>
						            <td><cc1:TextBox runat="server" ID="txtTracknum" Width="120" HelpText="如果已发货,在这里填写快递单号,便于用户查询"></cc1:TextBox></td>
                                </tr>
                                <tr>
						            <td class="itemtitle6">来源IP:</td>
						            <td><%=orderinfo.Ip%></td>
                                </tr>
					            <tr>
						            <td class="itemtitle6">运费价格:</td>
						            <td><%=orderinfo.Dprice%>元</td>
					            </tr>
                             <tr>
						            <td class="itemtitle6">总金额:</td>
						            <td><cc1:TextBox runat="server" ID="txtTotalprice" Width="70" RequiredFieldType="金额" CanBeNull="必填"></cc1:TextBox> 元</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
					            <tr>
						            <td class="itemtitle6" style=" vertical-align:top;">备注信息:</td>
						            <td>
                                       <cc1:TextBox runat="server" TextMode="MultiLine" Height="70" Width="450" ID="txtRemark"></cc1:TextBox>
                                    </td>
					            </tr>
					            <tr> 
						            <td class="itemtitle6">&nbsp;</td>
                                    <td><cc1:Button runat="server" ID="btn_EditOrder" Text="提交修改订单"/>
                                        &nbsp;
                                        <cc1:Button runat="server" ID="Button1" AutoPostBack="false" OnClientClick="location.href='orderlist.aspx'" Text="返回订单列表"/>
                                    </td>
					            </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

            <div class="conb-2">
            <div class="bar">
                &nbsp;&nbsp;收货人信息</div>
            <div class="con">
                <table>
                    <tr>
                        <td width="50%">
                            <table>
                                <tr>
						            <td class="itemtitle6">收货姓名:</td>
						            <td><%=useraddressinfo.Username.Trim()%>（<%=useraddressinfo.Parms.Split(',')[1]=="1"?"男":"女"%>）</td>
                                </tr>
                                <tr>
						            <td class="itemtitle6">电子邮件:</td>
						            <td><%=useraddressinfo.Email%></td>
                                </tr>

                            </table>
                        </td>
                        <td width="50%">
                            <table>
                                <tr>
						            <td class="itemtitle6">联系电话:</td>
						            <td><%=useraddressinfo.Phone%></td>
                                </tr>
                                <tr>
						            <td class="itemtitle6">邮政编码:</td>
						            <td><%=useraddressinfo.Postcode%></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
					            <tr>
						            <td class="itemtitle6">详细地址:</td>
						            <td>
                                      <%=useraddressinfo.Address%>
                                    </td>
					            </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

            <div class="conb-2">
            <div class="bar">
                &nbsp;&nbsp;订单产品</div>
            <div class="con">
                <asp:Repeater ID="prodData" runat="server">
                    <HeaderTemplate>
                        <table class="list">
                            <tr>
                                <th style="text-align:center" width="110">
                                    商品图片
                                </th>
                                <th>
                                    商品名称
                                </th>
                                <th>
                                    所属分类
                                </th>
                                <th>
                                    市场价(元)
                                </th>
                                <th>
                                    购买价(元)
                                </th>
                                <th>
                                    购买数量
                                </th>
                                <th>
                                   合计(元)
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center" width="110">
                                <span class="conimg" url="<%#((DataRowView)Container.DataItem)["img"]%>"></span>
                            </td>
                            <td>
                                <span class="ptip" onclick="window.open('../tools/view.aspx?id=<%#((DataRowView)Container.DataItem)["id"]%>&tid=<%#((DataRowView)Container.DataItem)["typeid"]%>&name=content','_blank')"><%#((DataRowView)Container.DataItem)["title"]%></span>
                            </td>
                            <td class="chlname" cid="<%#((DataRowView)Container.DataItem)["channelid"]%>">
                                <%#((DataRowView)Container.DataItem)["channelname"]%>
                            </td>
                            <td>
                                <%#string.Format("{0:C}",(((DataRowView)Container.DataItem)["ext_price"]).ToString())%>
                            </td>
                            <td>
                                <%#string.Format("{0:C}",(((DataRowView)Container.DataItem)["price"]).ToString())%>
                            </td>
                            <td>
                               <%#((DataRowView)Container.DataItem)["buynum"]%>
                            </td>
                            <td>
                               <%#(TypeParse.StrToDecimal(((DataRowView)Container.DataItem)["price"]) * TypeParse.StrToInt(((DataRowView)Container.DataItem)["buynum"])).ToString("0.00")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>

        </div>

        <div id="footer">
            <%=footer%>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        RegColumnImg(".conimg");
        RegColumnPostip(".ptip", 50, "..");
        $(".chlname").each(function () { var cid = $(this).attr("cid"); if (cid == "0") { $(this).html("未分类") } });
    </script>
</body>
</html>