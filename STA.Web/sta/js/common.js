jQuery.cookie = function (name, value, options) { if (typeof value != 'undefined') { options = options || {}; if (value === null) { value = ''; options.expires = -1 } var expires = ''; if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) { var date; if (typeof options.expires == 'number') { date = new Date(); date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000)) } else { date = options.expires } expires = '; expires=' + date.toUTCString() } var path = options.path ? '; path=' + (options.path) : ''; var domain = options.domain ? '; domain=' + (options.domain) : ''; var secure = options.secure ? '; secure' : ''; document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('') } else { var cookieValue = null; if (document.cookie && document.cookie != '') { var cookies = document.cookie.split(';'); for (var i = 0; i < cookies.length; i++) { var cookie = jQuery.trim(cookies[i]); if (cookie.substring(0, name.length + 1) == (name + '=')) { cookieValue = decodeURIComponent(cookie.substring(name.length + 1)); break } } } return cookieValue } };

if (!String.prototype.format) {
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined' ? args[number] : match;
        });
    };
};

var userAgent = navigator.userAgent.toLowerCase();
var is_opera = userAgent.indexOf('opera') != -1 && opera.version();
var is_moz = (navigator.product == 'Gecko') && userAgent.substr(userAgent.indexOf('firefox') + 8, 3);
var is_ie = (userAgent.indexOf('msie') != -1 && !is_opera) && userAgent.substr(userAgent.indexOf('msie') + 5, 3);
var is_mac = userAgent.indexOf('mac') != -1;

function ele(ele) {
    return document.getElementById(ele);
};

function toJson(data) {
    return eval('(' + data + ')');
};

String.prototype.replaceAll = function (reallyDo, replaceWith, ignoreCase) {
    if (!RegExp.prototype.isPrototypeOf(reallyDo)) {
        return this.replace(new RegExp(reallyDo, (ignoreCase ? "gi" : "g")), replaceWith);
    } else {
        return this.replace(reallyDo, replaceWith);
    }
};

function queryString(key) {
    var re = new RegExp('(?:\\?|&)' + key + '=(.*?)(?=&|$)', 'gi');
    var r = [], m;
    while ((m = re.exec(document.location.search)) != null) r.push(m[1]);
    return r;
}

