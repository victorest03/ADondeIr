$(function() {
    const $btnGuardarRuta = $("#btnGuardarRuta");
    const $btnGenerarRuta = $("#btnGenerarRuta");
    var map;

    $btnGenerarRuta.on("click", function () {
        $.ajax({
            url: "/Ruta/GetActiveRuta",
            type: "GET",
            cache: false,
            success: function (data) {
                if (data) {
                    var waypointsArray = [];
                    var pointA = new google.maps.LatLng(data.RutaProducto[0].Producto.cLatitud,
                            data.RutaProducto[0].Producto.cLongitud),
                        pointB = new google.maps.LatLng(
                            data.RutaProducto[data.RutaProducto.length - 1].Producto.cLatitud,
                            data.RutaProducto[data.RutaProducto.length - 1].Producto.cLongitud),
                        myOptions = {
                            zoom: 7,
                            center: pointA
                        },
                        map = new google.maps.Map(document.getElementById('map'), myOptions),
                        // Instantiate a directions service.
                        directionsService = new google.maps.DirectionsService,
                        directionsDisplay = new google.maps.DirectionsRenderer({
                            map: map
                        });
                    $.each(data.RutaProducto, function (i, e) {

                        if (i !== 0 && i !== (data.RutaProducto.length - 1)) {
                            waypointsArray.push({
                                location: new google.maps.LatLng(e.Producto.cLatitud, e.Producto.cLongitud),
                                stopover: true
                            });
                        }
                    });

                    calculateAndDisplayRoute(directionsService, directionsDisplay, pointA, pointB, waypointsArray);
                }
            }
        });
    });

    function calculateAndDisplayRoute(directionsService, directionsDisplay, pointA, pointB, waypointsArray) {
        directionsService.route({
            origin: pointA,
            destination: pointB,
            waypoints: waypointsArray.length === 0 ? null:waypointsArray,
            travelMode: google.maps.TravelMode.DRIVING
        }, function (response, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                directionsDisplay.setDirections(response);
            } else {
                window.alert('Directions request failed due to ' + status);
            }
        });
    }

    $btnGuardarRuta.on("click", function() {
        $.ajax({
            url: "/Ruta/GuardarRuta",
            type: "GET",
            cache: false,
            success: function (data) {
                if (data.Success) {
                    alert("Ruta Guardada Exitosamente");
                    location.href = "/Ruta";
                } else {
                    alert(`Algo Salio mal :${data.Message}`);
                }
            }
        });
    });
})