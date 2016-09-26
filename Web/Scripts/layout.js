$.extend({
    //打开窗口
    Open: function (title, url, width, height, id) {
        id = id == null ? "OpenWindow" : id;
        dialog({
            id: id,
            title: title,
            url: url,
            width: width,
            height: height
        }).showModal();
    },
    //打开带按钮的窗口
    OpenToButton: function (title, url, width, height, fun, id) {
        id = id == null ? "OpenWindow" : id;
        dialog({
            id: id,
            title: title,
            url: url,
            width: width,
            height: height,
            ok: function () {
                fun();
            },
            cancel: function () {
                if (document.getElementById("mainFrm") != null) {
                    if (typeof mainFrm.window.reloadDatagrid === 'function') {
                        main.window.reloadDatagrid();
                    }
                }
            }
        }).showModal();
    },
    //打开窗口关闭执行父窗口调用子窗口方法
    OpenLoad: function (title, url, width, height, id) {
        id = id == null ? "OpenWindow" : id;
        dialog({
            id: id,
            title: title,
            url: url,
            width: width,
            height: height,
            cancelDisplay: false,
            cancel: function () {
                if (document.getElementById("mainFrm") != null) {
                    if (typeof mainFrm.window.reloadDatagrid === 'function') {
                        mainFrm.window.reloadDatagrid();
                    }
                }
            }
        }).showModal();
    },
    //打开窗口关闭执行父窗口调用子窗口方法
    OpenLoadToButton: function (title, url, width, height, fun, id) {
        id = id == null ? "OpenWindow" : id;
        dialog({
            id: id,
            title: title,
            url: url,
            width: width,
            height: height,
            ok: function () {
                fun();
            },
            cancel: function () {
                if (document.getElementById("mainFrm") != null) {
                    if (typeof mainFrm.window.reloadDatagrid === 'function') {
                        mainFrm.window.reloadDatagrid();
                    }
                }
            }
        }).showModal();
    },
    //打开最大化窗口
    OpenMax: function (title, url, id) {
        id = id == null ? "OpenWindow" : id;
        dialog({
            title: title,
            id: id,
            width: document.documentElement.clientWidth - 2,
            height: document.documentElement.clientHeight - 60,
            left: 0,
            top: 0,
            padding: 0,
            url: url,
            cancelDisplay: false,
            cancel: function () {
                if (document.getElementById("mainFrm") != null) {
                    if (typeof mainFrm.window.reloadDatagrid === 'function') {
                        mainFrm.window.reloadDatagrid();
                    }
                }
            }
        }).showModal();
    },
    //打开弹层
    OpenContent: function (title, width, height, msg) {
        dialog({
            id: "OpenWindow",
            title: title,
            content: msg,
            width: width,
            height: height
        }).showModal();
    },
    //关闭
    CloseF: function (id) {
        id = id == null ? "OpenWindow" : id;
        var d = dialog({ id: id });
        d.close().remove();
    },
    //关闭返回数据执行父级方法
    CloseFun: function (data, id) {
        id = id == null ? "OpenWindow" : id;
        var d = dialog({ id: id });
        d.close().remove();
        if (typeof mainFrm.window.ExecuteFun === 'function') {
            mainFrm.window.ExecuteFun(data);
        }
    },
    //关闭执行方法
    CloseToFun: function (fun, id) {
        var d = dialog({ id: id });
        d.close().remove();
        fun();
    },
    //提示
    Tips: function (msg) {
        var d = dialog({
            content: msg
        });
        d.show();
        setTimeout(function () {
            d.close().remove();
        }, 1000);
    },
    //提示后回调
    TipsFun: function (msg, fun) {

        var d = dialog({
            content: msg,
        });
        d.show();
        setTimeout(function () {
            d.close().remove();
            fun();
        }, 1000);

    },
    //错误提示
    Alert: function (msg) {
        dialog({
            title: "提示",
            content: msg,
            ok: function () {
                return true;
            }
        }).showModal();
    },
    //错误提示回调
    AlertFun: function (msg, fun) {
        dialog({
            title: "提示",
            content: msg,
            ok: function () {
                fun();
                return true;
            }
        }).showModal();
    },
    //Swal提示
    SwalTips: function (msg, fun, text) {
        swal({
            title: msg,
            text: text,
            type: "success",
            timer: 2000,
            closeOnConfirm: false,
            showConfirmButton: false
        }, function () {
            if (typeof (fun) == "function") {
                fun();
            } else {
                swal.close();
            }
        });
    },
    //Swal错误提示
    SwalAlert: function (msg, type, fun, text) {
        if (type == null || type == "") {
            type = "error";
        }
        swal({
            title: msg,
            text: text,
            type: type,
            closeOnConfirm: false,
            confirmButtonText: "确定"
        }, function (isConfirm) {
            if (typeof (fun) == "function") {
                fun();
            } else {
                swal.close();
            }
        });
    },
    //Swal提问提示
    SwalConfirm: function (msg, fun, text) {
        swal({
            title: msg,
            text: text,
            type: "info",
            showConfirmButton: true,
            showCancelButton: true,
            closeOnConfirm: false,
            confirmButtonText: "确定",
            cancelButtonText: "取消"
        }, function () {
            if (typeof (fun) == "function") {
                fun();
            } else {
                swal.close();

            }
        });
    },
    //Swal关闭
    SwalClose: function () {
        swal.close();
    }
    ,
    //Swal警告提示
    SwalWarning: function (msg, fun, text) {
        swal({
            title: msg,
            text: text,
            type: "info",
            showConfirmButton: true,
            showCancelButton: true,
            closeOnConfirm: false,
            confirmButtonText: "确定",
            cancelButtonText: "取消"
        }, function () {
            if (typeof (fun) == "function") {
                fun();
            } else {
                swal.close();

            }
        });
    },
    //Swallprompt
    SwalPrompt: function (msg, fun, text) {
        swal({
            title: msg,
            text: text,
            type: "input",
            showConfirmButton: true,
            showCancelButton: true,
            closeOnConfirm: false,
            confirmButtonText: "确定",
            cancelButtonText: "取消"
        }, function (inputValue) {
            if (inputValue === false) return false;
            if (inputValue === "") { swal.showInputError("不能为空"); return false }
            if (typeof (fun) == "function") {
                fun(inputValue);
            } else {

            }
        });
    },
    //AJAX执行回调
    AjaxSend: function (method, url, datapara, funback) {
        $.ajax({
            type: method,
            url: url,
            data: datapara,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' },
            beforeSend: function () {
                if (document.getElementById("AjaxSendCount") == null) {
                    $(document.body).append("<input type='hidden' id='AjaxSendCount' value='1'/>");
                    $.fn.jqLoading({ height: 60, width: 250, text: "<div style=\"margin:10px auto;line-height: 40px;\"><img class=\"L\" width=\"40\" src=\"/Areas/BidMag/Content/v2images/loading.gif\">正在发送您的请求，请耐心等待......</div>" });
                }
                else {
                    if (document.getElementById("AjaxSendloading") == null) {

                        $.fn.jqLoading({ height: 60, width: 250, text: "<div style=\"margin:10px auto;line-height: 40px;\"><img class=\"L\" width=\"40\" src=\"/Areas/BidMag/Content/v2images/loading.gif\">正在发送您的请求，请耐心等待......</div>" });
                    }
                    var ThisCount = $("#AjaxSendCount").val();
                    $("#AjaxSendCount").val(parseInt(ThisCount) + 1);

                }

            },
            success: function (data) {
                var ThisCount = $("#AjaxSendCount").val();
                $("#AjaxSendCount").val(parseInt(ThisCount) - 1);
                if (parseInt($("#AjaxSendCount").val()) <= 0) {
                    $(this).jqLoading("destroy");

                }

                funback(data);

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //closelodding();
                if (XMLHttpRequest.status == 0);
                {
                    return;
                }
                $(this).jqLoading("destroy");
                $.Alert("操作失败，请重试！");
            }
        });
    },
    //Ajax按钮点击执行替换
    AjaxSendLink: function (LinkId, method, url, datapara, UpdateTargetId, InsertionMode, successFun) {
        $("#" + LinkId).unbind("click");
        $("#" + LinkId).on("click", function () {

            $.ajax({
                type: method,
                url: url,
                data: datapara,
                beforeSend: function () {

                    $.fn.jqLoading({ height: 60, width: 250, text: "<div style=\"margin:10px auto;line-height: 40px;\"><img class=\"L\" width=\"40\" src=\"/Areas/BidMag/Content/v2images/loading.gif\">正在发送您的请求，请耐心等待......</div>" });
                },
                success: function (data) {
                    $("#" + UpdateTargetId).html(data);
                    $(this).jqLoading("destroy");
                    if (typeof successFun === 'function') {
                        successFun(data);
                    }

                },
                error: function () {
                    $(this).jqLoading("destroy");
                    $.Alert("操作失败，请重试！");
                }
            });
        })
    },
    //AJA--before==false 终止执行AJX
    AjaxSendLinkBefore: function (LinkId, method, url, datapara, UpdateTargetId, InsertionMode, before, successFun) {
        $("#" + LinkId).unbind("click");
        $("#" + LinkId).on("click", function () {

            $.ajax({
                type: method,
                url: url,
                data: datapara,
                beforeSend: function () {
                    var result = before();
                    if (result) {
                        $.fn.jqLoading({ height: 60, width: 250, text: "<div style=\"margin:10px auto;line-height: 40px;\"><img class=\"L\" width=\"40\" src=\"/Areas/BidMag/Content/v2images/loading.gif\">正在发送您的请求，请耐心等待......</div>" });
                    }
                    else {
                        return false;
                    }

                },
                success: function (data) {
                    $("#" + UpdateTargetId).html(data);
                    $(this).jqLoading("destroy");
                    if (typeof successFun === 'function') {
                        successFun(data);
                    }

                },
                error: function () {
                    $(this).jqLoading("destroy");
                    Alert("操作失败，请重试！");
                }
            });
        })


    }
});
$(function () {

    $.fn.IsEmpty = function () {
        var $Input = $(this);
        var reg = /^\s*$/g;
        if (reg.test($Input.val())) {
            return true;
        } else {
            return false;
        }
    }
    $.fn.ajaxFrm = function (submitSuccess) {
        var frmObj = $(this);
        var frmId = frmObj.attr("id");
        $('#' + frmId).unbind("submit");
        $('#' + frmId).submit(function () {
            if (frmObj.valid()) {
                var f_method = frmObj.attr("method");
                var f_action = frmObj.attr("action");
                $.AjaxSend(f_method, f_action, frmObj.serialize(), function (data) {
                    if (typeof (submitSuccess) == "function") {
                        submitSuccess(data);
                    }

                });
                return false;
            }
            return false;
        });
    }
});