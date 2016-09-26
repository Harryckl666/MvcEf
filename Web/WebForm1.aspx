<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Web.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Content/js/jquery-1.8.2.min.js"></script>
    <%--<script src="Content/js/JavaScript1.js"></script>--%>
    <script src="Content/js/JavaScript2.js"></script>
    <script type="text/javascript">
        // 获取页面的高度、宽度
        function getPageSize() {
            var xScroll, yScroll;
            if (window.innerHeight && window.scrollMaxY) {
                xScroll = window.innerWidth + window.scrollMaxX;
                yScroll = window.innerHeight + window.scrollMaxY;
            } else {
                if (document.body.scrollHeight > document.body.offsetHeight) { // all but Explorer Mac    
                    xScroll = document.body.scrollWidth;
                    yScroll = document.body.scrollHeight;
                } else { // Explorer Mac...would also work in Explorer 6 Strict, Mozilla and Safari    
                    xScroll = document.body.offsetWidth;
                    yScroll = document.body.offsetHeight;
                }
            }
            var windowWidth, windowHeight;
            if (self.innerHeight) { // all except Explorer    
                if (document.documentElement.clientWidth) {
                    windowWidth = document.documentElement.clientWidth;
                } else {
                    windowWidth = self.innerWidth;
                }
                windowHeight = self.innerHeight;
            } else {
                if (document.documentElement && document.documentElement.clientHeight) { // Explorer 6 Strict Mode    
                    windowWidth = document.documentElement.clientWidth;
                    windowHeight = document.documentElement.clientHeight;
                } else {
                    if (document.body) { // other Explorers    
                        windowWidth = document.body.clientWidth;
                        windowHeight = document.body.clientHeight;
                    }
                }
            }
            // for small pages with total height less then height of the viewport    
            if (yScroll < windowHeight) {
                pageHeight = windowHeight;
            } else {
                pageHeight = yScroll;
            }
            // for small pages with total width less then width of the viewport    
            if (xScroll < windowWidth) {
                pageWidth = xScroll;
            } else {
                pageWidth = windowWidth;
            }
            arrayPageSize = new Array(pageWidth, pageHeight, windowWidth, windowHeight);
            return arrayPageSize;
        }



        $(function () {
            // 滚动条
            document.body.scrollTop;
            $(document).scrollTop();
            alert($(window).height()); //浏览器当前窗口可视区域高度 
            alert($(document).height()); //浏览器当前窗口文档的高度 
            alert($(document.body).height());//浏览器当前窗口文档body的高度 
            //alert($(document.body).outerHeight(true));//浏览器当前窗口文档body的总高度 包括border padding margin 
            //alert($(window).width()); //浏览器当前窗口可视区域宽度 
            //alert($(document).width());//浏览器当前窗口文档对象宽度 
            //alert($(document.body).width());//浏览器当前窗口文档body的高度 
            //alert($(document.body).outerWidth(true));//浏览器当前窗口文档body的总宽度 包括border padding margin 

            //$("abcd1").myPlugin();
            $("abcd2").mvcself();
            //$.a1();
            //$("abcd3").b1();
            console.log($.min(3, 5));
            console.log($.max(3, 2));
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="abcd1">
        </div>
        <div id="abcd2">
        </div>
        <div id="abcd3">
        </div>
    </form>
</body>
</html>
