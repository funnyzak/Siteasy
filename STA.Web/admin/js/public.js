jQuery.cookie = function (name, value, options) { if (typeof value != 'undefined') { options = options || {}; if (value === null) { value = ''; options.expires = -1 } var expires = ''; if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) { var date; if (typeof options.expires == 'number') { date = new Date(); date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000)) } else { date = options.expires } expires = '; expires=' + date.toUTCString() } var path = options.path ? '; path=' + (options.path) : ''; var domain = options.domain ? '; domain=' + (options.domain) : ''; var secure = options.secure ? '; secure' : ''; document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('') } else { var cookieValue = null; if (document.cookie && document.cookie != '') { var cookies = document.cookie.split(';'); for (var i = 0; i < cookies.length; i++) { var cookie = jQuery.trim(cookies[i]); if (cookie.substring(0, name.length + 1) == (name + '=')) { cookieValue = decodeURIComponent(cookie.substring(name.length + 1)); break } } } return cookieValue } };

(function ($) { $.fn.jqm = function (o) { var p = { overlay: 10, overlayClass: 'jqmOverlay', closeClass: 'jqmClose', trigger: '.jqModal', ajax: F, ajaxText: '', target: F, modal: F, toTop: F, onShow: F, onHide: F, onLoad: F }; return this.each(function () { if (this._jqm) return H[this._jqm].c = $.extend({}, H[this._jqm].c, o); s++; this._jqm = s; H[s] = { c: $.extend(p, $.jqm.params, o), a: F, w: $(this).addClass('jqmID' + s), s: s }; if (p.trigger) $(this).jqmAddTrigger(p.trigger) }) }; $.fn.jqmAddClose = function (e) { return hs(this, e, 'jqmHide') }; $.fn.jqmAddTrigger = function (e) { return hs(this, e, 'jqmShow') }; $.fn.jqmShow = function (t) { return this.each(function () { $.jqm.open(this._jqm, t) }) }; $.fn.jqmHide = function (t) { return this.each(function () { $.jqm.close(this._jqm, t) }) }; $.jqm = { hash: {}, open: function (s, t) { var h = H[s], c = h.c, cc = '.' + c.closeClass, z = (parseInt(h.w.css('z-index'))), z = (z > 0) ? z : 3000, o = $('<div></div>').css({ height: '100%', width: '100%', position: 'fixed', left: 0, top: 0, 'z-index': z - 1, opacity: c.overlay / 100 }); if (h.a) return F; h.t = t; h.a = true; h.w.css('z-index', z); if (c.modal) { if (!A[0]) L('bind'); A.push(s) } else if (c.overlay > 0) h.w.jqmAddClose(o); else o = F; h.o = (o) ? o.addClass(c.overlayClass).prependTo('body') : F; if (ie6) { $('html,body').css({ height: '100%', width: '100%' }); if (o) { o = o.css({ position: 'absolute' })[0]; for (var y in { Top: 1, Left: 1 }) o.style.setExpression(y.toLowerCase(), "(_=(document.documentElement.scroll" + y + " || document.body.scroll" + y + "))+'px'") } } if (c.ajax) { var r = c.target || h.w, u = c.ajax, r = (typeof r == 'string') ? $(r, h.w) : $(r), u = (u.substr(0, 1) == '@') ? $(t).attr(u.substring(1)) : u; r.html(c.ajaxText).load(u, function () { if (c.onLoad) c.onLoad.call(this, h); if (cc) h.w.jqmAddClose($(cc, h.w)); e(h) }) } else if (cc) h.w.jqmAddClose($(cc, h.w)); if (c.toTop && h.o) h.w.before('<span id="jqmP' + h.w[0]._jqm + '"></span>').insertAfter(h.o); (c.onShow) ? c.onShow(h) : h.w.show(); e(h); return F }, close: function (s) { var h = H[s]; if (!h.a) return F; h.a = F; if (A[0]) { A.pop(); if (!A[0]) L('unbind') } if (h.c.toTop && h.o) $('#jqmP' + h.w[0]._jqm).after(h.w).remove(); if (h.c.onHide) h.c.onHide(h); else { h.w.hide(); if (h.o) h.o.remove() } return F }, params: {} }; var s = 0, H = $.jqm.hash, A = [], ie6 = $.browser.msie && ($.browser.version == "6.0"), F = false, i = $('<iframe src="javascript:false;document.write(\'\');" class="jqm"></iframe>').css({ opacity: 0 }), e = function (h) { if (ie6) if (h.o) h.o.html('<p style="width:100%;height:100%"/>').prepend(i); else if (!$('iframe.jqm', h.w)[0]) h.w.prepend(i); f(h) }, f = function (h) { try { $(':input:visible', h.w)[0].focus() } catch (_) { } }, L = function (t) { $()[t]("keypress", m)[t]("keydown", m)[t]("mousedown", m) }, m = function (e) { var h = H[A[A.length - 1]], r = (!$(e.target).parents('.jqmID' + h.s)[0]); if (r) f(h); return !r }, hs = function (w, t, c) { return w.each(function () { var s = this._jqm; $(t).each(function () { if (!this[c]) { this[c] = []; $(this).click(function () { for (var i in { jqmShow: 1, jqmHide: 1 }) for (var s in this[i]) if (H[this[i][s]]) H[this[i][s]].w[i](this); return F }) } this[c].push(s) }) }) } })(jQuery);

