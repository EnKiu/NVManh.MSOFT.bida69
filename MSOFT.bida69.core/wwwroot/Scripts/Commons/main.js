$(document).ready(function () {
    var mainJS = new MainJS();
})

class MainJS {
    constructor() {
        this.initEvents();
    } 

    initEvents() {
        $('.root-sibar').on('click', this.rootSibarOnClick.bind(this));
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
            if (navBox.hasClass('m-nav-hidden')) {
                navBox.removeClass('m-nav-hidden');
            } else {
                navBox.addClass('m-nav-hidden');
            }
        }
    }
}