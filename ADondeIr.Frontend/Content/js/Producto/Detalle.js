$(function() {
    const $btnAddProductoToRuta = $("#btnAddProductoToRuta");

    $btnAddProductoToRuta.on("click", function() {
        $.ajax({
            url: "/Ruta/AddProduct/" + $btnAddProductoToRuta.attr("data-id"),
            type: "GET",
            cache: false,
            success: function (data) {
                if (data.Success) {
                    alert("Producto agregado Exitosamente");
                    location.reload();
                } else {
                    alert(`Algo Salio mal :${data.Message}`);
                }
            }
        });
    });
})