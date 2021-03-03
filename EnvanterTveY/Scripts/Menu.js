
function menu_tikla(div_main) {

    //var div_main = this;
    //alert(div_main.innerHTML);
    var div_main_content = div_main.nextElementSibling;
    //alert(div_main_content.length);
    var loading = $(".menu_background");

    var div_acik = document.getElementsByClassName("div_main_menu_box_acik");
    for (i = 0; i < div_acik.length; i++) {
        div_acik[i].className = "div_main_menu_box_kapali";
        loading.hide();
        document.getElementById("solAlan").className = "menu-dar";
    }

    var div_acik_ana = document.getElementsByClassName("main_menu_item menu_aktif");
    if (div_acik_ana.length > 0) {

        for (i = 0; i < div_acik_ana.length; i++) {
            if (div_acik_ana[i].id != div_main.id) {
                div_acik_ana[i].className = "main_menu_item";
                div_main.classList.toggle("menu_aktif");

                if (div_main_content.className == "div_main_menu_box_acik") {
                    div_main_content.className = "div_main_menu_box_kapali";
                    loading.hide();
                    document.getElementById("solAlan").className = "menu-dar";

                }
                else {
                    div_main_content.className = "div_main_menu_box_acik";
                    loading.show();
                    document.getElementById("solAlan").className = "menu-genis";
                }
            }
            else {
                div_main.classList.toggle("menu_aktif");
            }
        }
    }
    else {
        div_main.classList.toggle("menu_aktif");

        if (div_main_content.className == "div_main_menu_box_acik") {
            //alert("kapat");
            div_main_content.className = "div_main_menu_box_kapali";
            // document.getElementById("solAlan").className = "menu-dar";
            loading.hide();
            document.getElementById("solAlan").className = "menu-dar";

        }
        else {
            //alert("ac");
            div_main_content.className = "div_main_menu_box_acik";
            loading.show();
            document.getElementById("solAlan").className = "menu-genis";
        }

    }

}



menuler = [];
var menulen, i;

function menu_islem(menus) {

    //alert(menus);
    menulen = menus.length;
    //alert(raporlen);

    for (i = 0; i < menulen; i++) {
        //alert(menus[i]);
        //rapor_islem(rp1[i]);
        var div_main = document.getElementById(menus[i]);

        div_main.addEventListener('click', function () {

            //alert(div_main.innerText);
            //var div = document.getElementById(divs);
            var div_main_content = this.nextElementSibling;
            //alert(div_main_content.innerHTML);
            //alert("--"+div_main_content.className+"--");

            //page.classList.toggle("menu_aktif"); 
            //div_acik.className = "div_main_menu_box_kapali";
            //alert(div_acik.length);  

            var div_acik = document.getElementsByClassName("div_main_menu_box_acik");
            for (i = 0; i < div_acik.length; i++) {
                div_acik[i].className = "div_main_menu_box_kapali";
                //document.getElementById("solAlan").className = "menu-dar";
                menu_dar();
            }

            var div_acik_ana = document.getElementsByClassName("main_menu_item menu_aktif");
            if (div_acik_ana.length > 0) {

                for (i = 0; i < div_acik_ana.length; i++) {
                    if (div_acik_ana[i].id != this.id) {
                        //alert(div_acik_ana[i].id);
                        div_acik_ana[i].className = "main_menu_item";
                        this.classList.toggle("menu_aktif");

                        if (div_main_content.className == "div_main_menu_box_acik") {
                            //alert("kapat");
                            div_main_content.className = "div_main_menu_box_kapali";
                            //document.getElementById("solAlan").className = "menu-dar";
                            menu_dar();

                        }
                        else {
                            //alert("ac");
                            div_main_content.className = "div_main_menu_box_acik";
                            //document.getElementById("solAlan").className = "menu-genis";
                            menu_genis();
                        }
                    }
                    else {
                        this.classList.toggle("menu_aktif");
                    }
                }
            }
            else {
                this.classList.toggle("menu_aktif");

                if (div_main_content.className == "div_main_menu_box_acik") {
                    //alert("kapat");
                    div_main_content.className = "div_main_menu_box_kapali";
                    // document.getElementById("solAlan").className = "menu-dar";
                    menu_dar();

                }
                else {
                    //alert("ac");
                    div_main_content.className = "div_main_menu_box_acik";
                    menu_genis();
                }

            }


        });



    }

    function menu_genis() {
        //document.getElementsByClassName("loading").classList.toggle("gizli");
        var loading = $(".menu_background");
        loading.show();
        //alert("geniş");
        //alert(x.innerHTML);


        document.getElementById("solAlan").className = "menu-genis";
        //alert("bitti");

    }


    function menu_dar() {
        //alert("dar");
        var loading = $(".menu_background");
        loading.hide();

        //var menuback = $("menu-background");
        //menuback.hide();
        document.getElementById("solAlan").className = "menu-dar";


    }





    /*
    for (i = 1; i <= menu_say; i++)
    {
        //ctl00_repeater_sol_main_ctl02_div_menu_main_item
        //ctl00_repeater_sol_main_ctl02_div_main_menu_box
        var div_main = document.getElementById("ctl00_repeater_sol_main_ctl0"+i+"_div_menu_main_item");
        //var div_main_box = "ctl00_repeater_sol_main_ctl0" + i + "_div_main_menu_box";
        //var div = document.getElementsByClassName('drill_cursor')[0];
        //alert(div_main.innerHTML);
        div_main.addEventListener('click', function () { Menu_ac_kapat("ctl00_repeater_sol_main_ctl0" + i + "_div_main_menu_box"); } );
    }
    */

}

