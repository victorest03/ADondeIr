try {
    $.validator.methods.date = function (value, element) {
        var val = moment(value).format('DD-MM-YYYY');
        console.log(val);
        return this.optional(element) || (val);
    };

    
} catch (e) {

}
try {
    $.fn.datepicker.defaults.language = "es";
    $.fn.datepicker.defaults.format = "dd/mm/yyyy";
    $.fn.datepicker.defaults.autoclose = true;
} catch (e) {

}
