var OnSuccessUsuario, onFailureUsuario;
$(function () {
    const $modalUsuarioMant = $("#modalUsuarioMant");
    const $frmUsuarioMant = $("#frmUsuarioMant");
    OnSuccessUsuario = (data) => onSuccessForm(data, $frmUsuarioMant, $modalUsuarioMant);
    onFailureUsuario = () => onFailureForm();
})