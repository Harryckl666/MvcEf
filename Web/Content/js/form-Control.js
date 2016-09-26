var FileHandle =
   {
       delFile: function (uploader, fileid) {

           uploader.removeFile(uploader.getFile(fileid), true);
           $("#child_" + fileid).remove();

       },
       EditFile: function (fileid, FDescription) {
           alert(fileid);
       }
   };
(function ($, window) {
    var Null_Convert = function (val, defval) {
        try {
            if (val == null || val == "") {
                if (defval != null) {
                    if (defval != "") {
                        return defval;
                    }
                    else {
                        return "";
                    }
                }
                else {
                    return "";
                }
            }
            else {
                return val;
            }
        }
        catch (e) {
            if (defval != null) {
                if (defval != "") {
                    return defval;
                }
                else {
                    return "";
                }
            }
            else {
                return "";
            }
        }

    }

    $.fn.def_select2 = function () {
        var $ThisObject = $(this);
        var empty_val = $(this).attr("select2-empty");
        if (empty_val != null) {
            if (empty_val != "") {
                $ThisObject.append("<option selected  value=\"\">" + empty_val + "</option>");
            }
        }
        var select_val = $(this).attr("select2-val");
        if (select_val != null) {
            if (select_val != "") {
                $(this).val(select_val);
            }
        }
        $ThisObject.select2({
            placeholder: "\u8bf7\u9009\u62e9...",
            allowClear: true

        });
    }

    //$.fn.ajax_select2 = function (param, count_k) {
    //    var url = $(this).attr("select2-url");
    //    var multiple = $(this).attr("multiple");
    //    var method = $(this).attr("select2-method");
    //    var change_to = $(this).attr("change-to");
    //    var select2_val = $(this).attr("select2-val");
    //    var empty_val = $(this).attr("select2-empty");
    //    var $ThisObject = $(this);
    //    if (method == null || method == "") { method = "get"; }
    //    $.AjaxSend2(method, url, param, count_k, function (data, k) {

    //        $ThisObject.html("");
    //        $ThisObject.val(null).trigger("change");
    //        data = data == null ? "" : data;
    //        if (empty_val != null) {
    //            if (empty_val != "") {
    //                $ThisObject.append("<option selected  value=\"\">" + empty_val + "</option>");
    //            }
    //        }
    //        if (data != "") {
    //            for (var j = 0; j < data.length; j++) {
    //                if (("," + select2_val + ",").indexOf(("," + data[j].value + ",")) >= 0) {


    //                    if (!data[j].disabled) {
    //                        $ThisObject.append("<option selected  value=\"" + data[j].value + "\">" + data[j].item + "</option>");
    //                    }
    //                    else {
    //                        $ThisObject.append("<option selected disabled=\"disabled\" value=\"" + data[j].value + "\">" + data[j].item + "</option>");
    //                    }
    //                }
    //                else {
    //                    if (data[j].value == "" && multiple == "multiple") {
    //                        $ThisObject.append("<option disabled=\"disabled\" value=\"\">" + data[j].item + "</option>");
    //                    }
    //                    else {
    //                        if (!data[j].disabled) {
    //                            if (!data[j].selected) {
    //                                $ThisObject.append("<option  value=\"" + data[j].value + "\">" + data[j].item + "</option>");
    //                            }
    //                            else {
    //                                $ThisObject.append("<option selected  value=\"" + data[j].value + "\">" + data[j].item + "</option>");
    //                            }

    //                        }
    //                        else {
    //                            if (!data[j].selected) {
    //                                $ThisObject.append("<option  value=\"" + data[j].value + "\">" + data[j].item + "</option>");
    //                            }
    //                            else {
    //                                $ThisObject.append("<option selected=\"" + data[j].selected + "\" disabled=\"disabled\" value=\"" + data[j].value + "\">" + data[j].item + "</option>");
    //                            }
    //                        }
    //                    }
    //                }
    //            }

    //            if (change_to != null && change_to != "" && select2_val != null && select2_val != "") {

    //                $("#" + change_to).ajax_select2({ "parentId": select2_val }, 0);
    //            }

    //            if (change_to != null && change_to != "") {
    //                $ThisObject.unbind("change").on("change", function () {

    //                    var changeval = $(this).val();
    //                    //if (changeval == "")
    //                    //{

    //                    //    return false;
    //                    //}
    //                    //else
    //                    //{
    //                    $("#" + change_to).ajax_select2({ "parentId": $(this).val() }, 0);
    //                    // }
    //                });

    //            }
    //        }
    //        if ($ThisObject.attr("data-val-required") != null) {
    //            if ($ThisObject.attr("data-val-required") != "") {
    //                $ThisObject.on("change", function () { $("form").validate().element($ThisObject); });
    //            }
    //        }
    //        //初始化select2组件，统一初始化有问题
    //        $ThisObject.select2({
    //            placeholder: "\u8bf7\u9009\u62e9...",
    //            allowClear: true

    //        });

    //    }, function () { });

    //}
    $.fn.ajax_select2 = function (param) {
        var url = $(this).attr("select2-url");
        var multiple = $(this).attr("multiple");
        var method = $(this).attr("select2-method");
        var change_to = $(this).attr("change-to");
        
        var select2_val = $(this).attr("select2-val");
        var empty_val = $(this).attr("select2-empty");

        var $ThisObject = $(this);
        if (method == null || method == "") { method = "get"; }
        $.AjaxSend(method, url, param, function (data) {

            $ThisObject.html("");
           // $ThisObject.val(null).trigger("change");
            data = data == null ? "" : data;
            if (empty_val != null) {
                if (empty_val != "" && multiple != "multiple") {
                    $ThisObject.append("<option selected  value=\"\">" + empty_val + "</option>");
                }
            }
            if (data != "") {
                if (data.group) {
                    var group_html = "";
                    for (var g = 0; g < data.group.length; g++) {
                        group_html += "<optgroup label=\"" + data.group[g].item + "\">";
                        for (var g_i = 0; g_i < data.group[g].selectItem.length; g_i++) {
                            if (("," + select2_val + ",").indexOf(("," + data.group[g].selectItem[g_i].value + ",")) >= 0) {


                                if (!data.group[g].selectItem[g_i].disabled) {
                                    group_html += "<option selected  value=\"" + data.group[g].selectItem[g_i].value + "\">" + data.group[g].selectItem[g_i].item + "</option>";
                                }
                                else {
                                    group_html += "<option selected disabled=\"disabled\" value=\"" + data.group[g].selectItem[g_i].value + "\">" + data.group[g].selectItem[g_i].item + "</option>";
                                }
                            }
                            else {
                                if (data.group[g].selectItem[g_i].value == "" && multiple == "multiple") {
                                    group_html += "<option disabled=\"disabled\" value=\"\">" + data.group[g].selectItem[g_i].item + "</option>";
                                }
                                else {
                                    if (!data.group[g].selectItem[g_i].disabled) {
                                        if (!data.group[g].selectItem[g_i].selected) {
                                            group_html += "<option  value=\"" + data.group[g].selectItem[g_i].value + "\">" + data.group[g].selectItem[g_i].item + "</option>";
                                        }
                                        else {
                                            group_html += "<option selected  value=\"" + data.group[g].selectItem[g_i].value + "\">" + data.group[g].selectItem[g_i].item + "</option>";
                                        }

                                    }
                                    else {
                                        if (!data[j].selected) {
                                            group_html += "<option  value=\"" + data.group[g].selectItem[g_i].value + "\">" + data.group[g].selectItem[g_i].item + "</option>";
                                        }
                                        else {
                                            group_html += "<option selected=\"" + data.group[g].selectItem[g_i].selected + "\" disabled=\"disabled\" value=\"" + data.group[g].selectItem[g_i].value + "\">" + data.group[g].selectItem[g_i].item + "</option>";
                                        }
                                    }
                                }
                            }
                        }
                        group_html += "</optgroup>";
                    }
                    $ThisObject.append(group_html);
                }
                else {
                    for (var j = 0; j < data.length; j++) {

                        if (("," + select2_val + ",").indexOf(("," + data[j].value + ",")) >= 0) {


                            if (!data[j].disabled) {
                                $ThisObject.append("<option selected  value=\"" + data[j].value + "\">" + data[j].item + "</option>");
                            }
                            else {
                                $ThisObject.append("<option selected disabled=\"disabled\" value=\"" + data[j].value + "\">" + data[j].item + "</option>");
                            }
                        }
                        else {

                            if (data[j].value == "" && multiple == "multiple") {
                                $ThisObject.append("<option disabled=\"disabled\" value=\"\">" + data[j].item + "</option>");
                            }
                            else {
                                if (!data[j].disabled) {
                                    if (!data[j].selected) {
                                        $ThisObject.append("<option  value=\"" + data[j].value + "\">" + data[j].item + "</option>");
                                    }
                                    else {
                                        $ThisObject.append("<option selected  value=\"" + data[j].value + "\">" + data[j].item + "</option>");
                                    }

                                }
                                else {
                                    if (!data[j].selected) {
                                        $ThisObject.append("<option  value=\"" + data[j].value + "\">" + data[j].item + "</option>");
                                    }
                                    else {
                                        $ThisObject.append("<option selected=\"" + data[j].selected + "\" disabled=\"disabled\" value=\"" + data[j].value + "\">" + data[j].item + "</option>");
                                    }
                                }
                            }
                        }
                    }
                }

             
                
                
            }
            if (change_to != null && change_to != "") {

                $ThisObject.unbind("change");

                $ThisObject.on("change", function () {

                    var changeval = $(this).val();
                    if (change_to.indexOf(",") >= 0) {
                        for (var i = 0; i < change_to.split(",").length; i++) {

                            $("#" + change_to.split(",")[i]).ajax_select2({ "parentId": changeval });
                        }
                    }
                    else {
                        $("#" + change_to).ajax_select2({ "parentId": changeval });
                    }

                    // }
                });

                $ThisObject.trigger("change");
            }
            //初始化select2组件，统一初始化有问题
            $ThisObject.select2({
                placeholder: "\u8bf7\u9009\u62e9...",
                allowClear: true

            });

        }, "json");

    }
    $.fn.ajax_CheckBox = function (param, backfun) {
        var url = $(this).attr("ck-url");
        var method = $(this).attr("ck-method");
        // var change_to = $(this).attr("change-to");
        var ck_val = $(this).attr("ck-val");
        var ck_name = $(this).attr("ck-name");
        // $("#" + ck_name).remove();
        var $ThisObject = $(this);

        if (method == null || method == "") { method = "get"; }
        $.AjaxSend(method, url, param, function (data) {

            //$ThisObject.html("");
            data = data == null ? "" : data;

            if (data != "") {
                for (var j = 0; j < data.length; j++) {
                    if (("," + ck_val + ",").indexOf(("," + data[j].value + ",")) >= 0) {


                        if (!data[j].disabled) {
                            if (j == 0) {
                                if ($ThisObject.find("input[type=checkbox]").length != 0) {

                                    $ThisObject.find("input[type=checkbox]").eq(0).attr("checked", "checked");
                                    $ThisObject.find("input[type=checkbox]").eq(0).val(data[j].value);
                                    $ThisObject.find("label").eq(0).text(" " + data[j].item);
                                }
                                else {

                                    $ThisObject.append("<input type=\"checkbox\" id=\"" + ck_name + "_" + j + "\" name=\"" + ck_name + "\" checked class=\"form-control\" data-checkbox=\"icheckbox_flat-blue\" value=\"" + data[j].value + "\"><label for=\"" + ck_name + "_" + j + "\"> " + data[j].item + "</label>");
                                }
                            }
                            else {
                                $ThisObject.append("<input type=\"checkbox\" id=\"" + ck_name + "_" + j + "\" name=\"" + ck_name + "\" checked class=\"form-control\" data-checkbox=\"icheckbox_flat-blue\" value=\"" + data[j].value + "\"><label for=\"" + ck_name + "_" + j + "\"> " + data[j].item + "</label>");
                            }
                        }
                        else {
                            if (j == 0) {
                                if ($ThisObject.find("input[type=checkbox]").length != 0) {
                                    $ThisObject.find("input[type=checkbox]").eq(0).attr("checked", "checked");
                                    $ThisObject.find("input[type=checkbox]").eq(0).attr("disabled", "disabled");
                                    $ThisObject.find("input[type=checkbox]").eq(0).val(data[j].value);
                                    $ThisObject.find("label").eq(0).text(" " + data[j].item);
                                }
                                else {
                                    $ThisObject.append("<input type=\"checkbox\" id=\"" + ck_name + "_" + j + "\" name=\"" + ck_name + "\" disabled checked class=\"form-control\" data-checkbox=\"icheckbox_flat-blue\" value=\"" + data[j].value + "\"><label for=\"" + ck_name + "_" + j + "\"> " + data[j].item + "</label>");
                                }
                            }
                            else {
                                $ThisObject.append("<input type=\"checkbox\" id=\"" + ck_name + "_" + j + "\" name=\"" + ck_name + "\" disabled checked class=\"form-control\" data-checkbox=\"icheckbox_flat-blue\" value=\"" + data[j].value + "\"><label for=\"" + ck_name + "_" + j + "\"> " + data[j].item + "</label>");
                            }

                        }
                    }
                    else {

                        if (!data[j].disabled) {
                            if (!data[j].selected) {

                                if (j == 0) {
                                    if ($ThisObject.find("input[type=checkbox]").length != 0) {
                                        $ThisObject.find("input[type=checkbox]").eq(0).val(data[j].value);
                                        $ThisObject.find("label").eq(0).text(" " + data[j].item);
                                    }
                                    else {

                                        $ThisObject.append("<input type=\"checkbox\" id=\"" + ck_name + "_" + j + "\" name=\"" + ck_name + "\"  class=\"form-control\" data-checkbox=\"icheckbox_flat-blue\" value=\"" + data[j].value + "\"> <label for=\"" + ck_name + "_" + j + "\"> " + data[j].item + "</label>");
                                    }
                                }
                                else {
                                    $ThisObject.append("<input type=\"checkbox\" id=\"" + ck_name + "_" + j + "\" name=\"" + ck_name + "\"  class=\"form-control\" data-checkbox=\"icheckbox_flat-blue\" value=\"" + data[j].value + "\"> <label for=\"" + ck_name + "_" + j + "\"> " + data[j].item + "</label>");
                                }
                            }
                            else {
                                if (j == 0) {
                                    if ($ThisObject.find("input[type=checkbox]").length != 0) {
                                        $ThisObject.find("input[type=checkbox]").eq(0).attr("checked", "checked");
                                        $ThisObject.find("input[type=checkbox]").eq(0).val(data[j].value);
                                        $ThisObject.find("label").eq(0).text(" " + data[j].item);
                                    }
                                    else {
                                        $ThisObject.append("<input type=\"checkbox\" id=\"" + ck_name + "_" + j + "\" name=\"" + ck_name + "\" checked class=\"form-control\" data-checkbox=\"icheckbox_flat-blue\" value=\"" + data[j].value + "\"> <label for=\"" + ck_name + "_" + j + "\"> " + data[j].item + "</label>");
                                    }
                                }
                                else {
                                    $ThisObject.append("<input type=\"checkbox\" id=\"" + ck_name + "_" + j + "\" name=\"" + ck_name + "\" checked class=\"form-control\" data-checkbox=\"icheckbox_flat-blue\" value=\"" + data[j].value + "\"> <label for=\"" + ck_name + "_" + j + "\"> " + data[j].item + "</label>");
                                }

                            }

                        }
                        else {
                            if (!data[j].selected) {
                                if (j == 0) {
                                    if ($ThisObject.find("input[type=checkbox]").length != 0) {
                                        $ThisObject.find("input[type=checkbox]").eq(0).val(data[j].value);
                                        $ThisObject.find("label").eq(0).text(" " + data[j].item);
                                    }
                                    else {
                                        $ThisObject.append("<input type=\"checkbox\" id=\"" + ck_name + "_" + j + "\" name=\"" + ck_name + "\"  class=\"form-control\" data-checkbox=\"icheckbox_flat-blue\" value=\"" + data[j].value + "\"><label for=\"" + ck_name + "_" + j + "\"> " + data[j].item + "</label>");
                                    }
                                }
                                else {
                                    $ThisObject.append("<input type=\"checkbox\" id=\"" + ck_name + "_" + j + "\" name=\"" + ck_name + "\"  class=\"form-control\" data-checkbox=\"icheckbox_flat-blue\" value=\"" + data[j].value + "\"><label for=\"" + ck_name + "_" + j + "\"> " + data[j].item + "</label>");
                                }
                            }
                            else {
                                if (j == 0) {
                                    if ($ThisObject.find("input[type=checkbox]").length != 0) {
                                        $ThisObject.find("input[type=checkbox]").eq(0).attr("disabled", "disabled");
                                        $ThisObject.find("input[type=checkbox]").eq(0).val(data[j].value);
                                        $ThisObject.find("label").eq(0).text(" " + data[j].item);
                                    }
                                    else {
                                        $ThisObject.append("<input type=\"checkbox\" id=\"" + ck_name + "_" + j + "\" name=\"" + ck_name + "\" disabled class=\"form-control\" data-checkbox=\"icheckbox_flat-blue\" value=\"" + data[j].value + "\"><label for=\"" + ck_name + "_" + j + "\"> " + data[j].item + "</label>");
                                    }
                                }
                                else {
                                    $ThisObject.append("<input type=\"checkbox\" id=\"" + ck_name + "_" + j + "\" name=\"" + ck_name + "\" disabled class=\"form-control\" data-checkbox=\"icheckbox_flat-blue\" value=\"" + data[j].value + "\"><label for=\"" + ck_name + "_" + j + "\"> " + data[j].item + "</label>");
                                }

                            }
                        }

                    }
                }

            }
            //初始化Icheck组件
            $ThisObject.find("input[type=checkbox]").iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
                increaseArea: '20%'
            });
            $ThisObject.find("div").addClass("validates");
            $ThisObject.find(".validates input[type=checkbox]").on('ifChecked', function (event) { //如果是选中，点击后则为不选中 
                if ($("form").validate != null) {
                    $("form").validate().element($("#" + this.id));
                }
            });
            $ThisObject.find(".validates input[type=checkbox]").on('ifUnchecked', function (event) { //如果是选中，点击后则为不选中 
                if ($("form").validate != null) {
                    $("form").validate().element($("#" + this.id));
                }

            });
            if (typeof (backfun) == "function") {
                backfun();
            }
        }, "json");

    }
    $.fn.ajax_Radio = function (param) {
        var url = $(this).attr("radio-url");
        var method = $(this).attr("radio-method");
        var radio_val = $(this).attr("radio-val");
        var radio_name = $(this).attr("radio-name");
        var $ThisObject = $(this);

        if (method == null || method == "") { method = "get"; }
        $.AjaxSend(method, url, param, function (data) {

            //$ThisObject.html("");
            data = data == null ? "" : data;

            if (data != "") {
                for (var j = 0; j < data.length; j++) {
                    if (("," + radio_val + ",").indexOf(("," + data[j].value + ",")) >= 0) {


                        if (!data[j].disabled) {
                            if (j == 0) {
                                if ($ThisObject.find("input[type=radio]").length != 0) {
                                    $ThisObject.find("input[type=radio]").eq(0).attr("checked", "checked");
                                    $ThisObject.find("input[type=radio]").eq(0).val(data[j].value);
                                    $ThisObject.find("label").eq(0).text(" " + data[j].item);
                                }
                                else {
                                    $ThisObject.append("<input type=\"radio\" id=\"" + radio_name + "_" + j + "\" name=\"" + radio_name + "\" checked class=\"form-control\" data-radio=\"radio_flat-blue\" value=\"" + data[j].value + "\"><label for=\"" + radio_name + "_" + j + "\"> " + data[j].item + "</label>");
                                }
                            }
                            else {
                                $ThisObject.append("<input type=\"radio\" id=\"" + radio_name + "_" + j + "\" name=\"" + radio_name + "\" checked class=\"form-control\" data-radio=\"radio_flat-blue\" value=\"" + data[j].value + "\"><label for=\"" + radio_name + "_" + j + "\"> " + data[j].item + "</label>");
                            }
                        }
                        else {
                            if (j == 0) {
                                if ($ThisObject.find("input[type=radio]").length != 0) {
                                    $ThisObject.find("input[type=radio]").eq(0).attr("checked", "checked");
                                    $ThisObject.find("input[type=radio]").eq(0).attr("disabled", "disabled");
                                    $ThisObject.find("input[type=radio]").eq(0).val(data[j].value);
                                    $ThisObject.find("label").eq(0).text(" " + data[j].item);
                                }
                                else {
                                    $ThisObject.append("<input type=\"radio\" id=\"" + radio_name + "_" + j + "\" name=\"" + radio_name + "\" disabled checked class=\"form-control\" data-radio=\"radio_flat-blue\" value=\"" + data[j].value + "\"><label for=\"" + radio_name + "_" + j + "\"> " + data[j].item + "</label>");
                                }
                            }
                            else {
                                $ThisObject.append("<input type=\"radio\" id=\"" + radio_name + "_" + j + "\" name=\"" + radio_name + "\" disabled checked class=\"form-control\" data-radio=\"radio_flat-blue\" value=\"" + data[j].value + "\"><label for=\"" + radio_name + "_" + j + "\"> " + data[j].item + "</label>");
                            }

                        }
                    }
                    else {

                        if (!data[j].disabled) {
                            if (!data[j].selected) {
                                if (j == 0) {
                                    if ($ThisObject.find("input[type=radio]").length != 0) {
                                        $ThisObject.find("input[type=radio]").eq(0).val(data[j].value);
                                        $ThisObject.find("label").eq(0).text(" " + data[j].item);
                                    }
                                    else {
                                        $ThisObject.append("<input type=\"radio\" id=\"" + radio_name + "_" + j + "\" name=\"" + radio_name + "\"  class=\"form-control\" data-radio=\"radio_flat-blue\" value=\"" + data[j].value + "\"> <label for=\"" + radio_name + "_" + j + "\"> " + data[j].item + "</label>");
                                    }
                                }
                                else {
                                    $ThisObject.append("<input type=\"radio\" id=\"" + radio_name + "_" + j + "\" name=\"" + radio_name + "\"  class=\"form-control\" data-radio=\"radio_flat-blue\" value=\"" + data[j].value + "\"> <label for=\"" + radio_name + "_" + j + "\"> " + data[j].item + "</label>");
                                }
                            }
                            else {
                                if (j == 0) {
                                    if ($ThisObject.find("input[type=radio]").length != 0) {
                                        $ThisObject.find("input[type=radio]").eq(0).attr("checked", "checked");
                                        $ThisObject.find("input[type=radio]").eq(0).val(data[j].value);
                                        $ThisObject.find("label").eq(0).text(" " + data[j].item);
                                    }
                                    else {
                                        $ThisObject.append("<input type=\"checkbox\" id=\"" + radio_name + "_" + j + "\" name=\"" + radio_name + "\" checked class=\"form-control\" data-radio=\"radio_flat-blue\" value=\"" + data[j].value + "\"> <label for=\"" + radio_name + "_" + j + "\"> " + data[j].item + "</label>");
                                    }
                                }
                                else {
                                    $ThisObject.append("<input type=\"checkbox\" id=\"" + radio_name + "_" + j + "\" name=\"" + radio_name + "\" checked class=\"form-control\" data-radio=\"radio_flat-blue\" value=\"" + data[j].value + "\"> <label for=\"" + radio_name + "_" + j + "\"> " + data[j].item + "</label>");
                                }

                            }

                        }
                        else {
                            if (!data[j].selected) {
                                if (j == 0) {
                                    if ($ThisObject.find("input[type=radio]").length != 0) {
                                        $ThisObject.find("input[type=radio]").eq(0).val(data[j].value);
                                        $ThisObject.find("label").eq(0).text(" " + data[j].item);
                                    }
                                    else {
                                        $ThisObject.append("<input type=\"radio\" id=\"" + radio_name + "_" + j + "\" name=\"" + radio_name + "\"  class=\"form-control\" data-radio=\"radio_flat-blue\" value=\"" + data[j].value + "\"><label for=\"" + radio_name + "_" + j + "\"> " + data[j].item + "</label>");
                                    }
                                }
                                else {
                                    $ThisObject.append("<input type=\"radio\" id=\"" + radio_name + "_" + j + "\" name=\"" + radio_name + "\"  class=\"form-control\" data-radio=\"radio_flat-blue\" value=\"" + data[j].value + "\"><label for=\"" + radio_name + "_" + j + "\"> " + data[j].item + "</label>");
                                }
                            }
                            else {
                                if (j == 0) {
                                    if ($ThisObject.find("input[type=radio]").length != 0) {
                                        $ThisObject.find("input[type=radio]").eq(0).attr("disabled", "disabled");
                                        $ThisObject.find("input[type=radio]").eq(0).val(data[j].value);
                                        $ThisObject.find("label").eq(0).text(" " + data[j].item);
                                    }
                                    else {
                                        $ThisObject.append("<input type=\"checkbox\" id=\"" + radio_name + "_" + j + "\" name=\"" + radio_name + "\" disabled class=\"form-control\" data-radio=\"radio_flat-blue\" value=\"" + data[j].value + "\"><label for=\"" + radio_name + "_" + j + "\"> " + data[j].item + "</label>");
                                    }
                                }
                                else {
                                    $ThisObject.append("<input type=\"checkbox\" id=\"" + radio_name + "_" + j + "\" name=\"" + radio_name + "\" disabled class=\"form-control\" data-radio=\"radio_flat-blue\" value=\"" + data[j].value + "\"><label for=\"" + radio_name + "_" + j + "\"> " + data[j].item + "</label>");
                                }

                            }
                        }

                    }
                }

            }
            //初始化Icheck组件
            $ThisObject.find("input[type=radio]").iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
                increaseArea: '20%'
            });
            $ThisObject.find("div").addClass("validates");
            $ThisObject.find(".validates input[type=radio]").on('ifChecked', function (event) { //如果是选中，点击后则为不选中 
                if ($("form").validate != null) {
                    $("form").validate().element($("#" + this.id));
                }
            });
            $ThisObject.find(".validates input[type=radio]").on('ifUnchecked', function (event) { //如果是选中，点击后则为不选中 
                if ($("form").validate != null) {
                    $("form").validate().element($("#" + this.id));
                }
            });

        }, "json");
    }

    //全局变量
    var applicationPath = window.applicationPath === "" ? "" : window.applicationPath || "../..";

    function newGuid() {
        var guid = "";
        for (var i = 1; i <= 32; i++) {
            var n = Math.floor(Math.random() * 16.0).toString(16);
            guid += n;
            if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
                guid += "-";
        }
        return guid;
    }


    function initWebUpload(target, options) {
        //判断浏览器是否支持
        if (!WebUploader.Uploader.support()) {
            var error = "上传控件不支持您的浏览器！请尝试升级flash版本或者使用Chrome引擎的浏览器。<a target='_blank' href='http://se.360.cn'>下载页面</a>";

            if (window.console) {
                window.console.log(error);
            }
            $(target).text(error);
            return;
        }
        //文件MD5记录器
        var ArrMD5 = new Array();
        var defaults = {
            onAllComplete: function (event) { }, // 当所有file都上传后执行的回调函数
            onComplete: function (event) { },// 每上传一个file的回调函数
            onUploadSuccess: function (file, response) {
                var oPath_obj = document.getElementById("OPath_" + file.id);
                var tPath_obj = document.getElementById("TPath_" + file.id);
                var f_Description = document.getElementById("FDescription_" + file.id);
                if (oPath_obj != null) {
                    $("#OPath_" + file.id).val(response.OPath);
                }
            },//每个文件上传成功调用
            innerOptions: {},
            fileNumLimit: undefined,
            multiple: false,
            fileSizeLimit: undefined,
            fileSingleSizeLimit: undefined,
            chunkSize: 5 * 1024 * 1024,
            FileName: "",
            FileType: "",
            Startbtn: "",
            OPath: "",//原路径（Name）
            TPath: "",//缩略图路径（如果有【Name】）
            FDescription: ""//文件描述(如果有)【Name】
        };
        var opts = $.extend({}, defaults, options);
        var hdFileData = $("#" + opts.hiddenInputId);
        var fileType = opts.FileType;
        var FileAccept = {};
        if (fileType === "office") {
            FileAccept = {
                title: 'office文件',
                extensions: 'doc,docx,xls,xlsx',
                mimeTypes: 'application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/msword,application/vnd.ms-excel,application/x-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
            };
        }
        else if (fileType === "image") {
            FileAccept = {
                title: '图片文件',
                extensions: 'gif,jpg,jpeg,bmp,png',
                mimeTypes: 'image/*'
            }
        }
        else if (fileType === "zip") {
            FileAccept = {
                title: '压缩文件',
                extensions: 'zip,rar',
                mimeTypes: 'application/zip'
            }
        }

        var $btn = target.find('#btn-upload'),//上传
            state = 'pending',
            uploader;
        var jsonData = {
            fileList: []
        };
        var $ThisId = target.attr("id");
        var webuploaderoptions = $.extend({
            chunked: true,
            method: "post",
            auto: (opts.Startbtn == "" ? true : false),
            swf: '/static/plugins/webuploader-0.1.5/Uploader.swf',
            // 文件接收服务端。
            //  server: '/BlogAdmin/WebUploader/Upload' + (opts.innerOptions.chunked == true ? "Chunked" : ""),
            server: '/BlogAdmin/WebUploader/Upload',
            // 选择文件的按钮。可选。
            // 内部根据当前运行是创建，可能是input元素，也可能是flash.
            pick: { id: '#' + $ThisId, multiple: opts.multiple },
            // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
            threads: 1,
            accept: FileAccept,
            resize: false,
            multiple: opts.multiple,
            chunkSize: opts.chunkSize,
            fileNumLimit: opts.fileNumLimit,
            fileSizeLimit: opts.fileSizeLimit,
            fileSingleSizeLimit: opts.fileSingleSizeLimit,
            formData: { fileMd5: newGuid(), FileName: opts.FileName }
        },
        opts.innerOptions);
        var uploader = WebUploader.create(webuploaderoptions);
        if (document.getElementById("up_father_" + $ThisId) == null) {
            target.parent().after("<div class =\"col-md-12 dropzone\" id=\"up_father_" + $ThisId + "\"></div>");
        }
        //当有文件被添加进队列的时候触发
        uploader.on('fileQueued', function (file) {
            $thisuploader = uploader;

            /*保存源文件，压缩文件，文件描述*/
            var FileHtml = "";
            if (opts.multiple) {

                var ThisFileCount = $("#up_father_" + $ThisId).find(".dz-preview").length;
                if (opts.OPath != "") {
                    FileHtml += "<input type=\"hidden\" id=\"OPath_" + file.id + "\" name=\"" + $ThisId + "[" + ThisFileCount + "]." + opts.OPath + "\"/>";
                }
                if (opts.TPath != "") {
                    FileHtml += "<input type=\"hidden\" id=\"TPath_" + file.id + "\" name=\"" + $ThisId + "[" + ThisFileCount + "]." + opts.TPath + "\"/>";
                }
                if (opts.FDescription != "") {
                    FileHtml += "<input type=\"hidden\" id=\"FDescription_" + file.id + "\" name=\"" + $ThisId + "[" + ThisFileCount + "]." + opts.FDescription + "\"/>";
                }
            }
            else {
                $("#up_father_" + $ThisId).html("");
                if (opts.OPath != "") {
                    FileHtml += "<input type=\"hidden\" id=\"OPath_" + file.id + "\" name=\"" + opts.OPath + "\"/>";
                }
                if (opts.TPath != "") {
                    FileHtml += "<input type=\"hidden\" id=\"TPath_" + file.id + "\" name=\"" + opts.TPath + "\"/>";
                }
                if (opts.FDescription != "") {
                    FileHtml += "<input type=\"hidden\" id=\"FDescription_" + file.id + "\" name=\"" + opts.FDescription + "\"/>";
                }
            }
            var html = "<div id=\"child_" + file.id + "\" class=\"dz-preview dz-file-preview dz-processing\">"
                      + "<div class=\"dz-details\">"
                      + "<div class=\"dz-filename\">"
                      + "<span data-dz-name=\"\">" + file.name + "</span>"
                      + "<span>等待上传...</span>"
                      + "<span></span>"
                      + "</div>"
                      + "<div class=\"dz-size\" data-dz-size=\"\">"
                      + "<strong>" + WebUploader.formatSize(file.size) + "</strong>"
                      + "</div>"
                      + "<img data-dz-thumbnail=\"\" src=\"\">"
                      + "</div>"
                      + "<div class=\"dz-progress\">"
                      + "<span class=\"dz-upload\" data-dz-uploadprogress=\"\">"
                      + "</span>"
                      + "</div>"
                      + "<div class=\"dz-success-mark\">"
                      + "<span>✔</span>"
                      + "</div>"
                      + "<div class=\"dz-success-message\">"
                      + "<span data-dz-successmessage=\"\"></span>"
                      + "</div>"
                      + "<div class=\"dz-error-mark\">"
                      + "<span>✘</span>"
                      + "</div>"
                      + "<div class=\"dz-error-message\">"
                      + "<span data-dz-errormessage=\"\"></span>"
                      + "</div>"
                      + "<div class=\"tools\">" + (opts.FDescription == "" ? "" : "<button type=\"button\" onclick=\"FileHandle.EditFile('" + file.id + "','" + opts.FDescription + "');\" class=\"btn btn-xs green\">编辑<i class=\"fa  fa-edit\"></i></button>") + "<button type=\"button\" onclick=\"FileHandle.delFile($thisuploader,'" + file.id + "');\" class=\"btn btn-xs red\">删除<i class=\"fa  fa-trash-o\"></i></button></div>"
                      + FileHtml
            + "</div>";
            $("#up_father_" + $ThisId + "").append(html);

            // $(".dz-success-message").css("display","block");
            //文件MD5记录器
            ArrMD5[file.id] = newGuid();

        });
        // 文件上传过程中创建进度条实时显示。
        uploader.on("uploadProgress", function (file, percentage) {
            $("#child_" + file.id + " .dz-filename").find("span").eq(1).html("正在上传文件");
            $("#child_" + file.id + " .dz-filename").find("span").eq(2).html(WebUploader.formatSize(file.size * percentage));
            $("#child_" + file.id + " .dz-progress .dz-upload").css("width", percentage * 100 + "%");

        });

        uploader.on("uploadBeforeSend", function (o, d, h) {
            if (d["chunk"] == null) {
                //如果文件没有分块，则写入默认的参数信息
                d["chunk"] = "0";
                d["chunks"] = "1";


            }
            d["fileMd5"] = ArrMD5[d["id"]];

        });
        //上传成功
        uploader.on('uploadSuccess', function (file, response) {
            if ($("#child_" + file.id + " .dz-filename").find("span").eq(1).text() != "极速秒传") {
                $("#child_" + file.id + " .dz-filename").find("span").eq(1).html("上传成功！");
                $("#child_" + file.id + " .dz-filename").find("span").eq(2).html("");
            }
            //移除错误样式
            $("#child_" + file.id + "").removeClass("dz-error");
            $("#child_" + file.id + " .dz-error-message").find("span").eq(0).html("");
            //添加上传成功样式
            $("#child_" + file.id + "").addClass("dz-success");
            // alert(response.filepath);
            opts.onUploadSuccess(file, response);
        });
        //上传出错
        uploader.on('uploadError', function (file, reason) {
            $("#child_" + file.id + "").removeClass("dz-success");
            $("#child_" + file.id + "").addClass("dz-error");
            $("#child_" + file.id + " .dz-error-message").find("span").eq(0).html('上传出错');
            //$('#' + file.id).find('p.state').text('上传出错');
        });
        uploader.on('uploadComplete', function (file) {
            opts.onComplete(file);
        });

        uploader.on('all', function (type) {

            if (type === 'startUpload') {
                state = 'uploading';
            } else if (type === 'stopUpload') {
                state = 'paused';
            } else if (type === 'uploadFinished') {
                state = 'done';
            }
            if (state === 'uploading') {
                $("#" + opts.Startbtn).text('暂停上传');
                $("#" + opts.Startbtn).val('暂停上传');
            } else {
                $("#" + opts.Startbtn).text('开始上传');
                $("#" + opts.Startbtn).val('开始上传');
            }
        });
        //手动上传
        $("#" + opts.Startbtn).on('click', function () {
            //if (uploader.getStats().queueNum <= 0)
            //{
            //    alert("无可上传的文件！");
            //}
            //else
            //{
            if (state === 'uploading') {
                uploader.stop();
            } else {
                uploader.upload();
            }
            // }
        });
        //删除
        //$list.on("click", ".del", function () {
        //    var $ele = $(this);
        //    var id = $ele.parent().attr("id");
        //    var deletefile = {};
        //    $.each(jsonData.fileList, function (index, item) {
        //        if (item && item.queueId === id) {
        //            uploader.removeFile(uploader.getFile(id));//不要遗漏
        //            deletefile = jsonData.fileList.splice(index, 1)[0];
        //            $("#" + opts.hiddenInputId).val(JSON.stringify(jsonData));
        //            $.post(applicationi + "/Webploader/Delete", { 'filepathname': deletefile.filePath }, function (returndata) {
        //                $ele.parent().remove();
        //            });
        //            return;
        //        }
        //    });
        //});
    }

    $.fn.myUpFile = function (options) {
        initWebUpload($(this), options);
    }



    $(function () {

        $(".select2").each(function (i) {
            $(this).def_select2("", i);

        });
        $(".select2_ajax_load").each(function (i) {
            $(this).ajax_select2("", i);

        });
        $(".checkbox_ajax_load").each(function (i) {
            $(this).ajax_CheckBox("", i);

        });
        $(".radio_ajax_load").each(function (i) {
            $(this).ajax_Radio("", i);

        });

        //if ($(".myupload").length > 0) {


        //    WebUploader.Uploader.register({
        //        'before-send-file': 'beforeSendFile'
        //    }, {
        //        beforeSendFile: function (file) {
        //            var $this = this;
        //            $owner = this.owner,
        //            server = applicationPath + "/BlogAdmin/WebUploader/Md5File",
        //            deferred = WebUploader.Deferred();
        //            var IsDelFile = $("#IsDelFile_" + file.id).val();
        //            $owner.md5File(file.source)
        //            .progress(function (percentage) {
        //                //记录当前正在验证的文件是否已经被删除（是否存在）


        //                $("#child_" + file.id + " .dz-filename").find("span").eq(1).html("正在校验文件");
        //                $("#child_" + file.id + " .dz-filename").find("span").eq(2).html(Math.round(percentage * 100) + " %");


        //            })
        //            // 如果读取出错了，则通过reject告诉webuploader文件上传出错。
        //            .fail(function () {
        //                deferred.reject();
        //            })
        //            // md5值计算完成
        //            .then(function (md5) {
        //                // 与服务安验证
        //                $.ajax(server,
        //                    {
        //                        type: "POST",
        //                        dataType: 'json',
        //                        data: {
        //                            fileid: md5,
        //                            filemd5: md5
        //                        },
        //                        success: function (response) {
        //                            $this.options.formData.fileMd5 = md5;


        //                            $("#child_" + file.id + " .dz-filename").find("span").eq(1).empty();
        //                            $("#child_" + file.id + " .dz-filename").find("span").eq(2).empty();
        //                            // 如果验证已经上传过 则跳过
        //                            if (response.exist) {

        //                                $("#child_" + file.id + " .dz-filename").find("span").eq(1).html("极速秒传");
        //                                //添加上传成功样式
        //                                $("#child_" + file.id + "").addClass("dz-success");
        //                                $owner.skipFile(file);
        //                            }
        //                            // 介绍此promise, webuploader接着往下走。
        //                            deferred.resolve();
        //                        },
        //                        error: function (ex) {
        //                            if (typeof ex == "object") {
        //                                $("#child_" + file.id + "").removeClass("dz-success");
        //                                $("#child_" + file.id + "").addClass("dz-error");
        //                                $("#child_" + file.id + " .dz-error-message").find("span").eq(0).html(ex.responseText);
        //                            }
        //                        }
        //                    });
        //            });

        //            return deferred.promise();

        //        }
        //    });
        //    //在分片发送之前request，可以用来做分片验证，如果此分片已经上传成功了，可返回一个rejected promise来跳过此分片上传
        //    WebUploader.Uploader.register(
        //        {
        //            'before-send': 'beforeSend'
        //        },
        //    {
        //        beforeSend: function (file) {
        //            var $this = this;
        //            $owner = this.owner,
        //            server = applicationPath + "/BlogAdmin/WebUploader/Md5File",
        //            deferred = WebUploader.Deferred();
        //            $owner.md5File(file.blob)
        //            .progress(function (percentage) {

        //            })
        //            // 如果读取出错了，则通过reject告诉webuploader文件上传出错。
        //            .fail(function () {
        //                deferred.reject();
        //            })
        //            // md5值计算完成
        //            .then(function (md5) {
        //                // 与服务安验证
        //                $.ajax(server,
        //                    {
        //                        type: "POST",
        //                        dataType: 'json',
        //                        data: {
        //                            fileid: md5,
        //                            filemd5: $this.options.formData.fileMd5
        //                        },
        //                        success: function (response) {

        //                            // 如果验证已经上传过则跳过
        //                            if (response.exist) {

        //                                deferred.reject();
        //                            }
        //                            // 介绍此promise, webuploader接着往下走。
        //                            deferred.resolve();
        //                        },
        //                        error: function (ex) {
        //                            alert(ex);
        //                        }
        //                    });
        //            });
        //            return deferred.promise();
        //        }
        //    });
        //}
        //初始化文件上传控件
        $(".myupload").each(function (i) {

            var $ThisObject = $(this);
            var $ThisId = $ThisObject.attr("id");
            var multi = Null_Convert($ThisObject.attr("multi"), false);

            var startbtn = Null_Convert($ThisObject.attr("startbtn"), "");
            var chunksize = Null_Convert($ThisObject.attr("chunksize"), 5 * 1024 * 1024);
            var OPath = Null_Convert($ThisObject.attr("opath"), "");
            var TPath = Null_Convert($ThisObject.attr("tpath"), "");
            var FDescription = Null_Convert($ThisObject.attr("fdescription"), "");
            var FileName = Null_Convert($ThisObject.attr("filename"), "");
            var para = Null_Convert($ThisObject.attr("para"), "");
            var FileType = Null_Convert($ThisObject.attr("fileType"), "");
            var ops = { multiple: multi, chunkSize: chunksize, Startbtn: startbtn, OPath: OPath, TPath: TPath, FDescription: FDescription, FileName: FileName, FileType: FileType };

            $ThisObject.myUpFile(ops);
        });


    });

})(jQuery, window);