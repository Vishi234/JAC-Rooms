
$('[NumberOnly]').keydown(function (e) {
    if (e.shiftKey || e.ctrlKey || e.altKey) {
        e.preventDefault();
    }
    else {
        var key = e.keyCode;
        if (!((key == 8) || (key == 9) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
            e.preventDefault();
        }
    }
});

$('[AlphaNumeric]').keydown(function (e) {

    let keyCode = e.keyCode ? e.keyCode : e.which;
    let pressedKey = String.fromCharCode(keyCode);
    if (/[^A-Za-z0-9\t\b ]/.test(pressedKey)) {
        return false;
    }
    else {
        return true;
    }

});