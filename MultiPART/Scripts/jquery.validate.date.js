$(function () {
    $.validator.methods.date = function (value, element) {
        var dateParts = value.split("/");
        var dateStr = dateParts[2] + "-" + dateParts[1] + "-" + dateParts[0];
        return this.optional(element) || !/Invalid|NaN/.test(new Date(dateStr));
    };
});

//$(function () {
//    $.validator.methods.date = function (value, element) {


//        //ES - Chrome does not use the locale when new Date objects instantiated:
//        var d = new Date();

//        return this.optional(element) || !/Invalid|NaN/.test(new Date(d.toLocaleDateString(value)));

//    };
//});