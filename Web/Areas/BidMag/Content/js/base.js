var base = (function () {
    function webClick() {
        tagFun($(".centNav li"), $(".tagCom"), "click");

        function setTabWidth(_thise) {
           
          
            $(".tagCom.on .tuoDoTab").each(function () {
              
                var _table = $(this);
                var tabW = 0;
                var pWW = $(window.parent.document).width();
                var leftW = $(".leftCom", window.parent.document).width();
                var prW = pWW - leftW - 15;

                _table.find("th").not(".addTh").each(function() {
                    var _this = $(this);
                    var _thisW = _this.attr("width");

                    var _index = _this.index();
                    var _pTab = _this.parents("table");
                    //_this.html("<span class='noRows' style='width:" + _thisW + "px;'>" + _this.html() + "</span>");
                    _this.css("width", _thisW);

                    tabW += parseInt(_thisW);
                    _pTab.find("td").each(function() {
                        if ($(this).index() == _index) {
                            $(this).css("width", _thisW);
                            $(this).find(".noRows").css("width", _thisW);
                        }
                    });
                });
              
                if (prW < tabW) {
                    _table.parents(".centerBox").css("width", tabW);
                    _table.find(".addTh").remove();
                }
                else {
                   
                    _table.parents(".centerBox").css("width", prW);
                    if (_table.find(".addTh").length < 1) {
                        _table.find("th").last().after("<th class='addTh' style='width:" + parseInt(prW - tabW) + "px;'></th>");
                    }

                    else {
                       
                        _table.find(".addTh").css("width", parseInt(prW - tabW));
                        
                    }
                }

                TableMove($(".tagCom.on .table-bordered"));
            });
        }
        function tagFun(nav, tagCom, eleType) {

            nav.on(eleType, function() {
                var _ind = $(this).index();
                nav.removeClass("on");
                tagCom.removeClass("on");
                $(this).addClass("on");
                tagCom.eq(_ind).addClass("on");
                setTabWidth($(this));
                if (typeof (clickTab) == "function") {
                    clickTab(_ind);
                }
                
                // return false;
            });
            $(".centerBox .temTit").parents(".centerBox").css("padding-top", 0);
            var wW = 0;


            //$(".centSeach .seachItem").each(function () {
                
            //   wW+=$(this).width();
            //})
            //$(".centSeach").css("width", wW);
            
            //$(".centSeach")

            //$("table tr td").each(function () {

            //    if ($(this).attr("rowspan") > 1) {
            //        $(this).parents("tr").mouseover(function () {
            //            $(this).css("background-color", "#fff");
            //        })
            //    }
            //})
            //ËÑË÷¿òÌáÊ¾
            $(".seachText,.ltext").each(function () {
                var pVal = $(this).attr("placeholder");
                $(this).focus(function () {
                    $(this).attr("placeholder", "");
                }).blur(function () {
                    var thisVal = $(this).val();
                    if (thisVal != "") {
                        $(this).attr("placeholder", "");
                    }
                    else {
                        $(this).attr("placeholder", pVal);
                    }
                });
            });

        }

    }

    function rightNav() {
        var cW = $(".centerCom").width();
        var cH = $(".centerCom").height();
        var bH = $(".centerCom .bidLReg").height();
        var rW = $(".centerCom .prjRight").width();
        var rH = $(".centerCom .prjRight").height();
        //$(".centerCom .bdC").css("height", cH - 30);
        $(".centerCom .bdAdvBtn").css("padding-top", ((cH - 60) / 2));
        $(".centerCom .bidLReg").css("width", cW - rW - 8);
        //$(".projectCom .prjRight").css("min-height", cH);
        
    }
    return {
        webClickn: function () { webClick(); },
        rightNavn: function () { rightNav(); }
    }
})();

$(function () {
    var dH = $(document).height();
    $(".projectCom .prjRight").css("min-height", dH);
    base.webClickn();
    base.rightNavn();
    window.onload = function () {
        var dH = $(document).height();
        var wH = $(window).height();
        $(".centerCom .bdC").css("min-height", wH - 72);
        $(".projectCom .prjRight").css("min-height", dH);
    }
   
    

    $(window).resize(function () {
        base.rightNavn();
    });
    //setTimeout(base.rightNavn, 2000);
})