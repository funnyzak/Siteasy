ue_deftoolbars = [
    ['fullscreen', 'source', '|', 'undo', 'redo', '|',
    'bold', 'italic', 'underline', 'strikethrough', 'superscript', 'subscript', 'removeformat', 'formatmatch', 'autotypeset', 'blockquote', 'pasteplain', '|', 'forecolor', 'backcolor', 'insertorderedlist', 'insertunorderedlist', 'selectall', 'cleardoc'],
    ['lineheight', 'paragraph', '|', 'fontfamily', 'fontsize', '|', 'indent', '|',
    'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|', 'link', 'unlink', 'anchor'],
    ['imagenone', 'imageleft', 'imageright', 'imagecenter', '|', 'insertimage', 'attachment', 'insertvideo', 'music' , 'emotion', 'map', 'pagebreak', 'template', '|',
    'horizontal', 'spechars', '|', 'inserttable', '|', 'print', 'preview']
];

function checkedEnabledButton() {
    for (var i = 0; i < arguments[0].elements.length; i++) {
        var e = arguments[0].elements[i];
        if (e.name == arguments[1] && e.checked) {
            for (var j = 2; j < arguments.length; j++) {
                document.getElementById(arguments[j]).disabled = false;
            }
            return;
        }
    }
    for (var j = 2; j < arguments.length; j++) {
        document.getElementById(arguments[j]).disabled = true;
    }
};

function checkByName(form, tname, checked) {
    for (var i = 0; i < form.elements.length; i++) {
        var e = form.elements[i];
        if (e.name == tname) {
            e.checked = checked;
        }
    }
};

function saveProfileForm(formid) {
    loading();
    $.post(webconfig.webdir + "/useraction.aspx", $(formid).serialize() + "&action=saveprofile", function (resp) {
        if (resp == "") {
            $.unblockUI();
            $.dialog.tip("个人信息已保存");
        }
    });
};

function changePasswordForm(formid) {
    loading("数据传送中..");
    $.post(webconfig.webdir + "/useraction.aspx", $(formid).serialize() + "&action=changepassword", function (resp) {
        $.unblockUI();
        if (resp == "") {
            $(formid)[0].reset();
            $.dialog.tip("密码已成功修改,下次请用新密码登录");
        } else {
            $.dialog.alert(resp)
        }
    });
};

function changeEmailForm(formid) {
    loading("数据传送中..");
    $.post(webconfig.webdir + "/useraction.aspx", $(formid).serialize() + "&action=changeemail", function (resp) {
        $.unblockUI();
        if (resp == "") {
            $(formid)[0].reset();
            $.dialog.tip("邮箱已成功修改");
        } else {
            $.dialog.alert(resp)
        }
    });
};

function loading(msg) {
    $.blockUI({ message: "<div class=\"loading\">" + (msg || "数据保存中..") + "</div>", overlayCSS: { backgroundColor: '#fff', opacity: 0.7 }, css: { border: 'none', background: "transparent"} });
};

function delPms(pmids, url) {
    if (pmids == "") {
        $.dialog.alert("请选择要删除的项！");
        return;
    } else {
        $.dialog.confirm("确定要删除吗?", function () {
            loading("正在删除,请稍等..");
            $.post(webconfig.webdir + "/useraction.aspx", "action=managepm&rtype=delpms&pmids=" + pmids, function (resp) {
                $.unblockUI();
                if (resp == "1") {
                    $.dialog.tip("所选项已成功删除！", 1, function () {
                        location.href = (url == undefined || url == "") ? location.href : url;
                    });
                } else {
                    $.dialog.alert("删除失败,请检查您的网络是否畅通！");
                }
            });
        });
    }
};

function delFavorites(cids, typeid, url) {
    if (cids == "") {
        $.dialog.alert("请选择要删除的项！");
        return;
    } else {
        $.dialog.confirm("确定要删除吗?", function () {
            loading("正在删除,请稍等..");
            $.post(webconfig.webdir + "/useraction.aspx", "action=delfavorites&typeid=" + typeid + "&cids=" + cids, function (resp) {
                $.unblockUI();
                if (parseInt(resp) >= 1) {
                    $.dialog.tip("所选项已成功删除！", 1, function () {
                        location.href = (url == undefined || url == "") ? location.href : url;
                    });
                } else {
                    $.dialog.alert("删除失败！");
                }
            });
        });
    }
};

function delComment(cid, ctype, url) {
    $.dialog.confirm("确定要删除吗?", function () {
        loading("正在删除,请稍等..");
        $.post(webconfig.webdir + "/useraction.aspx", "action=delcomment&ctype=" + ctype + "&id=" + cid, function (resp) {
            $.unblockUI();
            if (parseInt(resp) >= 1) {
                $.dialog.tip("成功删除！", 1, function () {
                    location.href = (url == undefined || url == "") ? location.href : url;
                });
            } else {
                $.dialog.alert("删除失败！");
            }
        });
    });
};

