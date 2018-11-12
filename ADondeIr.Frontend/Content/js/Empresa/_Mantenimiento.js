var OnSuccessEmpresa, onFailureEmpresa;
$(function () {
    const $modalEmpresaMant = $("#modalEmpresaMant");
    const $frmEmpresaMant = $("#frmEmpresaMant");
    OnSuccessEmpresa = function (data) {
        $frmEmpresaMant.find("span[data-valmsg-for]").text("");
        if (data.Success === true) {
            swal("Bien!", "Registro Guardado Correctamente", "success");
            $modalEmpresaMant.modal("hide");
        } else {
            if (data.Errors) {
                $.each(data.Errors,
                    function (i, item) {
                        if ($("span[data-valmsg-for=" + item.Key + "]").length !== 0)
                            $("span[data-valmsg-for=" + item.Key + "]").text(item.Message);
                    });
            }
            swal("Algo Salio Mal!", data.Message ? data.Message : "Verifique los campos ingresados", "error");
        }
    };
    onFailureEmpresa = function () {
        swal("Algo Salio Mal!", "Ocurrio un error al Guardar", "error");
    };

    $modalEmpresaMant.on("hidden.bs.modal",
        function () {
            $(this).parent().remove();
        });
    $modalEmpresaMant.modal("show");
})