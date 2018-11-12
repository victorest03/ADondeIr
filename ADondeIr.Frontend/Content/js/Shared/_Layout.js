try {
    window.addEventListener("submit", function (e) {
        var form = e.target;
        if (form.getAttribute("enctype") === "multipart/form-data") {
            if (form.dataset.ajax) {
                e.preventDefault();
                e.stopImmediatePropagation();
                var xhr = new XMLHttpRequest();
                xhr.open(form.method, form.action);

                xhr.addEventListener("loadend", function () {
                    if (form.getAttribute("data-ajax-loading") != null && form.getAttribute("data-ajax-loading") != "")
                        document.getElementById(form.getAttribute("data-ajax-loading").substr(1)).style.display = "none";

                    if (form.getAttribute("data-ajax-complete") != null && form.getAttribute("data-ajax-complete") != "")
                        window[form.getAttribute("data-ajax-complete")]
                });

                xhr.onreadystatechange = function () {
                    if (xhr.readyState === 4) {
                        if (xhr.status === 200)
                            window[form.getAttribute("data-ajax-success")].apply(this, [JSON.parse(xhr.responseText)]);
                        else
                            window[form.getAttribute("data-ajax-failure")].apply(this, [JSON.parse(xhr.responseText), xhr.status]);
                    }
                };

                if (form.getAttribute("data-ajax-loading") != null && form.getAttribute("data-ajax-loading") != "")
                    document.getElementById(form.getAttribute("data-ajax-loading").substr(1)).style.display = "block";

                if (form.getAttribute("data-ajax-begin") != null && form.getAttribute("data-ajax-begin") != "")
                    window[form.getAttribute("data-ajax-begin")]

                xhr.send(new FormData(form));
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
function invocarModal(url, onSuccess) {
    $.ajax({
        url: url,
        type: "GET",
        dataType: "html",
        cache: false,
        success: function (data) {
            $("body").append(data);
            if (onSuccess) onSuccess(data);
        },
        beforeSend: function () {
            $("#loading").show();
        },
        complete: function () {
            $("#loading").hide();
        }
    });
}
function actionAjax(url, data, type, messageSuccess, onSuccess, message) {
    swal({
        title: "Confirmacion",
        text: message ? message : "Esta Seguro ??",
        type: "warning",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true
    },
        function () {
            $.ajax({
                url: url,
                data: data,
                type: type,
                cache: false,
                success: function (data) {
                    if (data.Success === true) {
                        swal("Bien!", messageSuccess ? messageSuccess : "Proceso realizado Correctamente", "success");
                        if (onSuccess) onSuccess(data);
                    } else {
                        swal("Algo Salio Mal!", data.Message, "error");
                    }
                }
            });
        });
}
function getAjax(url, parameters, onSuccess) {
    $.ajax({
        url: url,
        type: "GET",
        data: parameters,
        cache: false,
        success: function (data) {
            if (onSuccess) onSuccess(data);
        },
        beforeSend: function () {
            $("#loading").show();
        },
        complete: function () {
            $("#loading").hide();
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