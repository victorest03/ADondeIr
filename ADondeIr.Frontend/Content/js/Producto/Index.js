﻿$(function () {
    const $tableProducto = $("#tableProducto");
    const $btnNuevaProducto = $("#btnNuevaProducto");
    const $dataTableProducto = $tableProducto.DataTable({
        "lengthChange": true,
        "ajax": "/Producto/Listado",
        "columns": [
            {
                data: "pkProducto", title: "Codigo",
                render: function (data) {
                    return `PR${zfill(data, 5)}`;
                }
            },
            { data: "cProducto", title: "Descripción" },
            { data: "TipoActividad.cTipoActividad", title: "Tipo de Actividad" },
            { data: "Distrito.cDistrito", title: "Distrito" },
            {
                data: "dPrecio", title: "Precio",
                render: function (data) {
                    return data.toLocaleString(undefined, { minimumFractionDigits: 2 });
                }
            },
            {
                data: "cGoogleMaps", title: "Google Maps",
                render: function (data) {
                    return `<a class="btn btn-link btn-xs" href="${data}" target="_blank">Ir a Google Maps</a>`; 
                }
            },
            {
                data: "fFechaCrea", title: "F. Creación",
                render: function (data) {
                    return new Date(data).format("dd-mm-yyyy");
                }
            },
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

    function invocarModalProducto(id) {
        invocarModal(`/Producto/PartialMantenimiento/${id ? id : ""}`, () => $dataTableProducto.ajax.reload(null, false));
    }
    $btnNuevaProducto.on("click", () => invocarModalProducto());

    $tableProducto.on("click", ".btn-delete, .btn-update", function () {
        const id = $dataTableProducto.row($(this).parents("tr")).data().pkProducto;

        if ($(this).hasClass("btn-update")) invocarModalProducto(id);
        else {
            confirmAjax(`/Producto/Delete/${id}`, null, "POST", null, "Se Elimino Correctamente", function () {
                $dataTableProducto.ajax.reload(null, false);
            });
        }
    });
})