﻿$(function () {

    function getQueryVariable(variable) {
        const query = window.location.search.substring(1);
        const vars = query.split("&");
        for (let i = 0; i < vars.length; i++) {
            const pair = vars[i].split("=");
            if (pair[0] == variable) {
                return pair[1];
            }
        }
        return false;
    }

    if (getQueryVariable("ErrorSesion") !== false) {
        $("#frmLogin").find("span[data-valmsg-for='ErrorSesion']").text(decodeURI(getQueryVariable("ErrorSesion")));
        $("#sign-in").trigger("click");
    }

    $("#register").on("click", function () {
        $("#registerForm").css("display", "block");
        $("#forgot_pw").css("display", "none");
        $("#login").css("display", "none");
        $("#sign-in-title").text("Registrarse");
    });

    $("#registerReturn").on("click", function () {
        $("#registerForm").css("display", "none");
        $("#forgot_pw").css("display", "none");
        $("#login").css("display", "block");
        $("#sign-in-title").text("Iniciar sesión");
    });

    $("#forgot").on("click", function () {
        $("#registerForm").css("display", "none");
        $("#forgot_pw").css("display", "block");
        $("#login").css("display", "none");
        $("#sign-in-title").text("Recordar Contraseña");
    });

    $("#forgotReturn").on("click", function () {
        $("#registerForm").css("display", "none");
        $("#forgot_pw").css("display", "none");
        $("#login").css("display", "block");
        $("#sign-in-title").text("Iniciar sesión");
    });

    $("#btnRestablecerContrasena").on("click", function () {

        if ($("#email_forgot").val() === "") alert("ingrese un email");

        $.ajax({
            url: "/Login/Forgot",
            data: {
                email: $("#email_forgot").val()
            },
            type: "POST",
            cache: false,
            success: function (data) {
                if (data.Success) {
                    alert("Se envio un email a la cuenta indicada!!!");
                    $("#email_forgot").val("");
                    $("#forgotReturn").trigger("click");
                } else {
                    alert(`Ocurrio un error: ${data.Message}`);
                }
            }
        });
    });
})