(function ($) { if (/1\.(0|1|2)\.(0|1|2)/.test($.fn.jquery) || /^1.1/.test($.fn.jquery)) { alert('blockUI requires jQuery v1.2.3 or later!  You are using v' + $.fn.jquery); return } $.fn._fadeIn = $.fn.fadeIn; var noOp = function () { }; var mode = document.documentMode || 0; var setExpr = $.browser.msie && (($.browser.version < 8 && !mode) || mode < 8); var ie6 = $.browser.msie && /MSIE 6.0/.test(navigator.userAgent) && !mode; $.blockUI = function (opts) { install(window, opts) }; $.unblockUI = function (opts) { remove(window, opts) }; $.growlUI = function (title, message, timeout, onClose) { var $m = $('<div class="growlUI"></div>'); if (title) $m.append('<h1>' + title + '</h1>'); if (message) $m.append('<h2>' + message + '</h2>'); if (timeout == undefined) timeout = 3000; $.blockUI({ message: $m, fadeIn: 700, fadeOut: 1000, centerY: false, timeout: timeout, showOverlay: false, onUnblock: onClose, css: $.blockUI.defaults.growlCSS }) }; $.fn.block = function (opts) { return this.unblock({ fadeOut: 0 }).each(function () { if ($.css(this, 'position') == 'static') this.style.position = 'relative'; if ($.browser.msie) this.style.zoom = 1; install(this, opts) }) }; $.fn.unblock = function (opts) { return this.each(function () { remove(this, opts) }) }; $.blockUI.version = 2.38; $.blockUI.defaults = { message: '<h1>Please wait...</h1>', title: null, draggable: true, theme: false, css: { padding: 0, margin: 0, width: '30%', top: '40%', left: '35%', textAlign: 'center', color: '#000', border: '3px solid #aaa', backgroundColor: '#fff', cursor: 'wait' }, themedCSS: { width: '30%', top: '40%', left: '35%' }, overlayCSS: { backgroundColor: '#000', opacity: 0.6, cursor: 'wait' }, growlCSS: { width: '350px', top: '10px', left: '', right: '10px', border: 'none', padding: '5px', opacity: 0.6, cursor: 'default', color: '#fff', backgroundColor: '#000', '-webkit-border-radius': '10px', '-moz-border-radius': '10px', 'border-radius': '10px' }, iframeSrc: /^https/i.test(window.location.href || '') ? 'javascript:false' : 'about:blank', forceIframe: false, baseZ: 1000, centerX: true, centerY: true, allowBodyStretch: true, bindEvents: true, constrainTabKey: true, fadeIn: 200, fadeOut: 400, timeout: 0, showOverlay: true, focusInput: true, applyPlatformOpacityRules: true, onBlock: null, onUnblock: null, quirksmodeOffsetHack: 4, blockMsgClass: 'blockMsg' }; var pageBlock = null; var pageBlockEls = []; function install(el, opts) { var full = (el == window); var msg = opts && opts.message !== undefined ? opts.message : undefined; opts = $.extend({}, $.blockUI.defaults, opts || {}); opts.overlayCSS = $.extend({}, $.blockUI.defaults.overlayCSS, opts.overlayCSS || {}); var css = $.extend({}, $.blockUI.defaults.css, opts.css || {}); var themedCSS = $.extend({}, $.blockUI.defaults.themedCSS, opts.themedCSS || {}); msg = msg === undefined ? opts.message : msg; if (full && pageBlock) remove(window, { fadeOut: 0 }); if (msg && typeof msg != 'string' && (msg.parentNode || msg.jquery)) { var node = msg.jquery ? msg[0] : msg; var data = {}; $(el).data('blockUI.history', data); data.el = node; data.parent = node.parentNode; data.display = node.style.display; data.position = node.style.position; if (data.parent) data.parent.removeChild(node) } var z = opts.baseZ; var lyr1 = ($.browser.msie || opts.forceIframe) ? $('<iframe class="blockUI" style="z-index:' + (z++) + ';display:none;border:none;margin:0;padding:0;position:absolute;width:100%;height:100%;top:0;left:0" src="' + opts.iframeSrc + '"></iframe>') : $('<div class="blockUI" style="display:none"></div>'); var lyr2 = opts.theme ? $('<div class="blockUI blockOverlay ui-widget-overlay" style="z-index:' + (z++) + ';display:none"></div>') : $('<div class="blockUI blockOverlay" style="z-index:' + (z++) + ';display:none;border:none;margin:0;padding:0;width:100%;height:100%;top:0;left:0"></div>'); var lyr3, s; if (opts.theme && full) { s = '<div class="blockUI ' + opts.blockMsgClass + ' blockPage ui-dialog ui-widget ui-corner-all" style="z-index:' + z + ';display:none;position:fixed">' + '<div class="ui-widget-header ui-dialog-titlebar ui-corner-all blockTitle">' + (opts.title || '&nbsp;') + '</div>' + '<div class="ui-widget-content ui-dialog-content"></div>' + '</div>' } else if (opts.theme) { s = '<div class="blockUI ' + opts.blockMsgClass + ' blockElement ui-dialog ui-widget ui-corner-all" style="z-index:' + z + ';display:none;position:absolute">' + '<div class="ui-widget-header ui-dialog-titlebar ui-corner-all blockTitle">' + (opts.title || '&nbsp;') + '</div>' + '<div class="ui-widget-content ui-dialog-content"></div>' + '</div>' } else if (full) { s = '<div class="blockUI ' + opts.blockMsgClass + ' blockPage" style="z-index:' + z + ';display:none;position:fixed"></div>' } else { s = '<div class="blockUI ' + opts.blockMsgClass + ' blockElement" style="z-index:' + z + ';display:none;position:absolute"></div>' } lyr3 = $(s); if (msg) { if (opts.theme) { lyr3.css(themedCSS); lyr3.addClass('ui-widget-content') } else lyr3.css(css) } if (!opts.theme && (!opts.applyPlatformOpacityRules || !($.browser.mozilla && /Linux/.test(navigator.platform)))) lyr2.css(opts.overlayCSS); lyr2.css('position', full ? 'fixed' : 'absolute'); if ($.browser.msie || opts.forceIframe) lyr1.css('opacity', 0.0); var layers = [lyr1, lyr2, lyr3], $par = full ? $('body') : $(el); $.each(layers, function () { this.appendTo($par) }); if (opts.theme && opts.draggable && $.fn.draggable) { lyr3.draggable({ handle: '.ui-dialog-titlebar', cancel: 'li' }) } var expr = setExpr && (!$.boxModel || $('object,embed', full ? null : el).length > 0); if (ie6 || expr) { if (full && opts.allowBodyStretch && $.boxModel) $('html,body').css('height', '100%'); if ((ie6 || !$.boxModel) && !full) { var t = sz(el, 'borderTopWidth'), l = sz(el, 'borderLeftWidth'); var fixT = t ? '(0 - ' + t + ')' : 0; var fixL = l ? '(0 - ' + l + ')' : 0 } $.each([lyr1, lyr2, lyr3], function (i, o) { var s = o[0].style; s.position = 'absolute'; if (i < 2) { full ? s.setExpression('height', 'Math.max(document.body.scrollHeight, document.body.offsetHeight) - (jQuery.boxModel?0:' + opts.quirksmodeOffsetHack + ') + "px"') : s.setExpression('height', 'this.parentNode.offsetHeight + "px"'); full ? s.setExpression('width', 'jQuery.boxModel && document.documentElement.clientWidth || document.body.clientWidth + "px"') : s.setExpression('width', 'this.parentNode.offsetWidth + "px"'); if (fixL) s.setExpression('left', fixL); if (fixT) s.setExpression('top', fixT) } else if (opts.centerY) { if (full) s.setExpression('top', '(document.documentElement.clientHeight || document.body.clientHeight) / 2 - (this.offsetHeight / 2) + (blah = document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop) + "px"'); s.marginTop = 0 } else if (!opts.centerY && full) { var top = (opts.css && opts.css.top) ? parseInt(opts.css.top) : 0; var expression = '((document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop) + ' + top + ') + "px"'; s.setExpression('top', expression) } }) } if (msg) { if (opts.theme) lyr3.find('.ui-widget-content').append(msg); else lyr3.append(msg); if (msg.jquery || msg.nodeType) $(msg).show() } if (($.browser.msie || opts.forceIframe) && opts.showOverlay) lyr1.show(); if (opts.fadeIn) { var cb = opts.onBlock ? opts.onBlock : noOp; var cb1 = (opts.showOverlay && !msg) ? cb : noOp; var cb2 = msg ? cb : noOp; if (opts.showOverlay) lyr2._fadeIn(opts.fadeIn, cb1); if (msg) lyr3._fadeIn(opts.fadeIn, cb2) } else { if (opts.showOverlay) lyr2.show(); if (msg) lyr3.show(); if (opts.onBlock) opts.onBlock() } bind(1, el, opts); if (full) { pageBlock = lyr3[0]; pageBlockEls = $(':input:enabled:visible', pageBlock); if (opts.focusInput) setTimeout(focus, 20) } else center(lyr3[0], opts.centerX, opts.centerY); if (opts.timeout) { var to = setTimeout(function () { full ? $.unblockUI(opts) : $(el).unblock(opts) }, opts.timeout); $(el).data('blockUI.timeout', to) } }; function remove(el, opts) { var full = (el == window); var $el = $(el); var data = $el.data('blockUI.history'); var to = $el.data('blockUI.timeout'); if (to) { clearTimeout(to); $el.removeData('blockUI.timeout') } opts = $.extend({}, $.blockUI.defaults, opts || {}); bind(0, el, opts); var els; if (full) els = $('body').children().filter('.blockUI').add('body > .blockUI'); else els = $('.blockUI', el); if (full) pageBlock = pageBlockEls = null; if (opts.fadeOut) { els.fadeOut(opts.fadeOut); setTimeout(function () { reset(els, data, opts, el) }, opts.fadeOut) } else reset(els, data, opts, el) }; function reset(els, data, opts, el) { els.each(function (i, o) { if (this.parentNode) this.parentNode.removeChild(this) }); if (data && data.el) { data.el.style.display = data.display; data.el.style.position = data.position; if (data.parent) data.parent.appendChild(data.el); $(el).removeData('blockUI.history') } if (typeof opts.onUnblock == 'function') opts.onUnblock(el, opts) }; function bind(b, el, opts) { var full = el == window, $el = $(el); if (!b && (full && !pageBlock || !full && !$el.data('blockUI.isBlocked'))) return; if (!full) $el.data('blockUI.isBlocked', b); if (!opts.bindEvents || (b && !opts.showOverlay)) return; var events = 'mousedown mouseup keydown keypress'; b ? $(document).bind(events, opts, handler) : $(document).unbind(events, handler) }; function handler(e) { if (e.keyCode && e.keyCode == 9) { if (pageBlock && e.data.constrainTabKey) { var els = pageBlockEls; var fwd = !e.shiftKey && e.target === els[els.length - 1]; var back = e.shiftKey && e.target === els[0]; if (fwd || back) { setTimeout(function () { focus(back) }, 10); return false } } } var opts = e.data; if ($(e.target).parents('div.' + opts.blockMsgClass).length > 0) return true; return $(e.target).parents().children().filter('div.blockUI').length == 0 }; function focus(back) { if (!pageBlockEls) return; var e = pageBlockEls[back === true ? pageBlockEls.length - 1 : 0]; if (e) e.focus() }; function center(el, x, y) { var p = el.parentNode, s = el.style; var l = ((p.offsetWidth - el.offsetWidth) / 2) - sz(p, 'borderLeftWidth'); var t = ((p.offsetHeight - el.offsetHeight) / 2) - sz(p, 'borderTopWidth'); if (x) s.left = l > 0 ? (l + 'px') : '0'; if (y) s.top = t > 0 ? (t + 'px') : '0' }; function sz(el, p) { return parseInt($.css(el, p)) || 0 } })(jQuery);

