$(function () {
    var MyFunction = {
        loadSelectData: function (url, httpdata, defval, loadIndex, successfun) {//return selecthtml
            var loadNextId = "";
            $.ajax({
                type: "get",
                url: url,
                dataType: "json",
                data: httpdata,
                beforeSend: function () {

                },
                success: function (data) {

                    if (data != null) {

                        var select_html = "";
                        for (var i = 0; i < data.length; i++) {
                            if (i == 0) {
                                select_html += "<option value=\"\">请选择</option>";
                            }
                            if (defval != null) {
                                var sp_defval = defval.split(',');
                                if (data[i].value == sp_defval[loadIndex]) {

                                    select_html += "<option selected=\"selected\" value=\"" + data[i].value + "\">" + data[i].item + "</option>";
                                    loadNextId = data[i].value;
                                }
                                else {
                                    select_html += "<option value=\"" + data[i].value + "\">" + data[i].item + "</option>";
                                }
                            }
                            else {
                                select_html += "<option value=\"" + data[i].value + "\">" + data[i].item + "</option>";
                            }
                        }
                    }
                    else {
                        select_html = "";
                    }
                    if (typeof (successfun) == "function") {

                        successfun(select_html, loadNextId);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {

                    //closelodding();
                    var HttpStatus = parseInt(XMLHttpRequest.status);
                    if (HttpStatus != 0);
                    {

                        alert("操作失败，请重试！" + errorThrown);
                        return false;
                    }
                    return false;

                }
            });
        },
        AddSelect: function (container, ops, loadIndex, httpdata) {
            var newSelectId = $("#" + container).find("select").length;
            MyFunction.loadSelectData(ops.url, httpdata, ops.defval, loadIndex, function (select_html, loadNextId) {
                if (select_html != "") {
                    $("#" + container).append("<select style=\"" + ops.selectstyle + "\" class=\"" + ops.selectclass + "\" id=\"Multi_" + newSelectId + "\"></select>");
                    $("#Multi_" + newSelectId).html(select_html);

                    if (loadNextId != "") {
                        var new_loadIndex = loadIndex + 1;
                        MyFunction.AddSelect(container, ops, new_loadIndex, { ParentId: loadNextId });
                    }

                    $("#Multi_" + newSelectId).unbind("change");
                    $("#Multi_" + newSelectId).on("change", function () {
                        ops.defval = "";
                        var this_select_index = $("#Multi_" + newSelectId).index();
                        var this_val = $(this).val();

                        $("#" + container).find("select").each(function () {
                            var child_index = $(this).index();
                            if (child_index > this_select_index) {
                                $(this).select2("destroy");
                                $(this).remove();

                            }
                        });

                        if (this_val != "") {
                            var new_loadIndex = loadIndex + 1;
                            MyFunction.AddSelect(container, ops, new_loadIndex, { ParentId: this_val });
                        }
                        else {
                            var var_arr = new Array();
                            $("#" + container).find("select").each(function () {
                                $(this).select2({
                                    placeholder: "\u8bf7\u9009\u62e9...",
                                    allowClear: true

                                });
                                var child_val = $(this).val();
                                if (child_val != "") {
                                    var_arr.push(child_val);
                                }
                            });
                            var acceptId_val = "";
                            if (ops.getall) {
                                acceptId_val = var_arr.join(",");
                            }
                            else {
                                acceptId_val = var_arr[var_arr.length - 1];
                            }
                            if (ops.getallId !== "") {
                               
                                $("#" + ops.getallId).val(var_arr.join(","));
                            }
                            $("#" + ops.acceptId).val(acceptId_val);
                           
                        }
                    });



                }
                var var_arr = new Array();
                $("#" + container).find("select").each(function () {

                    var child_val = $(this).val();
                    $(this).select2({
                        placeholder: "\u8bf7\u9009\u62e9...",
                        allowClear: true

                    });
                    if (child_val != "") {
                        var_arr.push(child_val);
                    }

                });
                var acceptId_val = "";
                if (ops.getall) {
                    acceptId_val = var_arr.join(",");
                }
                else {
                    acceptId_val = var_arr[var_arr.length - 1];
                }
                if (ops.getallId !== "") {

                    $("#" + ops.getallId).val(var_arr.join(","));
                }
                $("#" + ops.acceptId).val(acceptId_val);
            });
        }

    };
    //容器Id,url,选择后赋值对象
    $.fn.MultipleSelect = function (options) {
        var $this_obj = $(this);
        var container = $this_obj.attr("id");
        var dft = {
            url: "",
            acceptId: "",
            defval: "",
            selectclass: "",
            selectstyle: "",
            getall: true,
            getallId: ""
        }
        var ops = $.extend(dft, options);
        MyFunction.AddSelect(container, ops, 0, {});
    };
});