
function showModal(modalNme, effectClass, removeClass) {
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

function CheckEmailValidation(Email) {
    var regrex = new RegExp("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
    return regrex.test(Email);
}

function CheckPasswordComplexity() {
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

function getJsonData(path) {
    var resData = new Object();
    $.ajax(
        {
            url: path,
            type: 'get',
            dataType: 'json',
            async: false,
            success: function (response) {
                resData = response;
            },
            error: function (xhr, status, error) {

            }
        })
    return resData;
}
function BindCountryDropDown(ddlID) {
    try {
        var Countires = getJsonData('../../Location Data/countries.json');
        $.each(Countires.countries, function (i, item) {
            var option = document.createElement("option");
            option.text = item.name;
            option.value = item.id;
            var select = document.getElementById(ddlID);
            if (option != null) select.appendChild(option);
        });


    }
    catch (e) {

    }
}
function BindStateDropDown(ddlID, CountyID) {

    var states = getJsonData('../../Location Data/states.json');
    let cID = (CountyID == "" || CountyID == null) ? "101" : CountyID;
    let StateBindData = $.grep(states.states, function (n, i) {
        return n.country_id == cID;
    });
    $('#' + ddlID).empty();
    var select = document.getElementById(ddlID);
    $.each(StateBindData, function (i, item) {
        var option = document.createElement("option");
        option.text = item.name;
        option.value = item.id;
        if (option != null) {
            select.appendChild(option);
        }
    });
    $('#' + ddlID).selectpicker('refresh');
}
function BindCityDropDown(ddlID, StateID) {
    var citiess = getJsonData('../../Location Data/cities.json');
    let SID = (StateID == "" || StateID == null) ? "10" : StateID;

    let cityBindData = $.grep(citiess.cities, function (n, i) {
        return n.state_id == SID;
    });
    $('#' + ddlID).empty();
    var select = document.getElementById(ddlID);
    $.each(cityBindData, function (i, item) {
        var option = document.createElement("option");
        option.text = item.name;
        option.value = item.id;

        select.appendChild(option);
    });
    $('#' + ddlID).selectpicker('refresh');
}
function checkMail(controlClass, module) {

    let Email = $('.' + controlClass).val();
    var obj = {
        email: Email,
        isAgent: module
    }
    var result;
    $.ajax({
        type: "POST",
        url: '/User/GetEmail',
        data: JSON.stringify(obj),
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            result = response.res
        },
        error: function () {

        }
    });
    return result;
}
function toTitleCase(str) {
    str = str.toLowerCase().replace(/\b[a-z]/g, function (letter) {
        return letter.toUpperCase();
    });
    return str;
}