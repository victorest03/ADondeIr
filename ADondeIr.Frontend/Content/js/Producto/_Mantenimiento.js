var OnSuccessProducto, onFailureProducto;
$(function () {
    const $modalProductoMant = $("#modalProductoMant");
    const $frmProductoMant = $("#frmProductoMant");
    OnSuccessProducto = (data) => onSuccessForm(data, $frmProductoMant, $modalProductoMant);
    onFailureProducto = () => onFailureForm();
})