$(function () {
    const $tableTipoActividad = $("#tableTipoActividad");
    const $btnNuevaTipoActividad = $("#btnNuevaTipoActividad");
    const $dataTableTipoActividad = $tableTipoActividad.DataTable({
        "lengthChange": true,
        "ajax": "/TipoActividad/Listado",
        "columns": [
            { data: "cTipoActividad", title: "Descripción" },
            {
                data: null,
                defaultContent:
                    "<button type='button' class='btn btn-primary btn-update' data-toggle='tooltip' title='Editar'><i class='fa fa-pencil'></i></button>",
                "orderable": false,
                "searchable": false,
                "width": "26px"
            }, {
                data: null,
                defaultContent:
                    "<button type='button' class='btn btn-danger btn-delete' data-toggle='tooltip' title='Eliminar'><i class='fa fa-trash'></i></button>",
                "orderable": false,
                "searchable": false,
                "width": "26px"
            }
        ]
    });

    function invocarModalTipoActividad(id) {
        invocarModal(`/TipoActividad/PartialMantenimiento/${id ? id : ""}`, () => $dataTableTipoActividad.ajax.reload(null, false));
    }
    $btnNuevaTipoActividad.on("click", () => invocarModalTipoActividad());

    $tableTipoActividad.on("click", ".btn-delete, .btn-update", function () {
        const id = $dataTableTipoActividad.row($(this).parents("tr")).data().pkTipoActividad;

        if ($(this).hasClass("btn-update")) invocarModalTipoActividad(id);
        else {
            confirmAjax(`/TipoActividad/Delete/${id}`, null, "POST", null, "Se Elimino Correctamente", function () {
                $dataTableTipoActividad.ajax.reload(null, false);
            });
        }
    });
})