var userAgent = navigator.userAgent.toLowerCase();
var is_opera = userAgent.indexOf('opera') != -1 && opera.version();
var is_moz = (navigator.product == 'Gecko') && userAgent.substr(userAgent.indexOf('firefox') + 8, 3);
var is_ie = (userAgent.indexOf('msie') != -1 && !is_opera) && userAgent.substr(userAgent.indexOf('msie') + 5, 3);
var is_mac = userAgent.indexOf('mac') != -1;

var extra_config = { expand: false };
var allPlugin = {
    maxmin: { c: 'xheIcon xheBtnFullscreen', t: '切换编辑区大小', e: function () {
        var _this = this;
        var _table = $('.xheLayout');
        var _editor = $('.xheIframeArea');

        if (!extra_config.expand) {
            extra_config.ec_width = _table.width();
            extra_config.ec_height = _editor.height();
            _table.width(670);
            _editor.height(300);
            extra_config.expand = true;
        } else {
            _table.width(extra_config.ec_width);
            _editor.height(extra_config.ec_height);
            extra_config.expand = false;
        }

    }
    }
};

var xhconfig = { plugins: allPlugin, tools: 'Source,|,Fontface,FontSize,Bold,Italic,Underline,|,FontColor,BackColor,SelectAll,Removeformat,|,Blocktag,List,|,Link,Unlink,Img,Flash,|,Table,maxmin,|,Preview,', skin: 'default', hoverExecDelay: 500, layerShadow: 0, emotMark: true, inlineScript: true, internalScript: true, forcePtag: false, modalWidth: 630, modalHeight: 330 };

