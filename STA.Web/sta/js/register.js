var fs = { "username": { def: "6-20位小写字母、数字组成", wrong: "用户名输入有误！", right: "用户名可以使用" },
    "nickname": { def: "1-20位字母、数字或汉字（汉字算两位）组成", wrong: "昵称输入有误！", right: "输入正确" },
    "password": { def: "长度必须大于6位，数字、字母混合", wrong: "密码输入有误！", right: "输入正确" },
    "repassword": { def: "请再次确认你的密码", wrong: "请确保两次输入的密码一样！", right: "输入正确" },
    "email": { def: "输入常用邮箱，以便于账号验证或密码找回", wrong: "邮箱输入有误！", right: "邮箱可以使用" },
    "vcode": { def: "", wrong: "请输入验证码！", right: ""}
};

function ValidateS(source) {
    var patrn = /^[a-z0-9]+$/
    return patrn.exec(source);
};

function ValidatePwd(source) {
    var patrn = /^[a-zA-Z0-9-_]+$/
    return patrn.exec(source);
};

function ValidateEmail(source) {
    var patrn = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
    return patrn.exec(source);
};

function TxtFocus(name) {
    $("#" + name).css("border-color", "#a8a5a5");
    $(".tip" + name).removeClass("fwrong fright").html(fs[name].def);
};

function Ok(name) {
    $("#" + name).css("border-color", "#a8a5a5");
    $(".tip" + name).removeClass("fwrong").addClass("fright").html(fs[name].right);
};

function Err(name) {
    $("#" + name).css("border-color", "#ff0000");
    $(".tip" + name).removeClass("fright").addClass("fwrong").html(fs[name].wrong);
};

function UsernameExist(val) {
    ajax("usernameexist&username=" + escape(val), function (r) {
        if (webconfig.regforbidwords.indexOf(val) >= 0 || parseInt(r) > 0) {
            $(".tipusername").removeClass("fright").addClass("fwrong").html("用户名“" + val + "”已经存在，请使用其他用户名注册！");
        } else {
            Ok("username");
        }
    });
};

function UseremailExist(val) {
    ajax("useremailexist&email=" + escape(val), function (r) {
        if (parseInt(r) > 0) {
            $(".tipemail").removeClass("fright").addClass("fwrong").html("该邮箱已经存在，请使用其他邮箱注册！");
        } else {
            Ok("email");
        }
    });
};

function CheckTxt(name) {
    var val = $.trim($("#" + name).val());
    switch (name) {
        case "username":
            if (val.length < 6 || val.length > 20 || !ValidateS(val)) {
                Err(name);
            } else {
                UsernameExist(val);
            }
            break;
        case "nickname":
            if (val.length < 1 || val.length > 20) {
                Err(name);
            } else {
                Ok(name);
            }
            break;
        case "password":
            if (val.length < 6 || !ValidatePwd(val)) {
                Err(name);
            } else {
                Ok(name);
            }
            break;
        case "repassword":
            if (val.length < 6 || val != $("#password").val()) {
                Err(name);
            } else {
                Ok(name);
            }
            break;
        case "email":
            if (val == "" || !ValidateEmail(val)) {
                Err(name);
            } else {
                if (webconfig.emailmult == 1) {
                    Ok(name);
                } else {
                    UseremailExist(val);
                }
            }
            break;
        case "vcode":
            if (val == "") {
                Err(name);
            } else {
                Ok(name);
            }
            break;
    }
};

function SubRegForm() {
    $.each(["username", "nickname", "password", "repassword", "email", "vcode"], function (idx, obj) { CheckTxt(obj) });
    if ($(".fwrong").length > 0 || !$("#aggree").attr("checked")) return false;
    $("form").get(0).submit();
};

