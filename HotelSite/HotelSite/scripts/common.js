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

function CheckEmail()
{
    var regrex = new RegExp("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
    var a = regrex.test()
}
