var OnSuccessEmpresa, onFailureEmpresa;
$(function () {
    const $modalEmpresaMant = $("#modalEmpresaMant");
    const $frmEmpresaMant = $("#frmEmpresaMant");
    OnSuccessEmpresa = (data) => onSuccessForm(data, $frmEmpresaMant, $modalEmpresaMant);
    onFailureEmpresa = () => onFailureForm();
})