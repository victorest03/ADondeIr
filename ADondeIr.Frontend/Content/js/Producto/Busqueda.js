$(function () {
    const lengthPage = 4,
        arrayTipoServicio = [],
        arrayDistrito = [],
        $panelSearchDesktop = $("#panelSearchDesktop"),
        $containerProducts = $("#containerProducts"),
        $template = Handlebars.compile($("#product-template").html()),
        $btnLoadMore = $("#btnLoadMore");

    let startPage = 1;

    function onLoadProducts(moreResult) {
        if (moreResult !== true) {
            startPage = 1;
        }
        const dataSent = {
            start: startPage,
            length: lengthPage
        };

        let search, fkDistrito, fkTipoActividad;

        if ($panelSearchDesktop.is(":visible")) {
            search = $("#search").val();
            fkDistrito = $("#fkDistrito").val() === "Dónde..." ? "" : $("#fkDistrito").val();
            fkTipoActividad = $("#fkTipoActividad").val() === "Todas las categorias" ? "" : $("#fkTipoActividad").val();
        } else {
            search = $("#searchMovil").val();
            fkDistrito = $("#fkDistritoMovil").val() === "Dónde..." ? "" : $("#fkDistrito").val();
            fkTipoActividad = $("#fkTipoActividadMovil").val() === "Todas las categorias" ? "" : $("#fkTipoActividad").val();
        }

        if (search !== "") {
            dataSent.search = search;
        }

        if (fkDistrito !== "" || arrayDistrito.length > 0) {
            dataSent.distrito = arrayDistrito.length === 0 ? fkDistrito : arrayDistrito.join(",");
        }

        if (fkTipoActividad !== "" || arrayTipoServicio.length > 0) {
            dataSent.tipoActividad = arrayTipoServicio.length === 0 ? fkTipoActividad : arrayTipoServicio.join(",");
        }
        
        $.ajax({
            url: "/Producto/ListadoLazy",
            data: dataSent,
            type: "GET",
            cache: false,
            success: function (result) {
                if (result) {
                    const recordCurrent = result.data.length + (startPage === 1 ? 0 : lengthPage * startPage);
                    
                    if (recordCurrent >= result.recordsTotal) {
                        $btnLoadMore.hide();
                    }
                    if (moreResult !== true) {
                        $containerProducts.html(result.data.length > 0 ? $template(result.data) : "<div class='text-center'><p>No se encuentran productos disponibles...</p></div>");
                    } else {
                        $containerProducts.append($template(result.data));
                    }
                    startPage++;
                }
            }
        });
    }

    $btnLoadMore.on("click", function () {
        onLoadProducts(true);
    });

    $("#btnSearch,#btnSearchMovil").on("click", function() {
        onLoadProducts();
    });

    $("body").on("click", ".chkTipoActividad", function() {
        const $this = $(this);
        if ($this.is(":checked")) {
            arrayTipoServicio.push($this.attr("data-value"));
        } else {
            arrayTipoServicio.splice(arrayTipoServicio.indexOf($this.attr("data-value")), 1);
        }
        onLoadProducts();
    });

    $("body").on("click", ".chkDistrito", function () {
        const $this = $(this);
        if ($this.is(":checked")) {
            arrayDistrito.push($this.attr("data-value"));
        } else {
            arrayDistrito.splice(arrayDistrito.indexOf($this.attr("data-value")), 1);
        }
        onLoadProducts();
    });

    onLoadProducts();
})