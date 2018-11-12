var OnSuccessTipoActividad, onFailureTipoActividad;
$(function () {
    const $modalTipoActividadMant = $("#modalTipoActividadMant");
    const $frmTipoActividadMant = $("#frmTipoActividadMant");
    OnSuccessTipoActividad = (data) => onSuccessForm(data, $frmTipoActividadMant, $modalTipoActividadMant);
    onFailureTipoActividad = () => onFailureForm();
})