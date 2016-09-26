// JavaScript Document
//支持Enter键登录
document.onkeydown = function (e) {
    if ($(".bac").length == 0) {
        if (!e) e = window.event;
        if ((e.keyCode || e.which) == 13) {
            var obtnLogin = document.getElementById("btnSubmit");
            obtnLogin.focus();
        }
    }
}

$(function () {
    //提交表单
    $('#btnSubmit').click(function () {
        show_loading();
        // var myReg = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/; //邮件正则
        if ($('#UserOrEmail').val() == '') {
            show_err_msg('帐号或邮箱还没填呢！');
            $('#UserOrEmail').focus();
        }
            //else if (!myReg.Test($('#email').val())) {
            //    show_err_msg('您的邮箱格式错咯！');
            //    $('#email').focus();} 
        else if ($('#Password').val() == '') {
            show_err_msg('密码还没填呢！');
            $('#Password').focus();
        } else {
            //ajax提交表单，#login_form为表单的ID。 如：$('#login_form').ajaxSubmit(function(data) { ... });
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "/Admin/Security/LogIn",
                data: $("#login_form").serialize(),
                beforeSend: function() {
                    return true;
                },
                success: function (data) {
                    if (data.Code === 1) {
                        show_msg("登录成功咯正在为您跳转...", "Home/index");
                        return;
                    }
                    show_err_msg(data.Message);
                }
            });

        }
    });
});