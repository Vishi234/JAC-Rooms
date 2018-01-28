function showModal(modalName, effectClass, removeClass) {
    $(".modal-bg").css('display', 'block');
    $(".modal-bg").removeClass(removeClass);
    $("." + modalName).removeClass(removeClass);
    $("." + modalName).addClass(effectClass);

}
function hideModal(modalName, effectClass, removeClass) {
    $("." + modalName).removeClass(removeClass);
    $("." + modalName).addClass(effectClass);
    $(".modal-bg").addClass(effectClass);
    setTimeout(function () { $(".modal-bg").css('display', 'none'); }, 1000);
}

function CheckEmail(Email)
{
    var regrex = new RegExp("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
    return regrex.test(Email);
}

function CheckPasswordComplexity()
{
    var regex = new RegExp("^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");
}
function myFunction(id) {
    var x = document.getElementById(id);
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}
function calltoast(text) {
    $.toast({
        text: text,
        hideAfter: 3000,
        textAlign: 'left',
        bgColor: '#000000',
        position: 'bottom-right',
    })
}
