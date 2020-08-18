$(document).ready(function () {
    var mainJS = new MainJS();
})

class MainJS {
    constructor() {
        this.initEvents();
    }

    initEvents() {
        $('.root-sibar').on('click', this.rootSibarOnClick.bind(this));
        $(window).resize(this.windowOnResize.bind(this));
        $('.nav-item').click(this.navItemOnClick.bind(this));
    }

    rootSibarOnClick() {
        var navBox = $('.m-nav');
        var smallScreen = window.matchMedia("(min-width: 240px)");
        var screen768 = window.matchMedia("(min-width: 768px)");
        var screen992 = window.matchMedia("(min-width: 992px)");
        var screen1200 = window.matchMedia("(min-width: 1200px)");
        if (screen768.matches || screen992.matches || screen1200.matches) {
            if (navBox.hasClass('m-nav-hidden')) {
                navBox.removeClass('m-nav-hidden');
                $('.m-content').css("left", "200px");
                $('.m-content').css("with", "100%");
            } else {
                navBox.addClass('m-nav-hidden');
                $('.m-content').css("left", "0");
                $('.m-content').css("with", "100%");
            }
        } else {
            $('.m-content').css("left", "0");
            if (navBox.hasClass('m-nav-hidden') || navBox.css('display') == 'none') {
                navBox.css({ 'display': '' });
                navBox.removeClass('m-nav-hidden');
                navBox.addClass('m-nav-block');
            } else {
                navBox.addClass('m-nav-hidden');
                navBox.removeClass('m-nav-block');
            }
        }
    }
    windowOnResize(sender) {
        if ($('.m-content').css('left') == '0px') {
            $('.m-nav').removeClass('m-nav-block');
        } 
        var screen576Min = window.matchMedia("(min-width: 576px)");
        var screen576Max = window.matchMedia("(min-width: 768px)");
        if (screen576Min.matches) {
            $('.m-content').css({ 'left': '' });
            $('.m-nav').removeClass('m-nav-block');
            $('.m-nav').css({ 'display': '' });
        }
        if (screen576Max.matches) {
            $('.m-nav').removeClass('m-nav-hidden');
            $('.m-nav').css('display', 'block');
        }
        
    }

    navItemOnClick() {
        var screen576Min = window.matchMedia("(min-width: 768px)");
        if (!screen576Min.matches) {
            $('.m-nav').removeClass('m-nav-block');
            $('.m-nav').addClass('m-nav-hidden');
            $('.m-nav').css({ 'display': '' });
        }
    }
}