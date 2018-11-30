$(function () {
    const $tableEmpresa = $("#tableEmpresa");
    const $btnNuevaEmpresa = $("#btnNuevaEmpresa");
    const $dataTableEmpresa = $tableEmpresa.DataTable({
        "lengthChange": true,
        "ajax": "/Empresa/Listado",
        "columns": [
            {
                data: "pkEmpresa", title: "Codigo",
                render: function (data) {
                    return `EM${zfill(data,5)}`;
                }
            },
            { data: "cEmpresa", title: "Descripción" },
            { data: "cDireccion", title: "Direccion" },
            { data: "cTelefono", title: "Teléfono" },
            { data: "eTotalProductos", title: " # Productos" },
            {
                data: "fFechaCrea", title: "F. Creación",
                render: function(data) {
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
    
    function invocarModalEmpresa(id) {
        invocarModal(`/Empresa/PartialMantenimiento/${id ? id : ""}`, () => $dataTableEmpresa.ajax.reload(null, false));
    }
    $btnNuevaEmpresa.on("click", () => invocarModalEmpresa());

    $tableEmpresa.on("click", ".btn-delete, .btn-update", function () {
        const id = $dataTableEmpresa.row($(this).parents("tr")).data().pkEmpresa;

        if ($(this).hasClass("btn-update")) invocarModalEmpresa(id);
        else {
            confirmAjax(`/Empresa/Delete/${id}`, null, "POST", null, "Se Elimino Correctamente", function () {
                $dataTableEmpresa.ajax.reload(null, false);
            });
        } 
    });
})