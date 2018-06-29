function login() {
    $('#login').css("display", "block");
    $('#register').css("display", "none");    
}

function register() {
    $('#login').css("display", "none");
    $('#register').css("display", "block");
}

/**
 * 注册
 */

//验证用户名
var usernameindex;
var usernamemsg;
function verifyUserName() {
    
    var $dom = $('#regusername');
    var username = $dom.val();

    layer.close(usernameindex);

    if (username === null || username === undefined || username==='') {
        usernameindex = layer.tips('用户名不能为空！', $dom, {
            tips: [2, '#c00'],
        });
        return false;
    } else {
        $.ajax({
            type: "POST",
            url: "VerifyUserName",
            data: 'username=' + username,
            dataType: "json",
            success: function (msg) {
                usernamemsg = msg;
                if (msg.stateType == 0) {
                    usernameindex = layer.tips(msg.message, $dom, {
                        tips: [2, '#c00'],
                    });
                } else {
                }
            }
        });
        return true;
    }    
}


//验证手机号
var phoneindex
var phonemsg;
function verifyUserPhone() {
    var phone = $('#reguserphone');
    var userphone = phone.val();

    layer.close(phoneindex);

    var ver = /(^1[3|4|5|7|8]\d{9}$)|(^09\d{8}$)/;
    if (userphone === null || !ver.test(phone.val())) {
        phoneindex = layer.tips('手机号码格式不正确', phone, {
            tips: [2, '#c00'],
        });
        return false;
    } else {
        $.ajax({
            type: "POST",
            url: "VerifyUserPhone",
            data: "userphone=" + userphone,
            dataType: "json",
            success: function (msg) {
                phonemsg = msg;
                if (msg.stateType == 0) {
                    phoneindex = layer.tips(msg.message, phone, {
                        tips: [2, '#c00'],
                    });
                } else {
                }
            }
        });
        return true;
    }
}

//验证密码
var pwdindex;
function verifyPwd() {
    var pwd = $('#regpwd');
  
    layer.close(pwdindex);

    if (pwd.val().length < 6) {
        pwdindex = layer.tips('密码不能少于六位', pwd, {
            tips:[2,'#c00'],
        });
        return false;
    }

    return true;
}

//注册
function Reg() {
    if (verifyUserName() && verifyPwd() && verifyUserPhone() && $('#regcheckbox').is(':checked')) {
        if (usernamemsg.stateType != 0 && phonemsg.stateType != 0) {
            var username = $('#regusername').val();
            var pwd = $('#regpwd').val();
            var userphone = $('#reguserphone').val();

            var account = { "UserName": username, "PassWord": pwd, "PhoneNum": userphone };

            $.ajax({
                type: "POST",
                url: "Register",
                data: account,
                dataType: "json",
                success: function (msg) {
                    if (msg.stateType === 0) {
                        layer.msg(msg.message, {
                            anim: 6
                        });
                    } else {
                        layer.msg('注册成功！', {
                            anim: 6
                        });
                    }
                }
            });

        }
    }    
}

/*
 * 登录
 */
//验证登录用户名
var loginusernameindex;
function loginUserName() {
    var usernamedom = $('#username');
    var username = usernamedom.val();

    layer.close(loginusernameindex);

    if (username === null || username === undefined || username === '') {
        loginusernameindex = layer.tips('用户名不能为空', usernamedom, {
            tips: [2, '#c00']
        });

        return false;
    } else {
        return true;
    }
}

//验证密码
var loginpwdindex
function loginPwd() {
    var pwddom = $('#pwd');
    var pwd = pwddom.val();

    layer.close(loginpwdindex);

    if (pwd.length < 6) {
        loginpwdindex = layer.tips('密码应不小于6位数', pwddom, {
            tips: [2, '#c00']
        });
        return false;
    }
    else {
        return true;
    }
}

//登录
function userLogin() {
    if (loginUserName() && loginPwd()) {
        var username = $('#username').val();
        var pwd = $('#pwd').val();
        var isremember = $('#rememberpwd').is(':checked');
        var logintype;
        

        var ver = /(^1[3|4|5|7|8]\d{9}$)|(^09\d{8}$)/;
        if (ver.test(username)) {
            logintype = 0;
        } else {
            logintype = 1;
        }

        var logindata = { "logintype": logintype, "username": username, "pwd": pwd, "isremember": isremember };

        $.ajax({
            type: "POST",
            url: "Login",
            data: logindata,
            dataType: "json",
            success: function (msg) {
                if (msg.stateType === 0) {
                    layer.msg(msg.message, {
                        anim: 6
                    });
                } else {
                    layer.msg('登录成功！', {
                        anim: 6
                    });
                }
            }
        });
    }
}