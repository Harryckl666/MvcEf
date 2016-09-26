/* File Created: 七月 11, 2015 */
//复选框
$.fn.ruleSingleCheckbox = function () {
    var singleCheckbox = function (parentObj) {
        //查找复选框
        var checkObj = parentObj.children('input:checkbox').eq(0);
        parentObj.children().hide();
        //添加元素及样式
        var newObj = $('<a href="javascript:;">'
		+ '<i class="off">否</i>'
		+ '<i class="on">是</i>'
		+ '</a>').prependTo(parentObj);
        parentObj.addClass("single-checkbox");
        //判断是否选中
        if (checkObj.prop("checked") == true) {
            newObj.addClass("selected");
        }
        //检查控件是否启用
        if (checkObj.prop("disabled") == true) {
            newObj.css("cursor", "default");
            return;
        }
        //绑定事件
        $(newObj).click(function () {
            if ($(this).hasClass("selected")) {
                $(this).removeClass("selected");
                //checkObj.prop("checked", false);
            } else {
                $(this).addClass("selected");
                //checkObj.prop("checked", true);
            }
            checkObj.trigger("click"); //触发对应的checkbox的click事件
        });
    };
    return $(this).each(function () {
        singleCheckbox($(this));
    });
};

//多项复选框
$.fn.ruleMultiCheckbox = function () {
    var multiCheckbox = function (parentObj) {
        parentObj.addClass("multi-checkbox"); //添加样式
        parentObj.children().hide(); //隐藏内容
        var divObj = $('<div class="boxwrap"></div>').prependTo(parentObj); //前插入一个DIV
        parentObj.find(":checkbox").each(function () {
            var indexNum = parentObj.find(":checkbox").index(this); //当前索引
            var newObj = $('<a href="javascript:;">' + parentObj.find('label').eq(indexNum).text() + '</a>').appendTo(divObj); //查找对应Label创建选项
            if ($(this).prop("checked") == true) {
                newObj.addClass("selected"); //默认选中
            }
            //检查控件是否启用
            if ($(this).prop("disabled") == true) {
                newObj.css("cursor", "default");
                return;
            }
            //绑定事件
            $(newObj).click(function () {
                if ($(this).hasClass("selected")) {
                    $(this).removeClass("selected");
                    //parentObj.find(':checkbox').eq(indexNum).prop("checked",false);
                } else {
                    $(this).addClass("selected");
                    //parentObj.find(':checkbox').eq(indexNum).prop("checked",true);
                }
                parentObj.find(':checkbox').eq(indexNum).trigger("click"); //触发对应的checkbox的click事件
                //alert(parentObj.find(':checkbox').eq(indexNum).prop("checked"));
            });
        });
    };
    return $(this).each(function () {
        multiCheckbox($(this));
    });
}

//多项选项PROP
$.fn.ruleMultiPorp = function () {
    var multiPorp = function (parentObj) {
        parentObj.addClass("multi-porp"); //添加样式
        parentObj.children().hide(); //隐藏内容
        var divObj = $('<ul></ul>').prependTo(parentObj); //前插入一个DIV
        parentObj.find(":checkbox").each(function () {
            var indexNum = parentObj.find(":checkbox").index(this); //当前索引
            var liObj = $('<li></li>').appendTo(divObj)
            var newObj = $('<a href="javascript:;">' + parentObj.find('label').eq(indexNum).text() + '</a><i></i>').appendTo(liObj); //查找对应Label创建选项
            if ($(this).prop("checked") == true) {
                liObj.addClass("selected"); //默认选中
            }
            //检查控件是否启用
            if ($(this).prop("disabled") == true) {
                newObj.css("cursor", "default");
                return;
            }
            //绑定事件
            $(newObj).click(function () {
                if ($(this).parent().hasClass("selected")) {
                    $(this).parent().removeClass("selected");
                } else {
                    $(this).parent().addClass("selected");
                }
                parentObj.find(':checkbox').eq(indexNum).trigger("click"); //触发对应的checkbox的click事件
                //alert(parentObj.find(':checkbox').eq(indexNum).prop("checked"));
            });
        });
    };
    return $(this).each(function () {
        multiPorp($(this));
    });
}

