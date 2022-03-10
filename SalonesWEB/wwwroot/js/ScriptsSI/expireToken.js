//Funcion que visualiza el tiempo restante de la vida util del teken de acceso actual
var _second1 = 1000;
var _minute1 = _second1 * 60;
var _hour1 = _minute1 * 60;
var _day1 = _hour1 * 24;
var timer1;
function showRemaining1() {
    var end1 = new Date(localStorage.getItem('expiresToken'));
    var now1 = new Date();
    var distance1 = end1 - now1;
    if (distance1 < 0) {
        clearInterval(timer1);
        $('#RefresTokenModal').modal('show');
        return;
    }
    var days1 = Math.floor(distance1 / _day1);
    var hours1 = Math.floor((distance1 % _day1) / _hour1);
    var minutes1 = Math.floor((distance1 % _hour1) / _minute1);
    var seconds1 = Math.floor((distance1 % _minute1) / _second1);

    document.getElementById('ExpireTokenAcces').innerHTML = days1 +' D,';
    document.getElementById('ExpireTokenAcces').innerHTML += hours1 +' H,';
    document.getElementById('ExpireTokenAcces').innerHTML += minutes1 + ' M y ';
    document.getElementById('ExpireTokenAcces').innerHTML += seconds1 + ' S';
}
timer = setInterval(showRemaining1, 1000);