$(function () {
    var jcrop_api, boundx, boundy;

    var avatar = {
        upload: null,
        elm: {},
        params: {},
        avtf: null,
        init: function () {
            var fieldkey = "useravatar";
            this.upload = fileUpload({
                submitBtn: $("input[name='" + fieldkey + "']"),
                trigger: 'change',
                action: webconfig.webdir + '/useraction.aspx?action=useravatarup&filekey=' + fieldkey,
                complete: function (response) {
                    $.unblockUI();
                    //console.log(response);
                    var finfo = toJson(response) || {};
                    if (finfo.msg != "1") {
                        $.dialog.alert(finfo.msg);
                    } else {
                        avatar.startCorp(finfo.path);
                    }
                },
                afterUpLoad: function () {
                    loading("头像上传中,请稍等..")
                }
            });

            this.elm.target = $('#target');
            this.elm.lPrev = $('#preview_l');
            this.elm.mPrev = $('#preview_m');
            this.elm.sPrev = $('#preview_s');
            this.elm.btnSave = $('#avatarsave');
            this.elm.btnSave.click(function () {
                var coords = "350," + avatar.params.x + "," + avatar.params.y + "," + avatar.params.w + "," + avatar.params.h;
                $("#hidcoords").val(coords);
                //console.log(avatar.params.x + "," + avatar.params.y + "," + avatar.params.w + "," + avatar.params.h);
                loading("处理中,请稍等..")
                $.post(webconfig.webdir + "/useraction.aspx", { action: "useravatarset", avatar: encodeURI(avatar.avtf), coords: coords },
                function (ret) {
                    $.unblockUI();
                    if (ret == "") {
                        $.dialog.tip("头像设置成功");
                    } else {
                        $.dialog.tip(ret);
                    }
                });
            });

            if (this.elm.target.attr("src").indexOf("noavatar") < 0) {
                this.startCorp(this.elm.target.attr("src"));
            }
        },
        startCorp: function (imgsrc) {
            var img = new Image();
            img.src = avatar.avtf = imgsrc;
            $("#hiduseravatar").val(imgsrc);
            $(img).load(function () { avatar.updateScale(img); });
        },
        updateScale: function (img) {
            var w = img.width, h = img.height;
            avatar.elm.target.attr('src', img.src);
            avatar.elm.sPrev.attr('src', img.src);
            avatar.elm.mPrev.attr('src', img.src);
            avatar.elm.lPrev.attr('src', img.src);
            avatar.elm.btnSave.attr("disabled", false);
            if (w > h) {
                s = 350 * h / w;
                avatar.elm.target.css({ width: '350px', height: s + 'px' });
                avatar.elm.target.parent().css({
                    padding: ((350 - s) / 2) + 'px 0 0 0',
                    height: 350 - ((350 - s) / 2) + 'px', width: '350px'
                });
            } else {
                s = 350 * w / h;
                avatar.elm.target.css({ height: '350px', width: s + 'px' });
                avatar.elm.target.parent().css({
                    padding: '0 0 0 ' + ((350 - s) / 2) + 'px',
                    width: 350 - ((350 - s) / 2) + 'px',
                    height: '350px'
                });
            }
            if (jcrop_api != null) {
                jcrop_api.destroy();
            }
            avatar.elm.target.Jcrop({
                minSize: [50, 50],
                aspectRatio: 1,
                onChange: avatar.updatePreview,
                onSelect: avatar.updatePreview,
                onRelease: avatar.updatePreview
            }, function () {
                jcrop_api = this;
                var bounds = this.getBounds();
                boundx = bounds[0];
                boundy = bounds[1];
            });

            jcrop_api.animateTo([0, 0, 100, 100]);
        },
        updatePreview: function (c) {
            if (parseInt(c.w) > 0) {
                var rx = 150 / c.w;
                var ry = 150 / c.h;
                var rx_m = 90 / c.w;
                var ry_m = 90 / c.h;
                var rx_s = 50 / c.w;
                var ry_s = 50 / c.h;
                $('#preview_l').css({
                    width: Math.round(rx * boundx) + 'px',
                    height: Math.round(ry * boundy) + 'px',
                    marginLeft: '-' + Math.round(rx * c.x) + 'px',
                    marginTop: '-' + Math.round(ry * c.y) + 'px'
                });
                $('#preview_m').css({
                    width: Math.round(rx_m * boundx) + 'px',
                    height: Math.round(ry_m * boundy) + 'px',
                    marginLeft: '-' + Math.round(rx_m * c.x) + 'px',
                    marginTop: '-' + Math.round(ry_m * c.y) + 'px'
                });
                $('#preview_s').css({
                    width: Math.round(rx_s * boundx) + 'px',
                    height: Math.round(ry_s * boundy) + 'px',
                    marginLeft: '-' + Math.round(rx_s * c.x) + 'px',
                    marginTop: '-' + Math.round(ry_s * c.y) + 'px'
                });
            }
            avatar.updateCoords(c);
        },
        updateCoords: function (c) {
            if (c) {
                this.params = {
                    x: c.x, y: c.y, w: c.w, h: c.h,
                    _w: boundx, _h: boundy, avtf: avatar.avtf
                };
            } else {
                this.params = { avtf: avatar.avtf };
            }
        }
    };

    avatar.init()
});