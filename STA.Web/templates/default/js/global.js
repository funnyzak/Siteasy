function loading(msg) {
    $.blockUI({ message: "<div class=\"loading2\">" + (msg || "加载中,请稍等..") + "</div>", overlayCSS: { backgroundColor: '#fff', opacity: 0.7 }, css: { border: 'none', background: "transparent"} });
};

function setHomePage(url) {
    if (document.all) {
        document.body.style.behavior = 'url(#default#homepage)';
        document.body.setHomePage(url);
    } else if (window.sidebar) {
        if (window.netscape) {
            try {
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
            } catch (e) {
                alert("亲爱的用户你好：\n你使用的不是IE浏览器，此操作被浏览器阻挡了，你可以选择手动设置为首页！\n给你带来的不便，本站深表歉意。");
            }
        }
    }
};

var ay = ["键入关键字搜索..", "/images/pub/nopic.png"];

function searchEvent(txtobj, btnobj) {
    var gosearch = function () {
        var v = $.trim($(txtobj || ".query").val());
        if (v.length == 0 || v == ay[0]) {
            $(txtobj || ".query").select();
            return false;
        };
        location.href = webconfig.webdir + "/search.aspx?persize=15&query=" + escape(v);
    };
    $(btnobj || ".gosearch").bind("click", gosearch);
    $(txtobj || ".query").keypress(function (event) {
        if (event.which == '13') { gosearch(); }
    });
};

function resizeImg(ImgD, iwidth, iheight) {
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


jQuery.getPos = function (e) {
    var l = 0;
    var t = 0;
    var w = jQuery.intval(jQuery.css(e, 'width'));
    var h = jQuery.intval(jQuery.css(e, 'height'));
    var wb = e.offsetWidth;
    var hb = e.offsetHeight;
    while (e.offsetParent) {
        l += e.offsetLeft + (e.currentStyle ? jQuery.intval(e.currentStyle.borderLeftWidth) : 0);
        t += e.offsetTop + (e.currentStyle ? jQuery.intval(e.currentStyle.borderTopWidth) : 0);
        e = e.offsetParent;
    }
    l += e.offsetLeft + (e.currentStyle ? jQuery.intval(e.currentStyle.borderLeftWidth) : 0);
    t += e.offsetTop + (e.currentStyle ? jQuery.intval(e.currentStyle.borderTopWidth) : 0);
    return { x: l, y: t, w: w, h: h, wb: wb, hb: hb };
};
jQuery.getClient = function (e) {
    if (e) {
        w = e.clientWidth;
        h = e.clientHeight;
    } else {
        w = (window.innerWidth) ? window.innerWidth : (document.documentElement && document.documentElement.clientWidth) ? document.documentElement.clientWidth : document.body.offsetWidth;
        h = (window.innerHeight) ? window.innerHeight : (document.documentElement && document.documentElement.clientHeight) ? document.documentElement.clientHeight : document.body.offsetHeight;
    }
    return { w: w, h: h };
};
jQuery.getScroll = function (e) {
    if (e) {
        t = e.scrollTop;
        l = e.scrollLeft;
        w = e.scrollWidth;
        h = e.scrollHeight;
    } else {
        if (document.documentElement && document.documentElement.scrollTop) {
            t = document.documentElement.scrollTop;
            l = document.documentElement.scrollLeft;
            w = document.documentElement.scrollWidth;
            h = document.documentElement.scrollHeight;
        } else if (document.body) {
            t = document.body.scrollTop;
            l = document.body.scrollLeft;
            w = document.body.scrollWidth;
            h = document.body.scrollHeight;
        }
    }
    return { t: t, l: l, w: w, h: h };
};
jQuery.intval = function (v) {
    v = parseInt(v);
    return isNaN(v) ? 0 : v;
};
jQuery.fn.ScrollTo = function (s) {
    o = jQuery.speed(s);
    return this.each(function () {
        new jQuery.fx.ScrollTo(this, o);
    });
};
jQuery.fx.ScrollTo = function (e, o) {
    var z = this;
    z.o = o;
    z.e = e;
    z.p = jQuery.getPos(e);
    z.s = jQuery.getScroll();
    z.clear = function () { clearInterval(z.timer); z.timer = null };
    z.t = (new Date).getTime();
    z.step = function () {
        var t = (new Date).getTime();
        var p = (t - z.t) / z.o.duration;
        if (t >= z.o.duration + z.t) {
            z.clear();
            setTimeout(function () { z.scroll(z.p.y, z.p.x) }, 13);
        } else {
            st = ((-Math.cos(p * Math.PI) / 2) + 0.5) * (z.p.y - z.s.t) + z.s.t;
            sl = ((-Math.cos(p * Math.PI) / 2) + 0.5) * (z.p.x - z.s.l) + z.s.l;
            z.scroll(st, sl);
        }
    };
    z.scroll = function (t, l) { window.scrollTo(l, t) };
    z.timer = setInterval(function () { z.step(); }, 13);
};