var xhconfig2 = { tools: 'Fontface,FontSize,Bold,Italic,Underline,|,FontColor,BackColor,SelectAll,Removeformat,|,Blocktag,List,|,Link,Unlink,Img,Flash,|,Table,maxmin,|,Source,|,Preview,Print,', hoverExecDelay: 500, layerShadow: 0, emotMark: true, inlineScript: true, internalScript: true, forcePtag: false };

//扩展string
String.prototype.ToCharArray = function () {
    return this.split("");
}

String.prototype.Reverse = function () {
    return this.split("").reverse().join("");
}

String.prototype.IsNumeric = function () {
    var tmpFloat = parseFloat(this);
    if (isNaN(tmpFloat)) return false;
    var tmpLen = this.length - tmpFloat.toString().length;
    return tmpFloat + "0".Repeat(tmpLen) == this;
};

String.prototype.isInt = function () {
    if (this == "NaN") return false;
    return this == parseInt(this).toString();
};

String.prototype.replaceAll = function (reallyDo, replaceWith, ignoreCase) {
    if (!RegExp.prototype.isPrototypeOf(reallyDo)) {
        return this.replace(new RegExp(reallyDo, (ignoreCase ? "gi" : "g")), replaceWith);
    } else {
        return this.replace(reallyDo, replaceWith);
    }
};

String.prototype.trim = function () {
    return this.replace(/(^s+)|(s+$)/g, "");
};

// bytes length
String.prototype.getRealLength = function () {
    return this.replace(/[^x00-xff]/g, "--").length;
};

String.prototype.left = function (n) {
    return this.slice(0, n);
};

String.prototype.right = function (n) {
    return this.slice(this.length - n);
};

Array.prototype.remove = function (dx) {
    if (isNaN(dx) || dx > this.length) { return false; }
    for (var i = 0, n = 0; i < this.length; i++) {
        if (this[i] != this[dx]) {
            this[n++] = this[i]
        }
    }
    this.length--;
};

String.prototype.endsWith = function (suffix) {
    return this.indexOf(suffix, this.length - suffix.length) !== -1;
};

function Ele(ele) {
    return document.getElementById(ele);
};

function CheckAll(obj, parent) {
    $(obj).parentsUntil(parent | '.list').find(":checkbox").attr("checked", obj.checked);
};

function CheckAllByBtn(obj, check, parent) {
    $(obj).parentsUntil(parent | '.list').find(":checkbox").attr("checked", check);
};

function SubmitForm(action, value, formname) {
    $("#hidAction").val(action);
    $("#hidValue").val(value || "");
    $("#" + (formname || "form1")).submit();
};

function InvertCheck(obj, parent) {
    $(obj).parentsUntil(parent | '.list').find(":checkbox").each(function (index) {
        if (index == 0) return;
        $(this).attr("checked", !$(this).attr("checked"));
    });
};

$(function () {
    //table列表注册
    $("table.list").find("tr").mouseover(function () {
        if ($(this).find("th").length > 0) return;
        $(this).addClass("on");
    }).mouseout(function () {
        $(this).removeClass("on");
    });
    //切换tab内容
    if ($(".contab").length > 0) {
        var count = $(".contab").find("li").length;
        $(".contab").find("li").each(function (index) {
            $(this).bind("click", function () {
                $(".contab").find("li").removeClass();
                $(".cont-3").css("display", "none");
                $(this).addClass("on");
                $(".cont-3:eq(" + index + ")").css("display", "block");
            });
        });
        $(".contab").find("li:eq(0)").trigger("click");
    }
});

function Message(second, content, title, clsfun) {
    $("#msgtitle").html("&nbsp;" + ((title == "" || title == undefined) ? "\u4FE1\u606F\u63D0\u793A" : title));
    $("#msgcontent").html((content == undefined || content == "") ? "\u6267\u884C\u7684\u64CD\u4F5C\u5DF2\u5B8C\u6210\uFF01" : content);
    if (second == undefined) second = 2;
    $("#msgclose").unbind("click").click(function () {
        $("#msgbox").jqmHide();
        if (clsfun != undefined && clsfun != null) { clsfun(); }
    });
    if (second > 0) {
        window.setTimeout(function () { $("#msgclose").trigger("click"); }, second * 1000);
    }
    $("#msgbox").jqmShow();
};

function SAlert(content, second, clsfun) {
    Message(second || 2, content, "\u4FE1\u606F\u63D0\u793A", clsfun);
};

function SConfirm(func, content) {
    $("#confirmcontent").html((content == undefined || content == "") ? "\u786E\u5B9A\u7EE7\u7EED\u6267\u884C\u6B64\u64CD\u4F5C\u5417\uFF1F" : content);
    $("#confirmok").unbind("click");
    $("#confirmok").click(function () { $("#confirmbox").jqmHide(); func(); });
    $("#confirmcancel").click(function () { $("#confirmbox").jqmHide(); });
    $("#confirmbox").jqmShow();
};

function IframeLoaded(iframe, func) {
    if (iframe.attachEvent) {
        iframe.attachEvent("onload", func);
    } else {
        iframe.onload = func;
    }
};

function Loading(msg) {
    $.blockUI({ message: "<div class=\"loading2\">" + (msg || "加载中,请稍等..") + "</div>", overlayCSS: { backgroundColor: '#fff', opacity: 0.7 }, css: { border: 'none', background: "transparent"} });
};

function UnLoading(func) {
    $.unblockUI({ onUnblock: func });
};

