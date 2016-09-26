
//选择项必须小于N项
$.validator.addMethod('cleckminlength', function (value, element, params) {
    var length = $.isArray(value) ? value.length : this.getLength($.trim(value), element);
    return this.optional(element) || length >= params.value;
});

$.validator.unobtrusive.adapters.add("cleckminlength", ['value'], function (options) {
    options.rules["cleckminlength"] = {
        value: options.params.value
    };
    options.messages["cleckminlength"] = options.message;
});

//选择项必须大于N项
$.validator.addMethod('cleckmaxlength', function (value, element, params) {
    var length = $.isArray(value) ? value.length : this.getLength($.trim(value), element);
    return this.optional(element) || length <= params.value;
});

$.validator.unobtrusive.adapters.add("cleckmaxlength", ['value'], function (options) {
    options.rules["cleckmaxlength"] = {
        value: options.params.value
    };
    options.messages["cleckmaxlength"] = options.message;
});


//bootStrap风格验证样式
$.validator.setDefaults({
    focusInvalid: true,
    unhighlight: function (element, errorClass, validClass) {
        $(element).closest('.form-group').removeClass('has-error').addClass('has-info');
    },
    highlight: function (element, errorClass, validClass) {
        $(element).closest('.form-group').removeClass('has-info').addClass('has-error');
        $($.validator.unobtrusive).bind('onError', function (data, error) {
            $(error).addClass('help-block');
            
            var container = $("[data-valmsg-summary=true]");
            var cssClass = "alert alert-danger";
            $.each(container.find("ul li"), function (i) {
                if ($(this).is(":visible")) {
                    if (!container.hasClass(cssClass))
                        container.addClass(cssClass).show();
                   
                    return false;
                }
            });
        });
    }
});


//光标验证
$.validator.setDefaults({
    //光标移出时
    onfocusout: function (element) {
        this.element(element);
    },
    //光标移入时
    onfocusin: function (element, event) {
        //找到显示错误提示的标签并移除,针对jquery.validate.unobtrusive
        var errorElement = $(element).next('span.field-validation-error');
        if (errorElement) {
            errorElement.children().remove();
        }
    }
});
