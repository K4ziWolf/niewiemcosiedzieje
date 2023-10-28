$(document).ready(function () {
    var theme = 'dark-theme';

    // Funkcja do zmiany motywu
    function toggleTheme() {
        var body = $("body");
        var navbar = $(".navbar-unique");
        var navbarItem = $(".navbar-item");
        var navbarItem3 = $("#navbar-item3");
        var classMenuItems = $(".class-menu-item");
        var cardBody = $(".card-body-theme");
        var btns = $(".dark-theme-kazi-btns")
        var addbtn = $(".dark-theme-kazi-add-btns")
        if (theme === 'dark-theme') {
            navbarItem3.removeClass("texthello-kazi text-navbar-kazi").addClass("navbar-text");
            body.removeClass('light-theme').addClass("dark-theme");
            navbarItem.each(function () {
                $(this).removeClass('text-navbar-kazi').addClass("navbar-text");
            });
            classMenuItems.each(function () {
                $(this).removeClass("card-div-kazi").addClass("class-menu");
            });
            cardBody.each(function (){
                $(this).removeClass("card-body-kazi").addClass("card-body");
            });
            navbar.each(function () {
                $(this).removeClass("navbar-kazi").addClass("navbar-edit");
            });
            btns.each(function () {
                $(this).removeClass("dark-theme-kazi-btn").addClass("card-btn");
            });
            addbtn.each(function () {
                $(this).removeClass("dark-theme-kazi-add-btn").addClass("add-classes-btn");
            });
            theme = 'light-theme';
        } else {
            addbtn.each(function () {
                $(this).removeClass("add-classes-btn").addClass("dark-theme-kazi-add-btn");
            });
            body.removeClass("dark-theme").addClass('light-theme');
            navbarItem.each(function () {
                $(this).removeClass("navbar-text").addClass("text-navbar-kazi");
            });
            navbarItem3.removeClass("navbar-text").addClass("texthello-kazi text-navbar-kazi");
            classMenuItems.each(function () {
                $(this).removeClass("class-menu").addClass("card-div-kazi");
            });
            cardBody.each(function () {
                $(this).removeClass("card-body").addClass("card-body-kazi");
            });
            navbar.each(function () {
                $(this).removeClass("navbar-edit").addClass("navbar-kazi");
            });
            btns.each(function () {
                $(this).removeClass("card-btn").addClass("dark-theme-kazi-btn");
            });

            theme = 'dark-theme';
        }
    }
    $("#changeThemeButton").click(toggleTheme);
});