function PopWindow(title, content, model, width, height, bindele, clsfun) {
    width = width != undefined && width != 0 ? width : 350;
    height = height != undefined && height != 0 ? height : "auto";
    $("#edittitle").html("&nbsp;" + ((title == undefined || title == "") ? "\u4FE1\u606F\u7A97\u53E3" : title));
    $("#editbox").width(width);
    $("#editcontent").css("height", height);
    $("#editcontent").html(model == "common" ? content : ('<iframe height="100%" width="100%" id="editcontentiframe" scrolling="auto" src="' + content + '" frameborder="0"></iframe>'));
    if (clsfun != undefined && clsfun != null) {
        $("#editclose").unbind("click").click(function () { $("#editbox").jqmHide(); clsfun(); });
    }
    if (bindele != undefined && bindele != null) {
        var pos = {};
        if (bindele.at) { pos = bindele; }
        else { pos = { of: bindele, my: 'left top', at: "left bottom", offset: '0 1px', collision: "fit none" }; }
        $("#editbox").css({ "position": "absolute", "top": "auto", "left": "auto", "margin-left": "auto" }).position(pos);
    }
    else {
        $("#editbox").css({ "position": "fixed", "top": "30%", "left": "50%", "margin-left": -((width || 350) / 2) + "px" });
    }

    //    if (model != "common") {
    //        Loading();
    //        IframeLoaded(Ele("editcontentiframe"), function () { $.unblockUI({ onUnblock: function () { $("#editbox").jqmShow(); } });  });
    //    }
    //    else {
    $("#editbox").jqmShow();
    //    }
};

function ControlPostBack(name, msg) {
    SConfirm(function () {
        __doPostBack(name, '');
    }, msg || "\u786E\u8BA4\u6267\u884C\u6B64\u64CD\u4F5C\u5417\uFF1F");
};

//arguments[0]为指定form，arguments[1]为复选框的name，arguments[2]～arguments[arguments.length - 1]为要激活的按钮
function CheckedEnabledButton() {
    for (var i = 0; i < arguments[0].elements.length; i++) {
        var e = arguments[0].elements[i];
        if (e.name == arguments[1] && e.checked) {
            for (var j = 2; j < arguments.length; j++) {
                Ele(arguments[j]).disabled = false;
            }
            return;
        }
    }
    for (var j = 2; j < arguments.length; j++) {
        Ele(arguments[j]).disabled = true;
    }
};

function CheckByName(form, tname, checked) {
    for (var i = 0; i < form.elements.length; i++) {
        var e = form.elements[i];
        if (e.name == tname) {
            e.checked = checked;
        }
    }
};

function RegisterTableHover() {
    $("table.list").find("tr:gt").hover(function () {
        if ($(this).find("th").length > 0) return;
        $(this).toggleClass("on");
    });
};

function RegisterConTab() {
    $(".contab").find("li").each(function (index) {
        $(this).bind("click", function () {
            $(".contab").find("li").removeClass();
            $(".cont-3").css("display", "none");
            $(this).addClass("on");
            $(".cont-3:eq(" + index + ")").css("display", "block");
        });
    });
    $(".contab").find("li:eq(0)").trigger("click");
};

function EditForList(id, form, page, isone, func) {
    var one = isone == undefined ? false : isone, page = page || "";
    $("#" + id).bind("click", function () {
        var loop = 0, values = "";
        for (var i = 0; i < form.elements.length; i++) {
            var e = form.elements[i];
            if (e.type == "checkbox" && e.name != 'checkall' && e.checked) {
                loop++;
                values += e.value + ",";
                if (loop > 1 && one) {
                    SAlert("\u6B64\u64CD\u4F5C\u53EA\u80FD\u9009\u62E9\u4E00\u884C\uFF0C\u8BF7\u4E0D\u8981\u591A\u9009\uFF01", 2);
                    return;
                }
            }
        }
        values = values.substring(0, values.length - 1);
        if (page != "") {
            location.href = page + values;
        }
        else {
            if (func != undefined && func != null) { func(values) }
            else { __doPostBack(id, ''); }
        }
    });
};

function InsertEditorHTML(editorid, html) {
    eval("CKEDITOR.instances." + editorid + ".insertHtml(html);");
};

function SetEditorHTML(editorid, html) {
    eval("CKEDITOR.instances." + editorid + ".setData(html);");
};

function GetEditorHTML(editorid) {
    return eval("CKEDITOR.instances." + editorid + ".getData();"); ;
};

function InsertPagingTag() {
    var title = $.trim($("#txtPagingTitle").val())
    InsertEditorHTML("txtContent", title == "" ? '[STA:PAGE]' : ("[STA:PAGE=" + title + "]"));
};

function Ajax(action, success, dataType) {
    AjaxPage("../ajax.aspx", "t=" + action, success || null, dataType || "html");
};

function AjaxPage(url, data, func, dtype) {
    $.post(url, data, func, dtype || "html");
};

function ToJson(data) {
    return eval('(' + data + ')');
};

function GetContentList(action, success) {
    Ajax("getcontentlist&" + action, success);
};

function ShowMenuSiteMap() {
    SAlert("暂无地图");
};

function Advise() {
    window.open("http://weibo.com/stacms/profile", "_blank");
};

function RegisterPopInsertText(data, name, field, append) {
    if (ReplaceAll($.trim(data), ",", "") == "") {
        Message(2, "\u6682\u65E0\u6570\u636E\uFF01");
        return;
    }
    var html = "<div class='inner-pad-3'>";
    var list = data.split(",");
    for (var i = 0; i < list.length; i++) {
        if (list[i] == "") continue;
        var strclick = "$('#" + name + "').val('" + list[i] + "');$('#editbox').jqmHide();";
        if (append != undefined && append != null && append) {
            strclick = "Ele('" + name + "').value += (!Ele('" + name + "').value.endsWith(',')&&Ele('" + name + "').value.trim()!=''? ',':'') + '" + list[i] + "';";
        }
        html += "<a href='#' class='inserttext' title='" + list[i] + "' onclick=\"" + strclick + "\" style='margin:0 5px 0 0;'>" + list[i] + "</a>";
    }
    html += "</div>";
    if (field != undefined && field != "") {
        setstr = "<div style=\"padding:0 7px 0 0;font-size:13px;text-align:right;font-weight:bold;cursor:pointer;\" onclick=\"RegEditFieldValue('" + field + "','" + name + "')\"><b>\u8BBE\u7F6E<b><div>";
        html += setstr;
    }
    PopWindow("\u5FEB\u901F\u63D2\u5165", html, "common", 0, 0, $("#" + name));
};