//多项单选
$.fn.ruleMultiRadio = function () {
    var multiRadio = function (parentObj) {
        parentObj.addClass("multi-radio"); //添加样式
        parentObj.children().hide(); //隐藏内容
        var divObj = $('<div class="boxwrap"></div>').prependTo(parentObj); //前插入一个DIV
        parentObj.find('input[type="radio"]').each(function () {
            var indexNum = parentObj.find('input[type="radio"]').index(this); //当前索引
            var newObj = $('<a href="javascript:;">' + parentObj.find('label').eq(indexNum).text() + '</a>').appendTo(divObj); //查找对应Label创建选项
            if ($(this).prop("checked") == true) {
                newObj.addClass("selected"); //默认选中
            }
            //检查控件是否启用
            if ($(this).prop("disabled") == true) {
                newObj.css("cursor", "default");
                return;
            }
            //绑定事件
            $(newObj).click(function () {
                $(this).siblings().removeClass("selected");
                $(this).addClass("selected");
                parentObj.find('input[type="radio"]').prop("checked", false);
                parentObj.find('input[type="radio"]').eq(indexNum).prop("checked", true);
                parentObj.find('input[type="radio"]').eq(indexNum).trigger("click"); //触发对应的radio的click事件
                //alert(parentObj.find('input[type="radio"]').eq(indexNum).prop("checked"));
            });
        });
    };
    return $(this).each(function () {
        multiRadio($(this));
    });
}

//单选下拉框
$.fn.ruleSingleSelect = function () {
    var singleSelect = function (parentObj) {
        parentObj.addClass("single-select"); //添加样式
        parentObj.children().hide(); //隐藏内容
        var divObj = $('<div class="boxwrap"></div>').prependTo(parentObj); //前插入一个DIV
        //创建元素
        var titObj = $('<a class="select-tit" href="javascript:;"><span></span><i></i></a>').appendTo(divObj);
        var itemObj = $('<div class="select-items"><ul></ul></div>').appendTo(divObj);
        var arrowObj = $('<i class="arrow"></i>').appendTo(divObj);
        var selectObj = parentObj.find("select").eq(0); //取得select对象
        //遍历option选项
        selectObj.find("option").each(function (i) {
            var indexNum = selectObj.find("option").index(this); //当前索引
            var liObj = $('<li>' + $(this).text() + '</li>').appendTo(itemObj.find("ul")); //创建LI
            if ($(this).prop("selected") == true) {
                liObj.addClass("selected");
                titObj.find("span").text($(this).text());
            }
            //检查控件是否启用
            if ($(this).prop("disabled") == true) {
                liObj.css("cursor", "default");
                return;
            }
            //绑定事件
            liObj.click(function () {
                $(this).siblings().removeClass("selected");
                $(this).addClass("selected"); //添加选中样式
                selectObj.find("option").prop("selected", false);
                selectObj.find("option").eq(indexNum).prop("selected", true); //赋值给对应的option
                titObj.find("span").text($(this).text()); //赋值选中值
                arrowObj.hide();
                itemObj.hide(); //隐藏下拉框
                selectObj.trigger("change"); //触发select的onchange事件
                //alert(selectObj.find("option:selected").text());
            });
        });
        //设置样式
        //titObj.css({ "width": titObj.innerWidth(), "overflow": "hidden" });
        //itemObj.children("ul").css({ "max-height": $(document).height() - titObj.offset().top - 62 });

        //检查控件是否启用
        if (selectObj.prop("disabled") == true) {
            titObj.css("cursor", "default");
            return;
        }
        //绑定单击事件
        titObj.click(function (e) {
            e.stopPropagation();
            if (itemObj.is(":hidden")) {
                //隐藏其它的下位框菜单
                $(".single-select .select-items").hide();
                $(".single-select .arrow").hide();
                //位于其它无素的上面
                arrowObj.css("z-index", "1");
                itemObj.css("z-index", "1");
                //显示下拉框
                arrowObj.show();
                itemObj.show();
            } else {
                //位于其它无素的上面
                arrowObj.css("z-index", "");
                itemObj.css("z-index", "");
                //隐藏下拉框
                arrowObj.hide();
                itemObj.hide();
            }
        });
        //绑定页面点击事件
        $(document).click(function (e) {
            selectObj.trigger("blur"); //触发select的onblure事件
            arrowObj.hide();
            itemObj.hide(); //隐藏下拉框
        });
    };
    return $(this).each(function () {
        singleSelect($(this));
    });
}
//========================基于Validform插件========================
function ValidformTipType(msg, o, cssctl) {
    if (!o.obj.is("form")) {
        //定位到相应的Tab页面
        if (o.obj.is(o.curform.find(".Validform_error:first"))) {
            var tabobj = o.obj.parents(".tab-content"); //显示当前的选项
            var tabindex = $(".tab-content").index(tabobj); //显示当前选项索引
            if (!$(".content-tab ul li").eq(tabindex).children("a").hasClass("selected")) {
                $(".content-tab ul li a").removeClass("selected");
                $(".content-tab ul li").eq(tabindex).children("a").addClass("selected");
                $(".tab-content").hide();
                tabobj.show();
                var fileDiv = $(".selectdiv");
                for (var i = 0; i < fileDiv.length; i++) {

                    var fileColunmId = fileDiv[i].id.toString().split('_')[1];
                    var DisplayStatus = $("#del_" + fileColunmId).css("display");
                    if (DisplayStatus.toString() == "none") {
                        var this_FlashId = $("#hd_falsh_" + fileColunmId).val();
                        $("#" + this_FlashId + "_flash_container").remove();
                        Init_upPage(fileColunmId, "false", $("#hd_fileName").val());
                    }
                }
            }
            // $.dialog.tips('请检查必填未填项', 1, 'alert.gif');
        }
        //页面上不存在提示信息的标签时，自动创建;
        if (o.obj.parents("dd").find(".Validform_checktip").length == 0) {
            o.obj.parents("dd").append("<span class='Validform_checktip' />");
            o.obj.parents("dd").next().find(".Validform_checktip").remove();
        }
        var objtip = o.obj.parents("dd").find(".Validform_checktip");
        cssctl(objtip, o.type);
        objtip.text(msg);
    }
    //if (o.type != 2 && o.type == 1) {
    //    if (typeof $.fn.jqLoading == 'function') {
    //        $.fn.jqLoading({ height: 52, width: 240, text: "正在提交您的数据，请耐心等待......" });
    //    }
    //}
}

