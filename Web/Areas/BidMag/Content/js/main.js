var main = (function () {
    function windFun() {
        var widH = $(window).height(),
            headH = $("#header").height(),
            footH = $("#footer").height(),
            mainH = widH - headH - footH,
            widW = $(window).width(),
            leftW = $(".leftCom").width(),
            mainW = widW - leftW - 10;
        $(".leftCom").css("height", mainH);
        $(".rightCom").css("height", mainH - 30);
        $(".rightCom").css({ "width": mainW, "left": leftW + 5 });
       
        if (widH < 720) {
            $("#center .leftCom .ulItem .liItem").addClass("mH");
        }
        else {
           
            $("#center .leftCom .ulItem .liItem").removeClass("mH");
        }
    }

    function mainClick() {
        $(".treeItem").each(function () {
            $(this).parent().find(".aItem").addClass("onVi");
        });
        $(".liItemMore .aItem").toggle(function() {
            $(this).siblings(".treeItem").slideDown();
            $(this).parent().addClass("on");
            $(this).parent().siblings().find(".treeItem").slideUp();
            $(this).parent().siblings().removeClass("on");

        }, function() {
            $(this).siblings(".treeItem").slideUp();
            $(this).parent().removeClass("on");
        });
      
        //菜单、标签切换
        srcFun($(".ulItem .liItem .aItem"), "click");
        srcFun($(".treeItem li a"), "click");
        function srcFun(nav, eleType) {
            nav.on(eleType, function () {
                var pLi = $(this).parent();
                var src = $(this).attr("target");
                pLi.siblings().removeClass("on");
                pLi.addClass("on");
                $("#contentIframe").attr("src", src);

            });
        }

    }

    return {
        windFunm: function () { windFun(); },
        mainClickm: function () { mainClick(); }
    }
})();

$(function () {
    main.windFunm();
    main.mainClickm();
    $(window).resize(function ()
    {
        main.windFunm();
    });
    
  
})