try {
    window.addEventListener("submit", function (e) {
        const form = e.target;
        if (form.getAttribute("enctype") === "multipart/form-data") {
            if (form.dataset.ajax) {
                e.preventDefault();
                e.stopImmediatePropagation();
                const xhr = new XMLHttpRequest();
                xhr.open(form.method, form.action);

                xhr.addEventListener("loadend", function () {
                    if (form.getAttribute("data-ajax-loading") != null && form.getAttribute("data-ajax-loading") !== "")
                        document.getElementById(form.getAttribute("data-ajax-loading").substr(1)).style.display = "none";

                    if (form.getAttribute("data-ajax-complete") != null &&
                        form.getAttribute("data-ajax-complete") !== "")
                        window[form.getAttribute("data-ajax-complete")].apply(this, []);;
                });

                xhr.onreadystatechange = function () {
                    if (xhr.readyState === 4) {
                        if (xhr.status === 200)
                            window[form.getAttribute("data-ajax-success")].apply(this, [JSON.parse(xhr.responseText)]);
                        else
                            window[form.getAttribute("data-ajax-failure")].apply(this, [xhr.status]);
                    }
                };

                const confirm = form.getAttribute("data-ajax-confirm");

                if (confirm) {
                    swal({
                        title: "Confirmacion",
                        text: confirm,
                        type: "info",
                        showCancelButton: true,
                        closeOnConfirm: false,
                        showLoaderOnConfirm: true
                    },
                        function () {
                            if (form.getAttribute("data-ajax-begin") != null && form.getAttribute("data-ajax-begin") !== "")
                                window[form.getAttribute("data-ajax-begin")].apply(this, []);

                            xhr.send(new FormData(form));
                        });
                } else {
                    if (form.getAttribute("data-ajax-loading") != null && form.getAttribute("data-ajax-loading") !== "")
                        document.getElementById(form.getAttribute("data-ajax-loading").substr(1)).style.display = "block";

                    if (form.getAttribute("data-ajax-begin") != null && form.getAttribute("data-ajax-begin") !== "")
                        window[form.getAttribute("data-ajax-begin")].apply(this, []);

                    xhr.send(new FormData(form));
                }
            }
        }
    }, true);
} catch (err) { }
try {
    $.validator.setDefaults({
        ignore: [],
        // other default options
    });

} catch (err) { }
$(function () {
    $(".sidebar-menu").tree();

    $(document).on("show.bs.modal", ".modal", function (event) {
        var zIndex = 1040 + (10 * $(".modal:visible").length);
        $(this).css("z-index", zIndex);
        setTimeout(function () {
            $(".modal-backdrop").not(".modal-stack").css("z-index", zIndex - 1).addClass("modal-stack");
        }, 0);

        $("body").css("overflow", "hidden");
    });

    $(document).on("hidden.bs.modal", ".modal", function (event) {
        if ($(".modal.fade.in").length === 0) {
            $("body").css("overflow", "auto");
        }
    });

    $(document).tooltip({
        selector: '[data-toggle="tooltip"]',
        container: "body",
        trigger: "hover"
    });

    $(document).on("keyup keypress", "form", function (e) {
        const keyCode = e.keyCode || e.which;
        if (keyCode === 13) {
            e.preventDefault();
            return false;
        }
    });

    moment.locale("es");
    $.ajaxSetup({ cache: false });

});

function invocarModal(url, onHiddenModal) {
    $.ajax({
        url: url,
        type: "GET",
        dataType: "html",
        cache: false,
        success: function (data) {
            const $modal = $("<div class='parent'>").append(data);
            $modal.find(">.modal").on("hidden.bs.modal", function() {
                $(this).parent().remove();
                if (onHiddenModal) onHiddenModal();
            });
            $modal.find(">.modal").modal("show");

            $("body").append($modal);
        },
        beforeSend: function () {
            $("#loading").show();
        },
        complete: function () {
            $("#loading").hide();
        }
    });
}

function onSuccessForm(data, $form, $modal) {
    $form.find("span[data-valmsg-for]").text("");
    if (data.Success === true) {
        swal("Bien!", "Registro Guardado Correctamente", "success");
        if ($modal) $modal.modal("hide");
    } else {
        if (data.Errors) {
            $.each(data.Errors,
                function (i, item) {
                    if ($form.find("span[data-valmsg-for=" + item.Key + "]").length !== 0)
                        $form.find("span[data-valmsg-for=" + item.Key + "]").text(item.Message);
                });
        }
        swal("Algo Salio Mal!", data.Message ? data.Message : "Verifique los campos ingresados", "error");
    }
}

function onFailureForm() {
     swal("Algo Salio Mal!", "Ocurrio un error al Guardar22", "error");
}

function confirmAjax(url, parameters, type, msg, msgSuccess, onSuccess) {
    swal({
            title: "Confirmacion",
            text: msg ? msg : "Esta Seguro ??",
            type: "warning",
            showCancelButton: true,
            closeOnConfirm: false,
            showLoaderOnConfirm: true
        },
        function () {
            actionAjax(url, parameters, type, onSuccess, true, msgSuccess);
        });
}

function actionAjax(url, parameters, type, onSuccess, isToConfirm, msgSuccess) {
    $.ajax({
        url: url,
        data: parameters,
        type: type,
        cache: false,
        success: function (data) {
            if (data.Success === true) {
                if (isToConfirm === true) swal("Bien!", msgSuccess ? msgSuccess : "Proceso realizado Correctamente", "success");
                if (onSuccess) onSuccess(data);
            } else {
                if (isToConfirm === true) swal("Algo Salio Mal!", data.Message, "error");
            }
        },
        beforeSend: function () {
            if (isToConfirm !== true) $("#loading").show();
        },
        complete: function () {
            if (isToConfirm !== true) $("#loading").hide();
        }
    });
}

function createModal(title, body, onHidden) {
    const template = `<div id="myModal" class="modal fade" role="dialog">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">${title}</h4>
                          </div>
                          <div class="modal-body">
                            ${body}
                          </div>
                          <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                          </div>
                        </div>
                      </div>
                    </div>`;

    const $modal = $(template);
    $modal.on("hidden.bs.modal", function () {
        $modal.remove();
        if (onHidden) onHidden();
    });

    $modal.modal("show");
}
function getDate() {
    const now = new Date();
    const primerDia = new Date(now.getFullYear(), now.getMonth(), 1);
    const ultimoDia = new Date(now.getFullYear(), now.getMonth() + 1, 0);
    return {
        Now: now,
        FirstDay: primerDia,
        LastDay: ultimoDia
    };
}
function getFormatDate(date) {
    const array = date.split("/");
    const f = new Date(array[2], array[1] - 1, array[0]);
    return f.format("yyyy-mm-dd");
}