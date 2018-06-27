function login() {
    $('#login').css("display", "block");
    $('#register').css("display", "none");    
}

function register() {
    $('#login').css("display", "none");
    $('#register').css("display", "block");
}
var index;
function test() {
        index = layer.tips('我是另外一个tips，只不过我长得跟之前那位稍有些不一样。', '#regusername', {
        tips: [2, '#c00'],
        success: function (index) {
            console.log(index);
        }
    });
}

function close() {
    alert("OK!");
    layer.close(index);
}