function RegEditFieldValue(field, name) {
    PopWindow("\u53C2\u6570\u8BBE\u7F6E", "../tools/commonfieldedit.aspx?field=" + field, "", 350, 190, $("#" + name))
};

function ReplaceAll(str, oChr, dChr) {
    var len = str.length;
    if (len <= 0) return "";
    for (var i = 0; i < len; i++) {
        str = str.replace(oChr, dChr);
    }
    return str;
};

function SelectExtChannels(ele) {
    var selected = $.trim($("#txtExtChannels").val());
    var html = "<div class='inner-pad-3' id=\"clists\" style=\"height:300px\;overflow-y:auto;\">";
    for (var i = 0; i < channels.length; i++) {
        var checked = "", icheck = "", text = "";
        if (selected != "" && selected.indexOf("," + channels[i].value + ",") >= 0)
            checked = "checked='checked'";
        text = channels[i].text;
        icheck = "<input type='checkbox' " + checked + " value='" + channels[i].value.replace("[X]", "") + "'/>"
        html += "<span title='" + text.replace("[X]", "") + "' style='margin:0 5px 0 0;'>" + icheck + text.replace("[X]", "") + "</span><br/>";
    }
    html += "</div>";
    PopWindow("\u526F\u9891\u9053\u9009\u62E9", html, "common", 400, 0, ele);
    $("#clists").find(":checkbox").each(function () {
        $(this).click(function () {
            if ($(this).attr("checked"))
                $("#txtExtChannels").val($("#txtExtChannels").val() + $(this).val() + ",");
            else
                $("#txtExtChannels").val($("#txtExtChannels").val().replace($(this).val() + ",", ""));
        });
    });
};

function RegPreviewImg(valid, trigger, imgbox) {
    var disimg = function () {
        var imgUrl = $.trim($(valid).val());
        //        if (!IsImgFilename(imgUrl.toLowerCase())) {
        //            if (trigger != undefined && trigger != null) {
        //                $(trigger).attr("href", "javascript:;").unbind("click").hide();
        //            }
        //            if (imgbox != undefined && imgbox != null) {
        //                $(imgbox).html("").hide();
        //            }
        //        } else {
        if (trigger != undefined && trigger != null) {
            if (imgUrl != "") {
                RegPreviewBox(trigger, imgUrl).show();
            } else {
                $(trigger).attr("href", "javascript:;").unbind("click").hide();
            }
        }
        if (imgbox != undefined && imgbox != null) {
            if ($(imgbox).find("img:eq(0)").attr("src") == imgUrl) { return; }
            if (imgUrl != "") {
                $(imgbox).html("<a href=\"" + imgUrl + "\" target=\"_blank\" title=\"查看大图\"><img src='" + imgUrl + "' width='110' height='70'/></a>").show();
            } else {
                $(imgbox).html("");
            }
        }
        //        }
    }
    setInterval(disimg, 500);
};

function RegPreviewVideo(valid, trigger) {
    var disvideo = function () {
        var videourl = $.trim($(valid).val()).toLowerCase();
        if (!IsPlayVideoFilename(videourl)) {
            $(trigger).attr("href", "javascript:;").unbind("click").hide();
        } else {
            $(trigger).attr("href", "../tools/playvideo.aspx?filename=" + encodeURIComponent(videourl));
            RegFancyBoxUrl(trigger).show();
        }
    }
    setInterval(disvideo, 500);
};

function RegPreviewFlv(valid, trigger) {
    var disvideo = function () {
        var videourl = $.trim($(valid).val());
        if (videourl == "") {
            $(trigger).attr("href", "javascript:;").unbind("click").hide();
        } else {
            $(trigger).attr("href", "../tools/playvideo.aspx?type=1&width=700&height=450&filename=" + encodeURIComponent(videourl));
            RegFancyBoxUrl(trigger, 700, 450).show();
        }
    }
    setInterval(disvideo, 500);
};


function RegPreviewFlash(valid, trigger) {
    var disflash = function () {
        var flashurl = $.trim($(valid).val()).toLowerCase();
        var temp = flashurl.split(".");
        if (temp[temp.length - 1].toLowerCase() != "swf") {
            $(trigger).attr("href", "javascript:;").unbind("click").hide();
        } else {
            RegPreviewBox(trigger, flashurl).show();
        }
    }
    setInterval(disflash, 500);
};

function IsPlayVideoFilename(video) {
    var temp = video.split(".");
    return $.inArray(temp[temp.length - 1].toLowerCase(), ["flv", "mp4"]) >= 0;
};

function IsImgFilename(img) {
    var temp = img.split(".");
    return $.inArray(temp[temp.length - 1].toLowerCase(), ["jpg", "jpeg", "bmp", "gif", "png"]) >= 0;
};

function RegPreviewBox(trigger, imgurl) {
    return RegFancyBox($(trigger).attr("href", imgurl || ($(trigger).attr("href"))));
};

function RegFancyBox(selector) {
    return $(selector).fancybox({
        openEffect: 'elastic',
        closeEffect: 'elastic',
        helpers: {
            title: {
                type: 'outside'
            },
            overlay: {
                speedIn: 500,
                opacity: 0.6, //parseFloat(config.overlay) / 100
                css: {
                    'background-color': '#000' //$("#overlay_backgroundcolor").css("background-color"),
                }
            }
        }
    });
};

function RegFancyBoxUrl(selector, width, height) {
    return $(selector).fancybox({
        maxWidth: 2000,
        maxHeight: 1500,
        fitToView: false,
        width: width || '80%',
        height: height || '70%',
        autoSize: false,
        closeClick: false,
        openEffect: 'none',
        closeEffect: 'none'
    });
};

function RegStopInput(selector) {
    $(selector).bind("keydown", function () { return false; });
};