function Menu_ac_kapat(divid) {
    alert(divid);
    var div = document.getElementById(divid);
    //alert(div.innerHTML);

    if (div.className != "div_main_menu_box_acik") {
        div.className = "div_main_menu_box_kapali";
    }
    else {
        div.className = "div_main_menu_box_acik";
    }


}





function menu_kapat() {
    var loading = $(".menu_background");
    loading.hide();

    var div_acik = document.getElementsByClassName("div_main_menu_box_acik");
    for (i = 0; i < div_acik.length; i++) {
        div_acik[i].className = "div_main_menu_box_kapali";
    }

    var div_acik_ana = document.getElementsByClassName("main_menu_item menu_aktif");
    if (div_acik_ana.length > 0) {

        for (i = 0; i < div_acik_ana.length; i++) {
            div_acik_ana[i].className = "main_menu_item";
        }
    }
    document.getElementById("solAlan").className = "menu-dar";
}



function uyari_kapat() {

    uayri_background_close();
    arama_background_close();

    div_mesaj_close();
    div_uyari_close();
    div_user_kapat();
    div_arama_close();


}


function escapeRegExp(string) {
    return string.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
}

/* Define functin to find and replace specified term with replacement string */
function replaceAll2(str, term, replacement) {
    return str.replace(new RegExp(escapeRegExp(term), 'g'), replacement);
}

function sorgu_degistir() {
    //alert("sdasd");
    //var txt = $("#ctl00_ContentPlaceHolder1_txtsorgu").;
    var txt = document.getElementById('ctl00_ContentPlaceHolder1_txtsorgu').value
    //alert(txt);
    txt = replaceAll2(txt, "<", "#1");
    txt = replaceAll2(txt, ">", "#2");
    txt = replaceAll2(txt, "/", "#3");
    txt = replaceAll2(txt, "\\", "#4");
    document.getElementById('ctl00_ContentPlaceHolder1_txtsorgu').value = txt;
    return true;
}



function RaporGetir(id) {

    alert(id);
    $.ajax({
        type: "POST",
        url: "RaporGoster.aspx?tip=text&id=" + id,
        data: '{id: ' + id + ',tip:text}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response.d);
        },
        error: function (response) {
            alert(response.d);
        }
    });

}

function OnSuccess(response) {


    alert("başarılı");
    alert(response);
}


//============               MODAL KAPATMA               ================



function bar_goster() {
    document.getElementById("myProgress").style.display = '';
}



function bar_move() {
    var elem = document.getElementById("myBar");
    var width = 1;
    var id = setInterval(frame, 50);
    elem.style.display = '';

    function frame() {
        if (width >= 100) {
            clearInterval(id);
        } else {
            width++;
            elem.style.width = width + '%';
        }
    }
}

