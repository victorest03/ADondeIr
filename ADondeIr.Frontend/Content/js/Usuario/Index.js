$(function () {
    const $tableUsuario = $("#tableUsuario");
    const $btnNuevaUsuario = $("#btnNuevaUsuario");
    const $dataTableUsuario = $tableUsuario.DataTable({
        "lengthChange": true,
        "ajax": "/Usuario/Listado",
        "columns": [
            { data: "cNombres", title: "Nombre" },
            { data: "cApellidos", title: "Apellidos" },
            { data: "cEmail", title: "Email" },
            {
                data: "isAdmin", title: "Administrador?",
                render: function(data) {
                    return data ? "SI" : "NO";
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

    function invocarModalUsuario(id) {
        invocarModal(`/Usuario/PartialMantenimiento/${id ? id : ""}`, () => $dataTableUsuario.ajax.reload(null, false));
    }
    $btnNuevaUsuario.on("click", () => invocarModalUsuario());

    $tableUsuario.on("click", ".btn-delete, .btn-update", function () {
        const id = $dataTableUsuario.row($(this).parents("tr")).data().pkUsuario;

        if ($(this).hasClass("btn-update")) invocarModalUsuario(id);
        else {
            confirmAjax(`/Usuario/Delete/${id}`, null, "POST", null, "Se Elimino Correctamente", function () {
                $dataTableUsuario.ajax.reload(null, false);
            });
        }
    });
})