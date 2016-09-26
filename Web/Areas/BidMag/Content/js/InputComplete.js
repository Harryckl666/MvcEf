(function ($) {

    $.fn.Complete_Text = function (options) { //定义插件的名称，Complete_Text
        var dft = {
            textId: "",
            btnid: "",
            url: "",
            CompleteUrl: ""
        };
        var ops = $.extend(dft, options);

        var input_width = $("#" + ops.textId).css("width");
        //.inputComplete{width:190px; height:25px; line-height:25px; position:relative;} 
        var style = "<style id=\"CompleteStyle\"> .posi{width:" + input_width + "; position:absolute; border:1px solid #ccc; margin-top:0px;*margin-top:30px;margin-left:0px;*margin-left:-198px;z-index:9999;} .posi ul li{height:25px; padding:0 0 0 10px; line-height:25px; list-style-type:none; overflow:hidden; width:185px; white-space:nowrap;text-overflow: ellipsis;} .posi ul{padding:0;margin:0;min-height:150px;height:150px;overflow-y:scroll;overflow-x:hidden;background-color:White;} .posi ul li:hover{background-color:Blue;color:White;cursor:pointer;}</style>"; //调用默认的样式
        var cpTxt = "<div onmouseover=\"javascript:$('#div_CompleteStyle" + ops.textId + "').show();\" onmouseout=\"javascript:$('#div_CompleteStyle" + ops.textId + "').hide();\" id=\"div_CompleteStyle" + ops.textId + "\" class=\"posi\"><ul id=\"ul_" + ops.btnid + "\"><li>暂无信息</li></ul></div>"; //生成html代码
        //        $("#CompleteStyle").remove();
        //        $("#div_CompleteStyle" + ops.textId).remove();
        if (document.getElementById("div_CompleteStyle" + ops.textId) == null) {

            $("#" + ops.btnid).after(style + "<br/>" + cpTxt); //把文字加入到想显示的div
        }
        else {
            $("#div_CompleteStyle" + ops.textId).show();
        }
        $.ajax({
            type: "get",
            url: ops.url,
            dataType: "json",
            data: { r: Math.random(), Val: $("#" + ops.textId).val() },
            beforeSend: function () {

            },
            success: function (data) {
                if (data != null) {
                    if (data.length > 0) {
                        $("#ul_" + ops.btnid).html("");
                        for (var i = 0; i < data.length; i++) {
                            $("#ul_" + ops.btnid).append("<li style='display: block;' onclick=\"loadAjax('div_CompleteStyle" + ops.textId + "','" + data[i].value + "','" + ops.CompleteUrl + "');\">" + data[i].item + "</li>");
                        }
                    }
                    else {

                        $("#ul_" + ops.btnid).html("<li>暂无信息</li>");
                    }
                }
                else {
                    $("#ul_" + ops.btnid).html("<li>暂无信息</li>");
                }

                $(document).click(function (e) {
                    var e_id = $(e.target).attr('id');
                    if (e_id != ops.btnid) {
                        $("#div_CompleteStyle" + ops.textId).hide();
                    }
                })
            }


        });

    }


})(jQuery);

function loadAjax(divId, id, url) {
    $("#" + divId).hide();
    $.ajax({
        type: "get",
        url: url,
        dataType: "json",
        data: { r: Math.random(), Id: id },
        beforeSend: function () {

        },
        success: function (data) {
            if (data != null) {

                for (var key in data) {
                    // alert( + "," + key);
                    if (data[key] != 0 && data[key] != null) {
                        $("#" + key).val(data[key]);
                    }
                }

            }
            else {

            }

        }
    });
}