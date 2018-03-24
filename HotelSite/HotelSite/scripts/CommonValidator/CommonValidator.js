
$('[NumberOnly]').keypress(function (e) {

    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        //display error message
        e.preventDefault();
    }
});

$('[AlphaNumeric]').keypress(function (e) {

    var regex = new RegExp("^[a-zA-Z0-9]+$");
    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (regex.test(str)) {
        return true;
    }
    e.preventDefault();
    ////if (e.shiftKey == true) {
    ////    return false;
    ////}
    //let keyCode = e.keyCode ? e.keyCode : e.which;
    //let pressedKey = String.fromCharCode(keyCode);
    //if (/[^A-Za-z0-9\b ]/.test(pressedKey)) {
    //    return false;
    //}
    //else {
    //    return true;
    //}

});
$('[SpecialCharacter]').keypress(function (e) {

    var regex = /^[a-zA-Z0-9!\@,\&*\)\(+= ._-]+/g
    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (regex.test(str)) {
        return true;
    }
    e.preventDefault();
});
$('.Alpha').keypress(function (event) {
    var inputValue = event.which;
    // allow letters and whitespaces only.
    if (!(inputValue >= 65 && inputValue <= 120) && (inputValue != 32 && inputValue != 0)) {
        event.preventDefault();
    }
});