function RegPostip(selctor) {
    return $(selctor).poshytip({ className: 'tip-yellowsimple', alignTo: 'target', alignX: 'right', alignY: 'center', offsetX: 5, offsetY: 5 });
};

function RegSelectFilePopWin(triggerid, title, query, trigger, bindele, pos, selectpath) {
    var url = (selectpath || "../tools/selectfile.aspx") + "?" + query;
    var popwin = function () { PopWindow((title || "文件浏览器"), url, "", 630, 300, bindele, pos); };
    $("#" + triggerid).bind((trigger || "dblclick"), popwin);
};

function RegSelectPathPopWin(triggerid, title, query, trigger, bindele, pos, selectpath) {
    var popwin = function () { PopWindow((title || "路径选择器"), (selectpath || "../tools/selectpath.aspx") + "?" + query, "", 510, 270, bindele, pos); };
    $("#" + triggerid).bind((trigger || "dblclick"), popwin);
};

function SetRadioValue(name, val) {
    $.each($("input[name='" + name + "']"), function (idx, obj) {
        if ($(obj).val() == val) { $(obj).attr("checked", "checked"); }
    });
};

function GetRadioValue(name) {
    return $.grep($("input[name='" + name + "']"), function (obj) { return $(obj).attr("checked") == "checked"; })[0].value;
};

function GetFileExtStr(ext) {
    if ($.inArray(ext, ("jpg,jpeg,gif,bmp,png,tga,exif").split(",")) >= 0) { return "图片"; }
    else if (ext == "swf") { return "Flash动画"; }
    else if ($.inArray(ext, ("mp3,wma,wav,mod,cd").split(",")) >= 0) { return "音乐"; }
    else if ($.inArray(ext, ("avi,rmvb,flv,mkv,3gp,mp4,mod,cd,wmv").split(",")) >= 0) { return "视频"; }
    else return "附件";
};

function ClipReg(content, id, tip) {
    ZeroClipboard.setMoviePath('../images/ZeroClipboard.swf');
    clip = new ZeroClipboard.Client();
    clip.setHandCursor(true);
    clip.setText(content);
    clip.addEventListener('onComplete', function () { SAlert(tip || '复制成功！', 1); });
    clip.glue(id);
};

