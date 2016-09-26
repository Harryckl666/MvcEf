; (function ($, window, document, undefined) {
    var abc = function (ele, opt) {
        this.$element = ele;
        this.defaults = {};
        this.options = $.extend({}, this.defaults, opt)
    }
    abc.prototype = {
        a: function () {
            alert('a');
        },
        b: function () { },
        c: function () { }
    }

    $.fn.myPlugin = function (options) {
        var abc1 = new abc(this, options);
        return abc1.a();
    }
})(jQuery, window, document)