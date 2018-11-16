var OnSuccessProducto, onFailureProducto;
$(function () {
    const $modalProductoMant = $("#modalProductoMant");
    const $frmProductoMant = $("#frmProductoMant");
    const $foto = $frmProductoMant.find("#foto");
    const $ObservacionGeneral = $frmProductoMant.find("#cObservacionGeneral");

    OnSuccessProducto = (data) => onSuccessForm(data, $frmProductoMant, $modalProductoMant);
    onFailureProducto = () => onFailureForm();

    $frmProductoMant.find("#dPrecio").inputmask("currency", { prefix: "S/ ", removeMaskOnSubmit: true, autoUnmask: true });

    $foto.fileinput({
        showRemove: false,
        browseClass: "btn btn-primary btn-block",
        showUpload: false,
        showCaption: false,
        allowedFileExtensions: ["png", "jpeg", "jpg"],
        maxFileCount: 1,
        maxFileSize: 5120
    }).on("fileclear",
        function () {
            $(this).fileinput("enable");
        }).on("fileerror",
        function (data) {
            swal("Algo Salio Mal!", data.MessageError, "error");
            $foto.fileinput("clear");
        });

    if ($foto.attr("data-preimagen")) {
        $foto.fileinput("refresh", {
            initialPreview: [
                `/Producto/Foto/${$foto.attr("data-preimagen")}`
            ],
            initialPreviewAsData: true
        });
        $foto.fileinput("disable");
    }

    $frmProductoMant.data("validator").settings.ignore = ".note-editor *";

    $ObservacionGeneral.summernote({
        lang: "es-ES",
        placeholder: "Ingresa una descripcion",
        tabsize: 2,
        dialogsInBody: true,
        height: 300,
        minHeight: null,
        maxHeight: null
    });

    $modalProductoMant.on("hidden.bs.modal", function () {
        $ObservacionGeneral.summernote("destroy");
    });
})