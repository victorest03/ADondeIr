$(function () {
    const lengthPage = 4,
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
            fkDistrito = $("#fkDistrito").val();
            fkTipoActividad = $("#fkTipoActividad").val();
        } else {
            search = $("#searchMovil").val();
            fkDistrito = $("#fkDistritoMovil").val();
            fkTipoActividad = $("#fkTipoActividadMovil").val();
        }

        if (search !== "") {
            dataSent.search = search;
        }

        if (fkDistrito !== "") {
            dataSent.distrito = fkDistrito;
        }

        if (fkTipoActividad !== "") {
            dataSent.tipoActividad = fkTipoActividad;
        }
        

        $.ajax({
            url: "/Producto/ListadoLazy",
            data: dataSent,
            type: "GET",
            cache: false,
            success: function (result) {
                if (result) {
                    const recordCurrent = result.data.length + (startPage === 1 ? 0 : lengthPage * startPage);
                    console.log(recordCurrent);
                    
                    if (recordCurrent >= result.recordsTotal) {
                        $btnLoadMore.hide();
                    }
                    if (moreResult !== true) {
                        $containerProducts.html(result.data.length > 0 ? $template(result.data) : "<div class='text-center'><p>No se encuentran productos disponibles...</p></div>");
                    } else {
                        $containerProducts.append($template(result.data));
                    }
                    startPage++;
                    console.log(startPage);
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

    onLoadProducts();
})