$.extend({
    //打开窗口
    OpenWindow: function (title, url, width, height, id) {
        id = id == null ? "OpenWindow" : id;
        id = id === "" ? "OpenWindow" : id;
        $.CloseWindow(id);
        $.dialog({
            id: id,
            title: title,
            content: 'url:' + url,
            width: width,
            height: height,
            lock: true,
            fixed: true,
            min: false,
            max: false,
            close: function () {
            }
        });
        $("#" + id + " .ui_close").css("textDecoration", "none");
        $("#" + id + " .ui_border").addClass("ui_border");
        $("#" + id + " .ui_inner").addClass("ui_inner");
        $("#" + id + " .ui_dialog").addClass("ui_dialog");
        $("#" + id + " .ui_c").find("tr:first").show();
        $("#" + id + " .ui_state_lock>tbody>tr:last").show();
        $("#" + id + " .ui_state_focus>tbody>tr:first").show();
    },
    OpenWindowMax: function (title, url, id) {
        var newurl = url;
        if (url.indexOf("?") > -1) {
            newurl += "&";
        }
        else {
            newurl += "?";
        }
        newurl += "w=" + document.documentElement.clientWidth;
        newurl += "&h=" + (document.documentElement.clientHeight - 45);
        id = id == null ? "OpenWindow" : id;
        id = id === "" ? "OpenWindow" : id;

        $.CloseWindow(id);
        var width = document.documentElement.clientWidth;
        var height = document.documentElement.clientHeight - 45;
        $(window).resize(function () {
            width = document.documentElement.clientWidth;
            height = document.documentElement.clientHeight - 45;
            $("#" + id).css("width", width);
            $("#" + id).css("height", height);

            $("#" + id).find(".ui_loading").css("height", height);
            $("#" + id).find(".ui_main").css("height", height);
            //alert(height);
        });
        $.dialog({
            title: title,
            id: id,
            lock: true,
            height: height,
            width: width,
            fixed: true,
            min: false,
            max: false,
            content: 'url:' + newurl,
            close: function () {
            }
        });
        $("#" + id + " .ui_close").css("textDecoration", "none");
        $("#" + id + " .ui_border").addClass("ui_border");
        $("#" + id + " .ui_inner").addClass("ui_inner");
        $("#" + id + " .ui_dialog").addClass("ui_dialog");
        $("#" + id + " .ui_c").find("tr:first").show();
        $("#" + id + " .ui_state_lock>tbody>tr:last").show();
        $("#" + id + " .ui_state_focus>tbody>tr:first").show();
    },
    OpenNoFrame: function (url, width, height, id) {
        id = id == null ? "OpenWindow" : id;
        id = id === "" ? "OpenWindow" : id;
        if (width == null || height == null) {
            $(window).resize(function () {
                width = document.documentElement.clientWidth;
                height = document.documentElement.clientHeight;
                $("#" + id).css("width", width);
                $("#" + id).css("height", height);

                $("#" + id).find(".ui_loading").css("height", height);
                $("#" + id).find(".ui_main").css("height", height);
                //alert(height);
            });
        }

        if (width == null) {
            width = document.documentElement.clientWidth;
        }
        if (height == null) {
            height = document.documentElement.clientHeight;
        }
        $.CloseWindow(null, id);
        $.dialog({
            id: id,
            title: "",
            padding: 0,
            content: 'url:' + url,
            width: width,
            height: height,
            lock: true,
            fixed: true,
            min: false,
            max: false

        });
        $("#" + id + " .ui_border").removeClass("ui_border");
        $("#" + id + " .ui_inner").removeClass("ui_inner");
        $("#" + id + " .ui_dialog").removeClass("ui_dialog");
        $("#" + id + " .ui_c").find("tr:first").hide();
        $("#" + id + " .ui_state_lock>tbody>tr:last").hide();
        $("#" + id + " .ui_state_focus>tbody>tr:first").hide();
    },
    //关闭执行方法
    CloseWindow: function (exfun, id) {
        id = id == null ? "OpenWindow" : id;
        id = id == "" ? "OpenWindow" : id;
        $.dialog({ id: id }).close();
        if (typeof (exfun) == "function") {
            exfun();
        }
    },
    CloseLoadGrid: function (winid, gridId, reloadType) {
        winid = winid == null ? "OpenWindow" : winid;
        winid = winid === "" ? "OpenWindow" : winid;
        $.dialog({ id: winid }).close();
        if (reloadType == null) {
            reloadType = "reload";
        }

        if (reloadType === "") {
            reloadType = "reload";
        }
        try {
            mainFrm.$("#" + gridId).TBgrid(reloadType);
        } catch (e) {
            $("#" + gridId).TBgrid(reloadType);
        }

    },
    CloseLoadTreeGrid: function (winid, gridId, reloadType) {
        winid = winid == null ? "OpenWindow" : winid;
        winid = winid === "" ? "OpenWindow" : winid;
        $.dialog({ id: winid }).close();

        if (reloadType == null) {
            reloadType = "reload";
        }

        if (reloadType === "") {
            reloadType = "reload";
        }
        try {
            mainFrm.$("#" + gridId).TBTgrid(reloadType);
        } catch (e) {
            $("#" + gridId).TBTgrid(reloadType);
        }

    },
    //提示
    Tips: function (msg, ico, exfun) {
        $.dialog({
            id: "frmsubmitTip", time: 1.5, title: "提示", lock: true, content: msg, icon: ico + '.gif', close: function () {
                if (typeof (exfun) == "function") {
                    exfun();
                }
            }
        });
        $("#frmsubmitTip .ui_close").hide();

    },

    //错误提示
    Alert: function (msg) {
        $.dialog.alert(msg);
    },
    Confirm: function (msg, exfun) {
        $.dialog.confirm(msg, exfun);
    },

    Loadding: function (msg, title) {
        if (msg == null) {
            msg = "正在向服务器获得结果，请等待......";
        }
        if (title == null) {
            title = "加载中...";
        }
        if (title === "") {
            title = "加载中...";
        }

        $.dialog({ id: "windowloadding", title: title, lock: true, content: msg, icon: 'loading.gif' });
        $("#windowloadding .ui_close").hide();
    },
    CloseLoading: function () {
        // $("#AjaxSendCount").val("0");
        // $("#AjaxSendloading").remove();
        // $("#ldg_lockmask").remove();
        $.dialog({ id: 'windowloadding' }).close();
    },

    SoftDel: function (url, deldata, tips, exfun) {
        if (tips == null) {
            tips = "将要删除所选的数据，是否继续？";
        }
        if (tips == "") {
            tips = "将要删除所选的数据，是否继续？";
        }
        $.Confirm(tips, function () {
            //如果是
            $.AjaxSend("post", url, deldata, exfun);
        });
    },
    LoadView: function (id, url, refData) {
        $("#" + id).html("");
        $.AjaxSend("get", url, refData, function (data) {
            
            try {
                data = JSON.parse(data);
            }
            catch (e) {
                $("#" + id).html(data);
            }
            if (data != null) {
                if (data.Code != null) {
                    if (data.Code !== 1) {
                        //window.parent.$.CloseLoading();
                        $.Alert(data.Message);
                        $("#" + id).html("");
                    }

                }

            }
           
            //$("#AjaxSendCount").remove();
            //$("#AjaxSendloading").remove();
            //$("#ldg_lockmask").hide();
            //$("#windowloadding").hide();
            //$.CloseLoading();
        }, "html");


        //$("#" + id).load(url, refData, function (data) {




        //});

    },
    //AJAX执行回调
    AjaxSend: function (method, url, datapara, funback, dataType) {
        dataType = dataType == null ? "json" : dataType;
        dataType = dataType === "" ? "json" : dataType;
        $.ajax({
            type: method,
            url: url,
            cache: false,
            dataType: dataType,
            data: datapara,
            beforeSend: function () {
                if (document.getElementById("AjaxSendCount") == null) {
                    $(document.body).append("<input type='hidden' id='AjaxSendCount' value='1'/>");
                    $.Loadding("正在向服务器获得结果，请等待......");

                }
                else {
                    var ThisCount = $("#AjaxSendCount").val();
                    $("#AjaxSendCount").val(parseInt(ThisCount) + 1);
                }
                $.Loadding("正在向服务器获得结果，请等待......");
            },
            success: function (data) {

                funback(data);
                var ThisCount = $("#AjaxSendCount").val();
                ThisCount = parseInt(ThisCount) - 1;
                $("#AjaxSendCount").val(ThisCount);
                
                if (parseInt($("#AjaxSendCount").val()) <= 0) {
                
                    $.CloseLoading();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

                console.log(errorThrown);
                var HttpStatus = parseInt(XMLHttpRequest.status);
                if (HttpStatus != 0);
                {
                    $("#AjaxSendCount").val("0");
                    $.CloseLoading();
                    $.Alert("操作失败，请重试！");
                    return;
                }


            }
        });
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