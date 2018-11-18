$(function () {
    const lengthPage = 8,
        $panelSearchDesktop = $("#panelSearchDesktop"),
        $containerProducts = $("#containerProducts"),
        $template = Handlebars.compile($("#product-template").html()),
        $btnLoadMore = $("#btnLoadMore");

    let startPage = 0;

    function onLoadProducts(moreResult) {
        if (moreResult !== true) {
            startPage = 0;
        } else {
            startPage++;
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
                    if (recordCurrent === result.recordsTotal) {
                        $btnLoadMore.hide();
                    }
                    if (moreResult !== true) {
                        $containerProducts.html($template(result.data));
                    } else {
                        $containerProducts.append($template(result.data));
                    }
                }
            }
        });
    }

    $btnLoadMore.on("click", function () {
        onLoadProducts(true);
    });

    onLoadProducts();
})