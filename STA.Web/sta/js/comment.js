var cmtlist = [], cmtcid = 0, cmtcurpage = 0, cmtperpage = 10, cmtpcount = 0, cmtrcount = 0, cmtorder = "";

function getcomment(id) {
    if (id == "0" || cmtlist.length == 0) { return null };
    for (var i = 0; i < cmtlist.length; i++) {
        if (cmtlist[i].id == id) return cmtlist[i];
    }
    return null;
};

function cmtdataload(cid, page, okfunc) {
    cmtcid = cid;
    $(".cmtload").show();
    $(".cmtsofa").hide();
    $(".cmtlist").html("").hide();
    ajax("getconcomment&ctype=1&order=" + cmtorder + "&cid=" + cid + "&pagesize=" + cmtperpage + "&page=" + page, function (dstr) {
        //console.log(dstr);
        if ($.trim(dstr) == "") {
            $(".cmtload").hide();
            $(".cmtsofa").show();
        } else {
            $(".cmtload").hide();
            $(".cmtsofa").hide();
            var data = toJson(dstr);
            cmtrcount = parseInt(data.recordcount);
            cmtpcount = parseInt(data.pagecount);
            cmtcurpage = page;
            cmtlist = data.content;
            $.each(cmtlist, function (idx, obj) {
                $(".cmtlist").append(getcommentstr(obj));
            });
            $(".cmtlist").show();
            cmtpaging();
        }
        if (okfunc != undefined) { okfunc(); }
    });
};

function pageselectCallback(page_index, jq) {
    var alist = $(".cmtpage").find("a");
    $.each(alist, function (idx, obj) {
        var html = $(this).html();
        if (html == "上一页") {
            if (cmtcurpage > 1) {
                $(obj).click(function () { cmtdataload(cmtcid, cmtcurpage - 1); });
            }
        } else if (html == "下一页") {
            if (cmtcurpage < cmtpcount) {
                $(obj).click(function () { cmtdataload(cmtcid, cmtcurpage + 1); });
            }
        } else {
            var tempage = parseInt(html);
            if (tempage != cmtcurpage) {
                $(obj).click(function () { cmtdataload(cmtcid, tempage); });
            }
        }
    });
    return false;
}

function cmtpaging() {
    $(".cmtpage").pagination(cmtrcount, {
        items_per_page: cmtperpage,
        num_display_entries: 5,
        num_edge_entries: 2,
        current_page: cmtcurpage - 1,
        prev_text: "上一页",
        next_text: "下一页",
        callback: pageselectCallback
    });
}

function cmtgopage(page) {
    location.href = location.href.substring(0, location.href.indexOf("#")) + "#page=" + page;
}

function getreplaystr(replay, id, msgwidth) {
    var vcode = "<img src=\"" + (webconfig.webdir + "/sta/vcode/validatecode.aspx?cookiename=stausercommentsubmit" + id + "&live=0") + "\" onclick=\"this.src='" + webconfig.webdir + "/sta/vcode/validatecode.aspx?cookiename=stausercommentsubmit" + id + "&live=0&date='+ new Date();\" height=\"23\" width=\"70\" style=\"cursor:pointer;\" class=\"img-vcode\" title=\"刷新验证码\"/>";
    return $(".replaytpl").html().replace("#replayid", replay).replaceAll("#id", id).replace("#vcode", vcode);
}

function cmtreplay(id, tempid) {
    if (cmtmustlogin()) return;

    var rpy = $(".replay-" + tempid);
    if (rpy.html() == null) {
        $(".com-" + tempid).append(getreplaystr(id, tempid));
    } else {
        rpy.remove();
    }
};

function cmtdigg(obj, id, type) {
    var tstr = type == "digg" ? "顶" : "踩", cname = "stacommentdigg" + id, cmt = getcomment(id);
    var val = $.cookie(cname);
    if (val == null || val == "") {
        ajax("com" + type + "&id=" + id, function (ret) {
            if (ret == "True") {
                $(obj).html(tstr + "[" + (parseInt($(obj).html().replace(tstr,"").replace("]","").replace("[","")) + 1) + "]");
                $.cookie(cname, tstr, { path: "/", expires: 30000 });
                //$.dialog.tip(tstr + "了一下！");
            } else {
                $.dialog.tip(tstr + "失败,请稍后再试！");
            }
        });
    } else {
        $.dialog.tip("您已经" + val + "过了！");
    }
};
function getcommentstr(cmt) {
    var face = "<img src=\"" + (userAvatar(cmt.uid)) + "\" onerror=\"this.src='" + webconfig.weburl + webconfig.webdir + "/sta/pics/avator/noavatar_medium.gif'\"/>";
    var uurl = cmt.uid == "0" ? "javascript:;" : (webconfig.webdir + "/userinfo.aspx?id=" + cmt.uid);
    return $(".comtpl").html().replace("#quote", cmt.quote).replaceAll("cmtid",cmt.id).replaceAll("#digg", cmt.diggcount).replaceAll("#stamp", cmt.stampcount).replaceAll("#uid", cmt.uid).replaceAll("#username", cmt.username).replaceAll("#time", cmt.addtime.replaceAll("\/", "-")).replace("#msg", cmt.msg).replaceAll("#replay", cmt.replay).replace("#face", face).replaceAll("#uurl", uurl).replaceAll("#tempid", cmtrandstr(12)).replaceAll("#id", cmt.id).replace("#city", cmt.city == "" ? "火星" : cmt.city);
};

function cmtmustlogin() {
    if (webconfig.commentlogin == 1 && $.inArray(stauser.userid, ["", "0", "-1"]) >= 0) {
        artDialog.tips("登陆后方可评论,现在为您转到登陆页...", 2, "", function () { location.href = webconfig.webdir + "/login.aspx?returnurl=" + location.href; });
        return true;
    }
    return false;
};

function subcomment(id, cid, ctitle, title, okfunc) {
    if (cmtmustlogin()) return;

    var msg = escape($(".msg" + id).val());
    if (msg == "") {
        $.dialog.tip("评论内容不能为空！");
        return;
    }
    var data = "subcomment&vcode=" + $(".vcode" + id).val() + "&cname=stausercommentsubmit" + id + "&cid=" + cid + "&contitle=" + escape(ctitle) + "&uid=" + stauser.userid + "&username=" + stauser.username + "&title=" + escape(title) + "&msg=" + msg + "&replay=" + $(".replay" + id).val();
    loading("正在提交评论,请稍等..");
    ajax(data, function (ret) {
        $.unblockUI();
        if (ret.indexOf("！") >= 0) {
            $.dialog.tip(ret);
        } else {
            if (ret == "0") {
                $.dialog.tip("评论提交失败！");
            } else {
                $(".comment").val("");
                $(".replay-" + id).css("display", "none");
                if (webconfig.commentverify == 1) {
                    $.dialog.tip("您的评论已经提交,但需要审核方能显示！", 3);
                } else {
                    $.dialog.tip("评论发布成功！", 1)
                    cmtdataload(cid, 1, okfunc);
                }
            }
        }
    });
};

function cmtrandstr(len) {
    var array = new Array("0", "1", "2", "3", "4", "5", "6", "7", "8", "9"), ret = "";
    for (var i = 0; i < len; i++) {
        ret += array[Math.floor((array.length) * Math.random())];
    }
    return ret;
};
