$(function () {
    const $btnNuevaEmpresa = $("#btnNuevaEmpresa");
    
    function invocarModalEmpresa(id) {
        invocarModal(`/Empresa/PartialMantenimiento/${id ? id : ""}`,
            function (data) {
                $("#modalEmpresaMant").on("hidden.bs.modal");
            });
    }
    $btnNuevaEmpresa.on("click", () => invocarModalEmpresa());
})