function ConvertSize(size) {
    if (!size) { return '0 B'; }
    var sizeNames = ['B', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB']; //Bytes
    var i = Math.floor(Math.log(size) / Math.log(1024));
    var p = (i > 1) ? 2 : 0;
    return (size / Math.pow(1024, Math.floor(i))).toFixed(p) + " " + sizeNames[i];
};

function IsFileReadType(file) {
    var temp = file.toLowerCase().split(".");
    return $.inArray(temp[temp.length - 1], ("html,htm,jhtml,shtml,js,css,aspx,cs,ini,asp,aspx,ashx,asmx,asax,sitemap,txt,php,jsp,config,xml,sql").split(",")) >= 0;
};

function GetCutString(text, size, tail) {
    var x = 0;
    str = text.replace(/[\s\S]/g, function (d, i, s) {
        if (d.charCodeAt(0) > 127) x++;
        if (x + i >= size) return "";
        return d;
    });
    return str != "" ? (str != text ? (str + tail) : str) : str;
};

function RegColumnPostip(selector, maxlength, tail) {
    $(selector).each(function () {
        var title = $.trim($(this).html()), target;
        target = GetCutString(title, maxlength, tail);
        if (target != "" && target.length < title.length) {
            $(this).attr("title", title).html(target);
            $(this).poshytip({ className: 'tip-yellowsimple', alignTo: 'target', alignX: 'center', alignY: 'top', offsetX: 5, offsetY: 5 });
        }
        else {
            $(this).attr("title", "");
        }
    });
};

function RegColumnImg(selector, nopicurl) {
    $(selector).each(function (idx, obj) {
        var url = $.trim($(this).attr("url"));
        var isimg = !(url == "" || !IsImgFilename(url));
        url = !isimg ? (nopicurl || "../images/nopic.png") : url;
        $("<img/>").attr("src", url).load(function () {
            var img = ResizeImg($(this), 80, 50);
            $(obj).html("<a href=\"" + url + "\" target=\"_blank\" title=\"单击查看大图\"><img src=\"" + url + "\" height=\"" + img.height + "\" width=\"" + img.width + "\" onerror=\"this.src='../images/nopic.png'\"/></a>");
        });

    });
};

function RegColorPicer(trigger, target, onchange) {
    $(trigger).ColorPicker({
        color: $(target || trigger).val(),
        onChange: function (hsb, hex, rg) {
            if (onchange != undefined && onchange != null) { onchange("#" + hex) }
            $(target || trigger).val(hex);
        },
        onSubmit: function (hsb, hex, rgb, el) {
            $(el).ColorPickerHide();
        }
    });
};

function RegChannelSelect(ddrid) {
    $("#" + ddrid).change(function () {
        if ($("#" + ddrid + " option:selected").text().indexOf("[X]") > 0) {
            Message(2, "不是有效的频道类型,带<strong>[X]</strong>的不能被选择!");
            $("#" + ddrid).attr("value", "0");
        }
    }).trigger("change");
};

function FastEditTr(url, id, colspan, width, height) {
    var fid = "#fastedit" + id, width = width || "100%", height = height || "100%";
    if (!$(fid).attr("id")) {
        $(".fastedit").remove();
        var iframetr = '<tr class="fastedit" id="fastedit' + id + '" style="display:none;"><td colspan="' + colspan + '"><iframe id="fasteditiframe" height="' + height + '" width="100%" scrolling="no" src="' + url + id + '" frameborder="0"></iframe></td></tr>';
        $("tr[tid='" + id + "']").after(iframetr);

        Loading();
        IframeLoaded(Ele("fasteditiframe"), function () { $.unblockUI({ onUnblock: function () { $(fid).css("display", ""); } }); });
    } else {
        $(fid).remove();
    }
};

function ResizeImg(ImgD, iwidth, iheight) {
    var image = new Image(), width = iwidth, height = iheight;
    image.src = $(ImgD).attr("src");
    if (image.width > 0 && image.height > 0) {
        if (image.width / image.height >= iwidth / iheight) {
            if (image.width > iwidth) {
                width = iwidth;
                height = (image.height * iwidth) / image.width;
            } else {
                width = image.width;
                height = image.height;
            }
        }
        else {
            if (image.height > iheight) {
                height = iheight;
                width = (image.width * iheight) / image.height;
            } else {
                width = image.width;
                height = image.height;
            }
        }
    }
    return { width: width, height: height };
};

function IsUrl(str) {
    if ($.trim(str) == "") { return false; }
    if ((str.indexOf('http://') != -1) || (str.indexOf('https://') != -1) || (str.indexOf('HTTP://') != -1) || (str.indexOf('HTTPS://') != -1)) {
        if ((str == 'http://') || (str == 'https://') || (str == 'HTTP://') || (str == 'HTTPS://')) {
            return false;
        }
        return true;
    }
    return false;
};

function OpenInputLink(input) {
    var link = $(input).val();
    if (IsUrl(link)) {
        window.open(link, '_blank');
    } else {
        SAlert("请设置正确的超链接格式！");
    }

};

function RandStr(len) {
    var array = new Array("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"), ret = "";
    for (var i = 0; i < len; i++) {
        ret += array[Math.floor((array.length) * Math.random())];
    }
    return ret;
};

function ConStatus(status) {
    switch (parseInt(status)) {
        case 0: return "草稿";
        case 1: return "待审核";
        case 2: return "通过";
        case 3: return "<font color='#ff0000'>退稿</font>";
        default: return status;
    }
}

function RandNum(len) {
    var array = new Array("0", "1", "2", "3", "4", "5", "6", "7", "8", "9"), ret = "";
    for (var i = 0; i < len; i++) {
        ret += array[Math.floor((array.length) * Math.random())];
    }
    return ret;
};

function RegSetTargetValue(sources, target, trigger) {
    $.each(sources, function (idx, obj) {
        $(obj).bind(trigger || "click", function () {
            $(target).val($(obj).html());
        });
    });
};

function selByCheckName(name, checked) {
    $("input[name='" + name + "']").attr("checked", checked);
};

function selByCheckValue(value, checked) {
    $("input[value='" + value + "']").attr("checked", checked);
};

function queryString(key) {
    var re = new RegExp('(?:\\?|&)' + key + '=(.*?)(?=&|$)', 'gi');
    var r = [], m;
    while ((m = re.exec(document.location.search)) != null) r.push(m[1]);
    return r;
};

function hashString(queryname, def) {
    var qKeys = {};
    var re = /[?#]([^=]+)(?:=([^#]*))?/g;
    var matchInfo;
    while (matchInfo = re.exec(location.hash)) {
        qKeys[matchInfo[1]] = matchInfo[2];
    }
    return typeof (qKeys[queryname]) == 'undefined' ? (def || '') : qKeys[queryname];
};

function HtmlEncode(str) {
    var s = "";
    if (str.length == 0) return "";
    for (var i = 0; i < str.length; i++) {
        switch (str.substr(i, 1)) {
            case "<": s += "&lt;"; break;
            case ">": s += "&gt;"; break;
            case "&": s += "&amp;"; break;
            case " ":
                if (str.substr(i + 1, 1) == " ") {
                    s += " &nbsp;";
                    i++;
                } else s += " ";
                break;
            case "\"": s += "&quot;"; break;
            case "\n": s += "<br/>"; break;
            default: s += str.substr(i, 1); break;
        }
    }
    return s;
};

var ConFileUpLoad = function (config) {

    var ifr = null,
        fm = null,
        defConfig = {
            submitBtn: $('#fileup_submit'), //触发提交按钮
            trigger: "click", //触发提交事件,
            action: "", //post页面
            complete: function (response) { }, //上传成功后回调
            beforeUpLoad: function () { return true }, //点击提交未上传时回调
            afterUpLoad: function () { } //点击提交上传后回调
        };

    var IFRAME_NAME = 'fileUpLoadIframe' + RandNum(12);

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

        //上传完毕iframe onload事件
        ifr.load(function () {
            var response = this.contentWindow.document.body.innerHTML;
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

var ImgSetFileUpload = function (config) {
    defConfig = {
        fileKey: 'locfileuplad',
        set: $("#txtImg")
    };
    config = $.extend(defConfig, config);

    ConFileUpLoad({
        submitBtn: $("input[name='" + config.fileKey + "']"),
        trigger: 'change',
        action: '../tools/contdo.aspx?t=fileupload&filekey=' + config.fileKey + '&filetype=jpeg,jpg,gif,png,bmp',
        complete: function (response) {
            $.unblockUI();
            var finfo = ToJson(response);
            if (finfo.msg != "") {
                SAlert(finfo.msg);
            } else {
                config.set.val(finfo.path);
            }
        },
        afterUpLoad: function () {
            Loading("上传中,请稍等..")
        }
    });
};

function RegInputNoNullCheck(set) {
    defSet = {
        subtn: $("#SaveInfo"),
        list: [["#txtTitle", "标题不能为空"], ["#ddlConType", "请选择主频道"]],
        success: function () { __doPostBack("SaveInfo", ""); }
    };
    set = $.extend(defSet, set);
    set.subtn.click(function () {
        for (var i = 0; i < set.list.length; i++) {
            if ($.trim($(set.list[i][0]).val()) == "") {
                SAlert(set.list[i][1] || "信息填写不完整！");
                return;
            }
        }
        set.success();
    });
};

function ContAddOnSubmit(tname) {
    $("#SaveInfo").click(function () {
        var title = $.trim($("#txtTitle").val()), chlid = $("#ddlConType").val();
        if (title == "") {
            SAlert((tname || "") + "标题不能为空！");
            return;
        } else if (chlid == "0") {
            SAlert("请选择" + (tname || "") + "主频道！");
            return;
        }
        __doPostBack("SaveInfo", "");
    });
};

function GetCbCheckedVals(name, split) {
    var ret = "";
    $.each($("input[name='" + (name || "cbid") + "']:checked"), function (idx, obj) { ret += $(obj).val() + (split || ","); });
    return ret != "" ? ret.substring(0, ret.length - (split || ",").length) : ret;
};