function delCons(ids, url) {
    $.dialog.confirm("确定要删除吗?", function () {
        loading("正在删除,请稍等..");
        $.post(webconfig.webdir + "/useraction.aspx", "action=delcons&ids=" + ids, function (resp) {
            $.unblockUI();
            if (parseInt(resp) >= 1) {
                $.dialog.tip("成功删除了"+ resp +"条！", 1, function () {
                    location.href = (url == undefined || url == "") ? location.href : url;
                });
            } else {
                $.dialog.alert("删除失败！");
            }
        });
    });
};

function delAtts(ids, url) {
    $.dialog.confirm("确定要删除吗?", function () {
        loading("正在删除,请稍等..");
        $.post(webconfig.webdir + "/useraction.aspx", "action=delatts&ids=" + ids, function (resp) {
            $.unblockUI();
            if (parseInt(resp) >= 1) {
                $.dialog.tip("成功删除了" + resp + "个附件！", 1, function () {
                    location.href = (url == undefined || url == "") ? location.href : url;
                });
            } else {
                $.dialog.alert("删除失败！");
            }
        });
    });
};


function setPmsState(pmids, state, url) {
    if (pmids == "") {
        $.dialog.alert("请选择要标记的项！");
        return;
    } else {
        loading("正在标记,请稍等..");
        $.post(webconfig.webdir + "/useraction.aspx", "action=managepm&rtype=setstate&pmids=" + pmids + "&state=" + state, function (resp) {
            $.unblockUI();
            if (resp == "1") {
                $.dialog.tip("所选项已成功标记为" + ((state || 0) ? "未读" : "已读") + "！", 1, function () {
                    location.href = (url == undefined || url == "") ? location.href : url;
                });
            } else {
                $.dialog.alert("标记失败,请检查您的网络是否畅通！");
            }
        });
    }
};

function conImgUp(fieldkey, func) {
    this.upload = fileUpload({
        submitBtn: $("input[name='" + fieldkey + "']"),
        trigger: 'change',
        action: webconfig.webdir + '/useraction.aspx?action=conimgup&filekey=' + fieldkey,
        complete: function (response) {
            $.unblockUI();
            console.log(response);
            var finfo = toJson(response) || {};
            if (finfo.msg != "1") {
                $.dialog.alert(finfo.msg);
            } else {
                func(finfo.path);
            }
        },
        afterUpLoad: function () {
            loading("图片上传中,请稍等..")
        }
    });
};

function regPreviewImgBox(valid, imgbox) {
    $(valid).change(function () {
        var imgUrl = $.trim($(valid).val());
        if (imgbox != undefined && imgbox != null) {
            if ($(imgbox).find("img:eq(0)").attr("src") == imgUrl) { return; }
            if (imgUrl != "") {
                $(imgbox).html("<a href=\"" + imgUrl + "\" target=\"_blank\" title=\"查看大图\"><img onerror=\"this.src='" + webconfig.webdir + "/sta/pics/noimage.jpg'\" src='" + imgUrl + "' width='110' height='70'/></a>").show();
            } else {
                $(imgbox).html("");
            }
        }
    }).trigger("change");
};

function getCbCheckedVals(name, split) {
    var ret = "";
    $.each($("input[name='" + (name || "cbid") + "']:checked"), function (idx, obj) { ret += $(obj).val() + (split || ","); });
    return ret != "" ? ret.substring(0, ret.length - (split || ",").length) : ret;
};

function checkForOne(cbname) {
    if (getCbCheckedVals(cbname).split(",").length > 1) {
        $.dialog.alert("只能选择一行进行操作！");
        return false;
    }
    return true;
};

function convertSize(size) {
    if (!size) { return '0 B'; }
    var sizeNames = ['B', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB']; //Bytes
    var i = Math.floor(Math.log(size) / Math.log(1024));
    var p = (i > 1) ? 2 : 0;
    return (size / Math.pow(1024, Math.floor(i))).toFixed(p) + " " + sizeNames[i];
};

function getFileExtStr(ext) {
    if ($.inArray(ext, ("jpg,jpeg,gif,bmp,png,tga,exif").split(",")) >= 0) { return "图片"; }
    else if (ext == "swf") { return "Flash"; }
    else if ($.inArray(ext, ("mp3,wma,wav,mod,cd").split(",")) >= 0) { return "音乐"; }
    else if ($.inArray(ext, ("avi,rmvb,flv,mkv,3gp,mp4,mod,cd,wmv").split(",")) >= 0) { return "视频"; }
    else return "附件";
};