function PopupGizle() {
    bar_goster();
    bar_move();

    setTimeout(function () {
        //$("#ctl00_ContentPlaceHolder1_PanelBilgi").hide();
        //document.getElementById('<%= ImageButton2.ClientID %>').click();
        $("#ctl00_ContentPlaceHolder1_ImageButton2").click();

    }, 4900);
}


var typingTimer;                //timer identifier
var doneTypingInterval = 1000;  //time in ms, 5 second for example

var specialKeys = new Array();
specialKeys.push(8);  //Backspace
specialKeys.push(9);  //Tab
specialKeys.push(46); //Delete
specialKeys.push(36); //Home
specialKeys.push(35); //End
specialKeys.push(37); //Left
specialKeys.push(39); //Right

var turkcekey = new Array();
turkcekey.push(287); //Right
turkcekey.push(252); //Right
turkcekey.push(351); //Right
turkcekey.push(105); //Right
turkcekey.push(231); //Right
turkcekey.push(246); //Right
turkcekey.push(286); //Right
turkcekey.push(220); //Right
turkcekey.push(350); //Right
turkcekey.push(304); //Right
turkcekey.push(214); //Right
turkcekey.push(199); //Right


function txtara_keyup(e) {
    //alert(e.keyCode);
    var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
    var ret = ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || keyCode == 32 || keyCode == 8 || keyCode == 46 || keyCode == 219 || keyCode == 220 || keyCode == 221 || keyCode == 59 || keyCode == 73 || keyCode == 191 || (keyCode >= 97 && keyCode <= 122) || (turkcekey.indexOf(e.keyCode) != -1));
    //alert(ret);
    if (ret) {
        clearTimeout(typingTimer);
        var q = document.getElementById("txtara2").value;;
        var div = document.getElementById("div_arama_main");
        //alert(q);
        //alert(e.keyCode);
        if (q != "") {
            typingTimer = setTimeout(doneTyping, doneTypingInterval);
            //alert("sdas");

            if (div.classList.contains("gizli"))
                div.classList.remove('gizli');

            var div2 = document.getElementById("div_personelarama_ping");
            if (div2 != null) {
                if (div2.classList.contains("gizli"))
                    div2.classList.remove('gizli');
            }
            else {
                div.innerHTML = "<div id=\"div_personelarama_ping\"  class=\"center load_img\" ></div>";
            }
            arama_background_open();
        }
        else {
            arama_background_close();
            if (!div.classList.contains("gizli"))
                div.classList.add('gizli');
            div.innerHTML = null;
        }
    }
    else {
        //alert("false");
    }
}

function txtara_keydown() {
    clearTimeout(typingTimer);

}

function doneTyping() {
    //alert("div");
    var div = document.getElementById("div_arama_main");
    var q = document.getElementById("txtara2").value;;


    //alert(q);
    if (q.trim() != "") {
        setTimeout(function () {

            $("#div_arama_main").load("RaporGoster.aspx?tip=personelarama&q=" + q.replace(" ", "%20"), function (responseTxt, statusTxt, xhr) {


                if (statusTxt == "success") {

                    //alert(responseTxt);
                    div.innerHTML = responseTxt;

                }

            });

        }, 1000);

    }
    else {
        div.innerHTML = "Sorgu boş Yapılamaz";
    }
}

/*
//on keyup, start the countdown
$input.on('keyup', function () {
    clearTimeout(typingTimer);
    typingTimer = setTimeout(doneTyping, doneTypingInterval);
    alert("sdas");
});

//on keydown, clear the countdown 
$input.on('keydown', function () {
    clearTimeout(typingTimer);
});

//user is "finished typing," do something

*/



function IsAlphaNumeric(e) {
    var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
    var ret = ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || keyCode == 32 || (keyCode >= 97 && keyCode <= 122) || (turkcekey.indexOf(e.keyCode) != -1) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
    //document.getElementById("error").style.display = ret ? "none" : "inline";
    //alert(keyCode);
    //var ret = ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || keyCode == 32 || keyCode == 8 || keyCode == 46 || keyCode == 219 || keyCode == 220 || keyCode == 221 || keyCode == 59 || keyCode == 73 || keyCode == 191 || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
    //alert(ret);
    return ret;
}

