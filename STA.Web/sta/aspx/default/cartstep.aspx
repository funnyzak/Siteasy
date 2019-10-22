<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.Cartstep" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="STA.Common" %>
<%@ Import namespace="STA.Data" %>
<%@ Import namespace="STA.Core" %>
<%@ Import namespace="STA.Entity" %>
<%@ Import namespace="STA.Config" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Siteasy CMS Template Engine at 2019/10/22 21:38:16.
		本页面代码由Siteasy CMS模板引擎生成于 2019/10/22 21:38:16. 
	*/

	base.OnInit(e);


	int item__loop__id=0;
	int gp__loop__id=0;
	int im__loop__id=0;
	int i__loop__id=0;
	int citem__loop__id=0;
	int oitem__loop__id=0;


	DataTable list;
	DataTable pht;
	DataTable ls;
	DataTable lts;

	templateBuilder.Capacity = 220000;
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>核对订单信息 - ");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("</title>\r\n<meta name=\"description\" content=\"核对订单信息,");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\">\r\n<meta name=\"keywords\" content=\"核对订单信息,");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\">\r\n<meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\" />\r\n<link rel=\"shortcut icon\" href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/favicon.ico\" type=\"image/x-icon\" />\r\n<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/css/stacms.css\" type=\"text/css\" />\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/jquery.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/blockUI.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/config.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/common.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/js/global.js\"></");
	templateBuilder.Append("script>\r\n</head>\r\n<body>          \r\n<div class=\"vt_wrapper\">\r\n    ");
	templateBuilder.Append("    <div class=\"header\">\r\n    	<div class=\"left\">\r\n            <a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\"  title=\"");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\" target=\"_self\"><img title=\"");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\" src=\"");
	templateBuilder.Append(tempurl.ToString());
	templateBuilder.Append("/images/logo.png\" /></a>\r\n        </div>\r\n        <div class=\"right cgrey\">\r\n            ");
	if (userid>0)
	{

	templateBuilder.Append("\r\n                您好,<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/\">");
	templateBuilder.Append(oluser.Nickname.ToString().Trim());
	templateBuilder.Append("</a>&nbsp;&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/\">个人中心</a>&nbsp;|&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/user/profile.aspx\">我的资料</a>&nbsp;|&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/useraction.aspx?action=loginout\">退出登录</a>\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n                欢迎来到");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append(", 您可以&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/login.aspx?returnurl=");templateBuilder.Append(Utils.UrlEncode(cururl));
	templateBuilder.Append("\">登录</a>&nbsp;或&nbsp;<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/register.aspx\">注册</a>\r\n            ");
	}	//end if

	templateBuilder.Append("\r\n        </div>\r\n    </div>");

	templateBuilder.Append("\r\n    <div class=\"votetit\">\r\n    	核对订单信息\r\n    </div>\r\n    <form method=\"post\" action=\"\">\r\n    <div class=\"votecon\">\r\n        <input type=\"hidden\" value=\"\" name=\"adrid\" id=\"adrid\"/>\r\n        <table class=\"step\">\r\n        	<tr>\r\n            	<td class=\"cur\">下单购买</td>\r\n                <td>买家付款</td>\r\n                <td>确认收货</td>\r\n                <td>完成交易</td>\r\n           </tr>\r\n        </table>\r\n        <table class=\"cart olist\">\r\n        	<tr class=\"caption\">\r\n            	<th>&nbsp;&nbsp;<b>收货人信息</b> <a href=\"javascript:;\" class=\"s13 editaddress\">[修改]</a></th>\r\n            </tr>\r\n            <tr>\r\n            	<td>\r\n                	<div class=\"tbl-order deliveryinfo\">\r\n                    	<div class=\"row clearfix\">\r\n                        	<div class=\"left\">收货人姓名：</div>\r\n                            <div class=\"right dusername\"></div>\r\n                        </div>\r\n                    	<div class=\"row clearfix\">\r\n                        	<div class=\"left\">称呼：</div>\r\n                            <div class=\"right dgender\"></div>\r\n                        </div>\r\n                    	<div class=\"row clearfix\">\r\n                        	<div class=\"left\">收货地址：</div>\r\n                            <div class=\"right daddress\"></div>\r\n                        </div>\r\n                    	<div class=\"row clearfix\">\r\n                        	<div class=\"left\">邮编：</div>\r\n                            <div class=\"right dpostcode\"></div>\r\n                        </div>\r\n                    	<div class=\"row clearfix\">\r\n                        	<div class=\"left\">联系电话：</div>\r\n                            <div class=\"right dphone\"></div>\r\n                        </div>\r\n                    	<div class=\"row clearfix\">\r\n                        	<div class=\"left\">电子邮件：</div>\r\n                            <div class=\"right demail\"></div>\r\n                        </div>\r\n                    </div>\r\n                	<div class=\"tbl-order deliveryedit\" style=\"display:none;\">\r\n                     	<div class=\"row clearfix\">\r\n                        	<div class=\"left\">已有地址：</div>\r\n                            <div class=\"right\">\r\n                               <div class=\"addrlist\"></div>\r\n                               <input type=\"radio\" name=\"addrs\" value=\"0\" checked=\"checked\" id=\"addr_0\"/><label for=\"addr_0\" class=\"cblue\">添加新地址</label>\r\n                            </div>\r\n                        </div>\r\n                    	<div class=\"row clearfix\">\r\n                        	<div class=\"left\"><span class=\"cred\">*</span>收货人姓名：</div>\r\n                            <div class=\"right\"><input type=\"text\" name=\"username\" id=\"username\" class=\"ipt-com\"/></div>\r\n                        </div>\r\n                    	<div class=\"row clearfix\">\r\n                        	<div class=\"left\">称呼：</div>\r\n                            <div class=\"right\">\r\n                              <input type=\"radio\" name=\"gender\" value=\"1\" checked=\"checked\"/>先生&nbsp;\r\n                              <input type=\"radio\" name=\"gender\" value=\"0\"/>女生\r\n                            </div>\r\n                        </div>\r\n                    	<div class=\"row clearfix\">\r\n                        	<div class=\"left\">所在省份：</div>\r\n                            <div class=\"right\">\r\n								<input type='hidden' id='ext_nativeplace' name='ext_nativeplace' value='2502.11' />\r\n                                <span id='span_nativeplace'></span> <span id='span_nativeplace_son'></span> <span id='span_nativeplace_sec'></span>\r\n                                <script language='javascript' type='text/javascript' src='/sta/js/select.js'></");
	templateBuilder.Append("script>\r\n								<script language='javascript' type='text/javascript' src='/sta/data/select/nativeplace.js'></");
	templateBuilder.Append("script>\r\n								<script language='javascript' type='text/javascript'> MakeTopSelect('nativeplace', 0);</");
	templateBuilder.Append("script>\r\n                            </div>\r\n                        </div>\r\n                    	<div class=\"row clearfix\">\r\n                        	<div class=\"left\"><span class=\"cred\">*</span>收货地址：</div>\r\n                            <div class=\"right\"><input type=\"text\" name=\"address\" id=\"address\" class=\"ipt-com\" style=\"width:300px;\"></div>\r\n                        </div>\r\n                    	<div class=\"row clearfix\">\r\n                        	<div class=\"left\"><span class=\"cred\">*</span>邮编：</div>\r\n                            <div class=\"right\"><input type=\"text\" name=\"postcode\" id=\"postcode\" class=\"ipt-com\"/></div>\r\n                        </div>\r\n                    	<div class=\"row clearfix\">\r\n                        	<div class=\"left\"><span class=\"cred\">*</span>联系电话：</div>\r\n                            <div class=\"right\"><input type=\"text\" name=\"phone\" id=\"phone\" class=\"ipt-com\"/> <span class=\"cgrey\">电话和手机请至少填写一个</span></div>\r\n                        </div>\r\n                    	<div class=\"row clearfix\">\r\n                        	<div class=\"left\">电子邮件：</div>\r\n                            <div class=\"right\"><input type=\"text\" name=\"email\" id=\"email\" class=\"ipt-com\"/> <span class=\"cgrey\">用来接收此订单的最新状态邮件</span></div>\r\n                        </div>\r\n                    	<div class=\"row clearfix\">\r\n                        	<div class=\"left\"></div>\r\n                            <div class=\"right\"><input type=\"button\" class=\"com saveaddr\" value=\"保存收货地址\"/>\r\n                            &nbsp;&nbsp;&nbsp;<span class=\"cred errtip-addr\"></span>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </td>\r\n            </tr>\r\n        </table>\r\n        <table class=\"cart olist\">\r\n        	<tr class=\"caption\">\r\n            	<th colspan=\"2\">&nbsp;&nbsp;<b>配送方式</b></th>\r\n            </tr>\r\n            ");
	item__loop__id=0;
	foreach(DataRow item in delys.Rows)
	{
		item__loop__id++;

	templateBuilder.Append("\r\n             <tr>\r\n              <td style=\"width:230px;padding-left:45px;\">\r\n                 ");
	if (item__loop__id==1)
	{

	templateBuilder.Append("\r\n                <input type=\"radio\" name=\"did\" value=\"" + item["id"].ToString().Trim() + "\" checked=\"checked\" cost=\"" + item["cost"].ToString().Trim() + "\" id=\"did_" + item["id"].ToString().Trim() + "\"/>\r\n                ");
	}
	else
	{

	templateBuilder.Append("\r\n                <input type=\"radio\" name=\"did\" value=\"" + item["id"].ToString().Trim() + "\" cost=\"" + item["cost"].ToString().Trim() + "\" id=\"did_" + item["id"].ToString().Trim() + "\"/>\r\n                ");
	}	//end if

	templateBuilder.Append("\r\n                 <label for=\"did_" + item["id"].ToString().Trim() + "\">" + item["name"].ToString().Trim() + "</label> <span class=\"corange\">所需运费: " + item["cost"].ToString().Trim() + " 元</span>\r\n              </td>\r\n              <td style=\"width:645px;\" class=\"cgrey\">\r\n                    " + item["description"].ToString().Trim() + "\r\n              </td>\r\n            </tr>\r\n            ");
	}	//end loop

	templateBuilder.Append("\r\n        </table>\r\n        <table class=\"cart olist\">\r\n        	<tr class=\"caption\">\r\n            	<th colspan=\"2\">&nbsp;&nbsp;<b>支付方式</b></th>\r\n            </tr>\r\n            ");
	item__loop__id=0;
	foreach(DataRow item in payms.Rows)
	{
		item__loop__id++;


	if (item["isvalid"].ToString().Trim()=="1")
	{

	templateBuilder.Append("\r\n             <tr>\r\n              <td style=\"width:245px;padding-left:45px;\">\r\n                 ");
	if (item__loop__id==1)
	{

	templateBuilder.Append("\r\n                <input type=\"radio\" name=\"pid\" value=\"" + item["id"].ToString().Trim() + "\" checked=\"checked\" id=\"pid_" + item["id"].ToString().Trim() + "\"/>\r\n                ");
	}
	else
	{

	templateBuilder.Append("\r\n                <input type=\"radio\" name=\"pid\" value=\"" + item["id"].ToString().Trim() + "\" id=\"pid_" + item["id"].ToString().Trim() + "\"/>\r\n                ");
	}	//end if

	templateBuilder.Append("\r\n                 <label for=\"pid_" + item["id"].ToString().Trim() + "\">" + item["name"].ToString().Trim() + "</label>\r\n              </td>\r\n              <td style=\"width:630px;\" class=\"cgrey\">\r\n                    " + item["description"].ToString().Trim() + "\r\n              </td>\r\n            </tr>\r\n            ");
	}	//end if


	}	//end loop

	templateBuilder.Append("\r\n        </table>\r\n        <table class=\"cart olist olist-2\">\r\n        	<tr class=\"caption\">\r\n            	<th style=\"width:120px;text-align:center;\">商品编号</th>\r\n                <th>商品名称</th>\r\n                <th style=\"width:90px;\">市场价(元)</th>\r\n                <th style=\"width:90px;\">会员价(元)</th>\r\n                <th style=\"text-align:center;width:90px\">商品数量</th>\r\n            </tr>\r\n            ");
	item__loop__id=0;
	foreach(DataRow item in prods.Rows)
	{
		item__loop__id++;

	int pid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	string unit = item["ext_unit"].ToString().Trim().Split(' ')[0];
	
	templateBuilder.Append("\r\n            <tr>\r\n                <td style=\"text-align:center\">" + item["id"].ToString().Trim() + "</td>\r\n                <td>\r\n                	<div class=\"left\" style=\"width:58px;\"><a href=\"");
	templateBuilder.Append(Urls.Product(pid).ToString().Trim());
	templateBuilder.Append("\" target=\"_blank\"><img src=\"" + item["img"].ToString().Trim() + "\" width=\"50\" height=\"50\" style=\"border:1px solid #ccc;cursor:pointer;\" align=\"absmiddle\"/></a></div>\r\n                	<div class=\"left\" style=\"width:420px\"><a href=\"");
	templateBuilder.Append(Urls.Product(pid).ToString().Trim());
	templateBuilder.Append("\" target=\"_blank\">" + item["title"].ToString().Trim() + "</a></div></td>\r\n                <td>" + item["ext_price"].ToString().Trim() + "</td>\r\n                <td class=\"price2\">" + item["ext_vipprice"].ToString().Trim() + "</td>\r\n                <td style=\"text-align:center\"> \r\n                	" + item["num"].ToString().Trim() + " ");
	templateBuilder.Append(unit.ToString());
	templateBuilder.Append("\r\n                </td>\r\n            </tr>\r\n            ");
	}	//end loop

	templateBuilder.Append("\r\n        </table>\r\n        <table class=\"cart olist\">\r\n        	<tr class=\"caption\">\r\n            	<th colspan=\"2\">&nbsp;&nbsp;<b>确认订单信息</b></th>\r\n            </tr>\r\n            ");	decimal prodtotal = Pays.TotalProductPrice(prods);
	
	string prodcost = prodtotal.ToString("0.00");
	
	string invoicecost = Pays.InvoiceCost(prodtotal).ToString("0.00");
	
	templateBuilder.Append("\r\n              <tr>\r\n                <td style=\"text-align:right;width:110px;\">开具发票：</td>\r\n                <td><input type=\"radio\" name=\"isinvoice\" value=\"1\"/>是&nbsp;\r\n                  <input type=\"radio\" name=\"isinvoice\" value=\"0\" checked=\"checked\"/>否\r\n                </td>\r\n              </tr>\r\n              <tr style=\"display:none;\" class=\"tr-invoice\">\r\n                <td style=\"text-align:right;width:110px;\">发票抬头：</td>\r\n                <td>\r\n                    <input type=\"hidden\" id=\"invoicecost\" value=\"");
	templateBuilder.Append(invoicecost.ToString());
	templateBuilder.Append("\" />\r\n                	<input type=\"text\" name=\"invoicehead\" class=\"ipt-com\" style=\"width:100px;\"/> 发票费用：<span class=\"corange\">");
	templateBuilder.Append(invoicecost.ToString());
	templateBuilder.Append(" 元</span>\r\n                </td>\r\n              </tr>\r\n              <tr>\r\n                <td style=\"text-align:right;\">商品数量：</td>\r\n                <td>");
	templateBuilder.Append(Pays.TotalProductNum(prods).ToString().Trim());
	templateBuilder.Append("</td>\r\n              </tr>\r\n              <tr>\r\n                <td style=\"text-align:right;\">商品重量：</td>\r\n                ");	string weight = Pays.TotalProductWeight(prods).ToString("0.00");
	
	templateBuilder.Append("\r\n                <td class=\"cblue\">");
	templateBuilder.Append(weight.ToString());
	templateBuilder.Append(" KG</td>\r\n              </tr>\r\n              <tr>\r\n                <td style=\"text-align:right\">商品总价：</td>\r\n                <td class=\"corange prodcost\" cost=\"");
	templateBuilder.Append(prodcost.ToString());
	templateBuilder.Append("\">\r\n                  ");
	templateBuilder.Append(prodcost.ToString());
	templateBuilder.Append(" 元\r\n                </td>\r\n              </tr>\r\n              <tr>\r\n                <td style=\"text-align:right\">商品运费：</td>\r\n                <td class=\"corange dcost\"></td>\r\n              </tr>\r\n              <tr>\r\n                <td style=\"text-align:right\">您应该付：</td>\r\n                <td class=\"corange totalcost\">");
	templateBuilder.Append(prodcost.ToString());
	templateBuilder.Append(" 元</td>\r\n              </tr>\r\n              <tr>\r\n                <td style=\"text-align:right;\">订单备注：</td>\r\n                <td>\r\n                	<textarea class=\"tarea-1\" name=\"remark\"></textarea>\r\n                    <div class=\"cgrey\">如果有其他特殊要求请在此填写，如“我要一个白色一个蓝色”(300个字以内)</div>\r\n                </td>\r\n              </tr>\r\n              <tr>\r\n              	<td>&nbsp;</td>\r\n                <td style=\"height:70px;vertical-align:middle\"><input type=\"button\" class=\"com subform\" value=\"确认下单\"/>\r\n                    &nbsp;&nbsp;&nbsp;<span class=\"cred errtip-subform\"></span>\r\n                </td>\r\n              </tr>\r\n        </table>\r\n    </div>\r\n    </form>\r\n    ");
	templateBuilder.Append("    <div class=\"vt_footer\">\r\n        Copyright &copy; 2007-2011 <a href=\"http://www.stacms.com\" title=\"站易内容管理系统\" target=\"_blank\" class=\"cdarkgrey\">STACMS</a> 中视网维 版权所有");
	templateBuilder.Append(config.Extcode.ToString().Trim());
	templateBuilder.Append("\r\n    </div>");

	string djson = Utils.DataTableToJSON(addrs).ToString();
	
	templateBuilder.Append("\r\n    <script type=\"text/javascript\">\r\n        var addrs = ");
	templateBuilder.Append(djson.ToString());
	templateBuilder.Append(", curaddr = {};\r\n        $(\"table.olist\").each(function (idx, obj) {\r\n            $(this).find(\"tr:eq(1)\").find(\"td\").css(\"padding-top\", \"15px\");\r\n            $(this).find(\"tr:last\").find(\"td\").css(\"padding-bottom\", \"15px\");\r\n        });\r\n\r\n        function selectChangeAddr(val) {\r\n            $(\"#span_nativeplace,#span_nativeplace_son,#span_nativeplace_sec\").find(\"select\").change(function () {\r\n                $(\"#address\").val(getNativeplaceText(val || $(\"#ext_nativeplace\").val()));\r\n            });\r\n        };\r\n\r\n        function getAddrById(id){\r\n            for(var i = 0;i < addrs.length;i++){\r\n                if(addrs[i].id == id) return addrs[i];\r\n            }\r\n            return {};\r\n        };\r\n\r\n        function regAddrClick(){\r\n            $(\"input[name='addrs']\").click(function(){\r\n                var val = curaddr.id =$(this).val();\r\n                if(val == \"0\"){\r\n                    setAddress({});\r\n                }else{  \r\n                    setAddress(getAddrById(val));\r\n                }\r\n            });\r\n        };\r\n\r\n        function setAddress(ars) {\r\n            if(ars.parms == undefined) ars.parms = \"0,1\";\r\n            $(\"input[name='gender'][value='\" + ars.parms.split(\",\")[1] + \"']\").attr(\"checked\", true);\r\n            $(\"#username\").val(ars.username || \"\");\r\n            MakeTopSelect(\"nativeplace\", parseFloat(ars.parms.split(\",\")[0]));\r\n            $(\"#address\").val(ars.address || \"\");\r\n            $(\"#postcode\").val(ars.postcode || \"\");\r\n            $(\"#phone\").val(ars.phone || \"\");\r\n            $(\"#email\").val(ars.email || \"\");\r\n        };\r\n\r\n        function setAddressDisplay(ars){\r\n            $(\".deliveryinfo\").show();\r\n            $(\".deliveryedit\").hide();\r\n            $(\".dusername\").html(ars.username);\r\n            $(\".demail\").html(ars.email);\r\n            $(\".daddress\").html(ars.address);\r\n            $(\".dpostcode\").html(ars.postcode);\r\n            $(\".dphone\").html(ars.phone);\r\n            $(\".dgender\").html(ars.parms.split(\",\")[1] == \"1\"? \"先生\":\"女士\");\r\n            $(\"#adrid\").val(ars.id || 0);\r\n        };\r\n\r\n        function getAddress(){\r\n           return {\r\n                id: curaddr.id || 0,\r\n                uid: ");
	templateBuilder.Append(userid.ToString());
	templateBuilder.Append(",\r\n                username: $.trim($(\"#username\").val()),\r\n                title: $.trim($(\"#username\").val()) + \"的地址\",\r\n                email: $.trim($(\"#email\").val()),\r\n                address: $.trim($(\"#address\").val()),\r\n                postcode: $.trim($(\"#postcode\").val()),\r\n                phone: $.trim($(\"#phone\").val()),\r\n                parms: $(\"#ext_nativeplace\").val() + \",\" + $(\"input[name='gender']:checked\").val()\r\n           };\r\n        }\r\n\r\n        function editAddress(){\r\n            loading();\r\n            ajax(\"getuseraddress&uid=");
	templateBuilder.Append(userid.ToString());
	templateBuilder.Append("\", function(data){\r\n                $.unblockUI();\r\n                addrs = toJson(data);\r\n                $(\".deliveryinfo\").hide();\r\n                $(\".deliveryedit\").show();\r\n                $(\".addrlist\").empty();\r\n                $.each(addrs,function(idx, obj){\r\n                    $(\".addrlist\").append(\"<input type=\\\"radio\\\" name=\\\"addrs\\\" value=\\\"\" + obj.id + \"\\\" id=\\\"addr_\" + obj.id + \"\\\"/><label for=\\\"addr_\" + obj.id + \"\\\">\" + obj.title +\"</label><br/>\");\r\n                });\r\n                regAddrClick();\r\n                $(\"input[name='addrs'][value='\" + (curaddr.id || 0) + \"']\").attr(\"checked\", true).trigger(\"click\");\r\n            });\r\n        };\r\n\r\n        $(\".editaddress\").click(editAddress);\r\n\r\n        $(\"input[name='isinvoice']\").change(function () {\r\n            $(\".tr-invoice\").css(\"display\", $(this).val() == \"1\" ? \"\" : \"none\");\r\n        }).trigger(\"change\");\r\n\r\n        function getNativeplaceText(val) {\r\n            var o = getSelectFromData(\"nativeplace\", val);\r\n            return o.top + o.son + o.sec;\r\n        };\r\n\r\n        $(\".saveaddr\").click(function(){\r\n            var addr = getAddress();\r\n            if(addr.username == \"\" || addr.address == \"\" || addr.postcode == \"\" || addr.phone == \"\"){\r\n                $(\".errtip-addr\").html(\"收货地址填写不完整\").show().fadeOut(1000);\r\n                return;\r\n            }\r\n            loading(\"请稍等,正在保存地址..\");\r\n            var addr = getAddress(), data = \"\";\r\n            data = \"id=\"+addr.id+\"&uid=");
	templateBuilder.Append(userid.ToString());
	templateBuilder.Append("&username=\"+escape(addr.username)+\"&title=\"+escape(addr.title)+\"&email=\"+escape(addr.email)+\"&address=\"+escape(addr.address)+\"&postcode=\"+escape(addr.postcode)+\"&phone=\"+escape(addr.phone)+\"&parms=\"+addr.parms;\r\n            ajax(\"edituseraddress&\" + data, function(ret){\r\n                $.unblockUI();\r\n                if(addr.id == \"0\"){\r\n                    addr.id = ret;\r\n                }\r\n                setAddressDisplay(addr);\r\n            });\r\n        });\r\n\r\n        $(\".subform\").click(function(){\r\n            if($(\".deliveryinfo\").css(\"display\") == \"none\"){\r\n                $(\".errtip-subform\").html(\"请先保存“收货人信息”\").show().fadeOut(1000);\r\n                return;\r\n            }\r\n            loading(\"正在生成订单..\");\r\n            $(\"form\").get(0).submit();\r\n        });\r\n\r\n        $(\"input[name='did'],input[name='isinvoice']\").click(resetData);\r\n\r\n        function resetData(){\r\n            var prodcost = parseFloat($(\".prodcost\").attr(\"cost\")), dcost = parseFloat($(\"input[name='did']:checked\").attr(\"cost\"));\r\n            var invoicecost = parseFloat($(\"#invoicecost\").val());\r\n            $(\".totalcost\").html((prodcost + dcost + ($(\"input[name='isinvoice']:checked\").val() == \"1\"? invoicecost : 0 )).toFixed(2) + \" 元\");\r\n            $(\".dcost\").html(dcost.toFixed(2) + \" 元\");\r\n        };\r\n\r\n        $(function () {\r\n            setInterval(selectChangeAddr, 1000);\r\n            if(addrs.length>0){\r\n                curaddr = addrs[0];\r\n                setAddressDisplay(curaddr);\r\n            }else{\r\n                editAddress();\r\n            }\r\n            $(\"#adrid\").val(curaddr.id || \"0\");\r\n            resetData();\r\n        });\r\n	</");
	templateBuilder.Append("script>\r\n</div>\r\n</body>\r\n</html>\r\n");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
