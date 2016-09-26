$.fn.ajax_select2 = function (param, count_k) {
    var url = $(this).attr("select2-url");
    var multiple = $(this).attr("multiple");
    var method = $(this).attr("select2-method");
    var change_to = $(this).attr("change-to");
    var select2_val = $(this).attr("select2-val");
    var empty_val = $(this).attr("select2-empty");

    var $ThisObject = $(this);
    if (method == null || method == "") { method = "get"; }
    AjaxSend2(method, url, param, count_k, function (data, k) {

        $ThisObject.html("");
        $ThisObject.val(null).trigger("change");
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
            if (select2_val != null) {
                if (select2_val.indexOf(",") >= 0) {
                    $("#liandong2").val(select2_val.split(','));

                }
            }
            if (change_to != null && change_to != "" && select2_val != null && select2_val != "") {

                $("#" + change_to).ajax_select2({ "parentId": select2_val }, 0);
            }

            if (change_to != null && change_to != "") {
                $ThisObject.unbind("change");

                $ThisObject.on("change", function () {

                    var changeval = $(this).val();
                    //if (changeval == "")
                    //{

                    //    return false;
                    //}
                    //else
                    //{
                    if (change_to.indexOf(",") >= 0) {
                        for (var i = 0; i < change_to.split(",").length; i++) {

                            $("#" + change_to.split(",")[i]).ajax_select2({ "parentId": $(this).val() }, 0);
                        }
                    }
                    else {
                        $("#" + change_to).ajax_select2({ "parentId": $(this).val() }, 0);
                    }

                    // }
                });

            }
        }

        //初始化select2组件，统一初始化有问题
        $ThisObject.select2({
            placeholder: "\u8bf7\u9009\u62e9...",
            allowClear: true

        });

    }, function () { });

}
$.fn.ajax_select3 = function (param, count_k) {
    var url = $(this).attr("select2-url");
    var multiple = $(this).attr("multiple");
    var method = $(this).attr("select2-method");
    var change_to = $(this).attr("change-to");
    var select2_val = $(this).attr("select2-val");
    var $ThisObject = $(this);
    var empty_val = $(this).attr("select2-empty");
    if (method == null || method == "") { method = "get"; }
    AjaxSend2(method, url, param, count_k, function (data, k) {

        $ThisObject.html("");
        $ThisObject.val(null).trigger("change");
        data = data == null ? "" : data;
        if (empty_val != null) {
            if (empty_val != "" && multiple != "multiple") {
                $ThisObject.append("<option selected  value=\"\">" + empty_val + "</option>");
            }
        }
        if (data != "") {

            for (var j = 0; j < data.data.length; j++) {

                if (("," + select2_val + ",").indexOf(("," + data.data[j].value + ",")) >= 0) {


                    if (!data.data[j].disabled) {
                        $ThisObject.append("<option selected  value=\"" + data.data[j].value + "\">" + data.data[j].item + "</option>");
                    }
                    else {
                        $ThisObject.append("<option selected disabled=\"disabled\" value=\"" + data.data[j].value + "\">" + data.data[j].item + "</option>");
                    }
                }
                else {
                    if (data.data[j].value == "" && multiple == "multiple") {
                        $ThisObject.append("<option disabled=\"disabled\" value=\"\">" + data.data[j].item + "</option>");
                    }
                    else {
                        if (!data.data[j].disabled) {
                            if (!data.data[j].selected) {
                                $ThisObject.append("<option  value=\"" + data.data[j].value + "\">" + data.data[j].item + "</option>");
                            }
                            else {
                                $ThisObject.append("<option selected  value=\"" + data.data[j].value + "\">" + data.data[j].item + "</option>");
                            }

                        }
                        else {
                            if (!data.data[j].selected) {
                                $ThisObject.append("<option  value=\"" + data.data[j].value + "\">" + data.data[j].item + "</option>");
                            }
                            else {
                                $ThisObject.append("<option selected=\"" + data.data[j].selected + "\" disabled=\"disabled\" value=\"" + data.data[j].value + "\">" + data[j].item + "</option>");
                            }
                        }
                    }
                }
            }

            if (change_to != null && change_to != "" && select2_val != null && select2_val != "") {

                $("#" + change_to).ajax_select3({ "parentId": select2_val }, 0);
            }

            if (change_to != null && change_to != "") {
                $ThisObject.unbind("change");

                $ThisObject.on("change", function () {

                    var changeval = $(this).val();

                    //if (changeval == "")
                    //{

                    //    return false;
                    //}
                    //else
                    //{
                    $("#" + change_to).ajax_select3({ "parentId": $(this).val() }, 0);
                    // }
                });

            }
        }

        //初始化select2组件，统一初始化有问题
        $ThisObject.select2({
            placeholder: "\u8bf7\u9009\u62e9...",
            allowClear: true

        });

    }, function () { });

}

$.fn.def_select2 = function () {
    var $ThisObject = $(this);
    var empty_val = $(this).attr("select2-empty");
    if (empty_val != null) {
        if (empty_val != "") {
            $ThisObject.append("<option selected  value=\"\">" + empty_val + "</option>");
        }
    }
    $ThisObject.select2({
        placeholder: "\u8bf7\u9009\u62e9...",
        allowClear: true

    });
}
var InitSelect2 = function () {


    return {


        //main function to initiate the module
        init: function () {



            $(".select2").each(function (i) {

                $(this).def_select2("", i);

            });
            $(".select2_ajax_load").each(function (i) {

                $(this).ajax_select2("", i);

            });

            $(".select3_ajax_load").each(function (i) {

                $(this).ajax_select3("", i);

            });





        }

    };

}();

