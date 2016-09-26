$(function () {
    //文本框只能输入数字，并屏蔽输入法和粘贴
    $.fn.numeral = function () {
        $(this).css("ime-mode", "disabled");
        this.bind("keypress", function (e) {
            var code = (e.keyCode ? e.keyCode : e.which);  //兼容火狐 IE    
            if (!$.browser.msie && (e.keyCode == 0x8))  //火狐下不能使用退格键   
            {
                return;
            }
            return code >= 48 && code <= 57;
        });
        this.bind("blur", function () {
            if (this.value.lastIndexOf(".") == (this.value.length - 1)) {
                this.value = this.value.substr(0, this.value.length - 1);
            } else if (isNaN(this.value)) {
                this.value = "";
            }
        });
        this.bind("paste", function () {
            var s = clipboardData.getData('text');
            if (!/\D/.test(s));
            value = s.replace(/^0*/, '');
            return false;
        });
        this.bind("dragenter", function () {
            return false;
        });
        this.bind("keyup", function () {
            if (/(^0+)/.test(this.value)) {

                this.value = this.value.replace(/^0*/, '');
            }

        });
    };
    //生成按钮
    $.extend({
        TBbtnHtmlList: function (o1, o2, o3) {
            var tb_btn_html = "";
            var Identity_btn = "";
            for (var i in o3) {

                Identity_btn += "Identity_" + o3[i] + "=" + o1[o3[i]] + " ";
            }

            for (var i in o2) {
                tb_btn_html += "<button " + Identity_btn + " class=\"TBbtn " + o2[i].BtnAction + " " + o2[i].BtnClass + "\">" + o2[i].BtnName + " <i class=\"" + o2[i].BtnIco + "\"></i></button>";
            }
            return tb_btn_html;
        },
        TBbtnHtml: function (o1, o2, o3) {
            var Identity_btn = "";
            for (var i in o3) {

                Identity_btn += "Identity_" + o3[i] + "=" + o1[o3[i]] + " ";
            }
            return "<button " + Identity_btn + " class=\"TBbtn " + o2[i].BtnAction + "" + o2.BtnClass + "\">" + o2.BtnName + " <i class=\"" + o2.BtnIco + "\"></i></button>";
        },
        TBGetIdentityHtml: function (rowdata, IdentityObj) {
            var Identity_btn = "";
            for (var i in IdentityObj) {

                Identity_btn += "Identity_" + IdentityObj[i] + "=" + rowdata[IdentityObj[i]] + " ";
            }
            return Identity_btn;
        },
        TBfmtdate: function (val, format) {
            var date = new Date(parseInt(val.replace("/Date(", "").replace(")/", ""), 10));
            var o = {
                "AP": date.getHours() > 12 ? "下午" : "上午",//上下午标识
                "ap": date.getHours() > 12 ? "下午" : "上午",//上下午标识
                "M+": date.getMonth() + 1, //month
                "d+": date.getDate(), //day
                "H+": date.getHours(), //hour
                "h+": date.getHours() > 12 ? date.getHours() - 12 : date.getHours(), //hour
                "m+": date.getMinutes(), //minute
                "s+": date.getSeconds(), //second
                "q+": Math.floor((date.getMonth() + 3) / 3), //quarter
                "f": date.getMilliseconds() //millisecond
            }
            if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
            (date.getFullYear() + "").substr(4 - RegExp.$1.length));
            for (var k in o) if (new RegExp("(" + k + ")").test(format))
                format = format.replace(RegExp.$1,
                RegExp.$1.length == 1 ? o[k] :
                ("00" + o[k]).substr(("" + o[k]).length));
            return format;
        },
        ComputeColumnsCount: function (objcolumns) {
            var T_opsColumnCount = 0;
            for (var i = 0; i < objcolumns.length; i++) {
                for (var j = 0; j < objcolumns[i].length; j++) {
                    if (!objcolumns[i][j].ignore) {
                        T_opsColumnCount++;
                    }
                }
            }
            return T_opsColumnCount;
        }
    });


    //页面通用表格
    (function ($) {
        $.fn.TBgrid = function (options, object) {
            if (typeof (options) == "string") {

                switch (options) {
                    case "reload":
                        //该方法用来重载当前页面

                        document.getElementById("reload_" + $(this)[0].id).click();
                        break;
                    case "load":
                        //该方法是带有参数刷新页面
                        var strpara = "";
                        if (typeof (object) == "object") {
                            for (var key in object) {
                                strpara += key + "=" + object[key] + "&";
                            }
                            strpara = strpara.substr(0, strpara.length - 1)
                        }
                        else {
                            strpara = object;
                        }

                        $("#hd_para_" + $(this)[0].id).val(strpara);
                        document.getElementById("load_" + $(this)[0].id).click();
                        break;
                    case "getck":
                        //  class=\"ckchild\"
                        var This_ck_fatherId = $(this).attr("id");
                        var ckArray = new Array();
                        $("#tbody_" + This_ck_fatherId + "").find(".ckchild").each(function () {
                            var ThisTR = $(this);
                            var This_td_ck = ThisTR.find("input[type=checkbox]").is(':checked');
                            if (This_td_ck) {
                                var getval = ThisTR.parent("tr").find("td[field='" + object + "']").text();
                                ckArray.push(getval);
                            }

                        });
                        return ckArray;
                        
                        break;
                }
            }
            else {
                var dft = {
                    //以下为该插件的属性及其默认值
                    rownumbers: true,
                    rownotext: "",
                    ck: false,
                    Btn_Identity: [],
                    rows: 20,
                    pageItem: [5, 10, 20, 30, 40, 50],
                    queryParams: "",
                    totalId: "",
                    gridbtnId: "",
                    gridbtnText: "操作",
                    cookiePage: true,
                    striped: false,
                    pagination: true,
                    url: "",
                    uColor: "",
                    totalItem: false,
                    columns: "",
                    TBsuccess: function (data) { }//数据加载成功后出发此事件
                }
                if ($(".centNav li").length > 0) {

                    //清除缓存的页码
                    $.cookie('PageIndex', "1", { path: '/' });
                }

                var ops = $.extend(dft, options);
                // ops.pageItem = [5, 10, 20, 30, 40, 50];
                var queryPara = "";
                if (ops.queryParams != "") {

                    for (var key in ops.queryParams) {
                        queryPara += key + "=" + ops.queryParams[key] + "&";
                    }

                    ops.queryParams = queryPara.substr(0, queryPara.length - 1);
                }

                var ThisId = $(this)[0].id;
                if (document.getElementById("tb_Content_" + ThisId) != null) {
                    $("#tb_Content_" + ThisId).remove();
                    $("#hd_para_" + ThisId).remove();
                    $("#reload_" + ThisId).remove();
                    $("#load_" + ThisId).remove();
                    $("#PaginationContent_" + ThisId).remove();

                }

                var Table_Html = " <!--startprint--> <table id=\"tb_Content_" + ThisId + "\"  class=\"table table-striped table-bordered table-hover dataTable no-footer\"><tr id=\"head_" + ThisId + "\" class=\"tbheader\">";
                //$(ops.toolbar).remove();
                var Table_Html1 = "";
                if (ops.ck == true) {
                    if (ops.columns.length <= 1) {

                        Table_Html += "<th style=\"text-align:center;\" align=\"center\" width=\"40\"><input type=\"checkbox\" id=\"" + ThisId + "_input_ckall\"/></th>";

                    }
                    else {

                        Table_Html += "<th style=\"text-align:center;\" rowspan=\"2\" align=\"center\" width=\"40\"><input type=\"checkbox\" id=\"" + ThisId + "_input_ckall\"/></th>";

                    }
                }
                if (ops.rownumbers == true) {
                    if (ops.columns.length <= 1) {
                        if (ops.rownotext == "") {
                            Table_Html += "<th align=\"center\" width=\"40\">序号</th>";
                        } else {
                            Table_Html += "<th align=\"center\" width=\"40\">" + ops.rownotext + "</th>";
                        }
                    }
                    else {
                        if (ops.rownotext == "") {
                            Table_Html += "<th rowspan=\"" + ops.columns.length + "\" align=\"center\" width=\"40\">序号</th>";
                        } else {
                            Table_Html += "<th rowspan=\"" + ops.columns.length + "\" align=\"center\" width=\"40\">" + ops.rownotext + "</th>";
                        }
                    }
                }
                for (var i = 0; i < ops.columns.length; i++) {
                    if (i == 0) {
                        for (var j = 0; j < ops.columns[i].length; j++) {
                            if (ops.columns[i][j] != null) {
                                var width = ops.columns[i][j].width == "" || ops.columns[i][j].width == null ? "" : "width='" + ops.columns[i][j].width + "'";
                                var rowspan = ops.columns[i][j].rowspan == "" || ops.columns[i][j].rowspan == null ? "" : "rowspan=" + ops.columns[i][j].rowspan + "";
                                var colspan = ops.columns[i][j].colspan == "" || ops.columns[i][j].colspan == null ? "" : "colspan=" + ops.columns[i][j].colspan + "";
                                var th_class = ops.columns[i][j].TBclass == "" || ops.columns[i][j].TBclass == null ? "" : "class='" + ops.columns[i][j].TBclass + "'";
                                var th_hidden = "";
                                if (ops.columns[i][j].TBhidden) {
                                    th_hidden = "style=\"display:none;\"";
                                }
                                Table_Html += "<th  field=\"" + ops.columns[i][j].field + "\" " + th_hidden + " " + th_class + " " + width + " " + rowspan + " " + colspan + " align=\"center\">" + ops.columns[i][j].title + "</th>";
                            }
                        }
                    }
                    else {
                        for (var j = 0; j < ops.columns[i].length; j++) {
                            if (ops.columns[i][j] != null) {
                                var width = ops.columns[i][j].width == "" || ops.columns[i][j].width == null ? "" : "width='" + ops.columns[i][j].width + "'";
                                var rowspan = ops.columns[i][j].rowspan == "" || ops.columns[i][j].rowspan == null ? "" : "rowspan=" + ops.columns[i][j].rowspan + "";
                                var colspan = ops.columns[i][j].colspan == "" || ops.columns[i][j].colspan == null ? "" : "colspan=" + ops.columns[i][j].colspan + "";
                                var th_class = ops.columns[i][j].TBclass == "" || ops.columns[i][j].TBclass == null ? "" : "class='" + ops.columns[i][j].TBclass + "'";
                                var th_hidden = "";
                                if (ops.columns[i][j].TBhidden) {
                                    th_hidden = "style=\"display:none;\"";
                                }
                                Table_Html1 += "<th  field=\"" + ops.columns[i][j].field + "\" " + th_hidden + " " + th_class + " " + width + " " + rowspan + " " + colspan + " align=\"center\">" + ops.columns[i][j].title + "</th>";
                            }
                        }
                    }
                }


                if (Table_Html1 == "") {
                    Table_Html += "</tr><tbody id=\"tbody_" + $(this)[0].id + "\"></tbody></table><!--endprint--><input type='hidden' id=\"hd_para_" + ThisId + "\" value=\"\" />";
                }
                else {
                    Table_Html += "</tr><tr>" + Table_Html1 + "</tr><tbody id=\"tbody_" + $(this)[0].id + "\"></tbody></table><!--endprint--><input type='hidden' id=\"hd_para_" + ThisId + "\" value=\"\" />";
                }
                if (ops.pagination == true) {
                    Table_Html += "<div  class=\"page-list ng-isolate-scope\" id=\"PaginationContent_" + ThisId + "\"> <ul id=\"Pagination_" + ThisId + "\" class=\"pagination\"></ul><div style='display:none;' id=\"page-total-" + ThisId + "\" class=\"page-total\">第<input type=\"text\"  id=\"Pagination_" + ThisId + "_thisIndex\" class=\"ng-valid ng-dirty\">页 每页<select id=\"Pagination_" + ThisId + "_select\" class=\"ng-valid ng-dirty\"></select>/共<strong id=\"Pagination_" + ThisId + "_total\" class=\"ng-binding\">0</strong>条</div></div>"
                }

                Table_Html += "<input style='display:none' value=\"刷新\" type='button' id=\"reload_" + ThisId + "\"/><input style='display:none' value=\"查询\" type='button' id=\"load_" + ThisId + "\"/> ";
                $(this).append(Table_Html);
                // ops.pageItem = ops.pageItem == "" ? [5, 10, 20] : ops.pageItem;
                $("#Pagination_" + ThisId + "_select").html("");
                for (var s = 0; s < ops.pageItem.length; s++) {
                    if (ops.pageItem[s].toString() == ops.rows.toString()) {
                        $("#Pagination_" + ThisId + "_select").append("<option selected='selected' value=\"" + ops.pageItem[s] + "\">" + ops.pageItem[s] + "条</option>");
                    }
                    else {
                        $("#Pagination_" + ThisId + "_select").append("<option value=\"" + ops.pageItem[s] + "\">" + ops.pageItem[s] + "条</option>");
                    }
                }
                var Colspan = $.ComputeColumnsCount(ops.columns);


                if (ops.rownumbers) {
                    Colspan = Colspan + 1;
                }
                if (ops.ck) {
                    Colspan = Colspan + 1;
                }
                $("#tbody_" + ThisId).html("<tr><td style='padding:10px;' align=\"center\" colspan=\"" + Colspan + "\"><div style=\"width:130px;margin:0 auto;line-height: 40px;\">正在填充数据...</div></td></tr>");

                $("#Pagination_" + ThisId + "_select").unbind();
                $("#Pagination_" + ThisId + "_select").on("change", function () {
                    $("#" + ThisId).TBgrid("load", "rows=" + this.value);
                });
                var MyFunction = {
                    PageHtml: function (page) {
                        var para = $("#hd_para_" + ThisId).val() == null || $("#hd_para_" + ThisId).val() == "" ? "" : "&" + $("#hd_para_" + ThisId).val();
                        para = "page=" + page + "&rows=" + ops.rows + para;

                        var arrayPara = new Array();
                        ops.queryParams = ops.queryParams == "" ? "" : ops.queryParams;
                        ops.queryParams = ops.queryParams.toLocaleLowerCase();
                        if (para.indexOf("&") >= 0) {
                            for (var i = 0; i < para.split("&").length; i++) {
                                if (para.split("&")[i] != "") {
                                    arrayPara.push(para.split("&")[i].toLocaleLowerCase());
                                }
                            }
                        }
                        else {
                            if (para.indexOf("=") >= 0) {
                                arrayPara.push(para.toLocaleLowerCase());
                            }
                        }

                        for (var i = 0; i < arrayPara.length; i++) {
                            if (ops.queryParams.indexOf(arrayPara[i].split('=')[0]) >= 0) {
                                //替换
                                ops.queryParams = changeURLPar(ops.queryParams, arrayPara[i].split('=')[0], arrayPara[i].split('=')[1]);
                            }
                            else {
                                //追加
                                ops.queryParams = ops.queryParams + "&" + arrayPara[i];
                            }

                        }

                        var nPara = "&" + ops.queryParams;

                        nPara = nPara.replace(/&&&/g, "&").replace(/&&/g, "&").replace(/\?/g, "&");
                        //alert("page=" + page + "&rows=" + ops.rows + nPara);
                        $.AjaxSend("post", ops.url, nPara, function (data) {
                            $("#tbody_" + ThisId).html("");
                            if (ops.totalId != "") {
                                $("#" + ops.totalId).html(data.total);
                            }
                            if (data.total > 0) {

                                for (var r = 0; r < data.rows.length; r++) {
                                    var backColor = r % 2 != 0 ? "style='background-color:#ecf1f6'" : "";
                                    var Tbody_Html = "<tr " + backColor + ">";
                                    if (ops.ck == true) {
                                        var thisckId = data.rows[r].Id;
                                        Tbody_Html += "<td class=\"ckchild\" align=\"center\"><input type=\"checkbox\" id=\"" + thisckId + "_input_ck\"/></td>"
                                    }
                                    if (ops.rownumbers == true) {
                                        var thisIndex = page == 1 ? (r + 1) : (page - 1) * GetQueryString("rows", nPara) + r + 1;
                                        Tbody_Html += "<td align=\"center\">" + thisIndex + "</td>"
                                    }
                                    for (var i = 0; i < ops.columns.length; i++) {
                                        for (var j = 0; j < ops.columns[i].length; j++) {
                                            if (ops.columns[i][j] != null) {
                                                var UpperFild = ops.columns[i][j].field.toUpperCase();
                                                var tb_value = data.rows[r][ops.columns[i][j].field];

                                                if (ops.columns[i][j].TBbtn != null) {
                                                    if (typeof (ops.columns[i][j].TBbtn) == "function") {
                                                        //传入当前行的数据，列表按钮数据，按钮标识数据
                                                        tb_value = ops.columns[i][j].TBbtn(data.rows[r], data.button, ops.Btn_Identity);
                                                    }
                                                }
                                                if (ops.columns[i][j].TBfmtdate != null) {
                                                    if (typeof (ops.columns[i][j].TBfmtdate) == "function") {
                                                        tb_value = ops.columns[i][j].TBfmtdate(tb_value);
                                                    }
                                                }
                                                if (ops.columns[i][j].TBfmt != null) {
                                                    if (typeof (ops.columns[i][j].TBfmt) == "function") {
                                                        tb_value = ops.columns[i][j].TBfmt(tb_value, data.rows[r], ops.Btn_Identity);
                                                    }
                                                }
                                                else {

                                                    tb_value = tb_value;
                                                }
                                                if (!ops.columns[i][j].ignore) {
                                                    tb_value = tb_value == null ? "" : tb_value;
                                                    var th_hidden = "";
                                                    if (ops.columns[i][j].TBhidden) {
                                                        th_hidden = "style=\"display:none;\"";
                                                    }
                                                    Tbody_Html += "<td field=\"" + ops.columns[i][j].field + "\" " + th_hidden + " align=\"" + ops.columns[i][j].align + "\">" + tb_value + "</td>";
                                                }
                                            }
                                        }
                                    }

                                    Tbody_Html += "</tr>";


                                    $("#tbody_" + ThisId).append(Tbody_Html);

                                }
                                if (ops.totalItem) {

                                    $("#tbody_" + ThisId).append("<tr><td align='center'>合计</td><td align=\"center\" colspan=\"" + (Colspan - 1) + "\">" + data.totalItem + "</td></tr>");
                                }
                                $("#page-total-" + ThisId).show();
                            }
                            else {

                                $("#page-total-" + ThisId).hide();
                                $("#tbody_" + ThisId).append("<tr><td style='padding:10px;' align=\"center\" colspan=\"" + Colspan + "\">暂无数据...</td></tr>");
                            }

                            // if (para != "") {
                            MyFunction.InitPage(page, data.total, GetQueryString("rows", nPara), ThisId);
                            // }
                            if (typeof (ops.TBsuccess) == "function") {
                                ops.TBsuccess(data);
                            }
                        });
                
                    },
                    pageselectCallback: function (page_id) {

                        if (ops.cookiePage == true) {
                            $.cookie('PageIndex', parseInt(page_id) + 1, { path: '/' });
                        }
                        MyFunction.PageHtml(parseInt(page_id) + 1);
                        $("#" + ThisId + "_input_ckall").removeAttr("checked");
                    },
                    InitPage: function (PageIndex, PageCount, PageSize, loadDivId) {

                        $("#Pagination_" + loadDivId).pagination(PageCount, {
                            items_per_page: parseInt(PageSize),
                            current_page: (parseInt(PageIndex) - 1),
                            num_edge_entries: 1,
                            num_display_entries: 3,
                            pageHtmlId: loadDivId,
                            callback: this.pageselectCallback  //回调函数
                        });
                    },
                    loadPage: function (pageIndex) {
                        pageIndex = pageIndex == null || pageIndex == "" ? 1 : pageIndex;
                        var para = $("#hd_para_" + ThisId).val() == null || $("#hd_para_" + ThisId).val() == "" ? "" : "&" + $("#hd_para_" + ThisId).val();
                        para = "page=" + pageIndex + "&rows=" + ops.rows + para;

                        var arrayPara = new Array();
                        ops.queryParams = ops.queryParams == "" ? "" : ops.queryParams;
                        ops.queryParams = ops.queryParams.toLocaleLowerCase();
                        if (para.indexOf("&") >= 0) {
                            for (var i = 0; i < para.split("&").length; i++) {
                                if (para.split("&")[i] != "") {
                                    arrayPara.push(para.split("&")[i].toLocaleLowerCase());
                                }
                            }
                        }
                        else {
                            if (para.indexOf("=") >= 0) {
                                arrayPara.push(para.toLocaleLowerCase());
                            }
                        }

                        for (var i = 0; i < arrayPara.length; i++) {
                            if (ops.queryParams.indexOf(arrayPara[i].split('=')[0]) >= 0) {
                                //替换
                                ops.queryParams = changeURLPar(ops.queryParams, arrayPara[i].split('=')[0], arrayPara[i].split('=')[1]);
                            }
                            else {
                                //追加
                                ops.queryParams = ops.queryParams + "&" + arrayPara[i];
                            }

                        }

                        var nPara = "&" + ops.queryParams;

                        nPara = nPara.replace(/&&&/g, "&").replace(/&&/g, "&").replace(/\?/g, "&");
                        $.AjaxSend("post", ops.url, nPara, function (data) {
                            $("#tbody_" + ThisId).html("");
                            if (ops.totalId != "") {
                                $("#" + ops.totalId).html(data.total);
                            }
                            if (data.total > 0) {
                                var obj = ops;

                                $("#i_total_" + ThisId).html(data.total);
                                MyFunction.InitPage(1, data.total, ops.rows, ThisId);
                                for (var r = 0; r < data.rows.length; r++) {
                                    var backColor = r % 2 != 0 ? "style='background-color:#ecf1f6'" : "";
                                    var Tbody_Html = "<tr " + backColor + ">";
                                    if (ops.ck == true) {
                                        var thisckId = data.rows[r].Id;
                                        Tbody_Html += "<td class=\"ckchild\" align=\"center\"><input type=\"checkbox\" id=\"" + thisckId + "_input_ck\"/></td>"
                                    }
                                    if (ops.rownumbers == true) {
                                        Tbody_Html += "<td  align=\"center\">" + (r + 1) + "</td>";
                                    }
                                    for (var i = 0; i < ops.columns.length; i++) {
                                        for (var j = 0; j < ops.columns[i].length; j++) {
                                            if (ops.columns[i][j] != null) {
                                                var UpperFild = ops.columns[i][j].field.toUpperCase();
                                                var tb_value = data.rows[r][ops.columns[i][j].field];

                                                if (ops.columns[i][j].TBbtn != null) {
                                                    if (typeof (ops.columns[i][j].TBbtn) == "function") {
                                                        //传入当前行的数据，列表按钮数据，按钮标识数据
                                                        tb_value = ops.columns[i][j].TBbtn(data.rows[r], data.button, ops.Btn_Identity);
                                                    }
                                                }
                                                if (ops.columns[i][j].TBfmtdate != null) {
                                                    if (typeof (ops.columns[i][j].TBfmtdate) == "function") {

                                                        tb_value = ops.columns[i][j].TBfmtdate(tb_value);
                                                    }
                                                }
                                                if (ops.columns[i][j].TBfmt != null) {
                                                    if (typeof (ops.columns[i][j].TBfmt) == "function") {
                                                        tb_value = ops.columns[i][j].TBfmt(tb_value, data.rows[r], ops.Btn_Identity);
                                                    }
                                                }
                                                else {

                                                    tb_value = tb_value;
                                                }
                                                if (!ops.columns[i][j].ignore) {
                                                    tb_value = tb_value == null ? "" : tb_value;
                                                    var th_hidden = "";
                                                    if (ops.columns[i][j].TBhidden) {
                                                        th_hidden = "style=\"display:none;\"";
                                                    }
                                                    Tbody_Html += "<td  field=\"" + ops.columns[i][j].field + "\" " + th_hidden + " align=\"" + ops.columns[i][j].align + "\">" + tb_value + "</td>";
                                                }
                                            }
                                        }
                                    }

                                    Tbody_Html += "</tr>";

                                    $("#tbody_" + ThisId).append(Tbody_Html);
                                }
                                if (ops.totalItem) {

                                    $("#tbody_" + ThisId).append("<tr><td align='center'>合计</td><td align=\"center\" colspan=\"" + (Colspan - 1) + "\">" + data.totalItem + "</td></tr>");
                                }
                                $("#page-total-" + ThisId).show();
                            }
                            else {

                                $("#page-total-" + ThisId).hide();
                                $("#tbody_" + ThisId).append("<tr><td style='padding:10px;' align=\"center\" colspan=\"" + Colspan + "\">暂无数据...</td></tr>");
                            }
                            if (typeof (ops.TBsuccess) == "function") {
                                ops.TBsuccess(data);
                            }
                        });
                    
                    },
                    loadNoPage: function () {
                        $("#tbody_" + ThisId).html("");
                        var para = $("#hd_para_" + ThisId).val() == null || $("#hd_para_" + ThisId).val() == "" ? "" : "&" + $("#hd_para_" + ThisId).val();
                        var arrayPara = new Array();
                        ops.queryParams = ops.queryParams == "" ? "" : ops.queryParams;
                        ops.queryParams = ops.queryParams.toLocaleLowerCase();
                        if (para.indexOf("&") >= 0) {
                            for (var i = 0; i < para.split("&").length; i++) {
                                if (para.split("&")[i] != "") {
                                    arrayPara.push(para.split("&")[i].toLocaleLowerCase());
                                }
                            }
                        }
                        else {
                            if (para.indexOf("=") >= 0) {
                                arrayPara.push(para.toLocaleLowerCase());
                            }
                        }

                        for (var i = 0; i < arrayPara.length; i++) {
                            if (ops.queryParams.indexOf(arrayPara[i].split('=')[0]) >= 0) {
                                //替换
                                ops.queryParams = changeURLPar(ops.queryParams, arrayPara[i].split('=')[0], arrayPara[i].split('=')[1]);
                            }
                            else {
                                //追加
                                ops.queryParams = ops.queryParams + "&" + arrayPara[i];
                            }

                        }

                        var nPara = "&" + ops.queryParams;

                        nPara = nPara.replace(/&&&/g, "&").replace(/&&/g, "&").replace(/\?/g, "&");
                        $.AjaxSend("post", ops.url, nPara, function (data) {
                            if (ops.totalId != "") {
                                $("#" + ops.totalId).html(data.total);
                            }
                            $("#tbody_" + ThisId).html("");
                            // alert(data.rows[0].ProjectName);
                            //$("#i_total_" + ThisId).html(data.total);
                            if (data.total > 0) {

                                for (var r = 0; r < data.rows.length; r++) {
                                    var backColor = r % 2 != 0 ? "style='background-color:#ecf1f6'" : "";
                                    var Tbody_Html = "<tr " + backColor + ">";
                                    if (ops.ck == true) {
                                        var thisckId = data.rows[r].Id;
                                        Tbody_Html += "<td class=\"ckchild\" align=\"center\"><input type=\"checkbox\" id=\"" + thisckId + "_input_ck\"/></td>"
                                    }
                                    if (ops.rownumbers == true) {
                                        Tbody_Html += "<td align=\"center\">" + (r + 1) + "</td>"
                                    }
                                    for (var i = 0; i < ops.columns.length; i++) {
                                        for (var j = 0; j < ops.columns[i].length; j++) {
                                            if (ops.columns[i][j] != null) {
                                                var UpperFild = ops.columns[i][j].field.toUpperCase();
                                                var tb_value = data.rows[r][ops.columns[i][j].field];

                                                if (ops.columns[i][j].TBbtn != null) {
                                                    if (typeof (ops.columns[i][j].TBbtn) == "function") {
                                                        //传入当前行的数据，列表按钮数据，按钮标识数据
                                                        tb_value = ops.columns[i][j].TBbtn(data.rows[r], data.button, ops.Btn_Identity);
                                                    }
                                                }
                                                if (ops.columns[i][j].TBfmtdate != null) {
                                                    if (typeof (ops.columns[i][j].TBfmtdate) == "function") {

                                                        tb_value = ops.columns[i][j].TBfmtdate(tb_value);
                                                    }
                                                }
                                                if (ops.columns[i][j].TBfmt != null) {
                                                    if (typeof (ops.columns[i][j].TBfmt) == "function") {
                                                        tb_value = ops.columns[i][j].TBfmt(tb_value, data.rows[r], ops.Btn_Identity);
                                                    }
                                                }
                                                else {

                                                    tb_value = tb_value;
                                                }
                                                if (!ops.columns[i][j].ignore) {
                                                    tb_value = tb_value == null ? "" : tb_value;
                                                    var th_hidden = "";
                                                    if (ops.columns[i][j].TBhidden) {
                                                        th_hidden = "style=\"display:none;\"";
                                                    }
                                                    Tbody_Html += "<td  field=\"" + ops.columns[i][j].field + "\" " + th_hidden + " align=\"" + ops.columns[i][j].align + "\">" + tb_value + "</td>";
                                                }
                                            }
                                        }
                                    }

                                    Tbody_Html += "</tr>";
                                    $("#tbody_" + ThisId).append(Tbody_Html);
                                }
                                if (ops.totalItem) {

                                    $("#tbody_" + ThisId).append("<tr><td align='center'>合计</td><td align=\"center\" colspan=\"" + (Colspan - 1) + "\">" + data.totalItem + "</td></tr>");
                                }
                            }
                            else {


                                $("#tbody_" + ThisId).append("<tr><td style='padding:10px;' align=\"center\" colspan=\"" + Colspan + "\">暂无数据...</td></tr>");
                            }

                            if (typeof (ops.TBsuccess) == "function") {
                                ops.TBsuccess(data);
                            }
                        });
                      
                    }
               
                }
                if (ops.pagination == true) {
                    if (ops.cookiePage == true) {

                        var ThisIndex = $.cookie('PageIndex');
                        ThisIndex = ThisIndex == null || ThisIndex == "" ? 1 : ThisIndex;
                        MyFunction.PageHtml(ThisIndex);
                    }
                    else {
                        MyFunction.loadPage(1);
                    }


                    $("#reload_" + ThisId).on("click", function () {
                        var thisPage = $("#Pagination_" + ThisId).find("li");
                        var thisPageNum = 1;
                        for (var i = 0; i < thisPage.length; i++) {

                            if (thisPage[i].className.indexOf("active") >= 0) {
                                if (window.navigator.userAgent.toLowerCase().indexOf("firefox") != -1) {
                                    thisPageNum = thisPage[i].innerHTML;
                                }
                                else {
                                    thisPageNum = thisPage[i].innerText;
                                }
                            }
                        }

                        MyFunction.PageHtml(thisPageNum);
                        if ($("#i_total_" + ThisId).text() == "0") {
                            MyFunction.InitPage(thisPageNum, 1, ops.rows, ThisId);
                        } else {
                            MyFunction.InitPage(thisPageNum, $("#i_total_" + ThisId).text(), ops.rows, ThisId);
                        }

                        $("#" + ThisId + "_input_ckall").removeAttr("checked");
                        //MyFunction.PageHtml(2);MyFunction.InitPage(1, data.total, ops.rows, ThisId);
                    });
                    $("#load_" + ThisId).on("click", function () {

                        MyFunction.PageHtml(1);
                        $("#" + ThisId + "_input_ckall").removeAttr("checked");
                        //MyFunction.PageHtml(2);MyFunction.InitPage(1, data.total, ops.rows, ThisId);
                    });

                }
                else {
                    MyFunction.loadNoPage();

                    $("#reload_" + ThisId).on("click", function () {
                        MyFunction.loadNoPage();
                        $("#" + ThisId + "_input_ckall").removeAttr("checked");
                    });
                    $("#load_" + ThisId).on("click", function () {

                        MyFunction.loadNoPage();
                        $("#" + ThisId + "_input_ckall").removeAttr("checked");
                    });
                }
                $("#" + ThisId + "_input_ckall").unbind("click");
                $("#" + ThisId + "_input_ckall").on("click", function () {
                    var IsCheck = $("#" + ThisId + "_input_ckall").is(':checked');
                    if (IsCheck) {
                        $("#tbody_" + ThisId + "").find(".ckchild").each(function () {
                            var ThisTR = $(this);
                            ThisTR.find("input[type=checkbox]").prop('checked', true);

                        });
                    }
                    else {
                        $("#tbody_" + ThisId + "").find(".ckchild").each(function () {
                            var ThisTR = $(this);
                            ThisTR.find("input[type=checkbox]").prop('checked', false);

                        });
                    }
                });
                TableSort_ASC(ThisId);
                TableSort_DESC(ThisId);
                TableSort_NONE(ThisId);



            }



        }

    })(jQuery);
    //页面表格分页
    jQuery.fn.pagination = function (maxentries, opts) {
        opts = jQuery.extend({
            items_per_page: 1,
            pageHtmlId: "",
            num_display_entries: 10,
            current_page: 0,
            num_edge_entries: 0,
            link_to: "JavaScript:",
            prev_text: "上一页",
            next_text: "下一页",
            ellipse_text: "...",
            prev_show_always: true,
            next_show_always: true,
            callback: function () { return false; }
        }, opts || {});
        $("#Pagination_" + opts.pageHtmlId + "_total").html(maxentries);

        return this.each(function () {
            /**
            * Calculate the maximum number of pages
            */
            function numPages() {
                return Math.ceil(maxentries / opts.items_per_page);
            }
            $("#Pagination_" + opts.pageHtmlId + "_thisIndex").unbind();
            $("#Pagination_" + opts.pageHtmlId + "_thisIndex").numeral();
            $("#Pagination_" + opts.pageHtmlId + "_thisIndex").on("keyup", function () {
                if (parseInt(this.value) > parseInt(numPages())) {
                    pageSelected(numPages() - 1);
                }
                else if (this.value == 0) {
                    pageSelected(0);
                }
                else {
                    pageSelected(this.value - 1);
                }
            })
            /**
            * Calculate start and end point of pagination links depending on 
            * current_page and num_display_entries.
            * @return {Array}
            */
            function getInterval() {
                var ne_half = Math.ceil(opts.num_display_entries / 2);
                var np = numPages();
                var upper_limit = np - opts.num_display_entries;
                var start = current_page > ne_half ? Math.max(Math.min(current_page - ne_half, upper_limit), 0) : 0;
                var end = current_page > ne_half ? Math.min(current_page + ne_half, np) : Math.min(opts.num_display_entries, np);
                return [start, end];
            }

            /**
            * This is the event handling function for the pagination links. 
            * @param {int} page_id The new page number
            */
            function pageSelected(page_id, evt) {
                current_page = page_id;
                drawLinks();
                var continuePropagation = opts.callback(page_id, panel);
                if (!continuePropagation) {
                    if (evt != null) {
                        if (evt.stopPropagation) {
                            evt.stopPropagation();
                        }
                        else {
                            evt.cancelBubble = true;
                        }
                    }
                }
                return continuePropagation;
            }

            /**
            * This function inserts the pagination links into the container element
            */
            function drawLinks() {
                panel.empty();
                var interval = getInterval();
                var np = numPages();
                // This helper function returns a handler function that calls pageSelected with the right page_id
                var getClickHandler = function (page_id) {
                    return function (evt) { return pageSelected(page_id, evt); }
                }
                // Helper function for generating a single link (or a span tag if it'S the current page)
                var appendItem = function (page_id, appendopts) {
                    page_id = page_id < 0 ? 0 : (page_id < np ? page_id : np - 1); // Normalize page id to sane value
                    appendopts = jQuery.extend({ text: page_id + 1, classes: "" }, appendopts || {});
                    if (page_id == current_page) {
                        var lnk = $("<li class='ng-scope active'><span >" + (appendopts.text) + "</span></li>");
                    }
                    else {
                        var lnk = $("<li><span >" + (appendopts.text) + "</span></li>")
						.bind("click", getClickHandler(page_id))
						.attr('class', "ng-scope");
                        //  alert(opts.pageHtmlId);

                    }
                    if (appendopts.classes) { lnk.addClass(appendopts.classes); }
                    if (appendopts.classrl) { lnk.attr("class", appendopts.classrl); }
                    panel.append(lnk);
                }
                // Generate "Previous"-Link
                if (opts.prev_text && (current_page > 0 || opts.prev_show_always)) {
                    if (current_page == 0) {

                        appendItem(current_page - 1, { text: opts.prev_text, classrl: "disabled" });
                    }
                    else {
                        appendItem(current_page - 1, { text: opts.prev_text, classrl: "ng-scope" });
                    }
                    $("#Pagination_" + opts.pageHtmlId + "_thisIndex").val(current_page + 1);
                }
                // Generate starting points
                if (interval[0] > 0 && opts.num_edge_entries > 0) {
                    var end = Math.min(opts.num_edge_entries, interval[0]);
                    for (var i = 0; i < end; i++) {
                        appendItem(i);
                    }
                    if (opts.num_edge_entries < interval[0] && opts.ellipse_text) {
                        jQuery("<li><span>" + opts.ellipse_text + "</span></li>").appendTo(panel);
                    }
                }
                // Generate interval links
                for (var i = interval[0]; i < interval[1]; i++) {
                    appendItem(i);
                }
                // Generate ending points
                if (interval[1] < np && opts.num_edge_entries > 0) {
                    if (np - opts.num_edge_entries > interval[1] && opts.ellipse_text) {
                        jQuery("<li><span>" + opts.ellipse_text + "</span></li>").appendTo(panel);
                    }
                    var begin = Math.max(np - opts.num_edge_entries, interval[1]);
                    for (var i = begin; i < np; i++) {
                        appendItem(i);
                    }

                }
                // Generate "Next"-Link
                if (opts.next_text && (current_page < np - 1 || opts.next_show_always)) {

                    if ((current_page + 1) == np) {
                        appendItem(current_page + 1, { text: opts.next_text, classrl: "disabled" });
                    }
                    else {
                        appendItem(current_page + 1, { text: opts.next_text, classrl: "ng-scope" });
                    }
                }
            }

            // Extract current_page from options
            var current_page = opts.current_page;
            // Create a sane value for maxentries and items_per_page
            maxentries = (!maxentries || maxentries < 0) ? 1 : maxentries;
            opts.items_per_page = (!opts.items_per_page || opts.items_per_page < 0) ? 1 : opts.items_per_page;
            // Store DOM element for easy access from all inner functions
            var panel = jQuery(this);
            // Attach control functions to the DOM element 
            this.selectPage = function (page_id) { pageSelected(page_id); }
            this.prevPage = function () {
                if (current_page > 0) {
                    pageSelected(current_page - 1);
                    return true;
                }
                else {
                    return false;
                }
            }
            this.nextPage = function () {
                if (current_page < numPages() - 1) {
                    pageSelected(current_page + 1);
                    return true;
                }
                else {
                    return false;
                }
            }
            // When all initialisation is done, draw the links
            drawLinks();
        });

    }
    //替换url中的参数
    function changeURLPar(destiny, paramName, replaceWith) {
        var oUrl = destiny;
        var re = eval('/(' + paramName + '=)([^&]*)/gi');
        var nUrl = oUrl.replace(re, paramName + '=' + replaceWith);
        return nUrl;
    }
    //得到参数
    function GetQueryString(name, paraStr) {
        var valus = paraStr.split("&");
        var value = "";
        if (paraStr.indexOf(name) >= 0) {
            for (var i = 0; i < valus.length; i++) {
                if (valus[i].indexOf(name) >= 0) {
                    value = valus[i].split('=')[1];
                }
            }
        }
        return value;
    }

    function TableSort_ASC(ThisId) {
        $("#" + ThisId + " .dataTable  .sorting").unbind("click");
        $("#" + ThisId + " .dataTable  .sorting").on("click", function () {
            var $ThisObj = $(this);
            var field = $ThisObj.attr("field");
            $("#" + ThisId).TBgrid("load", "ordering=" + field);
            $("#" + ThisId + " .dataTable  .sorting_asc").each(function () {
                var ThisObj2 = $(this);
                ThisObj2.removeClass("sorting_asc").addClass("sorting");
            });
            $("#" + ThisId + " .dataTable  .sorting_desc").each(function () {
                var ThisObj3 = $(this);
                ThisObj3.removeClass("sorting_desc").addClass("sorting");
            });
            $ThisObj.removeClass("sorting").addClass("sorting_asc");
            TableSort_DESC(ThisId);
        });
    }
    function TableSort_DESC(ThisId) {

        $("#" + ThisId + " .dataTable  .sorting_asc").unbind("click");
        $("#" + ThisId + " .dataTable  .sorting_asc").on("click", function () {
            var $ThisObj = $(this);
            var field = $ThisObj.attr("field");
            $("#" + ThisId).TBgrid("load", "ordering=" + field + " desc");
            $("#" + ThisId + " .dataTable  .sorting_asc").each(function () {
                var ThisObj2 = $(this);
                ThisObj2.removeClass("sorting_asc").addClass("sorting");
            });
            $("#" + ThisId + " .dataTable  .sorting_desc").each(function () {
                var ThisObj3 = $(this);
                ThisObj3.removeClass("sorting_desc").addClass("sorting");
            });
            $ThisObj.removeClass("sorting").addClass("sorting_desc");
            TableSort_NONE(ThisId);
        });
    }
    function TableSort_NONE(ThisId) {

        $("#" + ThisId + " .dataTable  .sorting_desc").unbind("click");
        $("#" + ThisId + " .dataTable  .sorting_desc").on("click", function () {
            var $ThisObj = $(this);
            var field = $ThisObj.attr("field");
            $("#" + ThisId).TBgrid("load", "ordering=");
            $("#" + ThisId + " .dataTable  .sorting_asc").each(function () {
                var ThisObj2 = $(this);
                ThisObj2.removeClass("sorting_asc").addClass("sorting");
            });
            $("#" + ThisId + " .dataTable  .sorting_desc").each(function () {
                var ThisObj3 = $(this);
                ThisObj3.removeClass("sorting_desc").addClass("sorting");
            });
            $ThisObj.removeClass("sorting_desc").addClass("sorting");
            TableSort_ASC(ThisId);
        });
    }
});
