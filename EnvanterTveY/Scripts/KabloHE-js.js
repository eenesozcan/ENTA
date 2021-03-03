
function onlyDotsAndNumbers(event) { //virgül ve sayılar
    var charCode = (event.which) ? event.which : event.keyCode
    if (charCode == 44) {
        return true;
    }
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function onlyNumberAndDot(event) { //nokta ve sayılar
    var charCode = (event.which) ? event.which : event.keyCode
    if (charCode == 46) {
        return true;
    }
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function sadecerakam(event) {
    var charCode = (event.which) ? event.which : event.keyCode

    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function scrollToElement(id) {
    document.getElementById(id).scrollTop = document.getElementById(id).scrollHeight;
}

function YeniMesaj(mesaj, messagetype) {
    var x = document.getElementById("YeniMesajDiv")
    var cssclass;
    switch (messagetype) {
        case 'Tamam':
            cssclass = 'mesajbox-tamam'
            break;
        case 'Hata':
            cssclass = 'mesajbox-hata'
            break;
        case 'Sil':
            cssclass = 'mesajbox-uyarı'
            break;
        default:
            cssclass = 'alert-info'
    }

    x.className = "showsnack " + cssclass
    x.innerHTML = mesaj;

    setTimeout(function () { x.className = x.className.replace("showsnack", ""); }, 3000);
    return false;
}

$(document).ready(function () {
    $(".show-toast").click(function () {
        $("#myToast").toast('show');
    });
});

function ModalAc(modalstr) {

    //alert(modalstr);
    $('#' + modalstr).modal('show');
}

function ModalKapat(modalstr) {

    $('#' + modalstr).modal('hide');
}

function SetTarget() {

    document.forms[0].target = "_blank";
}

function showWindow(myurl) {
    window.open(myurl, '_blank');
}

function ShowMessage(message, messagetype, yer) {
    //alert(message);
    var cssclass;
    switch (messagetype) {
        case 'Tamam':
            cssclass = 'alert-success'
            break;
        case 'Hata':
            cssclass = 'alert-danger'
            break;
        case 'Sil':
            cssclass = 'alert-warning'
            break;
        default:
            cssclass = 'alert-info'
    }
    $('#alert_container_' + yer).append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 0px 0px 2px #999;" class="alert  ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong></strong> <span>' + message + '</span></div>');
}



function ModalGizle(modaladi) {

    var divAcct = document.getElementById(modaladi);
    divAcct.style.display = "";
}

function ModalAc2(modaladi) {

    var divAcct = document.getElementById(modaladi);
    divAcct.style.display = "block";
}

// Şifre Değişir modülünde Textbox'ların boş olması durumunda çıkacak olan alarm
$("#btnLogin").click(function (event) {

    var form = $("#loginForm");

    if (form[0].checkValidity() === false) {
        event.preventDefault();
        event.stopPropagation();
    }

    // if validation passed form
    // would post to the server here

    form.addClass('was-validated');
});
