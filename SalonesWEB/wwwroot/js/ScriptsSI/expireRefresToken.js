//Funcion que visualiza el tiempo restante en la modal de la vida util del teken de refresco del token de  acceso actual
var _second = 1000;
var _minute = _second * 60;
var _hour = _minute * 60;
var _day = _hour * 24;
var timer;
var end = new Date(localStorage.getItem('expiresToken'));
var addminutos = 1;
end.setMinutes(end.getMinutes() + addminutos);
var fechaEnd = end.toISOString();
var fecha_date = fechaEnd.split('T');
var fecha_time = fecha_date[1].split('.');
var fecha_time = fecha_date[1].split(':');
var fecha_time = fecha_date[1].split(':');
fecha_date = fecha_date[0];
var fechaEnd2 = fecha_date + ' ' + fecha_time[0] + ':' + fecha_time[1] + ':' + fecha_time[2];
var fechaEnd3 = new Date(fechaEnd2);

function showRemaining() {
    var now = new Date();
    var distance = fechaEnd3 - now;
    if (distance < 0) {
        clearInterval(timer);
        document.getElementById('ExpireTokenAcces').innerHTML = 'Ups! se te acabo el tiempo, debes iniciar sesion!';
        document.getElementById('expireTokencountdown2').innerHTML = 'Ups! se te acabo el tiempo, debes iniciar sesion!';
        return;
    }
    var days = Math.floor(distance / _day);
    var hours = Math.floor((distance % _day) / _hour);
    var minutes = Math.floor((distance % _hour) / _minute);
    var seconds = Math.floor((distance % _minute) / _second);

    document.getElementById('expireTokencountdown').innerHTML = days;
    document.getElementById('expireTokencountdown').innerHTML += hours;
    document.getElementById('expireTokencountdown').innerHTML += minutes + ' M y ';
    document.getElementById('expireTokencountdown').innerHTML += seconds + ' S';
}
timer = setInterval(showRemaining, 1000);