function hashString(queryname, def) {
    var qKeys = {};
    var re = /[?#]([^=]+)(?:=([^#]*))?/g;
    var matchInfo;
    while (matchInfo = re.exec(location.hash)) {
        qKeys[matchInfo[1]] = matchInfo[2];
    }
    return typeof (qKeys[queryname]) == 'undefined' ? (def || '') : qKeys[queryname];
};

function ajax(action, success, dataType) {
    $.post(webconfig.webdir + "/sta/ajax.aspx", "t=" + action, success, dataType || "html");
};

function videoPlay(id, vfile, vimg, width, height, autoplay) {
    document.writeln("<object id=\"f4Player\" width=\"" + width + "\" height=\"" + height + "\" type=\"application/x-shockwave-flash\" data=\"/sta/plugin/videoplayer/f4player/player.swf?v1.3.5\"> ");
    document.writeln("  <param name=\"movie\" value=\"/sta/plugin/videoplayer/f4player/player.swf?v1.3.5\" /> ");
    document.writeln("  <param name=\"quality\" value=\"high\" /> ");
    document.writeln("  <param name=\"menu\" value=\"false\" /> ");
    document.writeln("  <param name=\"scale\" value=\"noscale\" /> ");
    document.writeln("  <param name=\"allowfullscreen\" value=\"true\"> ");
    document.writeln("  <param name=\"allowscriptaccess\" value=\"always\"> ");
    document.writeln("  <param name=\"wmode\" value=\"opaque\" /> ");
    document.writeln("  <param name=\"swlivevonnect\" value=\"true\" /> ");
    document.writeln("  <param name=\"cachebusting\" value=\"false\"> ");
    document.writeln("  <param name=\"flashvars\" value=\"skin=/sta/plugin/videoplayer/f4player/skins/mySkin.swf&thumbnail=" + vimg + "&video=" + vfile + "\"/> ");
    document.writeln("  <a href=\"http://www.adobe.com/go/flashplayer/\">Download it from Adobe.</a> ");
    document.writeln("</object>");
};

function addFavourite(name, url, err) {
    if (document.all) {
        window.external.addFavorite(url, name);
    } else if (window.sidebar) {
        window.sidebar.addPanel(name, url, "");
    } else {
        alert(err || "\u5bf9\u4e0d\u8d77\uff0c\u60a8\u7684\u6d4f\u89c8\u5668\u4e0d\u652f\u6301\u6b64\u64cd\u4f5c!\n\u8bf7\u60a8\u4f7f\u7528\u83dc\u5355\u680f\u6216Ctrl+D\u6536\u85cf\u672c\u7ad9\u3002");
    }
};

function checkBoxMaxChecked(selector, max) {
    $(selector).each(function (idx, obj) {
        $(obj).click(function () {
            if ($(selector + ":checked").length < max) {
                $(selector).attr("disabled", false);
            } else {
                $(selector).not(selector + ":checked").attr("disabled", true);
            }
        });
    });
};

function AC_FL_RunContent() {
    var ret = AC_GetArgs(arguments, "clsid:d27cdb6e-ae6d-11cf-96b8-444553540000", "application/x-shockwave-flash");
    var str = '';
    if (is_ie && !is_opera) {
        str += '<object ';
        for (var i in ret.objAttrs) {
            str += i + '="' + ret.objAttrs[i] + '" ';
        }
        str += '>';
        for (var i in ret.params) {
            str += '<param name="' + i + '" value="' + ret.params[i] + '" /> ';
        }
        str += '</object>';
    } else {
        str += '<embed ';
        for (var i in ret.embedAttrs) {
            str += i + '="' + ret.embedAttrs[i] + '" ';
        }
        str += '></embed>';
    }
    return str;
}

var fileUpload = function (config) {
    var ifr = null,
        fm = null,
        defConfig = {
            submitBtn: $('#fileup_submit'),
            trigger: "click",
            action: "",
            complete: function (response) { },
            beforeUpLoad: function () { return true },
            afterUpLoad: function () { }
        };

    var IFRAME_NAME = 'fileUpLoadIframe';

    config = $.extend(defConfig, config);

    config.submitBtn.bind(config.trigger, function (e) {
        e.preventDefault();

        if (config.beforeUpLoad.call(this) === false) {
            return;
        }

        ifr = $('<iframe name="' + IFRAME_NAME + '" id="' + IFRAME_NAME + '" style="display:none;"></iframe>');
        fm = this.form;
        var acname = fm.action;

        ifr.appendTo($('body'));
        fm.action = config.action;
        fm.target = IFRAME_NAME;

        ifr.load(function () {

            var response = this.contentWindow.document.body.innerHTML;
//            console.log("response:" + response );
            ifr.remove();
            ifr = null;

            config.complete.call(this, response);

            fm.action = acname;
            fm.target = '';
        });
        fm.submit();
        config.afterUpLoad.call(this);
    });
};

function AC_GetArgs(args, classid, mimeType) {
    var ret = new Object();
    ret.embedAttrs = new Object();
    ret.params = new Object();
    ret.objAttrs = new Object();
    for (var i = 0; i < args.length; i = i + 2) {
        var currArg = args[i].toLowerCase();
        switch (currArg) {
            case "classid": break;
            case "pluginspage": ret.embedAttrs[args[i]] = 'http://www.macromedia.com/go/getflashplayer'; break;
            case "src": ret.embedAttrs[args[i]] = args[i + 1]; ret.params["movie"] = args[i + 1]; break;
            case "codebase": ret.objAttrs[args[i]] = 'http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0'; break;
            case "onafterupdate": case "onbeforeupdate": case "onblur": case "oncellchange": case "onclick": case "ondblclick": case "ondrag": case "ondragend":
            case "ondragenter": case "ondragleave": case "ondragover": case "ondrop": case "onfinish": case "onfocus": case "onhelp": case "onmousedown":
            case "onmouseup": case "onmouseover": case "onmousemove": case "onmouseout": case "onkeypress": case "onkeydown": case "onkeyup": case "onload":
            case "onlosecapture": case "onpropertychange": case "onreadystatechange": case "onrowsdelete": case "onrowenter": case "onrowexit": case "onrowsinserted": case "onstart":
            case "onscroll": case "onbeforeeditfocus": case "onactivate": case "onbeforedeactivate": case "ondeactivate": case "type":
            case "id": ret.objAttrs[args[i]] = args[i + 1]; break;
            case "width": case "height": case "align": case "vspace": case "hspace": case "class": case "title": case "accesskey": case "name":
            case "tabindex": ret.embedAttrs[args[i]] = ret.objAttrs[args[i]] = args[i + 1]; break;
            default: ret.embedAttrs[args[i]] = ret.params[args[i]] = args[i + 1];
        }
    }
    ret.objAttrs["classid"] = classid;
    if (mimeType) {
        ret.embedAttrs["type"] = mimeType;
    }
    return ret;
};

function getSTAUser() {
    var ret = $.cookie("sta"), info = {};
    var getval = function (name) {
        if (ret == null || ret == "") { return ""; }
        var parms = ret.split("&");
        for (var i = 0; i < parms.length; i++) {
            if (parms[i].indexOf(name + "=") >= 0) {
                return unescape(parms[i].replace(name + "=", ""));
            }
        }
        return "";
    }
    info.userid = getval("userid");
    info.nickname = getval("nickname");
    info.username = getval("username");
    info.ip = getval("ip");
    return info;
};

var stauser = getSTAUser();

function ShopCart(cartId) {
    this.cartId = cartId;
};

ShopCart.prototype = {
    listToStr: function (list) {
        var str = "";
        $.each(list, function (idx, obj) {
            str += obj.id + "=" + obj.num + "=" + obj.price + "&";
        });
        if (str == "") return str;
        return str.substring(0, str.length - 1);
    },
    ids: function () {
        var list = this.getProductlist(), strs = "";
        $.each(list, function (idx, obj) { strs += obj.id + ","; });
        return strs.length > 0 ? strs.substring(0, strs.length - 1) : "";
    },
    exist: function (prodid) {
        var list = this.getProductlist();
        for (var i = 0; i < list.length; i++) {
            if (list[i].id == prodid) return true;
        }
        return false;
    },
    setCookie: function (list) {
        $.cookie("cartgoods", this.listToStr(list), { path: "/", expires: 30 })
    },
    getCookie: function () {
        var cval = $.cookie("cartgoods");
        return cval == null ? "" : cval;
    },
    getProductlist: function () {
        var prods = this.getCookie().split('&'), prodlist = [];
        for (var i = 0; i < prods.length; i++) {
            if (prods[i] == "") continue;
            prodlist.push({ id: prods[i].split("=")[0], num: parseInt(prods[i].split("=")[1]), price: parseFloat(prods[i].split("=")[2]) });
        }
        return prodlist;
    },
    delProudct: function (prodid) {
        var list = this.getProductlist();
        $.each(list, function (idx, obj) {
            if (prodid == obj.id) list.splice(idx, 1);
        });
        this.setCookie(list);
    },
    setProduct: function (prod) {
        var list = this.getProductlist();
        //        if (prod.num == 0) {
        //            this.delProudct(prod.id);
        //        } else {
        if (this.exist(prod.id)) {
            $.each(list, function (idx, obj) {
                if (obj.id == prod.id) {
                    obj.num = prod.num == 0 ? (obj.num + 1) : prod.num;
                }
            });
        } else {
            list.push({ id: prod.id, num: (prod.num == 0 ? 1 : prod.num), price: prod.price });
        }
        this.setCookie(list);
        //        }
    },
    getProduct: function (prodid) {
        var prod = {}, list = this.getProductlist(); ;
        for (var i = 0; i < list.length; i++) {
            if (list[i].id = prodid) return list[i];
        }
        return prod;
    },
    clear: function () {
        this.setCookie([]);
    },
    money: function () {
        var m = 0, list = this.getProductlist();
        $.each(list, function (idx, obj) { m += obj.price * obj.num; });
        return m.toFixed(2);
    },
    count: function () {
        var count = 0, list = this.getProductlist();
        $.each(list, function (idx, obj) { count += obj.num; });
        return count;
    }
};

var shopCart = new ShopCart("funnyzak");

var tmpl = (function (cache, $) {
    return function (str, data) {
        var fn = !/\s/.test(str)
	? cache[str] = cache[str]
		|| tmpl(document.getElementById(str).innerHTML)

	: function (data) {
	    var i, variable = [$], value = [[]];
	    for (i in data) {
	        variable.push(i);
	        value.push(data[i]);
	    };
	    return (new Function(variable, fn.$))
		.apply(data, value).join("");
	};

        fn.$ = fn.$ || $ + ".push('"
	+ str.replace(/\\/g, "\\\\")
		 .replace(/[\r\t\n]/g, " ")
		 .split("<%").join("\t")
		 .replace(/((^|%>)[^\t]*)'/g, "$1\r")
		 .replace(/\t=(.*?)%>/g, "',$1,'")
		 .split("\t").join("');")
		 .split("%>").join($ + ".push('")
		 .split("\r").join("\\'")
	+ "');return " + $;

        return data ? fn(data) : fn;
    }
})({}, '$' + (+new Date));

function setContentClick(id, back, func) {
    if (webconfig.vtype >= 1) return;
    ajax("conclick&id=" + id + "&back=" + back, func);
};

function getConDiggcount(id, func) {
    if (webconfig.vtype >= 1) return;
    ajax("diggstamp&id=" + id, func);
};

function getDowncount(id, func) {
    if (webconfig.vtype >= 1) return;
    ajax("getsoftdowncount&id=" + id, func);
};

function getExtfieldval(id, ext, field, func) {
    if (webconfig.vtype >= 1) return;
    ajax("confieldval&cid=" + id + "&ext=" + ext + "&field=" + field, func);
};

function setConDiggcount(id, type, func) {
    var tstr = type == "digg" ? "赞" : "踩", obj = $("." + type + "count"), cname = "stacontentdigg" + id;
    var curcount = parseInt(obj.html());
    var val = $.cookie(cname);

    if (val == null || val == "") {
        ajax("con" + type + "&id=" + id, function (ret) {
            if (ret == "True") {
                $(obj).html(curcount + 1);
                $.cookie(cname, tstr, { path: "/", expires: 30000 });
            } else {
                alert(tstr + "失败,请稍后再试！");
            }
        });
    } else {
        alert("您已经" + val + "过了！");
    }
};

function setCommentCount(id, func) {
    if (webconfig.vtype >= 1) return;
    ajax("concomcount&id=" + id, func);
};

function userAvatar(uid, size) {
    var avatar_url = webconfig.weburl + webconfig.webdir + "/sta/avators/{0}/{1}/{2}/{3}_avatar_{4}.jpg", pad = "000000000", uidstr = uid.toString();
    uidstr = pad.substring(0, pad.length - uidstr.length) + uidstr;
    return avatar_url.format(uidstr.substring(0, 3), uidstr.substring(3, 5), uidstr.substring(5, 7), uidstr.substring(7, 9),(size==undefined? "medium" : size).toLowerCase());
};