function ValidformTipAlert(msg, o, cssctl) {

    if (!o.obj.is("form") && o.type != 2) {
        //定位到相应的Tab页面
        if (o.obj.is(o.curform.find(".Validform_error:first"))) {
            var tabobj = o.obj.parents(".tab-content"); //显示当前的选项
            var tabindex = $(".tab-content").index(tabobj); //显示当前选项索引
            if (!$(".content-tab ul li").eq(tabindex).children("a").hasClass("selected")) {
                $(".content-tab ul li a").removeClass("selected");
                $(".content-tab ul li").eq(tabindex).children("a").addClass("selected");
                $(".tab-content").hide();
                tabobj.show();
                var fileDiv = $(".selectdiv");
                for (var i = 0; i < fileDiv.length; i++) {

                    var fileColunmId = fileDiv[i].id.toString().split('_')[1];
                    var DisplayStatus = $("#del_" + fileColunmId).css("display");
                    if (DisplayStatus.toString() == "none") {
                        var this_FlashId = $("#hd_falsh_" + fileColunmId).val();
                        $("#" + this_FlashId + "_flash_container").remove();
                        Init_upPage(fileColunmId, "false", $("#hd_fileName").val());
                    }
                }

            }
            $.dialog.tips('请检查必填未填项', 1, 'alert.gif');

        }
        //else {
        //    if (o.type != 2 && o.type == 1) {
        //        $.fn.jqLoading({ height: 52, width: 240, text: "正在提交您的数据，请耐心等待......" });
        //    }
        //}

        //$.dialog.tips(msg, 1, 'alert.gif');
    }


}


function ValidformTipTable(msg, o, cssctl) {

    if (o.type == 3) {
        if (o.obj.parent().prev().html() != null) {
            var preText = o.obj.parent().prev().html().replace("/\n/g", "").replace(":", "").replace("：", "");
            if (o.obj[0].type.toString() != null) {

                if (o.obj[0].type.toString() == "text") {
                    if (o.obj[0].className.indexOf("Wdate") >= 0) {
                        if (msg == "请填写信息！") {
                            window.parent.parent.$.dialog.tips("请选择：" + preText, 1, 'alert.gif');
                        }

                    }
                    else {
                        if (msg == "请填写信息！") {
                            window.parent.parent.$.dialog.tips("请填写：" + preText, 1, 'alert.gif');
                        }
                        else {
                            window.parent.parent.$.dialog.tips("【" + preText + "】" + msg, 1, 'alert.gif');
                        }
                    }
                }
                else if (o.obj[0].tagName.toString() == "SELECT") {
                    if (msg == "请选择！") {
                        window.parent.parent.$.dialog.tips("请选择：" + preText, 1, 'alert.gif');
                    }
                    else {
                        window.parent.parent.$.dialog.tips("【" + preText + "】" + msg, 1, 'alert.gif');
                    }
                }
                else {
                    window.parent.parent.$.dialog.tips(msg, 1, 'alert.gif');
                }
            }
        }
        else {
            window.parent.parent.$.dialog.tips(msg, 1, 'alert.gif');
        }
    }
    //if (o.type == 1) {
    //    $.fn.jqLoading({ height: 52, width: 240, text: "正在提交您的数据，请耐心等待......" });
    //}
}

