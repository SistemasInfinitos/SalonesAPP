//La Sesion es cerrada por inactividad con el mouse    
var ratonParado = null;
var milisegundosLimite = 600000;

$(document).on('mousemove', function () {
    clearTimeout(ratonParado);

    ratonParado = setTimeout(function () {
        toastr.warning('Su Sesion a Caducado por Inactividad');
        $('#expireTokenModal').modal('show');
    }, milisegundosLimite);
});

//$(document).mousemove(function (event) {
//detecta actividad mouse
//});