function js_mesaj(mesaj) {
    alert(mesaj);
}



function onlyDotsAndNumbers(event) {
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


function SetTarget() {

    document.forms[0].target = "_blank";

}

function showWindow(myurl) {
    window.open(myurl, '_blank');
}

function showDate(datestr) {
    $find(datestr).show();
}


function YeniSekme(myurl) {
    window.open(myurl, '_blank');
}

function PopUpWindow(url) {
    alert(url);
    window.open("'" + url + "'", 'popup', 'width = 800, height = 600, left = 100, top = 100, resizable = yes');
}

function PopUpMail(div) {
    var id = div.getAttribute('data-id')
    //alert(id);
    window.open("Mailgosterr.aspx?id=" + id + "", 'popup', 'width = 800, height = 600, left = 100, top = 100, resizable = yes');
}

function panel_show_hide(paneldiv, ok) {
    //alert(paneldiv);
    var div = document.getElementById(paneldiv);
    var span = document.getElementById(ok);

    if (div.className != "panel-hide") {
        div.className = "panel-hide";
        span.className = "ok-alt";
    }
    else {
        div.className = "panel-show";
        span.className = "ok-ust";
    }




}


function panel_goster_gizle(paneldiv) {
    //alert(paneldiv);
    var div = document.getElementById(paneldiv);

    if (div.className != "panel-hide") {
        div.className = "panel-hide";
    }
    else {
        div.className = "panel-show";
    }


}


function Personel_detay(link) {
    ShowProgress2();
    window.location.replace("PersonelDetay.aspx?idh=" + link);
    //window.location.replace("PersonelDetay.aspx?idh="+link);
}



function Personel_detay2(div) {
    ShowProgress2();
    var id = div.getAttribute('data-id')

    window.location.replace("PersonelDetay.aspx?idh=" + id);
    //window.location.replace("PersonelDetay.aspx?idh="+link);
}



function div_user_kapat() {
    var div = document.getElementById("user_altbilgi");
    if (!div.classList.contains("gizli"))
        div.classList.add('gizli');

}



function div_user_ac() {


    var div = document.getElementById("user_altbilgi");
    if (div.classList.contains("gizli")) {
        uyari_kapat();
        uayri_background_open();
        div.classList.remove('gizli');

    }
    else {
        uyari_kapat();
        //uayri_background_open();

    }


}

function dic_ac_kapat(divid) {

    var div = document.getElementById(divid);
    if (div.classList.contains("gizli")) {
        div.classList.remove('gizli');
    }
    else {
        div.classList.add('gizli');

    }




}





function div_bildirim_panel_ac() {


    var div = document.getElementById("uyari_panel_div");
    if (!div.classList.contains("gizli")) {

        uyari_kapat();

    }
    else {
        uyari_kapat();
        div.classList.remove('gizli');
        uayri_background_open();

        if ($('#uyari_panel_div').css('display') != 'none') {

            setTimeout(function () {

                $("#uyari_panel_div").load("RaporGoster.aspx?tip=uyari", function (responseTxt, statusTxt, xhr) {


                    if (statusTxt == "success") {

                        document.getElementById("div_uyari_ping").className = "gizli";

                    }

                });

            }, 1000);

        }
    }



}


function div_mesaj_panel_ac() {


    var div = document.getElementById("mesaj_panel_div");
    if (!div.classList.contains("gizli")) {
        uyari_kapat();
    }
    else {
        uyari_kapat();


        uayri_background_open();
        div.classList.remove('gizli');

        if ($('#mesaj_panel_div').css('display') != 'none') {

            setTimeout(function () {

                $("#mesaj_panel_div").load("RaporGoster.aspx?tip=mesaj", function (responseTxt, statusTxt, xhr) {


                    if (statusTxt == "success") {

                        document.getElementById("div_mesaj_ping").className = "gizli";

                    }

                });

            }, 1000);

        }
    }


}

function uyari_kapat() {

    uayri_background_close();
    arama_background_close();

    div_mesaj_close();
    div_uyari_close();
    div_user_kapat();
    div_arama_close();


}


//arama_background_id

function arama_background_open() {

    var div = document.getElementById("arama_background_id");
    if (div.classList.contains("gizli"))
        div.classList.remove('gizli');

}


function arama_background_close() {

    var div = document.getElementById("arama_background_id");
    if (!div.classList.contains("gizli"))
        div.classList.add('gizli');

    document.getElementById("txtara2").value = null;


}

function uayri_background_open() {

    var div = document.getElementById("uyari_background_id");
    if (div.classList.contains("gizli"))
        div.classList.remove('gizli');

}


function uayri_background_close() {

    var div = document.getElementById("uyari_background_id");
    if (!div.classList.contains("gizli"))
        div.classList.add('gizli');

}

function div_mesaj_close() {
    var div = document.getElementById("mesaj_panel_div");
    if (!div.classList.contains("gizli"))
        div.classList.add('gizli');
}

function div_uyari_close() {
    var div = document.getElementById("uyari_panel_div");
    if (!div.classList.contains("gizli"))
        div.classList.add('gizli');
}


function div_arama_close() {
    var div = document.getElementById("div_arama_main");
    if (!div.classList.contains("gizli"))
        div.classList.add('gizli');

    div.innerHTML = null;
}




function menu_kapat() {
    var loading = $(".menu_background");
    loading.hide();

    var div_acik = document.getElementsByClassName("div_main_menu_box_acik");
    for (i = 0; i < div_acik.length; i++) {
        div_acik[i].className = "div_main_menu_box_kapali";
    }

    var div_acik_ana = document.getElementsByClassName("main_menu_item menu_aktif");
    if (div_acik_ana.length > 0) {

        for (i = 0; i < div_acik_ana.length; i++) {
            div_acik_ana[i].className = "main_menu_item";
        }
    }
    document.getElementById("solAlan").className = "menu-dar";
}


//============               MODAL KAPATMA               ================




function modal_kontrol(modal1) {
    //alert("modal aç label  : ==" + modal1+"==");

    try {
        if (modal1 != "") {
            var modal_s = $find(modal1);
            modal_s.show();
        }
    }
    catch (err) {

    }
}


function modal_hide(isim) {

    //alert(isim);

    $find(isim).hide();
    // $find("MPEaltfirma").hide();
}


function modal_islem(isim) {
    isim = "ctl00_ContentPlaceHolder1_" + isim;
    //alert("modal islem "+isim);

    var background = $find(isim)._backgroundElement;
    background.onclick = function () {
        // modal_hide(isim);
        $("#ctl00_ContentPlaceHolder1_btnpanelkapat").click();

    }

}

modallar = [];
var modallar, mlen, i;
//fruits = ["Banana", "Orange", "Apple", "Mango"];

function modal_yukle(modallar) {
    //alert(modallar);
    mlen = modallar.length;
    for (i = 0; i < mlen; i++) {
        modal_islem(modallar[i]);
    }

}



function rapor_islem(id, divid) {

    //alert(id+ "  -  "+divid );
    //alert(divid);
    var div_isim = "ctl00_ContentPlaceHolder1_" + divid;
    //alert(div_isim);
    setTimeout(function () {

        $("#" + div_isim + "div_tablo").load(id, function (responseTxt, statusTxt, xhr) {


            if (statusTxt == "success") {

                document.getElementById(div_isim + "rapor_load").className = "gizli";

                /*
                alert("tikla");
                var background = $find( "div_rapor_ust")._backgroundElement;
                alert("tikla2");
                background.onclick = function () {
    
                    
                    window.open("Default.aspx", "_blank");
    
                }
                */
            }

            /*
            if (statusTxt == "error")
                    alert("Error: " + xhr.status + ": " + xhr.statusText);
                    */
        });


    }, 800);

}

raporlar = [];
var raporlar, raporlen, i;

function rapor_yukle(rp1) {
    //alert(rp1);
    raporlen = rp1.length;
    //alert(raporlen);

    for (i = 0; i < raporlen; i++) {
        //alert(i);
        rapor_islem(rp1[i], rp1[i + 1]);
        //  setTimeout(function () { rapor_islem(rp1[i], rp1[i + 1]);  }, 800);
        i++;

    }

}





