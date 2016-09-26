$.extend({
    //111
    a1: function () {
        alert('a1');
    },
    //1221
    a2: function () { },
    min: function (aa, bb) { return aa < bb ? aa : bb; },
    max: function (aa, bb) { return aa > bb ? aa : bb; }
});
$.fn.extend({
    b1: function () { alert('b1'); },
    b2: function () { },
    check: function () {
        return this.each(function () { this.checked = true; });
    },
    uncheck: function () {
        return this.each(function () { this.checked = false; });
    }
});
(function ($, window, document, undefined) {
    var abc = function (ele, opt) {
        this.$element = ele;
        this.defaults = {};
        this.options = $.extend({}, this.defaults, opt)
    };
    abc.prototype = {
        a: function () {
            alert('a');
        },
        b: function () {
            alert('ziji');
        },
        c: function () { }
    };
    $.fn.myPlugin = function (options) {
        var abc1 = new abc(this, options);
        return abc1.a();
    },
     $.fn.mvcself = function (options) {
         new abc(this, options).b();
     };
})(jQuery, window, document);


