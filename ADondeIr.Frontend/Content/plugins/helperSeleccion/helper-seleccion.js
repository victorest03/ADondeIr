jQuery.fn.extend({
    helpselect: function ($parametros) {
        /*
         * title,columns,ajax,columnfilter,multiSelect,fulldisplay,eventBegin,eventEnd,eventdbClickRow,eventBtnAllSelected,eventCancel,icon,multiFilter,btnNew, --Parametros
         */

        var $inputHelper = $(this);
        var $flagCancel = true;
        var $typeInput;
        try {
            $typeInput = $inputHelper[0].type;
        } catch (err) { }

        function EventOnClickHelper() {
            /*Evento Begin*/
            if ($parametros.eventBegin)
                if ($parametros.eventBegin() == false) return;

            $flagCancel = true;
            var $inner__inputHelper__action = $(this);
            if ($inner__inputHelper__action.attr("data-helpselect-open") == "false") {
                var $modalHelper_dialog = $('<div class="modal fade" tabindex="-1" role="dialog">');
                var $modalHelper_document = $('<div class="modal-dialog modal-lg" role="document">').appendTo($modalHelper_dialog);
                var $modalHelper_content = $('<div class="modal-content">').appendTo($modalHelper_document);
                var $modalHelper_content_header = $('<div class="modal-header">').appendTo($modalHelper_content);
                var $modalHelper_content_header_close = $('<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>').appendTo($modalHelper_content_header);
                var $modalHelper_content_header_title = $('<h4 class="modal-title">').appendTo($modalHelper_content_header);
                var $modalHelper_content_body = $('<div class="modal-body row">').appendTo($modalHelper_content);
                var $modalHelper_content_footer = $('<div class="modal-footer">').appendTo($modalHelper_content);
                var $modalHelper_content_footer_close = $('<button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>').appendTo($modalHelper_content_footer);

                //Titulo de la Modal
                $modalHelper_content_header_title.text($parametros.title ? $parametros.title : "Busqueda");

                //Tabla de busqueda
                var $modalHelper_content_body_table = $('<div class="col-sm-12"><table id="example" class="table table-bordered table-hover helper-seleccion" cellspacing="0" width="100%"></table></div>').appendTo($modalHelper_content_body);

                //Datatable
                $.extend(true, $.fn.dataTable.defaults, {
                    "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "Todo"]],
                    language: {
                        "sProcessing": "Procesando...",
                        "sLengthMenu": "Mostrar _MENU_ registros",
                        "sZeroRecords": "No se encontraron resultados",
                        "sEmptyTable": "Ningún dato disponible en esta tabla",
                        "sInfo": "Mostrando  _START_ al _END_ de un total de _TOTAL_ registros",
                        "sInfoEmpty": "Mostrando 0 al 0 de un total de 0 registros",
                        "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                        "sInfoPostFix": "",
                        "sSearch": "",
                        "sUrl": "",
                        "sInfoThousands": ",",
                        "sLoadingRecords": "Cargando...",
                        "oPaginate": {
                            "sFirst": "Primero",
                            "sLast": "Último",
                            "sNext": "Siguiente",
                            "sPrevious": "Anterior"
                        },
                        "oAria": {
                            "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                            "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                        }
                    },
                    "paging": true,
                    "lengthChange": false,
                    "searching": true,
                    "ordering": true,
                    "info": true,
                    "autoWidth": true,
                    "serverSide": $parametros.serverSide ? $parametros.serverSide : false,
                    initComplete: function () {
                        if ($parametros.multiFilter === true && $parametros.serverSide === true) {
                            const api = this.api();

                            api.columns().every(function () {
                                var that = this;

                                $("input", this.footer()).on("keyup change", function () {
                                    if (that.search() !== this.value) {
                                        that
                                            .search(this.value)
                                            .draw();
                                    }
                                });
                            });
                        }
                    }
            });

                //Agregando Footer de Busqueda
                if ($parametros.multiFilter === true) {
                    $tfoot = $("<tfoot>").append("<tr>");
                    $.each($parametros.columns, function (i, item) {
                        if (item.className == "select-checkbox")
                            $tfoot.find("tr").append("<th></th>");
                        else
                            $tfoot.find("tr").append('<th><input type="text" class="form-control" placeholder="Buscar ' + item.title + '" /></th>');
                    });
                    $modalHelper_content_body_table.find("table").append($tfoot);
                }

                var $p_url_ajax;
                var $inner_data = {};
                if ($parametros.ajax.parameters) {
                    $.each($parametros.ajax.parameters, function (index, value) {
                        $inner_data[value.attr("name")] = value.val();
                    });
                }

                if ($parametros.ajax.parametersDefault) {
                    Object.keys($parametros.ajax.parametersDefault).forEach(function (key) {
                        $inner_data[key] = $parametros.ajax.parametersDefault[key];
                    });
                }

                if (Object.keys($inner_data).length !== 0) {
                    if ($parametros.ajax.selectId == true && Object.keys($inner_data).length === 1) {
                        $p_url_ajax = $parametros.ajax.url + "/" + $inner_data[Object.keys($inner_data)[0]];
                    } else {
                        $p_url_ajax = {
                            url: $parametros.ajax.url,
                            data: $inner_data
                        };
                    }
                } else {
                    $p_url_ajax = $parametros.ajax;
                }

                $p_url_ajax.type = $parametros.ajax.type ? $parametros.ajax.type:"GET";

                var timer;
                var dbClickFunction = function() {
                    var rowData = $modalHelper_content_body_table_inner.row($(this)).data();
                    if ($typeInput == "text" || $typeInput == "number") {
                        $inner__inputHelper__action.removeClass("placeholder");
                        $inner__inputHelper__action.text("");
                    }

                    $flagCancel = false;
                    $modalHelper_dialog.modal("hide");
                    if ($parametros.eventdbClickRow)
                        $parametros.eventdbClickRow(rowData, $inner__inputHelper__action);
                };

                var $modalHelper_content_body_table_inner = $modalHelper_content_body_table.find("table")
                    .DataTable({
                        "ajax": $p_url_ajax,
                        "columns": $parametros.columns,
                        order: [[$parametros.multiSelect ? 1 : 0, "asc"]],
                        select: {
                            style: "multi",
                            selector: "td:first-child"
                        },
                        "dom": "<'row'<'col-xs-6 toolbar-tb-helperseleccion'l><'col-xs-6 text-right'f>>" +
                            "<'row'<'col-sm-12'<'table-responsive'tr>>>" +
                            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
                        createdRow: $parametros.createdRow ? $parametros.createdRow : null
                    }).on("click", "tbody tr", function () {
                        clearTimeout(timer);
                        var $this = this;
                        $($this).on("click", dbClickFunction);
                        timer = setTimeout((function () {
                            $($this).off("click", dbClickFunction);
                        }), 500);
                    });

                $modalHelper_content_body_table.on("click", "td.select-checkbox",
                    function () {
                        var t = $(this).parents('table');
                        $(this).parent().toggleClass('selected');

                        if (t.find("td.select-checkbox").parent(".selected").length === t.find("td.select-checkbox").length) {
                            t.find("th.select-checkbox").removeClass("indeterminate").addClass("selected");
                        } else if (t.find("td.select-checkbox").parent(".selected").length === 0) {
                            t.find("th.select-checkbox").removeClass("indeterminate").removeClass("selected");
                        } else {
                            t.find("th.select-checkbox").removeClass("selected").addClass("indeterminate");
                        }

                        $modalHelper_content_footer.find("button:eq(1)").trigger("change");
                    });

                $modalHelper_content_body_table.on("click", "th.select-checkbox",
                    function () {
                        var t = $(this).parents('table');


                        if (t.find("th.select-checkbox").hasClass("selected")) {
                            $modalHelper_content_body_table_inner.rows({ filter: 'applied' }).every(function () {
                                $(this.node()).removeClass('selected');
                            });
                            t.find("th.select-checkbox").removeClass("selected");
                        } else {
                            $modalHelper_content_body_table_inner.rows({ filter: 'applied' }).every(function () {
                                $(this.node()).addClass('selected');
                            });
                            t.find("th.select-checkbox").addClass("selected");
                        }

                        $modalHelper_content_footer.find("button:eq(1)").trigger("change");
                    });

                if ($parametros.multiFilter === true && $parametros.serverSide !== true) {
                    $modalHelper_content_body_table_inner.columns().every(function () {
                        var that = this;

                        $("input", this.footer()).on("keyup change", function () {
                            if (that.search() !== this.value) {
                                that
                                    .search(this.value)
                                    .draw();
                            }
                        });
                    });
                }

                if ($parametros.multiSelect == true) {
                    $modalHelper_content_body_table_inner.column("1:visible").order("asc").draw();
                    var $table = $modalHelper_content_body_table.find("table");
                    var selectedArray = [];
                    $modalHelper_content_footer_close.addClass("pull-left");
                    var $modalHelper_content_footer_allSelect = $('<button type="button" class="btn btn-primary" disabled>Todos los Selc.</button>').on("click", function () {

                        $flagCancel = false;
                        $modalHelper_dialog.modal("hide");

                        $.each($modalHelper_content_body_table_inner.rows('.selected').data(), function (i, e) {
                            selectedArray.push(e);
                        });

                        if ($parametros.eventBtnAllSelected)
                            $parametros.eventBtnAllSelected(selectedArray, $inner__inputHelper__action);
                    }).on("change", function () {
                        if ($table.find("td.select-checkbox").parent(".selected").length === 0) {
                            $(this).attr("disabled", "disabled");
                        } else {
                            $(this).removeAttr("disabled");
                        }

                    });

                    $modalHelper_content_footer.append($modalHelper_content_footer_allSelect);
                }

                if ($parametros.btnNew) {
                    var $toolbartb = $modalHelper_content_body_table.find(".toolbar-tb-helperseleccion");

                    var $btnNew_icon = $parametros.btnNew.icon ? $parametros.btnNew.icon : "glyphicon glyphicon-plus";
                    var $btnNew_class = $parametros.btnNew.class ? $parametros.btnNew.class : "btn btn-primary";
                    var $btnNew_text = $parametros.btnNew.text ? $parametros.btnNew.text : "Nuevo";
                    var $btnNew = $('<button type="button" class="' + $btnNew_class + '"><i class="' + $btnNew_icon + '"></i> ' + $btnNew_text + " </button>").on("click", function () {
                        $parametros.btnNew.action($modalHelper_dialog, $modalHelper_content_body_table_inner);
                    });
                    $btnNew.appendTo($toolbartb);

                }

                $modalHelper_dialog.on("hidden.bs.modal", function () {
                    $modalHelper_dialog.remove();
                    $inner__inputHelper__action.attr("data-helpselect-open", false);
                    if ($flagCancel == true) {
                        if ($typeInput == "text" || $typeInput == "number") {
                            if ($inputHelper.attr("placeholder")) {
                                $inner__inputHelper__action.addClass("placeholder").text($inputHelper.attr("placeholder"));
                            }
                        }

                        if ($parametros.eventCancel)
                            $parametros.eventCancel($inner__inputHelper__action);
                    }
                });

                $modalHelper_dialog.modal("show");
                var zIndex = 1040 + (10 * $(".modal:visible").length);
                $modalHelper_dialog.css("z-index", zIndex);
                setTimeout(function () {
                    $(".modal-backdrop").not(".modal-stack").css("z-index", zIndex - 1).addClass("modal-stack");
                }, 0);
                $inner__inputHelper__action.attr("data-helpselect-open", true);
            }

            /*Evento End*/
            if ($parametros.eventEnd)
                $parametros.eventEnd($inner__inputHelper__action);
        }

        if ($parametros.multiSelect == true) {
            $parametros.columns.splice(0, 0, {
                "data": "",
                "render": function () {
                    return '';
                },
                "searchable": false,
                "orderable": false,
                className: "select-checkbox"
            });
        }
        if ($typeInput == "button") {
            $inputHelper.attr("data-helpselect-open", false);
            $inputHelper.on("click", EventOnClickHelper);
        } else {
            var $icon = $parametros.icon ? $parametros.icon : "glyphicon glyphicon-search";
            var $inputHelper__icon = $('<i class="helper-seleccion-icon ' + $icon + '"></i>');

            var $inputHelper__wrap = $("<div>").addClass($inputHelper.attr("class")).addClass("helper-seleccion");
            if ($inputHelper.attr("disabled"))
                $inputHelper__wrap.attr("disabled", "disabled");
            $inputHelper.attr("class", "");
            if ($parametros.fulldisplay != false) {
                if (!$inputHelper.attr("disabled")) {
                    $inputHelper.attr("data-helpselect-open", false);
                    $inputHelper.on("click", EventOnClickHelper);
                }
            } else {
                if (!$inputHelper.attr("disabled")) {
                    $inputHelper__icon.attr("data-helpselect-open", false);
                    $inputHelper__icon.on("click", EventOnClickHelper);
                }
            }

            $inputHelper.addClass("helper-seleccion-input").wrap($inputHelper__wrap);
            if (!$inputHelper.attr("disabled"))
                $inputHelper.after($inputHelper__icon);

        }
    }
})