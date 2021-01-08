


function beforeAsyncPostBack() {
    var curtime = new Date();
    //alert('Time before PostBack:   ' + curtime);
    ShowProgress2();
}

function afterAsyncPostBack() {
    var curtime = new Date();
    // alert('Time after PostBack:   ' + curtime);
    ShowProgress2_hide();

}

Sys.Application.add_init(appl_init);

function appl_init() {
    var pgRegMgr = Sys.WebForms.PageRequestManager.getInstance();
    pgRegMgr.add_beginRequest(BeginHandler);
    pgRegMgr.add_endRequest(EndHandler);
}

function BeginHandler() {
    beforeAsyncPostBack();
}

function EndHandler() {
    afterAsyncPostBack();
}

function ShowProgress2() {


    var modal = $('<div />');
    var loading = $(".loading");
    loading.show();
    var top = 10;
    //var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
    var left = Math.max($(window).width() - 170 - loading[0].offsetWidth / 2, 0);
    loading.css({ top: top, left: left });

}

function ShowProgress2_hide() {

    var modal = $('<div />');
    var loading = $(".loading");
    loading.hide();


}


$(document).ready(function () {
    $('.ball, .ball1').removeClass('stop');
    $('.trigger').click(function () {
        $('.ball, .ball1').toggleClass('stop');
    });
});


