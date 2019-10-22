<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="STA.Page.Cart" %>
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
		This page was created by Siteasy CMS Template Engine at 2019/10/22 21:38:14.
		本页面代码由Siteasy CMS模板引擎生成于 2019/10/22 21:38:14. 
	*/

	base.OnInit(e);


	int item__loop__id=0;
	int gp__loop__id=0;
	int im__loop__id=0;
	int i__loop__id=0;


	DataTable list;
	DataTable pht;
	DataTable ls;

	templateBuilder.Capacity = 220000;
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>我的购物车 - ");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("</title>\r\n<meta name=\"description\" content=\"购物车,");
	templateBuilder.Append(webname.ToString());
	templateBuilder.Append("\">\r\n<meta name=\"keywords\" content=\"购物车,");
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
	templateBuilder.Append("/sta/js/config.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("/sta/js/common.js\"></");
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

	templateBuilder.Append("\r\n    <div class=\"votetit\">\r\n    	我的购物车\r\n    </div>\r\n    <div class=\"votecon\">\r\n    	<div class=\"cart-caption cdarkgrey\">我挑选的商品</div>\r\n        <table class=\"cart\">\r\n        	<tr class=\"caption\">\r\n            	<th>&nbsp;&nbsp;选择</th>\r\n            	<th>商品编号</th>\r\n                <th>商品名称</th>\r\n                <th style=\"width:90px;\">市场价(元)</th>\r\n                <th style=\"width:90px;\">会员价(元)</th>\r\n                <th style=\"text-align:center;width:80px;\">商品数量</th>\r\n                <th style=\"text-align:center;width:60px;\">操作</th>\r\n            </tr>\r\n            ");
	if (prods!=null)
	{


	item__loop__id=0;
	foreach(DataRow item in prods.Rows)
	{
		item__loop__id++;

	int pid = TypeParse.StrToInt(item["id"].ToString().Trim());
	
	templateBuilder.Append("\r\n            <tr pid=\"" + item["id"].ToString().Trim() + "\">\r\n            	<td>&nbsp;\r\n                    <input type=\"checkbox\" value=\"" + item["id"].ToString().Trim() + "\" name=\"chbprod\"/>\r\n                    <input type=\"hidden\" value=\"" + item["ext_weight"].ToString().Trim() + "\" id=\"weight" + item["id"].ToString().Trim() + "\"/>\r\n                    <input type=\"hidden\" value=\"" + item["ext_storage"].ToString().Trim() + "\" id=\"storage" + item["id"].ToString().Trim() + "\"/>\r\n                </td>\r\n                <td>" + item["id"].ToString().Trim() + "</td>\r\n                <td>\r\n                	<div class=\"left\" style=\"width:58px;\"><a href=\"");
	templateBuilder.Append(Urls.Content(pid).ToString().Trim());
	templateBuilder.Append("\" target=\"_blank\"><img src=\"" + item["img"].ToString().Trim() + "\" width=\"50\" height=\"50\" style=\"border:1px solid #ccc;cursor:pointer;\" align=\"absmiddle\"/></a></div>\r\n                	<div class=\"left\" style=\"width:420px\"><a href=\"");
	templateBuilder.Append(Urls.Content(pid).ToString().Trim());
	templateBuilder.Append("\" target=\"_blank\" class=\"prodname" + item["id"].ToString().Trim() + "\">" + item["title"].ToString().Trim() + "</a></div></td>\r\n                <td>" + item["ext_price"].ToString().Trim() + "</td>\r\n                <td class=\"price" + item["id"].ToString().Trim() + "\">" + item["ext_vipprice"].ToString().Trim() + "</td>\r\n                <td style=\"text-align:center\"> \r\n                	<div class=\"buynum clearfix\">\r\n                        <div class=\"delnum\" pid=\"" + item["id"].ToString().Trim() + "\">&nbsp;</div>\r\n                        <div class=\"iptnum\"><input type=\"text\" class=\"ipt-buynum buynum" + item["id"].ToString().Trim() + "\" value=\"" + item["num"].ToString().Trim() + "\" readonly=\"readonly\"/></div>\r\n                        <div class=\"addnum\" pid=\"" + item["id"].ToString().Trim() + "\">&nbsp;</div>\r\n                    </div>\r\n                </td>\r\n                <td style=\"text-align:center;\"><a href=\"javascript:;\" class=\"delprod\" pid=\"" + item["id"].ToString().Trim() + "\">删除</a></td>\r\n            </tr>\r\n            ");
	}	//end loop


	}	//end if

	templateBuilder.Append("\r\n            <tr>\r\n            	<td colspan=\"7\" style=\"background:none;\">\r\n                	<div class=\"operate\">\r\n                    	&nbsp;&nbsp;<a href=\"javascript:;\" class=\"delsel\">删除选中商品</a>\r\n                        &nbsp;<a href=\"javascript:;\" class=\"emptyprods\">清空购物车</a>\r\n                    </div>\r\n                    <div class=\"description\">\r\n						<span class=\"corange totalnum\">0</span>件商品,总重量<span class=\"corange totalweight\">0</span>KG,总金额(不含运费)：<span class=\"cred s14\"><b class=\"totalmoney\">￥");
	templateBuilder.Append(Pays.TotalProductPrice(prods).ToString().Trim());
	templateBuilder.Append("</b></span>元&nbsp;&nbsp;\r\n                    </div>\r\n                </td>\r\n            </tr>\r\n        </table>\r\n        <div class=\"votecon gobuy\">\r\n        	<input type=\"button\" class=\"com\" value=\"继续购物\" title=\"返回首页\" onclick=\"location.href='");
	templateBuilder.Append(weburl.ToString());
	templateBuilder.Append("'\"/>&nbsp;\r\n            <input type=\"button\" class=\"com nextsetp\" value=\"去结算\" />\r\n        </div>\r\n        <div class=\"votecon emptycart\" style=\"display:none;\">\r\n            购物车内暂时没有商品，您可以去<a href=\"");
	templateBuilder.Append(siteurl.ToString());
	templateBuilder.Append("\" target=\"_blank\" class=\"cblue\">首页</a>挑选喜欢的商品\r\n        </div>\r\n    </div>\r\n    ");
	templateBuilder.Append("    <div class=\"vt_footer\">\r\n        Copyright &copy; 2007-2011 <a href=\"http://www.stacms.com\" title=\"站易内容管理系统\" target=\"_blank\" class=\"cdarkgrey\">STACMS</a> 中视网维 版权所有");
	templateBuilder.Append(config.Extcode.ToString().Trim());
	templateBuilder.Append("\r\n    </div>");

	templateBuilder.Append("\r\n</div>\r\n<script type=\"text/javascript\">\r\n    function delProd(pid) {\r\n        shopCart.delProudct(pid);\r\n        $(\"tr[pid ='\" + pid + \"']\").remove();\r\n    };\r\n\r\n    function delProdConfirm(pid) {\r\n        if (confirm(\"确认删除\" + $(\"a.prodname\" + pid).html() + \"吗？\")) {\r\n            shopCart.delProudct(pid);\r\n            $(\"tr[pid ='\" + pid + \"']\").remove();\r\n        };\r\n    };\r\n\r\n    function getProd(pid){\r\n        return {\r\n            id: pid, \r\n            num: parseInt($(\"input.buynum\" + pid).val()),\r\n            price: parseFloat($(\"td.price\" + pid).html()),\r\n            name: $(\"a.prodname\" + pid).html(),\r\n            weight: parseFloat($(\"#weight\" + pid).val()),\r\n            storage: parseInt($(\"#storage\" + pid).val()) \r\n         };\r\n    };\r\n\r\n    function cartReset() {\r\n        var wht = 0;\r\n        $.each(shopCart.getProductlist(), function (idx, obj) {\r\n            $(\"input.buynum\" + obj.id).val(obj.num);\r\n            wht += getProd(obj.id).weight * obj.num;\r\n        });\r\n        $(\".totalweight\").html(wht.toFixed(2));\r\n        $(\".totalnum\").html(shopCart.count());\r\n        $(\".totalmoney\").html(\"￥\" + shopCart.money());\r\n        $(\".gobuy,table.cart,.cart-caption\").css(\"display\", shopCart.count() > 0 ? \"\" : \"none\");\r\n        $(\".emptycart\").css(\"display\", shopCart.count() > 0 ? \"none\" : \"\");\r\n    };\r\n\r\n    $(\".delprod\").click(function () {\r\n        delProdConfirm($(this).attr(\"pid\"));\r\n        cartReset();\r\n    });\r\n\r\n    $(\".delnum\").click(function () {\r\n        var prod = getProd($(this).attr(\"pid\"));\r\n        if (prod.num == 1) {\r\n            delProdConfirm(prod.id);\r\n        } else {\r\n            prod.num -= 1;\r\n            $(\"input.buynum\" + prod.id).val(prod.num);\r\n            shopCart.setProduct(prod);\r\n        }\r\n        cartReset();\r\n    });\r\n\r\n    $(\".delsel\").click(function () {\r\n        if ($('input[name=\"chbprod\"]:checked').length >0 && confirm(\"确认删除所选的商品吗？\")) {\r\n            $('input[name=\"chbprod\"]:checked').each(function (idx, obj) {\r\n                delProd($(this).val());\r\n            });\r\n            cartReset();\r\n        }\r\n    });\r\n\r\n    $(\".emptyprods\").click(function () {\r\n        if (confirm(\"确定清空您的购物车吗？\")) {\r\n            shopCart.clear(); \r\n            cartReset();\r\n        }\r\n    });\r\n\r\n    $(\".addnum\").click(function () {\r\n        var prod = getProd($(this).attr(\"pid\"));\r\n        prod.num += 1;\r\n        if (prod.num <= prod.storage) {\r\n            $(\"input.buynum\" + prod.id).val(prod.num);\r\n            shopCart.setProduct(prod);\r\n            cartReset();\r\n        } else {\r\n            alert(\"抱歉,\" + prod.name + \"没有足够的库存,不能继续添加了！\");\r\n        }\r\n    });\r\n\r\n    $(\".nextsetp\").click(function () {\r\n        location.href = \"cartstep.aspx\";\r\n    });\r\n    cartReset();\r\n</");
	templateBuilder.Append("script>\r\n</body>\r\n</html>\r\n");
	Response.Write(iscompress ? Utils.CompressHtml(templateBuilder.ToString()) : templateBuilder.ToString());
}
</script>