//初始化验证表单
$.fn.initValidform = function (successFun) {
    var checkValidform = function (formObj) {
        $(formObj).Validform({
            tiptype: function (msg, o, cssctl) {
                /*msg：提示信息;
                o:{obj:*,type:*,curform:*}
                obj指向的是当前验证的表单元素（或表单对象）；
                type指示提示的状态，值为1、2、3、4， 1：正在检测/提交数据，2：通过验证，3：验证失败，4：提示ignore状态；
                curform为当前form对象;
                cssctl:内置的提示信息样式控制函数，该函数需传入两个参数：显示提示信息的对象 和 当前提示的状态（既形参o中的type）；*/
                //全部验证通过提交表单时o.obj为该表单对象;
                //                alert(o.type);
                //                alert(o.curform.find(".Validform_error").length);

                ValidformTipType(msg, o, cssctl);
            },
            ajaxPost: true,
            showAllError: true,
            beforeSubmit: function (curform) {
                //在验证成功后，表单提交前执行的函数，curform参数是当前表单对象。
                //这里明确return false的话表单将不会提交;	
               // $.Loadding("正在提交数据...", "提交中");
            },
            callback: function (data) {
               
                    if (typeof (successFun) === "function") {
                        successFun(data);
                    }
                    return false;
                
               // $.CloseLoading();
                //                if (data.success) {
                //                    //$.dialog.tips(data.message,1, 'success.gif');
                //                    window.parent.showMsg(data.message);
                //                    // window.parent.frames[0].changesearch();
                //                    // closeiframe(data.tabtitle);
                //                }
                //                else {
                //                    $.dialog.alert(data.message);
                //                }
             
            }
        });
    };
    return $(this).each(function () {
        checkValidform($(this));
    });
}
//初始化验证表单(弹出)
$.fn.initAlertValidform = function (successFun) {
    var checkValidform = function (formObj) {
        $(formObj).Validform({
            tiptype: function (msg, o, cssctl) {
                /*msg：提示信息;
                o:{obj:*,type:*,curform:*}
                obj指向的是当前验证的表单元素（或表单对象）；
                type指示提示的状态，值为1、2、3、4， 1：正在检测/提交数据，2：通过验证，3：验证失败，4：提示ignore状态；
                curform为当前form对象;
                cssctl:内置的提示信息样式控制函数，该函数需传入两个参数：显示提示信息的对象 和 当前提示的状态（既形参o中的type）；*/
                //全部验证通过提交表单时o.obj为该表单对象;

                ValidformTipAlert(msg, o, cssctl);
            },
            ajaxPost: true,
            showAllError: false,
            tipSweep: true,
            postonce: true,
            beforeSubmit: function (curform) {
                //在验证成功后，表单提交前执行的函数，curform参数是当前表单对象。
                //这里明确return false的话表单将不会提交;	
              
            },
            callback: function(data) {
                if (typeof (successFun) === "function") {
                    successFun(data);
                }


                //                if (data.success) {
                //                    //$.dialog.tips(data.message,1, 'success.gif');
                //                    window.parent.showMsg(data.message);
                //                    // window.parent.frames[0].changesearch();
                //                    // closeiframe(data.tabtitle);
                //                }
                //                else {
                //                    $.dialog.alert(data.message);
                //                }
                return false;
            }
        });
    };
    return $(this).each(function () {
        checkValidform($(this));
    });
}


$.fn.initTableValidform = function (successFun) {
    var checkValidform = function (formObj) {
        $(formObj).Validform({
            datatype: {
                checktime: function (gets, obj, curform, regxp) {
                    //alert(gets);

                    dateDiff(gets, obj.parent().find("input")[0].value);
                }
            },
            tiptype: function (msg, o, cssctl) {
                /*msg：提示信息;
                o:{obj:*,type:*,curform:*}
                obj指向的是当前验证的表单元素（或表单对象）；
                type指示提示的状态，值为1、2、3、4， 1：正在检测/提交数据，2：通过验证，3：验证失败，4：提示ignore状态；
                curform为当前form对象;
                cssctl:内置的提示信息样式控制函数，该函数需传入两个参数：显示提示信息的对象 和 当前提示的状态（既形参o中的type）；*/
                //全部验证通过提交表单时o.obj为该表单对象;

                ValidformTipTable(msg, o, cssctl);
            },
            ajaxPost: true,
            showAllError: false,
            tipSweep: true,
            postonce: true,
            beforeSubmit: function (curform) {
                //在验证成功后，表单提交前执行的函数，curform参数是当前表单对象。
                //这里明确return false的话表单将不会提交;	
               
            },
            callback: function (data) {
                if (typeof (successFun) === "function") {
                    successFun(data);
                }
                return false;
            }
        });
    };
    return $(this).each(function () {
        checkValidform($(this));
    });
}
//======================以上基于Validform插件======================